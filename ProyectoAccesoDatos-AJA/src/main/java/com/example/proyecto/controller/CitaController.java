package com.example.proyecto.controller;

import com.example.proyecto.domain.Cita;
import com.example.proyecto.domain.Cliente;
import com.example.proyecto.repository.CitaRepository;
import com.example.proyecto.repository.ClienteRepository;
import com.example.proyecto.services.UserDetailsImpl;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.bind.annotation.*;

import java.time.LocalDate;
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

    @GetMapping("/fecha/{fecha}")
    public List<Cita> byFecha(@PathVariable String fecha) {
        try {
            return repo.findByFecha(LocalDate.parse(fecha));
        } catch (Exception e) {
            return List.of();
        }
    }

    @GetMapping("/servicio/{id}/{from}")
    public List<Cita> byServFrom(@PathVariable int id, @PathVariable String from) {
        try {
            return repo.findByServicioAndFromDate(id, LocalDate.parse(from));
        } catch (Exception e) {
            return List.of();
        }
    }

    @PostMapping
    public ResponseEntity<?> create(@RequestBody Cita c) {
        try {
            Authentication auth = SecurityContextHolder.getContext().getAuthentication();
            UserDetailsImpl userDetails = (UserDetailsImpl) auth.getPrincipal();

            // Verificar si es administrador
            boolean isAdmin = auth.getAuthorities().stream()
                    .anyMatch(grantedAuthority -> grantedAuthority.getAuthority().equals("ROLE_ADMINISTRADOR"));

            // Si no es administrador, verificar que el cliente existe y coincide con el usuario
            if (!isAdmin) {
                if (c.getCliente() == null || c.getCliente().getIdUsuario() == null) {
                    return ResponseEntity.badRequest()
                            .body(createError("Debe especificar un cliente para la cita"));
                }

                // Verificar que el usuario es el cliente de la cita
                if (!c.getCliente().getIdUsuario().equals(userDetails.getId())) {
                    return ResponseEntity.status(403)
                            .body(createError("No tiene permisos para crear citas para otros clientes"));
                }

                // Verificar que el cliente existe
                if (!clienteRepository.existsById(c.getCliente().getIdUsuario())) {
                    return ResponseEntity.badRequest()
                            .body(createError("El cliente especificado no existe"));
                }
            }

            // Los administradores pueden crear citas para cualquier cliente
            Cita citaGuardada = repo.save(c);
            return ResponseEntity.ok(citaGuardada);

        } catch (Exception e) {
            return ResponseEntity.badRequest()
                    .body(createError("Error al crear la cita: " + e.getMessage()));
        }
    }

    @PutMapping("/{id}")
    public ResponseEntity<?> update(@PathVariable int id, @RequestBody Cita c) {
        try {
            if (!repo.existsById(id)) {
                return ResponseEntity.notFound().build();
            }

            Authentication auth = SecurityContextHolder.getContext().getAuthentication();
            UserDetailsImpl userDetails = (UserDetailsImpl) auth.getPrincipal();

            // Verificar si es administrador
            boolean isAdmin = auth.getAuthorities().stream()
                    .anyMatch(grantedAuthority -> grantedAuthority.getAuthority().equals("ROLE_ADMINISTRADOR"));

            // Si no es administrador, verificar permisos
            if (!isAdmin) {
                Cita citaExistente = repo.findById(id).orElse(null);
                if (citaExistente == null) {
                    return ResponseEntity.notFound().build();
                }

                // Verificar que el usuario es el cliente de la cita
                if (citaExistente.getCliente() == null ||
                        !citaExistente.getCliente().getIdUsuario().equals(userDetails.getId())) {
                    return ResponseEntity.status(403)
                            .body(createError("No tiene permisos para modificar esta cita"));
                }
            }

            c.setIdCita(id);
            Cita citaActualizada = repo.save(c);
            return ResponseEntity.ok(citaActualizada);

        } catch (Exception e) {
            return ResponseEntity.badRequest()
                    .body(createError("Error al actualizar la cita: " + e.getMessage()));
        }
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<?> delete(@PathVariable int id) {
        try {
            if (!repo.existsById(id)) {
                return ResponseEntity.notFound().build();
            }

            Authentication auth = SecurityContextHolder.getContext().getAuthentication();
            UserDetailsImpl userDetails = (UserDetailsImpl) auth.getPrincipal();

            // Verificar si es administrador
            boolean isAdmin = auth.getAuthorities().stream()
                    .anyMatch(grantedAuthority -> grantedAuthority.getAuthority().equals("ROLE_ADMINISTRADOR"));

            // Si no es administrador, verificar permisos
            if (!isAdmin) {
                Cita citaExistente = repo.findById(id).orElse(null);
                if (citaExistente == null) {
                    return ResponseEntity.notFound().build();
                }

                // Verificar que el usuario es el cliente de la cita
                if (citaExistente.getCliente() == null ||
                        !citaExistente.getCliente().getIdUsuario().equals(userDetails.getId())) {
                    return ResponseEntity.status(403)
                            .body(createError("No tiene permisos para eliminar esta cita"));
                }
            }

            repo.deleteById(id);
            return ResponseEntity.noContent().build();

        } catch (Exception e) {
            return ResponseEntity.badRequest()
                    .body(createError("Error al eliminar la cita: " + e.getMessage()));
        }
    }

    // Método auxiliar para crear mensajes de error
    private Map<String, String> createError(String mensaje) {
        Map<String, String> error = new HashMap<>();
        error.put("error", mensaje);
        return error;
    }
}