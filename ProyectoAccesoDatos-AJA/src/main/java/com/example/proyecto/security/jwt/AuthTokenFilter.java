package com.example.proyecto.security.jwt;

import com.example.proyecto.services.UserDetailsServiceImpl;
// Importaciones de Jakarta Servlet (API de Spring Boot 3+) para manejar la solicitud/respuesta
import jakarta.servlet.FilterChain;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
// Importaciones de Spring Security y Logging
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.web.authentication.WebAuthenticationDetailsSource;
import org.springframework.util.StringUtils;
import org.springframework.web.filter.OncePerRequestFilter; // Asegura que el filtro se ejecute una sola vez por solicitud

import java.io.IOException;

/**
 * Filtro de Seguridad: Se ejecuta en cada solicitud para examinar la cabecera 'Authorization',
 * validar el token JWT y establecer la información de autenticación del usuario en el SecurityContext
 * de Spring si el token es válido.
 */
public class AuthTokenFilter extends OncePerRequestFilter {

    // Inyecta la utilidad JWT para la validación y extracción de datos del token.
    @Autowired
    private JwtUtils jwtUtils;

    // Inyecta el servicio para cargar los detalles completos del usuario (roles/permisos) por nombre de usuario.
    @Autowired
    private UserDetailsServiceImpl userDetailsService;

    // Logger para registrar errores durante el proceso de autenticación.
    private static final Logger logger = LoggerFactory.getLogger(AuthTokenFilter.class);

    /**
     * Lógica principal del filtro que se ejecuta en cada solicitud HTTP.
     */
    @Override
    protected void doFilterInternal(HttpServletRequest request, HttpServletResponse response, FilterChain filterChain)
            throws ServletException, IOException {
        try {
            // 1. Intentar extraer el token JWT de la cabecera de la solicitud.
            String jwt = parseJwt(request);

            // 2. Comprobar si se encontró un token y si es válido.
            if (jwt != null && jwtUtils.validateJwtToken(jwt)) {

                // 3. Si es válido, extraer el nombre de usuario (Subject) del token.
                String username = jwtUtils.getUserNameFromJwtToken(jwt);

                // 4. Cargar los detalles completos del usuario (roles, permisos) usando el nombre de usuario.
                UserDetails userDetails = userDetailsService.loadUserByUsername(username);

                // 5. Crear un objeto de autenticación para Spring Security.
                UsernamePasswordAuthenticationToken authentication =
                        new UsernamePasswordAuthenticationToken(
                                userDetails,
                                null, // La credencial (contraseña) es null en JWT después de la validación.
                                userDetails.getAuthorities()); // Incluye los roles/autoridades del usuario.

                // 6. Añadir detalles de la solicitud (como IP) al objeto de autenticación.
                authentication.setDetails(new WebAuthenticationDetailsSource().buildDetails(request));

                // 7. Establecer el usuario autenticado en el contexto de seguridad de Spring.
                // Esto permite que el resto de la aplicación (controladores) sepa quién está logueado.
                SecurityContextHolder.getContext().setAuthentication(authentication);
            }
        } catch (Exception e) {
            // Registrar cualquier error ocurrido al intentar establecer la autenticación.
            logger.error("Cannot set user authentication: {}", e.getMessage());
        }

        // 8. Continuar con el siguiente filtro en la cadena de Spring Security.
        filterChain.doFilter(request, response);
    }

    /**
     * Método auxiliar para extraer el token JWT de la cabecera 'Authorization'.
     * Espera el formato: Authorization: Bearer <token_jwt>
     * @param request La solicitud HTTP.
     * @return El token JWT como String, o null si no se encuentra el formato correcto.
     */
    private String parseJwt(HttpServletRequest request) {
        // Obtener la cabecera de autorización.
        String headerAuth = request.getHeader("Authorization");

        // Comprobar si la cabecera existe, tiene texto y comienza con "Bearer ".
        if (StringUtils.hasText(headerAuth) && headerAuth.startsWith("Bearer ")) {
            // Si es así, se corta el prefijo "Bearer " (que tiene 7 caracteres, incluyendo el espacio)
            // y se devuelve solo el token.
            return headerAuth.substring(7);
        }

        return null;
    }
}