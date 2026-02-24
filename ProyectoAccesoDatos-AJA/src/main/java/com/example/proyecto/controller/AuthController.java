package com.example.proyecto.controller;

import com.example.proyecto.domain.Cliente;
import com.example.proyecto.domain.Usuario;
import com.example.proyecto.domain.PasswordResetToken;
import com.example.proyecto.repository.ClienteRepository;
import com.example.proyecto.repository.UsuarioRepository;
import com.example.proyecto.repository.PasswordResetTokenRepository;
import com.example.proyecto.security.jwt.JwtUtils;
import com.example.proyecto.security.payload.JwtResponse;
import com.example.proyecto.security.payload.LoginRequest;
import com.example.proyecto.security.payload.MessageResponse;
import com.example.proyecto.security.payload.SignupRequest;
import com.example.proyecto.services.UserDetailsImpl;
import com.example.proyecto.services.EmailService;
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

import java.security.SecureRandom;
import java.util.HashMap;
import java.util.Map;
import java.util.Optional;
import java.util.UUID;

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
    PasswordResetTokenRepository passwordResetTokenRepository;

    @Autowired
    EmailService emailService;

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
        if (usuarioRepository.findByEmail(signUpRequest.getCorreo()) != null) {
            return ResponseEntity
                    .badRequest()
                    .body(new MessageResponse("Error: El correo ya está en uso"));
        }

        Usuario usuario = new Usuario();
        usuario.setNombre(signUpRequest.getNombre());
        usuario.setApellidos(signUpRequest.getApellidos());
        usuario.setCorreo(signUpRequest.getCorreo());
        usuario.setContrasena(encoder.encode(signUpRequest.getContrasena()));

        try {
            Usuario.Rol rol = Usuario.Rol.valueOf(signUpRequest.getRol().toUpperCase());
            usuario.setRol(rol);
        } catch (IllegalArgumentException e) {
            return ResponseEntity
                    .badRequest()
                    .body(new MessageResponse("Error: Rol inválido. Use: administrador, alumno o cliente"));
        }

        usuario = usuarioRepository.save(usuario);

        if (usuario.getRol() == Usuario.Rol.CLIENTE) {
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

    @PostMapping("/forgot-password")
    public ResponseEntity<?> forgotPassword(@RequestBody Map<String, String> request) {
        String correo = request.get("correo");

        if (correo == null || correo.trim().isEmpty()) {
            return ResponseEntity.badRequest()
                    .body(new MessageResponse("El correo es obligatorio"));
        }

        Usuario usuario = usuarioRepository.findByEmail(correo);
        if (usuario == null) {
            return ResponseEntity.status(404)
                    .body(new MessageResponse("No existe una cuenta con ese correo electrónico"));
        }

        try {
            String pin = generatePin();
            PasswordResetToken token = new PasswordResetToken(correo, pin);
            passwordResetTokenRepository.save(token);
            emailService.sendPasswordResetEmail(correo, pin, usuario.getNombre());
            return ResponseEntity.ok(new MessageResponse("PIN enviado al correo electrónico"));

        } catch (Exception e) {
            e.printStackTrace();
            return ResponseEntity.status(500)
                    .body(new MessageResponse("Error al enviar el correo: " + e.getMessage()));
        }
    }

    @PostMapping("/verify-pin")
    public ResponseEntity<?> verifyPin(@RequestBody Map<String, String> request) {
        String correo = request.get("correo");
        String pin = request.get("pin");

        if (correo == null || pin == null) {
            return ResponseEntity.badRequest().body(new MessageResponse("Correo y PIN son obligatorios"));
        }

        Optional<PasswordResetToken> tokenOpt = passwordResetTokenRepository
                .findByCorreoAndPinAndUsadoFalse(correo, pin);

        if (tokenOpt.isEmpty()) {
            return ResponseEntity.status(400).body(new MessageResponse("PIN inválido o ya utilizado"));
        }

        PasswordResetToken token = tokenOpt.get();

        if (token.isExpired()) {
            return ResponseEntity.status(400).body(new MessageResponse("El PIN ha expirado. Solicita uno nuevo."));
        }

        Map<String, Object> response = new HashMap<>();
        response.put("success", true);
        response.put("message", "PIN verificado correctamente");
        return ResponseEntity.ok(response);
    }

    @PostMapping("/reset-password")
    @Transactional
    public ResponseEntity<?> resetPassword(@RequestBody Map<String, String> request) {
        String correo = request.get("correo");
        String pin = request.get("pin");
        String nuevaContrasena = request.get("nuevaContrasena");

        if (correo == null || pin == null || nuevaContrasena == null) {
            return ResponseEntity.badRequest().body(new MessageResponse("Todos los campos son obligatorios"));
        }

        if (nuevaContrasena.length() < 6) {
            return ResponseEntity.badRequest().body(new MessageResponse("La contraseña debe tener al menos 6 caracteres"));
        }

        Optional<PasswordResetToken> tokenOpt = passwordResetTokenRepository
                .findByCorreoAndPinAndUsadoFalse(correo, pin);

        if (tokenOpt.isEmpty()) {
            return ResponseEntity.status(400).body(new MessageResponse("PIN inválido o ya utilizado"));
        }

        PasswordResetToken token = tokenOpt.get();

        if (token.isExpired()) {
            return ResponseEntity.status(400).body(new MessageResponse("El PIN ha expirado. Solicita uno nuevo."));
        }

        Usuario usuario = usuarioRepository.findByEmail(correo);
        if (usuario == null) {
            return ResponseEntity.status(404).body(new MessageResponse("Usuario no encontrado"));
        }

        usuario.setContrasena(encoder.encode(nuevaContrasena));
        usuarioRepository.save(usuario);

        token.setUsado(true);
        passwordResetTokenRepository.save(token);

        try {
            emailService.sendPasswordChangedConfirmation(correo, usuario.getNombre());
        } catch (Exception e) {
            System.err.println("Error enviando correo de confirmación: " + e.getMessage());
        }

        return ResponseEntity.ok(new MessageResponse("Contraseña restablecida exitosamente"));
    }

    // ✅ NUEVO: ENDPOINT PARA LOGIN SOCIAL (GOOGLE / FACEBOOK)
    @PostMapping("/social-login")
    @Transactional
    public ResponseEntity<?> authenticateSocialUser(@RequestBody Map<String, String> socialRequest) {
        String correo = socialRequest.get("correo");
        String nombreCompleto = socialRequest.get("nombre");
        String fotoPerfil = socialRequest.get("fotoUrl");

        String nombre = "Usuario";
        String apellidos = "";
        if (nombreCompleto != null && !nombreCompleto.isEmpty()) {
            String[] partes = nombreCompleto.split(" ", 2);
            nombre = partes[0];
            if (partes.length > 1) {
                apellidos = partes[1];
            }
        }

        Usuario usuario = usuarioRepository.findByEmail(correo);

        if (usuario == null) {
            usuario = new Usuario();
            usuario.setNombre(nombre);
            usuario.setApellidos(apellidos);
            usuario.setCorreo(correo);
            usuario.setContrasena(encoder.encode(UUID.randomUUID().toString()));
            usuario.setRol(Usuario.Rol.CLIENTE);
            if(fotoPerfil != null && !fotoPerfil.isEmpty()) {
                usuario.setFotoPerfil(fotoPerfil);
            }
            usuario = usuarioRepository.save(usuario);

            Cliente cliente = new Cliente();
            cliente.setIdUsuario(usuario.getIdUsuario());
            cliente.setTelefono("");
            cliente.setDireccion("");
            clienteRepository.save(cliente);
        }

        String jwt = jwtUtils.generateJwtTokenFromUsername(usuario.getCorreo());

        return ResponseEntity.ok(new JwtResponse(jwt,
                usuario.getIdUsuario(),
                usuario.getNombre(),
                usuario.getApellidos(),
                usuario.getCorreo(),
                usuario.getRol().name()));
    }

    private String generatePin() {
        SecureRandom random = new SecureRandom();
        int pin = 100000 + random.nextInt(900000);
        return String.valueOf(pin);
    }
}