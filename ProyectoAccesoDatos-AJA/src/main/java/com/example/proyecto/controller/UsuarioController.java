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

        // 🔹 Crear usuario
        Usuario usuario = new Usuario();
        usuario.setNombre((String) userData.get("nombre"));
        usuario.setApellidos((String) userData.get("apellidos"));
        usuario.setCorreo(correo);
        usuario.setContrasena(
                encoder.encode((String) userData.get("contrasena"))
        );

        // 🔥 ROL FIJO
        usuario.setRol(Usuario.Rol.CLIENTE);

        usuario = repo.save(usuario);

        // 🔹 Crear cliente
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

        // 🔹 Crear usuario
        Usuario usuario = new Usuario();
        usuario.setNombre((String) userData.get("nombre"));
        usuario.setApellidos((String) userData.get("apellidos"));
        usuario.setCorreo(correo);
        usuario.setContrasena(
                encoder.encode((String) userData.get("contrasena"))
        );

        // 🔥 ROL FIJO
        usuario.setRol(Usuario.Rol.ADMINISTRADOR);

        usuario = repo.save(usuario);

        // 🔹 Crear administrador
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
