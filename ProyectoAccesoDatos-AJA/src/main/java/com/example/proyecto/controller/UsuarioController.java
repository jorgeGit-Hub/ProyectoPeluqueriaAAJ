package com.example.proyecto.controller;

import com.example.proyecto.domain.Usuario;
import com.example.proyecto.domain.Cliente;
import com.example.proyecto.domain.Administrador;
import com.example.proyecto.repository.UsuarioRepository;
import com.example.proyecto.repository.ClienteRepository;
import com.example.proyecto.repository.AdministradorRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.crypto.password.PasswordEncoder;
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

    @Autowired
    private AdministradorRepository administradorRepository;

    @Autowired
    private PasswordEncoder encoder;

    // ✅ NUEVO: Listar todos los usuarios
    @GetMapping
    public List<Usuario> all() {
        return repo.findAll();
    }

    // ✅ NUEVO: Obtener un usuario por ID
    @GetMapping("/{id}")
    public ResponseEntity<Usuario> get(@PathVariable int id) {
        return repo.findById(id)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

    // ✅ NUEVO: Actualizar un usuario
    @PutMapping("/{id}")
    public ResponseEntity<?> update(@PathVariable int id, @RequestBody Map<String, Object> payload) {
        return repo.findById(id).map(usuario -> {
            if (payload.containsKey("nombre"))
                usuario.setNombre((String) payload.get("nombre"));
            if (payload.containsKey("apellidos"))
                usuario.setApellidos((String) payload.get("apellidos"));
            if (payload.containsKey("correo"))
                usuario.setCorreo((String) payload.get("correo"));
            if (payload.containsKey("contrasena") && !((String) payload.get("contrasena")).isEmpty())
                usuario.setContrasena(encoder.encode((String) payload.get("contrasena")));
            if (payload.containsKey("rol")) {
                try {
                    usuario.setRol(Usuario.Rol.valueOf(((String) payload.get("rol")).toUpperCase()));
                } catch (IllegalArgumentException ignored) {}
            }
            // ✅ NUEVO: Actualizar la foto de perfil si se envía
            if (payload.containsKey("fotoPerfil")) {
                usuario.setFotoPerfil((String) payload.get("fotoPerfil"));
            }
            return ResponseEntity.ok(repo.save(usuario));
        }).orElse(ResponseEntity.notFound().build());
    }

    // ✅ NUEVO: Eliminar un usuario
    @DeleteMapping("/{id}")
    public ResponseEntity<Void> delete(@PathVariable int id) {
        if (!repo.existsById(id)) {
            return ResponseEntity.notFound().build();
        }
        repo.deleteById(id);
        return ResponseEntity.noContent().build();
    }

    // =========================
    // CREAR USUARIO + CLIENTE
    // =========================
    @Transactional
    @PostMapping(value = "/with-cliente", consumes = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<Map<String, Object>> createUserWithCliente(
            @RequestBody Map<String, Object> payload) {

        Map<String, Object> userData = (Map<String, Object>) payload.get("usuario");
        Map<String, Object> clienteData = (Map<String, Object>) payload.get("cliente");

        String correo = (String) userData.get("correo");

        if (repo.findByEmail(correo) != null) {
            return ResponseEntity.badRequest()
                    .body(Map.of("error", "El correo ya está en uso"));
        }

        Usuario usuario = new Usuario();
        usuario.setNombre((String) userData.get("nombre"));
        usuario.setApellidos((String) userData.get("apellidos"));
        usuario.setCorreo(correo);
        usuario.setContrasena(encoder.encode((String) userData.get("contrasena")));
        usuario.setRol(Usuario.Rol.CLIENTE);
        usuario = repo.save(usuario);

        Cliente cliente = new Cliente();
        cliente.setIdUsuario(usuario.getIdUsuario());

        if (clienteData != null) {
            cliente.setTelefono((String) clienteData.get("telefono"));
            cliente.setDireccion((String) clienteData.get("direccion"));
            cliente.setAlergenos((String) clienteData.get("alergenos"));
            cliente.setObservaciones((String) clienteData.get("observaciones"));
        }

        cliente = clienteRepository.save(cliente);

        return ResponseEntity.ok(Map.of(
                "usuario", usuario,
                "cliente", cliente,
                "message", "Usuario cliente creado correctamente"
        ));
    }

    // ==============================
    // CREAR USUARIO + ADMINISTRADOR
    // ==============================
    @Transactional
    @PostMapping(value = "/with-administrador", consumes = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<Map<String, Object>> createUserWithAdministrador(
            @RequestBody Map<String, Object> payload) {

        Map<String, Object> userData = (Map<String, Object>) payload.get("usuario");
        Map<String, Object> adminData = (Map<String, Object>) payload.get("administrador");

        String correo = (String) userData.get("correo");

        if (repo.findByEmail(correo) != null) {
            return ResponseEntity.badRequest()
                    .body(Map.of("error", "El correo ya está en uso"));
        }

        Usuario usuario = new Usuario();
        usuario.setNombre((String) userData.get("nombre"));
        usuario.setApellidos((String) userData.get("apellidos"));
        usuario.setCorreo(correo);
        usuario.setContrasena(encoder.encode((String) userData.get("contrasena")));
        usuario.setRol(Usuario.Rol.ADMINISTRADOR);
        usuario = repo.save(usuario);

        Administrador administrador = new Administrador();
        administrador.setIdUsuario(usuario.getIdUsuario());

        if (adminData != null) {
            administrador.setEspecialidad((String) adminData.get("especialidad"));
        }

        administrador = administradorRepository.save(administrador);

        return ResponseEntity.ok(Map.of(
                "usuario", usuario,
                "administrador", administrador,
                "message", "Usuario administrador creado correctamente"
        ));
    }
}