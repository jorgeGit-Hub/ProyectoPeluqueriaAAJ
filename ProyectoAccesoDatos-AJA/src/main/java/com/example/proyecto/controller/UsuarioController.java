package com.example.proyecto.controller;

import com.example.proyecto.domain.Usuario;
import com.example.proyecto.domain.Cliente;
import com.example.proyecto.repository.UsuarioRepository;
import com.example.proyecto.repository.ClienteRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.http.MediaType;
import org.springframework.transaction.annotation.Transactional;

import java.util.*;

@RestController
@RequestMapping("/api/usuarios")
public class UsuarioController {

    @Autowired
    private UsuarioRepository repo;

    @Autowired
    private ClienteRepository clienteRepository;

    @GetMapping
    public List<Usuario> all() {
        return repo.findAll();
    }

    @GetMapping("/{id}")
    public ResponseEntity<Usuario> get(@PathVariable int id) {
        return repo.findById(id)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

    @PostMapping(consumes = MediaType.APPLICATION_JSON_VALUE)
    public Usuario create(@RequestBody Usuario u) {
        return repo.save(u);
    }

    @Transactional
    @PostMapping(value = "/with-cliente", consumes = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<Map<String, Object>> createUserWithCliente(@RequestBody Map<String, Object> payload) {
        try {
            // Paso 1: Crear y guardar Usuario
            Map<String, Object> userData = (Map<String, Object>) payload.get("usuario");
            Usuario usuario = new Usuario();
            usuario.setNombre((String) userData.get("nombre"));
            usuario.setApellidos((String) userData.get("apellidos"));
            usuario.setCorreo((String) userData.get("correo"));
            usuario.setContrasena((String) userData.get("contrasena"));

            String rolStr = (String) userData.get("rol");
            if (rolStr != null) {
                usuario.setRol(Usuario.Rol.valueOf(rolStr));
            }

            // Guardar usuario
            usuario = repo.save(usuario);

            // Paso 2: Crear Cliente con el mismo ID
            Map<String, Object> clienteData = (Map<String, Object>) payload.get("cliente");
            Cliente cliente = new Cliente();

            // Establecer el ID del usuario como ID del cliente
            cliente.setIdUsuario(usuario.getIdUsuario());

            // Establecer los campos del cliente
            if (clienteData.get("telefono") != null) {
                cliente.setTelefono((String) clienteData.get("telefono"));
            }
            if (clienteData.get("direccion") != null) {
                cliente.setDireccion((String) clienteData.get("direccion"));
            }
            if (clienteData.get("alergenos") != null) {
                cliente.setAlergenos((String) clienteData.get("alergenos"));
            }
            if (clienteData.get("observaciones") != null) {
                cliente.setObservaciones((String) clienteData.get("observaciones"));
            }

            // Guardar cliente
            cliente = clienteRepository.save(cliente);

            Map<String, Object> result = new HashMap<>();
            result.put("usuario", usuario);
            result.put("cliente", cliente);
            return ResponseEntity.ok(result);

        } catch (Exception e) {
            e.printStackTrace();
            Map<String, Object> error = new HashMap<>();
            error.put("error", e.getMessage());
            error.put("type", e.getClass().getSimpleName());
            return ResponseEntity.badRequest().body(error);
        }
    }

    @PutMapping(value = "/{id}", consumes = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<Usuario> update(@PathVariable int id, @RequestBody Usuario u) {
        if (!repo.existsById(id)) {
            return ResponseEntity.notFound().build();
        }
        u.setIdUsuario(id);
        return ResponseEntity.ok(repo.save(u));
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