package com.example.proyecto.controller;

import com.example.proyecto.domain.Cliente;
import com.example.proyecto.domain.Usuario;
import com.example.proyecto.repository.ClienteRepository;
import com.example.proyecto.repository.UsuarioRepository;
import com.example.proyecto.security.jwt.JwtUtils;
import com.example.proyecto.security.payload.JwtResponse;
import com.example.proyecto.security.payload.LoginRequest;
import com.example.proyecto.security.payload.MessageResponse;
import com.example.proyecto.security.payload.SignupRequest;
import com.example.proyecto.services.UserDetailsImpl;
import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.bind.annotation.*;

@CrossOrigin(origins = "*", maxAge = 3600)
@RestController
@RequestMapping("/api/auth")
public class AuthController {
    @Autowired
    AuthenticationManager authenticationManager;

    @Autowired
    UsuarioRepository usuarioRepository;

    @Autowired
    ClienteRepository clienteRepository;

    @Autowired
    PasswordEncoder encoder;

    @Autowired
    JwtUtils jwtUtils;

    @PostMapping("/signin")
    public ResponseEntity<?> authenticateUser(@Valid @RequestBody LoginRequest loginRequest) {
        Authentication authentication = authenticationManager.authenticate(
                new UsernamePasswordAuthenticationToken(loginRequest.getCorreo(), loginRequest.getContrasena()));

        SecurityContextHolder.getContext().setAuthentication(authentication);
        String jwt = jwtUtils.generateJwtToken(authentication);

        UserDetailsImpl userDetails = (UserDetailsImpl) authentication.getPrincipal();
        String rol = userDetails.getAuthorities().stream()
                .findFirst()
                .map(item -> item.getAuthority().replace("ROLE_", ""))
                .orElse("");

        return ResponseEntity.ok(new JwtResponse(jwt,
                userDetails.getId(),
                userDetails.getNombre(),
                userDetails.getApellidos(),
                userDetails.getCorreo(),
                rol));
    }

    @PostMapping("/signup")
    @Transactional
    public ResponseEntity<?> registerUser(@Valid @RequestBody SignupRequest signUpRequest) {
        // Verificar si el correo ya existe
        if (usuarioRepository.findByEmail(signUpRequest.getCorreo()) != null) {
            return ResponseEntity
                    .badRequest()
                    .body(new MessageResponse("Error: El correo ya está en uso"));
        }

        // Crear nuevo usuario
        Usuario usuario = new Usuario();
        usuario.setNombre(signUpRequest.getNombre());
        usuario.setApellidos(signUpRequest.getApellidos());
        usuario.setCorreo(signUpRequest.getCorreo());
        usuario.setContrasena(encoder.encode(signUpRequest.getContrasena()));

        // Asignar rol
        try {
            Usuario.Rol rol = Usuario.Rol.valueOf(signUpRequest.getRol().toLowerCase());
            usuario.setRol(rol);
        } catch (IllegalArgumentException e) {
            return ResponseEntity
                    .badRequest()
                    .body(new MessageResponse("Error: Rol inválido. Use: administrador, alumno o cliente"));
        }

        usuario = usuarioRepository.save(usuario);

        // Si el rol es cliente, crear también el registro en la tabla Cliente
        if (usuario.getRol() == Usuario.Rol.cliente) {
            Cliente cliente = new Cliente();
            cliente.setIdUsuario(usuario.getIdUsuario());
            cliente.setTelefono(signUpRequest.getTelefono());
            cliente.setDireccion(signUpRequest.getDireccion());
            cliente.setAlergenos(signUpRequest.getAlergenos());
            cliente.setObservaciones(signUpRequest.getObservaciones());
            clienteRepository.save(cliente);
        }

        return ResponseEntity.ok(new MessageResponse("Usuario registrado exitosamente"));
    }

    @GetMapping("/validate")
    public ResponseEntity<?> validateToken() {
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
        if (authentication != null && authentication.isAuthenticated()) {
            UserDetailsImpl userDetails = (UserDetailsImpl) authentication.getPrincipal();
            String rol = userDetails.getAuthorities().stream()
                    .findFirst()
                    .map(item -> item.getAuthority().replace("ROLE_", ""))
                    .orElse("");

            return ResponseEntity.ok(new JwtResponse(null,
                    userDetails.getId(),
                    userDetails.getNombre(),
                    userDetails.getApellidos(),
                    userDetails.getCorreo(),
                    rol));
        }
        return ResponseEntity.status(401).body(new MessageResponse("Token inválido"));
    }
}