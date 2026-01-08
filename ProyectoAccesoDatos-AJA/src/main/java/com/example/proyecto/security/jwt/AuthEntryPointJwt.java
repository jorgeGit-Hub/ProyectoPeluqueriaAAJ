package com.example.proyecto.security.jwt;

import com.fasterxml.jackson.databind.ObjectMapper; // Librería de Jackson para mapear objetos Java a JSON
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.MediaType;
import org.springframework.security.core.AuthenticationException;
import org.springframework.security.web.AuthenticationEntryPoint; // Interfaz que implementa esta clase
import org.springframework.stereotype.Component;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

/**
 * Componente que maneja el inicio de la autenticación. Se activa cuando un usuario no autenticado
 * intenta acceder a un recurso protegido, retornando una respuesta HTTP 401 (Unauthorized).
 * Es el manejador de errores estándar para las APIs REST basadas en JWT.
 */
@Component
public class AuthEntryPointJwt implements AuthenticationEntryPoint {

    private static final Logger logger = LoggerFactory.getLogger(AuthEntryPointJwt.class);

    /**
     * Este método se llama cada vez que se lanza una AuthenticationException,
     * indicando que el cliente necesita autenticarse para acceder al recurso.
     */
    @Override
    public void commence(HttpServletRequest request, HttpServletResponse response,
                         AuthenticationException authException) throws IOException, ServletException {

        // 1. Registrar el error en el log del servidor.
        logger.error("Unauthorized error: {}", authException.getMessage());

        // 2. Configurar la respuesta HTTP para devolver JSON y el código 401.
        response.setContentType(MediaType.APPLICATION_JSON_VALUE);
        response.setStatus(HttpServletResponse.SC_UNAUTHORIZED); // Establece el código de estado a 401

        // 3. Preparar el cuerpo de la respuesta JSON para el cliente.
        final Map<String, Object> body = new HashMap<>();
        body.put("status", HttpServletResponse.SC_UNAUTHORIZED); // 401
        body.put("error", "Unauthorized"); // Mensaje de error fijo
        body.put("message", authException.getMessage()); // Mensaje detallado de la excepción de Spring Security
        body.put("path", request.getServletPath()); // Ruta donde ocurrió el error

        // 4. Escribir el mapa de datos como JSON en el flujo de salida de la respuesta.
        final ObjectMapper mapper = new ObjectMapper();
        mapper.writeValue(response.getOutputStream(), body); // Convierte el mapa a JSON y lo envía al cliente
    }
}