package com.example.proyecto.controller;

import com.example.proyecto.domain.Cita;
import com.example.proyecto.domain.HorarioSemanal;
import com.example.proyecto.domain.Servicio;
import com.example.proyecto.repository.CitaRepository;
import com.example.proyecto.repository.ClienteRepository;
import com.example.proyecto.repository.ServicioRepository;
import com.example.proyecto.repository.HorarioSemanalRepository;
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
    private ServicioRepository servicioRepository;

    @Autowired
    private HorarioSemanalRepository horarioRepository;

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

            // 1. Validar Servicio
            if (c.getServicio() == null || c.getServicio().getIdServicio() == null) {
                return ResponseEntity.badRequest().body(createError("Debe especificar un servicio"));
            }
            Servicio servicioCompleto = servicioRepository.findById(c.getServicio().getIdServicio()).orElse(null);
            if (servicioCompleto == null) {
                return ResponseEntity.badRequest().body(createError("El servicio no existe"));
            }

            // 2. Validar fecha
            if (c.getFecha() == null) {
                return ResponseEntity.badRequest().body(createError("Debe especificar una fecha"));
            }

            // ✅ NUEVA VALIDACIÓN: No permitir fechas pasadas
            if (c.getFecha().isBefore(LocalDate.now())) {
                return ResponseEntity.badRequest().body(createError(
                        "No se pueden crear citas en fechas pasadas. La fecha debe ser hoy o posterior."
                ));
            }

            // 3. Validar horas
            if (c.getHoraInicio() == null || c.getHoraFin() == null) {
                return ResponseEntity.badRequest().body(createError("Debe especificar hora de inicio y fin"));
            }

            // ✅ NUEVA VALIDACIÓN: Si la fecha es hoy, validar que la hora no sea pasada
            if (c.getFecha().isEqual(LocalDate.now())) {
                LocalTime horaActual = LocalTime.now();
                if (c.getHoraInicio().isBefore(horaActual)) {
                    return ResponseEntity.badRequest().body(createError(
                            "No se pueden crear citas con hora de inicio pasada. Seleccione una hora posterior a las " +
                                    horaActual.toString()
                    ));
                }
            }

            // 4. Validar Cliente/Permisos
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
                // Si es admin, usar el cliente especificado en la cita
                if (c.getCliente() == null || c.getCliente().getIdUsuario() == null) {
                    return ResponseEntity.badRequest().body(createError("Debe especificar un cliente"));
                }
                idClienteCita = c.getCliente().getIdUsuario();
            }

            // ✅ NUEVA VALIDACIÓN: Verificar si ya existe una cita duplicada
            if (existeCitaDuplicada(
                    idClienteCita,
                    servicioCompleto.getIdServicio(),
                    c.getFecha(),
                    c.getHoraInicio(),
                    c.getHoraFin()
            )) {
                return ResponseEntity.badRequest().body(createError(
                        "Ya existe una cita para este cliente en el mismo servicio, fecha y horario. " +
                                "No se pueden crear citas duplicadas."
                ));
            }

            // 5. Convertir día de la semana
            String diaSemana = convertirDiaSemana(c.getFecha().getDayOfWeek());

            System.out.println("=== DEBUG CREAR CITA ===");
            System.out.println("Fecha: " + c.getFecha());
            System.out.println("Día de la semana (DayOfWeek): " + c.getFecha().getDayOfWeek());
            System.out.println("Día convertido: " + diaSemana);
            System.out.println("ID Servicio: " + servicioCompleto.getIdServicio());

            // 6. Buscar horarios del servicio para ese día
            List<HorarioSemanal> horariosDelDia = horarioRepository.findByServicioAndDia(
                    servicioCompleto.getIdServicio(),
                    diaSemana
            );

            System.out.println("Horarios encontrados: " + horariosDelDia.size());
            for (HorarioSemanal h : horariosDelDia) {
                System.out.println("  - Día: " + h.getDiaSemana() + ", Inicio: " + h.getHoraInicio() + ", Fin: " + h.getHoraFin());
            }

            if (horariosDelDia.isEmpty()) {
                return ResponseEntity.badRequest().body(createError(
                        "Este servicio no está disponible los " + diaSemana + ". Por favor, verifica los horarios del servicio."
                ));
            }

            // 7. Validar que la cita esté dentro de algún horario disponible
            boolean dentroDeHorario = false;
            HorarioSemanal horarioValido = null;

            for (HorarioSemanal horario : horariosDelDia) {
                if (!c.getHoraInicio().isBefore(horario.getHoraInicio()) &&
                        !c.getHoraFin().isAfter(horario.getHoraFin())) {
                    dentroDeHorario = true;
                    horarioValido = horario;
                    break;
                }
            }

            if (!dentroDeHorario) {
                return ResponseEntity.badRequest().body(createError(
                        "El horario seleccionado no está disponible para este servicio. Horarios disponibles los " +
                                diaSemana + ": " + formatearHorarios(horariosDelDia)
                ));
            }

            // 8. Validar capacidad del grupo (cantAlumnos)
            if (servicioCompleto.getGrupo() != null &&
                    servicioCompleto.getGrupo().getCantAlumnos() != null) {

                int capacidadMaxima = servicioCompleto.getGrupo().getCantAlumnos();

                int citasSimultaneas = contarCitasSimultaneas(
                        servicioCompleto.getIdServicio(),
                        c.getFecha(),
                        c.getHoraInicio(),
                        c.getHoraFin()
                );

                if (citasSimultaneas >= capacidadMaxima) {
                    return ResponseEntity.badRequest().body(createError(
                            "No hay capacidad disponible. Máximo: " + capacidadMaxima +
                                    " alumnos. Ya hay " + citasSimultaneas + " citas en ese horario."
                    ));
                }
            }

            c.setServicio(servicioCompleto);

            // 9. Establecer estado por defecto si no viene
            if (c.getEstado() == null) {
                c.setEstado(Cita.Estado.pendiente);
            }

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

            // Obtener la cita existente para comparar
            Cita citaExistente = repo.findById(id).orElse(null);
            if (citaExistente == null) {
                return ResponseEntity.notFound().build();
            }

            // ✅ NUEVA VALIDACIÓN: No permitir cambiar a fechas pasadas
            if (c.getFecha() != null && c.getFecha().isBefore(LocalDate.now())) {
                return ResponseEntity.badRequest().body(createError(
                        "No se puede actualizar la cita a una fecha pasada. La fecha debe ser hoy o posterior."
                ));
            }

            // ✅ NUEVA VALIDACIÓN: Si la fecha es hoy, validar que la hora no sea pasada
            if (c.getFecha() != null && c.getFecha().isEqual(LocalDate.now()) &&
                    c.getHoraInicio() != null) {
                LocalTime horaActual = LocalTime.now();
                if (c.getHoraInicio().isBefore(horaActual)) {
                    return ResponseEntity.badRequest().body(createError(
                            "No se puede actualizar la cita con hora de inicio pasada. " +
                                    "Seleccione una hora posterior a las " + horaActual.toString()
                    ));
                }
            }

            // Si se actualiza servicio o fecha, validar horarios y capacidad
            if (c.getServicio() != null && c.getFecha() != null &&
                    c.getHoraInicio() != null && c.getHoraFin() != null) {

                Servicio s = servicioRepository.findById(c.getServicio().getIdServicio()).orElse(null);
                if (s == null) {
                    return ResponseEntity.badRequest().body(createError("Servicio no encontrado"));
                }

                // ✅ VALIDAR CITA DUPLICADA (excluyendo la cita actual)
                Integer idCliente = c.getCliente() != null ? c.getCliente().getIdUsuario() :
                        citaExistente.getCliente().getIdUsuario();

                if (existeCitaDuplicadaExcluyendo(
                        idCliente,
                        s.getIdServicio(),
                        c.getFecha(),
                        c.getHoraInicio(),
                        c.getHoraFin(),
                        id
                )) {
                    return ResponseEntity.badRequest().body(createError(
                            "Ya existe otra cita para este cliente en el mismo servicio, fecha y horario."
                    ));
                }

                // Validar día
                String diaSemana = convertirDiaSemana(c.getFecha().getDayOfWeek());
                List<HorarioSemanal> horariosDelDia = horarioRepository.findByServicioAndDia(
                        s.getIdServicio(),
                        diaSemana
                );

                if (horariosDelDia.isEmpty()) {
                    return ResponseEntity.badRequest().body(createError(
                            "Este servicio no está disponible los " + diaSemana
                    ));
                }

                // Validar horario
                boolean dentroDeHorario = false;
                for (HorarioSemanal horario : horariosDelDia) {
                    if (!c.getHoraInicio().isBefore(horario.getHoraInicio()) &&
                            !c.getHoraFin().isAfter(horario.getHoraFin())) {
                        dentroDeHorario = true;
                        break;
                    }
                }

                if (!dentroDeHorario) {
                    return ResponseEntity.badRequest().body(createError(
                            "El horario de la cita debe estar dentro del horario del servicio"
                    ));
                }

                // Validar capacidad (excluyendo la cita actual)
                if (s.getGrupo() != null && s.getGrupo().getCantAlumnos() != null) {
                    int capacidadMaxima = s.getGrupo().getCantAlumnos();

                    int citasSimultaneas = contarCitasSimultaneasExcluyendo(
                            s.getIdServicio(),
                            c.getFecha(),
                            c.getHoraInicio(),
                            c.getHoraFin(),
                            id
                    );

                    if (citasSimultaneas >= capacidadMaxima) {
                        return ResponseEntity.badRequest().body(createError(
                                "No hay capacidad disponible. Máximo: " + capacidadMaxima +
                                        " alumnos. Ya hay " + citasSimultaneas + " citas en ese horario."
                        ));
                    }
                }
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

    /**
     * Convierte DayOfWeek de Java a String en español minúsculas
     * para comparar con el enum DiaSemana
     */
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
     * Formatea los horarios disponibles para mostrarlos en el mensaje de error
     */
    private String formatearHorarios(List<HorarioSemanal> horarios) {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < horarios.size(); i++) {
            HorarioSemanal h = horarios.get(i);
            sb.append(h.getHoraInicio()).append(" - ").append(h.getHoraFin());
            if (i < horarios.size() - 1) {
                sb.append(", ");
            }
        }
        return sb.toString();
    }

    /**
     * Cuenta cuántas citas existen que se solapan con el horario especificado
     * para un servicio en una fecha determinada
     */
    private int contarCitasSimultaneas(Integer idServicio, LocalDate fecha,
                                       LocalTime horaInicio, LocalTime horaFin) {
        List<Cita> citasDelDia = repo.findByServicioAndFromDate(idServicio, fecha);

        int contador = 0;
        for (Cita cita : citasDelDia) {
            if (!cita.getFecha().equals(fecha)) continue;
            if (cita.getEstado() == Cita.Estado.cancelada) continue;

            if (cita.getHoraInicio().isBefore(horaFin) &&
                    cita.getHoraFin().isAfter(horaInicio)) {
                contador++;
            }
        }

        return contador;
    }

    /**
     * Similar a contarCitasSimultaneas pero excluye una cita específica
     * (Útil para actualizaciones)
     */
    private int contarCitasSimultaneasExcluyendo(Integer idServicio, LocalDate fecha,
                                                 LocalTime horaInicio, LocalTime horaFin,
                                                 Integer idCitaExcluir) {
        List<Cita> citasDelDia = repo.findByServicioAndFromDate(idServicio, fecha);

        int contador = 0;
        for (Cita cita : citasDelDia) {
            if (cita.getIdCita().equals(idCitaExcluir)) continue;
            if (!cita.getFecha().equals(fecha)) continue;
            if (cita.getEstado() == Cita.Estado.cancelada) continue;

            if (cita.getHoraInicio().isBefore(horaFin) &&
                    cita.getHoraFin().isAfter(horaInicio)) {
                contador++;
            }
        }

        return contador;
    }

    /**
     * ✅ NUEVO MÉTODO: Verifica si existe una cita duplicada
     * (mismo cliente, servicio, fecha y horario exacto)
     */
    private boolean existeCitaDuplicada(Integer idCliente, Integer idServicio,
                                        LocalDate fecha, LocalTime horaInicio,
                                        LocalTime horaFin) {
        List<Cita> citasDelCliente = repo.findByClienteId(idCliente);

        for (Cita cita : citasDelCliente) {
            // Ignorar citas canceladas
            if (cita.getEstado() == Cita.Estado.cancelada) continue;

            // Verificar si es el mismo servicio, fecha y horario exacto
            if (cita.getServicio() != null &&
                    cita.getServicio().getIdServicio().equals(idServicio) &&
                    cita.getFecha().equals(fecha) &&
                    cita.getHoraInicio().equals(horaInicio) &&
                    cita.getHoraFin().equals(horaFin)) {
                return true; // Ya existe una cita duplicada
            }
        }

        return false;
    }

    /**
     * ✅ NUEVO MÉTODO: Verifica si existe una cita duplicada excluyendo una cita específica
     * (útil para actualizaciones)
     */
    private boolean existeCitaDuplicadaExcluyendo(Integer idCliente, Integer idServicio,
                                                  LocalDate fecha, LocalTime horaInicio,
                                                  LocalTime horaFin, Integer idCitaExcluir) {
        List<Cita> citasDelCliente = repo.findByClienteId(idCliente);

        for (Cita cita : citasDelCliente) {
            // Excluir la cita que se está actualizando
            if (cita.getIdCita().equals(idCitaExcluir)) continue;

            // Ignorar citas canceladas
            if (cita.getEstado() == Cita.Estado.cancelada) continue;

            // Verificar si es el mismo servicio, fecha y horario exacto
            if (cita.getServicio() != null &&
                    cita.getServicio().getIdServicio().equals(idServicio) &&
                    cita.getFecha().equals(fecha) &&
                    cita.getHoraInicio().equals(horaInicio) &&
                    cita.getHoraFin().equals(horaFin)) {
                return true; // Ya existe una cita duplicada
            }
        }

        return false;
    }
}