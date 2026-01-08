package com.example.proyecto.controller;

import com.example.proyecto.domain.Cliente;
import com.example.proyecto.repository.ClienteRepository;
import com.example.proyecto.repository.UsuarioRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/clientes")
public class ClienteController {

    @Autowired
    private ClienteRepository repo;

    @Autowired
    private UsuarioRepository usuarioRepository;

    @GetMapping
    public List<Cliente> all() {
        return repo.findAll();
    }

    @GetMapping("/{id}")
    public ResponseEntity<Cliente> get(@PathVariable int id) {
        return repo.findById(id)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

    @GetMapping("/telefono/{frag}")
    public List<Cliente> byPhone(@PathVariable String frag) {
        return repo.findByTelefonoLike(frag);
    }

    @GetMapping("/direccion/{frag}")
    public List<Cliente> byDireccion(@PathVariable String frag) {
        return repo.findByDireccionLike(frag);
    }

    @GetMapping("/alergenos/{alergeno}")
    public List<Cliente> byAlergenos(@PathVariable String alergeno) {
        return repo.findByAlergenosContaining(alergeno);
    }

    @PostMapping
    public ResponseEntity<?> create(@RequestBody Cliente c) {
        try {
            // Verificar que el idUsuario esté presente
            if (c.getIdUsuario() == null) {
                return ResponseEntity.badRequest().body("idUsuario es requerido");
            }

            // Verificar que el usuario existe
            if (!usuarioRepository.existsById(c.getIdUsuario())) {
                return ResponseEntity.badRequest().body("Usuario no encontrado con id: " + c.getIdUsuario());
            }

            return ResponseEntity.ok(repo.save(c));
        } catch (Exception e) {
            e.printStackTrace();
            return ResponseEntity.badRequest().body("Error al crear cliente: " + e.getMessage());
        }
    }

    @PutMapping("/{id}")
    public ResponseEntity<?> update(@PathVariable int id, @RequestBody Cliente c) {
        try {
            if (!repo.existsById(id)) {
                return ResponseEntity.notFound().build();
            }

            c.setIdUsuario(id);
            return ResponseEntity.ok(repo.save(c));
        } catch (Exception e) {
            e.printStackTrace();
            return ResponseEntity.badRequest().body("Error al actualizar cliente: " + e.getMessage());
        }
    }

    @PostMapping("/from-user/{userId}")
    public ResponseEntity<?> createFromUser(@PathVariable int userId, @RequestBody Cliente c) {
        try {
            // Verificar que el usuario existe
            if (!usuarioRepository.existsById(userId)) {
                return ResponseEntity.badRequest().body("Usuario no encontrado con id: " + userId);
            }

            c.setIdUsuario(userId);
            return ResponseEntity.ok(repo.save(c));
        } catch (Exception e) {
            e.printStackTrace();
            return ResponseEntity.badRequest().body("Error al crear cliente: " + e.getMessage());
        }
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> delete(@PathVariable int id) {
        if (!repo.existsById(id)) {
            return ResponseEntity.notFound().build();
        }
        repo.deleteById(id);
        return ResponseEntity.noContent().build();
    }
}