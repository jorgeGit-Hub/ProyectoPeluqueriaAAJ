package com.example.proyecto.controller;

import com.example.proyecto.domain.Cita;
import com.example.proyecto.domain.Valoracion;
import com.example.proyecto.repository.CitaRepository;
import com.example.proyecto.repository.ValoracionRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/api/valoraciones")
public class ValoracionController {

    @Autowired
    private ValoracionRepository repo;

    // ✅ NUEVO: Necesario para validar el estado de la cita
    @Autowired
    private CitaRepository citaRepository;

    @GetMapping
    public List<Valoracion> all() {
        return repo.findAll();
    }

    @GetMapping("/{id}")
    public ResponseEntity<?> get(@PathVariable int id) {
        return repo.findById(id)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

    @GetMapping("/min/{p}")
    public List<Valoracion> min(@PathVariable int p) {
        return repo.findByMinPuntuacion(p);
    }

    // ✅ PUNTO 1 y 2: Solo se puede valorar una cita con estado "realizada"
    //                 y todos los errores tienen mensajes personalizados
    @PostMapping
    public ResponseEntity<?> create(@RequestBody Valoracion v) {
        try {
            // Validar que viene una cita
            if (v.getCita() == null || v.getCita().getIdCita() == null) {
                return ResponseEntity.badRequest()
                        .body(createError("Debes indicar la cita que quieres valorar."));
            }

            // Buscar la cita en la base de datos
            Cita cita = citaRepository.findById(v.getCita().getIdCita()).orElse(null);
            if (cita == null) {
                return ResponseEntity.badRequest()
                        .body(createError("La cita indicada no existe. Verifica el identificador e inténtalo de nuevo."));
            }

            // ✅ VALIDACIÓN PRINCIPAL: Solo se pueden valorar citas realizadas
            if (cita.getEstado() == Cita.Estado.pendiente) {
                return ResponseEntity.badRequest()
                        .body(createError("No puedes valorar esta cita porque todavía está pendiente de realizarse. " +
                                "Solo puedes dejar una valoración una vez que el servicio haya sido completado."));
            }
            if (cita.getEstado() == Cita.Estado.cancelada) {
                return ResponseEntity.badRequest()
                        .body(createError("No puedes valorar esta cita porque fue cancelada. " +
                                "Solo se pueden valorar citas que hayan sido realizadas."));
            }

            // Validar puntuación
            if (v.getPuntuacion() == null) {
                return ResponseEntity.badRequest()
                        .body(createError("Debes indicar una puntuación para guardar la valoración."));
            }
            if (v.getPuntuacion() < 1 || v.getPuntuacion() > 5) {
                return ResponseEntity.badRequest()
                        .body(createError("La puntuación debe estar entre 1 y 5 estrellas."));
            }

            // Verificar que no exista ya una valoración para esta cita
            if (repo.findByCitaId(cita.getIdCita()).isPresent()) {
                return ResponseEntity.badRequest()
                        .body(createError("Esta cita ya tiene una valoración registrada. " +
                                "No se puede valorar la misma cita más de una vez."));
            }

            // Establecer la cita completa y guardar
            v.setCita(cita);
            return ResponseEntity.ok(repo.save(v));

        } catch (Exception e) {
            return ResponseEntity.badRequest()
                    .body(createError("Ha ocurrido un error al guardar la valoración: " + e.getMessage()));
        }
    }

    @PutMapping("/{id}")
    public ResponseEntity<?> update(@PathVariable int id, @RequestBody Valoracion v) {
        try {
            if (!repo.existsById(id)) {
                return ResponseEntity.notFound().build();
            }

            // Si se cambia la cita asociada, validar que también esté realizada
            if (v.getCita() != null && v.getCita().getIdCita() != null) {
                Cita cita = citaRepository.findById(v.getCita().getIdCita()).orElse(null);
                if (cita == null) {
                    return ResponseEntity.badRequest()
                            .body(createError("La cita indicada no existe. Verifica el identificador e inténtalo de nuevo."));
                }
                if (cita.getEstado() != Cita.Estado.realizada) {
                    return ResponseEntity.badRequest()
                            .body(createError("Solo puedes asociar una valoración a citas que hayan sido realizadas. " +
                                    "El estado actual de esta cita es: " + cita.getEstado() + "."));
                }
                v.setCita(cita);
            }

            // Validar puntuación si se envía
            if (v.getPuntuacion() != null && (v.getPuntuacion() < 1 || v.getPuntuacion() > 5)) {
                return ResponseEntity.badRequest()
                        .body(createError("La puntuación debe estar entre 1 y 5 estrellas."));
            }

            v.setIdValoracion(id);
            return ResponseEntity.ok(repo.save(v));

        } catch (Exception e) {
            return ResponseEntity.badRequest()
                    .body(createError("Ha ocurrido un error al actualizar la valoración: " + e.getMessage()));
        }
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<?> delete(@PathVariable int id) {
        if (!repo.existsById(id)) {
            return ResponseEntity.status(404)
                    .body(createError("No se encontró ninguna valoración con el identificador " + id + "."));
        }
        repo.deleteById(id);
        return ResponseEntity.noContent().build();
    }

    // Método auxiliar para crear respuestas de error con mensaje personalizado
    private Map<String, String> createError(String mensaje) {
        Map<String, String> error = new HashMap<>();
        error.put("error", mensaje);
        return error;
    }
}