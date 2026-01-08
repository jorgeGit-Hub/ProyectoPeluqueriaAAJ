package com.example.proyecto.controller;

import com.example.proyecto.domain.BloqueoHorario;
import com.example.proyecto.repository.BloqueoHorarioRepository;
import com.example.proyecto.services.UserDetailsImpl;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/api/bloqueos")
public class BloqueoHorarioController {

    @Autowired
    private BloqueoHorarioRepository repo;

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

            // Opcional: verificar que el administrador que crea el bloqueo es el mismo que se asigna
            // o permitir que cualquier administrador cree bloqueos para otros

            BloqueoHorario bloqueoGuardado = repo.save(b);
            return ResponseEntity.ok(bloqueoGuardado);

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

            Authentication auth = SecurityContextHolder.getContext().getAuthentication();
            UserDetailsImpl userDetails = (UserDetailsImpl) auth.getPrincipal();

            // Opcional: verificar que solo puede modificar sus propios bloqueos
            BloqueoHorario bloqueoExistente = repo.findById(id).orElse(null);
            if (bloqueoExistente == null) {
                return ResponseEntity.notFound().build();
            }

            // Comentar las siguientes líneas si quieres que cualquier admin pueda modificar cualquier bloqueo
            /*
            if (bloqueoExistente.getAdministrador() != null &&
                !bloqueoExistente.getAdministrador().getIdUsuario().equals(userDetails.getId())) {
                return ResponseEntity.status(403)
                        .body(createError("No tiene permisos para modificar este bloqueo"));
            }
            */

            b.setIdBloqueo(id);
            BloqueoHorario bloqueoActualizado = repo.save(b);
            return ResponseEntity.ok(bloqueoActualizado);

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

            Authentication auth = SecurityContextHolder.getContext().getAuthentication();
            UserDetailsImpl userDetails = (UserDetailsImpl) auth.getPrincipal();

            // Opcional: verificar que solo puede eliminar sus propios bloqueos
            BloqueoHorario bloqueoExistente = repo.findById(id).orElse(null);
            if (bloqueoExistente == null) {
                return ResponseEntity.notFound().build();
            }

            // Comentar las siguientes líneas si quieres que cualquier admin pueda eliminar cualquier bloqueo
            /*
            if (bloqueoExistente.getAdministrador() != null &&
                !bloqueoExistente.getAdministrador().getIdUsuario().equals(userDetails.getId())) {
                return ResponseEntity.status(403)
                        .body(createError("No tiene permisos para eliminar este bloqueo"));
            }
            */

            repo.deleteById(id);
            return ResponseEntity.noContent().build();

        } catch (Exception e) {
            return ResponseEntity.badRequest()
                    .body(createError("Error al eliminar el bloqueo: " + e.getMessage()));
        }
    }

    // Método auxiliar para crear mensajes de error
    private Map<String, String> createError(String mensaje) {
        Map<String, String> error = new HashMap<>();
        error.put("error", mensaje);
        return error;
    }
}