package com.example.proyecto.controller;

import com.example.proyecto.domain.Cita;
import com.example.proyecto.domain.HorarioSemanal;
import com.example.proyecto.domain.BloqueoHorario;
import com.example.proyecto.repository.CitaRepository;
import com.example.proyecto.repository.ClienteRepository;
import com.example.proyecto.repository.HorarioSemanalRepository;
import com.example.proyecto.repository.BloqueoHorarioRepository;
import com.example.proyecto.services.UserDetailsImpl;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.bind.annotation.*;

import java.time.DayOfWeek;
import java.time.LocalDate;
import java.time.LocalTime;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/api/citas")
public class CitaController {

    @Autowired
    private CitaRepository repo;

    @Autowired
    private ClienteRepository clienteRepository;

    @Autowired
    private HorarioSemanalRepository horarioRepository;

    // ✅ NUEVO: Inyectar el repositorio de bloqueos
    @Autowired
    private BloqueoHorarioRepository bloqueoRepository;

    // --- CONSULTAS ---

    @GetMapping
    public List<Cita> all() {
        return repo.findAll();
    }

    @GetMapping("/{id}")
    public ResponseEntity<Cita> get(@PathVariable int id) {
        return repo.findById(id)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

    @GetMapping("/mis-citas")
    public ResponseEntity<List<Cita>> misCitas() {
        Authentication auth = SecurityContextHolder.getContext().getAuthentication();
        UserDetailsImpl userDetails = (UserDetailsImpl) auth.getPrincipal();
        return ResponseEntity.ok(repo.findByClienteId(userDetails.getId()));
    }

    @GetMapping("/agenda-grupo/{idGrupo}/{fecha}")
    public List<Cita> agendaGrupo(@PathVariable int idGrupo, @PathVariable String fecha) {
        return repo.findByGrupoAndFecha(idGrupo, LocalDate.parse(fecha));
    }

    // --- GESTIÓN ---

    @PostMapping
    public ResponseEntity<?> create(@RequestBody Cita c) {
        try {
            Authentication auth = SecurityContextHolder.getContext().getAuthentication();
            UserDetailsImpl userDetails = (UserDetailsImpl) auth.getPrincipal();

            // 1. ✅ CAMBIO CRÍTICO: Validar que venga el HorarioSemanal
            if (c.getHorarioSemanal() == null || c.getHorarioSemanal().getIdHorario() == null) {
                return ResponseEntity.badRequest().body(createError("Debe especificar un horario semanal"));
            }

            // 2. Obtener el horario completo con todas sus relaciones
            HorarioSemanal horario = horarioRepository.findById(c.getHorarioSemanal().getIdHorario()).orElse(null);
            if (horario == null) {
                return ResponseEntity.badRequest().body(createError("El horario especificado no existe"));
            }

            // 3. Validar que el horario tenga servicio y grupo
            if (horario.getServicio() == null) {
                return ResponseEntity.badRequest().body(createError("El horario no tiene un servicio asociado"));
            }
            if (horario.getGrupo() == null) {
                return ResponseEntity.badRequest().body(createError("El horario no tiene un grupo asociado"));
            }

            // 4. Validar fecha
            if (c.getFecha() == null) {
                return ResponseEntity.badRequest().body(createError("Debe especificar una fecha"));
            }

            // ✅ VALIDACIÓN: No permitir fechas pasadas
            if (c.getFecha().isBefore(LocalDate.now())) {
                return ResponseEntity.badRequest().body(createError(
                        "No se pueden crear citas en fechas pasadas. La fecha debe ser hoy o posterior."
                ));
            }

            // ✅ VALIDACIÓN: Si la fecha es hoy, validar que la hora del horario no sea pasada
            if (c.getFecha().isEqual(LocalDate.now())) {
                if (horario.getHoraInicio().isBefore(java.time.LocalTime.now())) {
                    return ResponseEntity.badRequest().body(createError(
                            "No se pueden crear citas para horarios que ya han pasado hoy. " +
                                    "Hora actual: " + java.time.LocalTime.now().toString()
                    ));
                }
            }

            // ✅✅ NUEVO: VALIDAR BLOQUEOS DE HORARIO
            String motivoBloqueo = verificarBloqueoHorario(c.getFecha(), horario.getHoraInicio(), horario.getHoraFin());
            if (motivoBloqueo != null) {
                return ResponseEntity.badRequest().body(createError(
                        "No se puede crear la cita. " + motivoBloqueo
                ));
            }

            // 5. ✅ NUEVA VALIDACIÓN: Verificar que el día de la semana coincida con el horario
            String diaSemana = convertirDiaSemana(c.getFecha().getDayOfWeek());
            if (!horario.getDiaSemana().name().equals(diaSemana)) {
                return ResponseEntity.badRequest().body(createError(
                        "La fecha seleccionada (" + c.getFecha() + ") es " + diaSemana +
                                ", pero el horario es para " + horario.getDiaSemana().name()
                ));
            }

            // 6. Validar Cliente/Permisos
            boolean isAdmin = auth.getAuthorities().stream()
                    .anyMatch(a -> a.getAuthority().equals("ROLE_ADMINISTRADOR"));

            Integer idClienteCita;
            if (!isAdmin) {
                if (c.getCliente() == null || c.getCliente().getIdUsuario() == null) {
                    return ResponseEntity.badRequest().body(createError("Falta el cliente"));
                }
                if (!c.getCliente().getIdUsuario().equals(userDetails.getId())) {
                    return ResponseEntity.status(403).body(createError("No puedes crear citas para otros"));
                }
                idClienteCita = userDetails.getId();
            } else {
                if (c.getCliente() == null || c.getCliente().getIdUsuario() == null) {
                    return ResponseEntity.badRequest().body(createError("Debe especificar un cliente"));
                }
                idClienteCita = c.getCliente().getIdUsuario();
            }

            // 7. ✅ NUEVA VALIDACIÓN: Verificar si ya existe una cita duplicada
            if (existeCitaDuplicada(idClienteCita, horario.getIdHorario(), c.getFecha())) {
                return ResponseEntity.badRequest().body(createError(
                        "Ya existe una cita para este cliente en este horario y fecha. " +
                                "No se pueden crear citas duplicadas."
                ));
            }

            // 8. ✅ VALIDAR CAPACIDAD: Contar cuántas citas hay en este horario para esta fecha
            int capacidadMaxima = horario.getGrupo().getCantAlumnos() != null ?
                    horario.getGrupo().getCantAlumnos() : Integer.MAX_VALUE;

            int citasExistentes = contarCitasEnHorario(horario.getIdHorario(), c.getFecha());

            if (citasExistentes >= capacidadMaxima) {
                return ResponseEntity.badRequest().body(createError(
                        "No hay capacidad disponible. Máximo: " + capacidadMaxima +
                                " alumnos. Ya hay " + citasExistentes + " citas reservadas."
                ));
            }

            // 9. Establecer el horario completo y estado por defecto
            c.setHorarioSemanal(horario);
            if (c.getEstado() == null) {
                c.setEstado(Cita.Estado.pendiente);
            }

            System.out.println("=== DEBUG CREAR CITA ===");
            System.out.println("Fecha: " + c.getFecha());
            System.out.println("Horario ID: " + horario.getIdHorario());
            System.out.println("Servicio: " + horario.getServicio().getNombre());
            System.out.println("Grupo: " + horario.getGrupo().getCurso());
            System.out.println("Día semana: " + horario.getDiaSemana());
            System.out.println("Horario: " + horario.getHoraInicio() + " - " + horario.getHoraFin());

            return ResponseEntity.ok(repo.save(c));

        } catch (Exception e) {
            e.printStackTrace();
            return ResponseEntity.badRequest().body(createError("Error al crear la cita: " + e.getMessage()));
        }
    }

    @PutMapping("/{id}")
    public ResponseEntity<?> update(@PathVariable int id, @RequestBody Cita c) {
        try {
            if (!repo.existsById(id)) return ResponseEntity.notFound().build();

            Cita citaExistente = repo.findById(id).orElse(null);
            if (citaExistente == null) {
                return ResponseEntity.notFound().build();
            }

            // ✅ VALIDACIÓN: No permitir cambiar a fechas pasadas
            if (c.getFecha() != null && c.getFecha().isBefore(LocalDate.now())) {
                return ResponseEntity.badRequest().body(createError(
                        "No se puede actualizar la cita a una fecha pasada. La fecha debe ser hoy o posterior."
                ));
            }

            // Si se cambia el horario, validar todo de nuevo
            if (c.getHorarioSemanal() != null && c.getHorarioSemanal().getIdHorario() != null) {

                HorarioSemanal nuevoHorario = horarioRepository.findById(
                        c.getHorarioSemanal().getIdHorario()
                ).orElse(null);

                if (nuevoHorario == null) {
                    return ResponseEntity.badRequest().body(createError("El horario especificado no existe"));
                }

                // Validar día de la semana
                if (c.getFecha() != null) {
                    String diaSemana = convertirDiaSemana(c.getFecha().getDayOfWeek());
                    if (!nuevoHorario.getDiaSemana().name().equals(diaSemana)) {
                        return ResponseEntity.badRequest().body(createError(
                                "La fecha seleccionada no corresponde al día del horario"
                        ));
                    }

                    // ✅✅ NUEVO: VALIDAR BLOQUEOS DE HORARIO
                    String motivoBloqueo = verificarBloqueoHorario(
                            c.getFecha(),
                            nuevoHorario.getHoraInicio(),
                            nuevoHorario.getHoraFin()
                    );
                    if (motivoBloqueo != null) {
                        return ResponseEntity.badRequest().body(createError(
                                "No se puede actualizar la cita. " + motivoBloqueo
                        ));
                    }

                    // ✅ VALIDAR CITA DUPLICADA (excluyendo la cita actual)
                    Integer idCliente = c.getCliente() != null ? c.getCliente().getIdUsuario() :
                            citaExistente.getCliente().getIdUsuario();

                    if (existeCitaDuplicadaExcluyendo(
                            idCliente,
                            nuevoHorario.getIdHorario(),
                            c.getFecha(),
                            id
                    )) {
                        return ResponseEntity.badRequest().body(createError(
                                "Ya existe otra cita para este cliente en este horario y fecha."
                        ));
                    }

                    // Validar capacidad (excluyendo la cita actual)
                    if (nuevoHorario.getGrupo() != null && nuevoHorario.getGrupo().getCantAlumnos() != null) {
                        int capacidadMaxima = nuevoHorario.getGrupo().getCantAlumnos();
                        int citasExistentes = contarCitasEnHorarioExcluyendo(
                                nuevoHorario.getIdHorario(),
                                c.getFecha(),
                                id
                        );

                        if (citasExistentes >= capacidadMaxima) {
                            return ResponseEntity.badRequest().body(createError(
                                    "No hay capacidad disponible. Máximo: " + capacidadMaxima +
                                            " alumnos. Ya hay " + citasExistentes + " citas reservadas."
                            ));
                        }
                    }
                }

                c.setHorarioSemanal(nuevoHorario);
            }

            c.setIdCita(id);
            return ResponseEntity.ok(repo.save(c));

        } catch (Exception e) {
            return ResponseEntity.badRequest().body(createError("Error: " + e.getMessage()));
        }
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<?> delete(@PathVariable int id) {
        if (!repo.existsById(id)) return ResponseEntity.notFound().build();
        repo.deleteById(id);
        return ResponseEntity.noContent().build();
    }

    // --- MÉTODOS AUXILIARES ---

    private Map<String, String> createError(String mensaje) {
        Map<String, String> map = new HashMap<>();
        map.put("error", mensaje);
        return map;
    }

    private String convertirDiaSemana(DayOfWeek dayOfWeek) {
        switch (dayOfWeek) {
            case MONDAY: return "lunes";
            case TUESDAY: return "martes";
            case WEDNESDAY: return "miercoles";
            case THURSDAY: return "jueves";
            case FRIDAY: return "viernes";
            case SATURDAY: return "sabado";
            case SUNDAY: return "domingo";
            default: return "";
        }
    }

    /**
     * ✅✅ NUEVO: Verifica si existe un bloqueo de horario que impida crear la cita
     *
     * @param fecha Fecha de la cita
     * @param horaInicio Hora de inicio del horario de la cita
     * @param horaFin Hora de fin del horario de la cita
     * @return String con el motivo del bloqueo, o null si no hay bloqueo
     */
    private String verificarBloqueoHorario(LocalDate fecha, LocalTime horaInicio, LocalTime horaFin) {
        // Obtener todos los bloqueos para la fecha especificada
        List<BloqueoHorario> bloqueosDelDia = bloqueoRepository.findByFecha(fecha);

        if (bloqueosDelDia == null || bloqueosDelDia.isEmpty()) {
            return null; // No hay bloqueos para esta fecha
        }

        // Verificar si algún bloqueo interfiere con el horario de la cita
        for (BloqueoHorario bloqueo : bloqueosDelDia) {
            if (hayConflictoHorario(horaInicio, horaFin, bloqueo.getHoraInicio(), bloqueo.getHoraFin())) {
                String motivo = bloqueo.getMotivo() != null && !bloqueo.getMotivo().trim().isEmpty()
                        ? bloqueo.getMotivo()
                        : "Horario bloqueado";

                return "El horario está bloqueado de " +
                        bloqueo.getHoraInicio() + " a " + bloqueo.getHoraFin() +
                        ". Motivo: " + motivo;
            }
        }

        return null; // No hay conflictos
    }

    /**
     * ✅✅ NUEVO: Verifica si dos rangos de horario se solapan
     *
     * @param inicio1 Hora de inicio del primer rango
     * @param fin1 Hora de fin del primer rango
     * @param inicio2 Hora de inicio del segundo rango
     * @param fin2 Hora de fin del segundo rango
     * @return true si hay solapamiento, false si no
     */
    private boolean hayConflictoHorario(LocalTime inicio1, LocalTime fin1,
                                        LocalTime inicio2, LocalTime fin2) {
        // Dos rangos se solapan si:
        // - El inicio de uno está dentro del otro rango, O
        // - El fin de uno está dentro del otro rango, O
        // - Uno contiene completamente al otro

        return !(fin1.isBefore(inicio2) || fin1.equals(inicio2) ||
                inicio1.isAfter(fin2) || inicio1.equals(fin2));
    }

    /**
     * ✅ Cuenta cuántas citas existen para un horario específico en una fecha
     */
    private int contarCitasEnHorario(Integer idHorario, LocalDate fecha) {
        List<Cita> citas = repo.findByHorarioAndFromDate(idHorario, fecha);

        int contador = 0;
        for (Cita cita : citas) {
            if (cita.getFecha().equals(fecha) && cita.getEstado() != Cita.Estado.cancelada) {
                contador++;
            }
        }
        return contador;
    }

    /**
     * ✅ Cuenta citas excluyendo una específica (para updates)
     */
    private int contarCitasEnHorarioExcluyendo(Integer idHorario, LocalDate fecha, Integer idCitaExcluir) {
        List<Cita> citas = repo.findByHorarioAndFromDate(idHorario, fecha);

        int contador = 0;
        for (Cita cita : citas) {
            if (cita.getIdCita().equals(idCitaExcluir)) continue;
            if (cita.getFecha().equals(fecha) && cita.getEstado() != Cita.Estado.cancelada) {
                contador++;
            }
        }
        return contador;
    }

    /**
     * ✅ Verifica si existe una cita duplicada
     */
    private boolean existeCitaDuplicada(Integer idCliente, Integer idHorario, LocalDate fecha) {
        List<Cita> citasDelCliente = repo.findByClienteId(idCliente);

        for (Cita cita : citasDelCliente) {
            if (cita.getEstado() == Cita.Estado.cancelada) continue;

            if (cita.getHorarioSemanal() != null &&
                    cita.getHorarioSemanal().getIdHorario().equals(idHorario) &&
                    cita.getFecha().equals(fecha)) {
                return true;
            }
        }
        return false;
    }

    /**
     * ✅ Verifica cita duplicada excluyendo una cita específica
     */
    private boolean existeCitaDuplicadaExcluyendo(Integer idCliente, Integer idHorario,
                                                  LocalDate fecha, Integer idCitaExcluir) {
        List<Cita> citasDelCliente = repo.findByClienteId(idCliente);

        for (Cita cita : citasDelCliente) {
            if (cita.getIdCita().equals(idCitaExcluir)) continue;
            if (cita.getEstado() == Cita.Estado.cancelada) continue;

            if (cita.getHorarioSemanal() != null &&
                    cita.getHorarioSemanal().getIdHorario().equals(idHorario) &&
                    cita.getFecha().equals(fecha)) {
                return true;
            }
        }
        return false;
    }
}