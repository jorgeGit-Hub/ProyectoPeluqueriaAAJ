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
import java.time.format.TextStyle;
import java.util.HashMap;
import java.util.List;
import java.util.Locale;
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

            // 2. Validar que exista horario para ese día
            String diaSemana = convertirDiaSemana(c.getFecha().getDayOfWeek());
            List<HorarioSemanal> horariosDelDia = horarioRepository.findByServicioAndDia(
                    servicioCompleto.getIdServicio(),
                    diaSemana
            );

            if (horariosDelDia.isEmpty()) {
                return ResponseEntity.badRequest().body(createError(
                        "Este servicio no está disponible los " + diaSemana
                ));
            }

            // 3. Validar que la cita esté dentro de algún horario disponible
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
                        "El horario de la cita debe estar dentro del horario del servicio"
                ));
            }

            // 4. Validar capacidad del grupo (cantAlumnos)
            if (servicioCompleto.getGrupo() != null &&
                    servicioCompleto.getGrupo().getCantAlumnos() != null) {

                int capacidadMaxima = servicioCompleto.getGrupo().getCantAlumnos();

                // Contar citas que se solapan en ese horario para ese servicio
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

            // 5. Validar Cliente/Permisos
            boolean isAdmin = auth.getAuthorities().stream()
                    .anyMatch(a -> a.getAuthority().equals("ROLE_ADMINISTRADOR"));

            if (!isAdmin) {
                if (c.getCliente() == null || c.getCliente().getIdUsuario() == null) {
                    return ResponseEntity.badRequest().body(createError("Falta el cliente"));
                }
                if (!c.getCliente().getIdUsuario().equals(userDetails.getId())) {
                    return ResponseEntity.status(403).body(createError("No puedes crear citas para otros"));
                }
            }

            return ResponseEntity.ok(repo.save(c));

        } catch (Exception e) {
            return ResponseEntity.badRequest().body(createError("Error: " + e.getMessage()));
        }
    }

    @PutMapping("/{id}")
    public ResponseEntity<?> update(@PathVariable int id, @RequestBody Cita c) {
        try {
            if (!repo.existsById(id)) return ResponseEntity.notFound().build();

            // Si se actualiza servicio o fecha, validar horarios y capacidad
            if (c.getServicio() != null && c.getFecha() != null &&
                    c.getHoraInicio() != null && c.getHoraFin() != null) {

                Servicio s = servicioRepository.findById(c.getServicio().getIdServicio()).orElse(null);
                if (s == null) {
                    return ResponseEntity.badRequest().body(createError("Servicio no encontrado"));
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
                            id // Excluir la cita actual del conteo
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
     * Cuenta cuántas citas existen que se solapan con el horario especificado
     * para un servicio en una fecha determinada
     */
    private int contarCitasSimultaneas(Integer idServicio, LocalDate fecha,
                                       LocalTime horaInicio, LocalTime horaFin) {
        List<Cita> citasDelDia = repo.findByServicioAndFromDate(idServicio, fecha);

        int contador = 0;
        for (Cita cita : citasDelDia) {
            // Verificar si las fechas coinciden
            if (!cita.getFecha().equals(fecha)) continue;

            // Verificar solapamiento de horarios
            // Dos citas se solapan si:
            // (horaInicio1 < horaFin2) Y (horaFin1 > horaInicio2)
            if (cita.getHoraInicio().isBefore(horaFin) &&
                    cita.getHoraFin().isAfter(horaInicio)) {
                contador++;
            }
        }

        return contador;
    }

    /**
     * Similar a contarCitasSimultaneas pero excluye una cita específica
     * (útil para actualizaciones)
     */
    private int contarCitasSimultaneasExcluyendo(Integer idServicio, LocalDate fecha,
                                                 LocalTime horaInicio, LocalTime horaFin,
                                                 Integer idCitaExcluir) {
        List<Cita> citasDelDia = repo.findByServicioAndFromDate(idServicio, fecha);

        int contador = 0;
        for (Cita cita : citasDelDia) {
            // Excluir la cita actual
            if (cita.getIdCita().equals(idCitaExcluir)) continue;

            // Verificar si las fechas coinciden
            if (!cita.getFecha().equals(fecha)) continue;

            // Verificar solapamiento de horarios
            if (cita.getHoraInicio().isBefore(horaFin) &&
                    cita.getHoraFin().isAfter(horaInicio)) {
                contador++;
            }
        }

        return contador;
    }
}