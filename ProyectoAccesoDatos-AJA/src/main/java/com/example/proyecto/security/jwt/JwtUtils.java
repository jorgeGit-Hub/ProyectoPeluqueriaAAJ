package com.example.proyecto.security.jwt;

// Importaciones de la librería JJWT para manejar la creación, parseo y validación de tokens
import io.jsonwebtoken.*;
import io.jsonwebtoken.security.Keys;
// Importaciones de Spring Framework
import org.springframework.stereotype.Component;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.core.Authentication;

// Importaciones estándar y de registro (logging)
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.example.proyecto.services.UserDetailsImpl; // Implementación del usuario de Spring Security

import javax.crypto.SecretKey;
import java.nio.charset.StandardCharsets;
import java.util.Date;

/**
 * Clase de utilidad para manejar todas las operaciones relacionadas con JSON Web Tokens (JWT):
 * generación, validación y extracción de información.
 */
@Component
public class JwtUtils {

    // Inicializa un logger para registrar eventos y errores.
    private static final Logger logger = LoggerFactory.getLogger(JwtUtils.class);

    // Inyecta el valor del secreto JWT desde el archivo de propiedades (application.properties/yml).
    @Value("${proyecto.app.jwtSecret}")
    private String jwtSecret;

    // Inyecta el tiempo de expiración del JWT en milisegundos.
    @Value("${proyecto.app.jwtExpirationMs}")
    private int jwtExpirationMs;

    /**
     * Convierte la cadena secreta (jwtSecret) en una clave segura (SecretKey)
     * para firmar y verificar los tokens.
     * @return SecretKey utilizada para la firma HS512.
     */
    private SecretKey getSigningKey() {
        // Convierte el String secreto a un array de bytes usando UTF-8.
        byte[] keyBytes = jwtSecret.getBytes(StandardCharsets.UTF_8);
        // Usa JJWT para crear una clave HMAC SHA-512 segura a partir de los bytes.
        return Keys.hmacShaKeyFor(keyBytes);
    }

    /**
     * Genera un token JWT para un usuario autenticado.
     * @param authentication Objeto de autenticación de Spring Security.
     * @return El token JWT generado como String.
     */
    public String generateJwtToken(Authentication authentication) {
        // Obtiene el objeto UserDetailsImpl que contiene la información del usuario principal.
        UserDetailsImpl userPrincipal = (UserDetailsImpl) authentication.getPrincipal();

        // Comienza la construcción del JWT (JSON Web Token).
        return Jwts.builder()
                // Define el 'Subject' (sujeto) del token, que es el nombre de usuario.
                .setSubject(userPrincipal.getUsername())
                // Define la fecha de emisión (Issued At).
                .setIssuedAt(new Date())
                // Define la fecha de expiración, calculada como la hora actual + el tiempo de expiración configurado.
                .setExpiration(new Date((new Date()).getTime() + jwtExpirationMs))
                // Firma el token con la clave secreta y el algoritmo HS512.
                .signWith(getSigningKey(), SignatureAlgorithm.HS512)
                // Finaliza la construcción y compacta el token en su formato final de cadena.
                .compact();
    }

    /**
     * ✅ NUEVO: Genera un token JWT directamente desde un nombre de usuario (o correo).
     * Ideal para inicios de sesión sociales donde no hay contraseña.
     * @param username El correo o nombre del usuario.
     * @return El token JWT generado.
     */
    public String generateJwtTokenFromUsername(String username) {
        return Jwts.builder()
                .setSubject(username)
                .setIssuedAt(new Date())
                .setExpiration(new Date((new Date()).getTime() + jwtExpirationMs))
                .signWith(getSigningKey(), SignatureAlgorithm.HS512)
                .compact();
    }

    /**
     * Extrae el nombre de usuario (Subject) del token JWT.
     * @param token El token JWT.
     * @return El nombre de usuario contenido en el token.
     */
    public String getUserNameFromJwtToken(String token) {
        // Prepara el parser (analizador) de JWT.
        return Jwts.parserBuilder()
                // Define la clave que se debe usar para verificar la firma del token.
                .setSigningKey(getSigningKey())
                .build()
                // Analiza el token y verifica su firma y estructura.
                .parseClaimsJws(token)
                // Obtiene el cuerpo del token (claims/reclamaciones).
                .getBody()
                // Extrae el valor del campo 'Subject' (que es el nombre de usuario).
                .getSubject();
    }

    /**
     * Valida la integridad y validez de un token JWT.
     * Verifica la firma y si el token ha expirado.
     * @param authToken El token JWT a validar.
     * @return true si el token es válido, false en caso de error.
     */
    public boolean validateJwtToken(String authToken) {
        try {
            // Intenta analizar el token. Si el token está expirado o la firma es inválida,
            // lanzará una excepción que será capturada a continuación.
            Jwts.parserBuilder().setSigningKey(getSigningKey()).build().parseClaimsJws(authToken);
            return true; // Si llega aquí, el token es válido.
        } catch (SecurityException e) {
            // Error: La firma del token es inválida (ej. ha sido alterado).
            logger.error("Invalid JWT signature: {}", e.getMessage());
        } catch (MalformedJwtException e) {
            // Error: El token no está correctamente formado.
            logger.error("Invalid JWT token: {}", e.getMessage());
        } catch (ExpiredJwtException e) {
            // Error: El token ha expirado.
            logger.error("JWT token is expired: {}", e.getMessage());
        } catch (UnsupportedJwtException e) {
            // Error: El token no es compatible o no está soportado.
            logger.error("JWT token is unsupported: {}", e.getMessage());
        } catch (IllegalArgumentException e) {
            // Error: El token es nulo o la cadena de claims está vacía.
            logger.error("JWT claims string is empty: {}", e.getMessage());
        }

        return false; // Si se captura alguna excepción, el token no es válido.
    }
}