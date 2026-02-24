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
    @Autowired
    private BloqueoHorarioRepository bloqueoRepository;

    @GetMapping
    public List<Cita> all() { return repo.findAll(); }

    @GetMapping("/{id}")
    public ResponseEntity<Cita> get(@PathVariable int id) {
        return repo.findById(id).map(ResponseEntity::ok).orElse(ResponseEntity.notFound().build());
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

    @PostMapping
    public ResponseEntity<?> create(@RequestBody Cita c) {
        try {
            Authentication auth = SecurityContextHolder.getContext().getAuthentication();
            UserDetailsImpl userDetails = (UserDetailsImpl) auth.getPrincipal();

            if (c.getHorarioSemanal() == null || c.getHorarioSemanal().getIdHorario() == null) {
                return ResponseEntity.badRequest().body(createError("Debe especificar un horario semanal base"));
            }

            HorarioSemanal horario = horarioRepository.findById(c.getHorarioSemanal().getIdHorario()).orElse(null);
            if (horario == null) return ResponseEntity.badRequest().body(createError("El horario especificado no existe"));
            if (horario.getServicio() == null) return ResponseEntity.badRequest().body(createError("El horario no tiene un servicio asociado"));
            if (horario.getGrupo() == null) return ResponseEntity.badRequest().body(createError("El horario no tiene un grupo asociado"));

            if (c.getFecha() == null) return ResponseEntity.badRequest().body(createError("Debe especificar una fecha"));
            if (c.getFecha().isBefore(LocalDate.now())) {
                return ResponseEntity.badRequest().body(createError("No se pueden crear citas en fechas pasadas."));
            }

            // ✅ VALIDACIONES DE HORA ESPECÍFICA
            if (c.getHoraInicio() == null || c.getHoraFin() == null) {
                return ResponseEntity.badRequest().body(createError("Debe especificar hora de inicio y fin para la cita."));
            }
            if (!c.getHoraInicio().isBefore(c.getHoraFin())) {
                return ResponseEntity.badRequest().body(createError("La hora de inicio debe ser anterior a la hora de fin."));
            }

            // ✅ VALIDACIÓN: El horario de la cita debe estar dentro del bloque HorarioSemanal
            if (c.getHoraInicio().isBefore(horario.getHoraInicio()) || c.getHoraFin().isAfter(horario.getHoraFin())) {
                return ResponseEntity.badRequest().body(createError(
                        "El horario de la cita (" + c.getHoraInicio() + " a " + c.getHoraFin() +
                                ") queda fuera del turno disponible (" + horario.getHoraInicio() + " a " + horario.getHoraFin() + ")."
                ));
            }

            if (c.getFecha().isEqual(LocalDate.now()) && c.getHoraInicio().isBefore(LocalTime.now())) {
                return ResponseEntity.badRequest().body(createError("No se pueden reservar horas que ya han pasado hoy."));
            }

            String diaSemana = convertirDiaSemana(c.getFecha().getDayOfWeek());
            if (!horario.getDiaSemana().name().equals(diaSemana)) {
                return ResponseEntity.badRequest().body(createError("La fecha seleccionada no coincide con el día del horario semanal."));
            }

            // ✅ BLOQUEOS: Ahora evalúa la hora específica de la cita, no del bloque entero
            String motivoBloqueo = verificarBloqueoHorario(c.getFecha(), c.getHoraInicio(), c.getHoraFin());
            if (motivoBloqueo != null) {
                return ResponseEntity.badRequest().body(createError("No se puede crear la cita. " + motivoBloqueo));
            }

            // Permisos
            boolean isAdmin = auth.getAuthorities().stream().anyMatch(a -> a.getAuthority().equals("ROLE_ADMINISTRADOR"));
            Integer idClienteCita;
            if (!isAdmin) {
                if (c.getCliente() == null || c.getCliente().getIdUsuario() == null) return ResponseEntity.badRequest().body(createError("Falta el cliente"));
                if (!c.getCliente().getIdUsuario().equals(userDetails.getId())) return ResponseEntity.status(403).body(createError("No puedes crear citas para otros"));
                idClienteCita = userDetails.getId();
            } else {
                if (c.getCliente() == null || c.getCliente().getIdUsuario() == null) return ResponseEntity.badRequest().body(createError("Debe especificar un cliente"));
                idClienteCita = c.getCliente().getIdUsuario();
            }

            // ✅ CITA DUPLICADA: Evita solapamientos para un mismo cliente
            if (existeCitaSolapadaParaCliente(idClienteCita, c.getFecha(), c.getHoraInicio(), c.getHoraFin())) {
                return ResponseEntity.badRequest().body(createError("Ya tienes otra cita programada que se solapa con este horario."));
            }

            // ✅ CAPACIDAD: Cuenta alumnos concurrentes en ESE intervalo específico
            int capacidadMaxima = horario.getGrupo().getCantAlumnos() != null ? horario.getGrupo().getCantAlumnos() : Integer.MAX_VALUE;
            int citasConcurrentes = contarCitasSolapadasEnIntervalo(horario.getIdHorario(), c.getFecha(), c.getHoraInicio(), c.getHoraFin());

            if (citasConcurrentes >= capacidadMaxima) {
                return ResponseEntity.badRequest().body(createError(
                        "No hay capacidad disponible en esa franja horaria. Máximo: " + capacidadMaxima + " alumnos."
                ));
            }

            c.setHorarioSemanal(horario);
            if (c.getEstado() == null) c.setEstado(Cita.Estado.pendiente);

            return ResponseEntity.ok(repo.save(c));

        } catch (Exception e) {
            e.printStackTrace();
            return ResponseEntity.badRequest().body(createError("Error al crear la cita: " + e.getMessage()));
        }
    }

    @PutMapping("/{id}")
    public ResponseEntity<?> update(@PathVariable int id, @RequestBody Cita c) {
        try {
            Cita citaExistente = repo.findById(id).orElse(null);
            if (citaExistente == null) return ResponseEntity.notFound().build();

            if (c.getFecha() != null && c.getFecha().isBefore(LocalDate.now())) {
                return ResponseEntity.badRequest().body(createError("No se puede actualizar a una fecha pasada."));
            }

            if (c.getHorarioSemanal() != null && c.getHorarioSemanal().getIdHorario() != null) {
                HorarioSemanal nuevoHorario = horarioRepository.findById(c.getHorarioSemanal().getIdHorario()).orElse(null);
                if (nuevoHorario == null) return ResponseEntity.badRequest().body(createError("El horario no existe"));

                if (c.getHoraInicio() == null) c.setHoraInicio(citaExistente.getHoraInicio());
                if (c.getHoraFin() == null) c.setHoraFin(citaExistente.getHoraFin());

                if (!c.getHoraInicio().isBefore(c.getHoraFin())) {
                    return ResponseEntity.badRequest().body(createError("La hora de inicio debe ser anterior a la hora de fin."));
                }
                if (c.getHoraInicio().isBefore(nuevoHorario.getHoraInicio()) || c.getHoraFin().isAfter(nuevoHorario.getHoraFin())) {
                    return ResponseEntity.badRequest().body(createError("El horario de la cita está fuera del turno semanal asignado."));
                }

                if (c.getFecha() != null) {
                    String diaSemana = convertirDiaSemana(c.getFecha().getDayOfWeek());
                    if (!nuevoHorario.getDiaSemana().name().equals(diaSemana)) {
                        return ResponseEntity.badRequest().body(createError("La fecha no corresponde al día del horario."));
                    }

                    String motivoBloqueo = verificarBloqueoHorario(c.getFecha(), c.getHoraInicio(), c.getHoraFin());
                    if (motivoBloqueo != null) return ResponseEntity.badRequest().body(createError("Conflicto con bloqueo: " + motivoBloqueo));

                    Integer idCliente = c.getCliente() != null ? c.getCliente().getIdUsuario() : citaExistente.getCliente().getIdUsuario();
                    if (existeCitaSolapadaParaClienteExcluyendo(idCliente, c.getFecha(), c.getHoraInicio(), c.getHoraFin(), id)) {
                        return ResponseEntity.badRequest().body(createError("Solapamiento de horario con otra de tus citas."));
                    }

                    if (nuevoHorario.getGrupo() != null && nuevoHorario.getGrupo().getCantAlumnos() != null) {
                        int capacidadMaxima = nuevoHorario.getGrupo().getCantAlumnos();
                        int citasConcurrentes = contarCitasSolapadasEnIntervaloExcluyendo(nuevoHorario.getIdHorario(), c.getFecha(), c.getHoraInicio(), c.getHoraFin(), id);
                        if (citasConcurrentes >= capacidadMaxima) {
                            return ResponseEntity.badRequest().body(createError("No hay capacidad en este intervalo específico."));
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

    private String verificarBloqueoHorario(LocalDate fecha, LocalTime horaInicio, LocalTime horaFin) {
        List<BloqueoHorario> bloqueosDelDia = bloqueoRepository.findByFecha(fecha);
        if (bloqueosDelDia == null || bloqueosDelDia.isEmpty()) return null;

        for (BloqueoHorario bloqueo : bloqueosDelDia) {
            if (hayConflictoHorario(horaInicio, horaFin, bloqueo.getHoraInicio(), bloqueo.getHoraFin())) {
                String motivo = bloqueo.getMotivo() != null && !bloqueo.getMotivo().trim().isEmpty() ? bloqueo.getMotivo() : "Horario bloqueado";
                return "Bloqueado de " + bloqueo.getHoraInicio() + " a " + bloqueo.getHoraFin() + ". Motivo: " + motivo;
            }
        }
        return null;
    }

    private boolean hayConflictoHorario(LocalTime inicio1, LocalTime fin1, LocalTime inicio2, LocalTime fin2) {
        return !(fin1.isBefore(inicio2) || fin1.equals(inicio2) || inicio1.isAfter(fin2) || inicio1.equals(fin2));
    }

    // ✅ CUENTA CUANTAS CITAS HAY EXACTAMENTE EN ESE INTERVALO DE TIEMPO
    private int contarCitasSolapadasEnIntervalo(Integer idHorario, LocalDate fecha, LocalTime horaInicio, LocalTime horaFin) {
        List<Cita> citas = repo.findByHorarioAndFecha(idHorario, fecha);
        int contador = 0;
        for (Cita cita : citas) {
            if (cita.getEstado() != Cita.Estado.cancelada) {
                if (hayConflictoHorario(horaInicio, horaFin, cita.getHoraInicio(), cita.getHoraFin())) contador++;
            }
        }
        return contador;
    }

    private int contarCitasSolapadasEnIntervaloExcluyendo(Integer idHorario, LocalDate fecha, LocalTime horaInicio, LocalTime horaFin, Integer idCitaExcluir) {
        List<Cita> citas = repo.findByHorarioAndFecha(idHorario, fecha);
        int contador = 0;
        for (Cita cita : citas) {
            if (cita.getIdCita().equals(idCitaExcluir) || cita.getEstado() == Cita.Estado.cancelada) continue;
            if (hayConflictoHorario(horaInicio, horaFin, cita.getHoraInicio(), cita.getHoraFin())) contador++;
        }
        return contador;
    }

    // ✅ COMPRUEBA SI EL CLIENTE YA TIENE OTRA CITA EN ESE EXACTO MOMENTO
    private boolean existeCitaSolapadaParaCliente(Integer idCliente, LocalDate fecha, LocalTime horaInicio, LocalTime horaFin) {
        List<Cita> citasDelCliente = repo.findByClienteAndFecha(idCliente, fecha);
        for (Cita cita : citasDelCliente) {
            if (cita.getEstado() == Cita.Estado.cancelada) continue;
            if (hayConflictoHorario(horaInicio, horaFin, cita.getHoraInicio(), cita.getHoraFin())) return true;
        }
        return false;
    }

    private boolean existeCitaSolapadaParaClienteExcluyendo(Integer idCliente, LocalDate fecha, LocalTime horaInicio, LocalTime horaFin, Integer idCitaExcluir) {
        List<Cita> citasDelCliente = repo.findByClienteAndFecha(idCliente, fecha);
        for (Cita cita : citasDelCliente) {
            if (cita.getIdCita().equals(idCitaExcluir) || cita.getEstado() == Cita.Estado.cancelada) continue;
            if (hayConflictoHorario(horaInicio, horaFin, cita.getHoraInicio(), cita.getHoraFin())) return true;
        }
        return false;
    }
}