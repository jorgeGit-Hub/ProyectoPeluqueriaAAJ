package com.example.proyecto.controller;

import com.example.proyecto.domain.Cliente;
import com.example.proyecto.domain.Usuario;
import com.example.proyecto.repository.ClienteRepository;
import com.example.proyecto.repository.UsuarioRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

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

    // ✅ NUEVO: Devuelve clientes con nombre, apellidos y correo del Usuario
    @GetMapping("/completos")
    public List<Map<String, Object>> allCompletos() {
        List<Cliente> clientes = repo.findAll();
        List<Map<String, Object>> resultado = new ArrayList<>();

        for (Cliente cliente : clientes) {
            Map<String, Object> datos = new HashMap<>();
            datos.put("idUsuario", cliente.getIdUsuario());
            datos.put("telefono", cliente.getTelefono());
            datos.put("direccion", cliente.getDireccion());
            datos.put("alergenos", cliente.getAlergenos());
            datos.put("observaciones", cliente.getObservaciones());

            // Añadir datos del Usuario relacionado
            usuarioRepository.findById(cliente.getIdUsuario()).ifPresent(usuario -> {
                datos.put("nombre", usuario.getNombre());
                datos.put("apellidos", usuario.getApellidos());
                datos.put("correo", usuario.getCorreo());
            });

            resultado.add(datos);
        }

        return resultado;
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
            if (c.getIdUsuario() == null) {
                return ResponseEntity.badRequest().body("idUsuario es requerido");
            }
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