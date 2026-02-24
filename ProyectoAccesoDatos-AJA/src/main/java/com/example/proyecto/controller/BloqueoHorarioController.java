package com.example.proyecto.controller;

import com.example.proyecto.domain.BloqueoHorario;
import com.example.proyecto.domain.Cita;
import com.example.proyecto.repository.BloqueoHorarioRepository;
import com.example.proyecto.repository.CitaRepository;
import com.example.proyecto.repository.HorarioSemanalRepository;
import com.example.proyecto.services.UserDetailsImpl;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.bind.annotation.*;

import java.time.LocalTime;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/api/bloqueos")
public class BloqueoHorarioController {

    @Autowired
    private BloqueoHorarioRepository repo;

    // ✅ PUNTO 3: Necesario para cancelar citas pendientes afectadas por el bloqueo
    @Autowired
    private CitaRepository citaRepository;

    @Autowired
    private HorarioSemanalRepository horarioRepository;

    // Todos pueden consultar (administradores y alumnos)
    @GetMapping
    public List<BloqueoHorario> all() {
        return repo.findAll();
    }

    @GetMapping("/{id}")
    public ResponseEntity<BloqueoHorario> get(@PathVariable int id) {
        return repo.findById(id)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

    @GetMapping("/admin/{id}")
    public List<BloqueoHorario> byAdmin(@PathVariable int id) {
        return repo.findByAdmin(id);
    }

    // Solo administradores pueden crear bloqueos
    @PreAuthorize("hasRole('ADMINISTRADOR')")
    @PostMapping
    public ResponseEntity<?> create(@RequestBody BloqueoHorario b) {
        try {
            Authentication auth = SecurityContextHolder.getContext().getAuthentication();
            UserDetailsImpl userDetails = (UserDetailsImpl) auth.getPrincipal();

            // Validar que el bloqueo tiene un administrador asignado
            if (b.getAdministrador() == null || b.getAdministrador().getIdUsuario() == null) {
                return ResponseEntity.badRequest()
                        .body(createError("Debe especificar un administrador para el bloqueo"));
            }

            if (b.getFecha() == null || b.getHoraInicio() == null || b.getHoraFin() == null) {
                return ResponseEntity.badRequest()
                        .body(createError("Debe especificar fecha, hora de inicio y hora de fin para el bloqueo"));
            }

            // ✅ PUNTO 3: Guardar el bloqueo
            BloqueoHorario bloqueoGuardado = repo.save(b);

            // ✅ PUNTO 3: Buscar y eliminar todas las citas PENDIENTES que choquen con este bloqueo
            List<Cita> citasAfectadas = cancelarCitasPendientesAfectadas(bloqueoGuardado);

            // Devolver el bloqueo creado junto con info de las citas afectadas
            Map<String, Object> respuesta = new HashMap<>();
            respuesta.put("bloqueo", bloqueoGuardado);
            respuesta.put("citasEliminadas", citasAfectadas.size());
            if (!citasAfectadas.isEmpty()) {
                respuesta.put("mensaje", "Bloqueo creado correctamente. Se han eliminado " +
                        citasAfectadas.size() + " cita(s) pendiente(s) que coincidían con este bloqueo.");
            } else {
                respuesta.put("mensaje", "Bloqueo creado correctamente. No había citas pendientes afectadas.");
            }

            return ResponseEntity.ok(respuesta);

        } catch (Exception e) {
            return ResponseEntity.badRequest()
                    .body(createError("Error al crear el bloqueo: " + e.getMessage()));
        }
    }

    // Solo administradores pueden modificar bloqueos
    @PreAuthorize("hasRole('ADMINISTRADOR')")
    @PutMapping("/{id}")
    public ResponseEntity<?> update(@PathVariable int id, @RequestBody BloqueoHorario b) {
        try {
            if (!repo.existsById(id)) {
                return ResponseEntity.notFound().build();
            }

            BloqueoHorario bloqueoExistente = repo.findById(id).orElse(null);
            if (bloqueoExistente == null) {
                return ResponseEntity.notFound().build();
            }

            b.setIdBloqueo(id);
            BloqueoHorario bloqueoActualizado = repo.save(b);

            // ✅ PUNTO 3: Al modificar un bloqueo también cancelar las nuevas citas pendientes afectadas
            List<Cita> citasAfectadas = cancelarCitasPendientesAfectadas(bloqueoActualizado);

            Map<String, Object> respuesta = new HashMap<>();
            respuesta.put("bloqueo", bloqueoActualizado);
            respuesta.put("citasEliminadas", citasAfectadas.size());
            if (!citasAfectadas.isEmpty()) {
                respuesta.put("mensaje", "Bloqueo actualizado correctamente. Se han eliminado " +
                        citasAfectadas.size() + " cita(s) pendiente(s) que coincidían con el nuevo horario bloqueado.");
            } else {
                respuesta.put("mensaje", "Bloqueo actualizado correctamente. No había citas pendientes afectadas.");
            }

            return ResponseEntity.ok(respuesta);

        } catch (Exception e) {
            return ResponseEntity.badRequest()
                    .body(createError("Error al actualizar el bloqueo: " + e.getMessage()));
        }
    }

    // Solo administradores pueden eliminar bloqueos
    @PreAuthorize("hasRole('ADMINISTRADOR')")
    @DeleteMapping("/{id}")
    public ResponseEntity<?> delete(@PathVariable int id) {
        try {
            if (!repo.existsById(id)) {
                return ResponseEntity.notFound().build();
            }

            repo.deleteById(id);
            return ResponseEntity.noContent().build();

        } catch (Exception e) {
            return ResponseEntity.badRequest()
                    .body(createError("Error al eliminar el bloqueo: " + e.getMessage()));
        }
    }

    // =========================================================================
    // MÉTODOS AUXILIARES
    // =========================================================================

    /**
     * ✅ PUNTO 3: Busca todas las citas PENDIENTES que choquen con el bloqueo
     * (por fecha y solapamiento de horario) y las elimina.
     * Las citas con estado "realizada" o "cancelada" NO se tocan.
     *
     * @param bloqueo El bloqueo recién creado o modificado
     * @return Lista de citas que han sido eliminadas
     */
    private List<Cita> cancelarCitasPendientesAfectadas(BloqueoHorario bloqueo) {
        List<Cita> citasEliminadas = new ArrayList<>();

        // Buscar todas las citas de ese mismo día
        List<Cita> citasDelDia = citaRepository.findByFecha(bloqueo.getFecha());

        for (Cita cita : citasDelDia) {
            // ✅ Solo afectar citas PENDIENTES — respetar las realizadas
            if (cita.getEstado() != Cita.Estado.pendiente) {
                continue;
            }

            // Obtener el horario de la cita para comparar horas
            if (cita.getHorarioSemanal() == null) {
                continue;
            }

            LocalTime horaInicioCita = cita.getHoraInicio();
            LocalTime horaFinCita = cita.getHoraFin();

            // Comprobar si hay solapamiento entre el bloqueo y la cita
            if (hayConflictoHorario(horaInicioCita, horaFinCita,
                    bloqueo.getHoraInicio(), bloqueo.getHoraFin())) {
                citaRepository.deleteById(cita.getIdCita());
                citasEliminadas.add(cita);
            }
        }

        return citasEliminadas;
    }

    /**
     * Verifica si dos rangos horarios se solapan.
     * Dos rangos se solapan si uno empieza antes de que el otro termine.
     */
    private boolean hayConflictoHorario(LocalTime inicio1, LocalTime fin1,
                                        LocalTime inicio2, LocalTime fin2) {
        return !(fin1.isBefore(inicio2) || fin1.equals(inicio2) ||
                inicio1.isAfter(fin2) || inicio1.equals(fin2));
    }

    private Map<String, String> createError(String mensaje) {
        Map<String, String> error = new HashMap<>();
        error.put("error", mensaje);
        return error;
    }
}