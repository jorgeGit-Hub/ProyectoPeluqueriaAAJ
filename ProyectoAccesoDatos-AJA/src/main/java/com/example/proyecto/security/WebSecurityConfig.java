package com.example.proyecto.security;

import com.example.proyecto.security.jwt.AuthEntryPointJwt;
import com.example.proyecto.security.jwt.AuthTokenFilter;
import com.example.proyecto.services.UserDetailsServiceImpl;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.dao.DaoAuthenticationProvider;
import org.springframework.security.config.annotation.authentication.configuration.AuthenticationConfiguration;
import org.springframework.security.config.annotation.method.configuration.EnableMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;

@Configuration
@EnableWebSecurity
@EnableMethodSecurity(prePostEnabled = true)
public class WebSecurityConfig {
    @Autowired
    UserDetailsServiceImpl userDetailsService;

    @Autowired
    private AuthEntryPointJwt unauthorizedHandler;

    @Bean
    public AuthTokenFilter authenticationJwtTokenFilter() {
        return new AuthTokenFilter();
    }

    @Bean
    public DaoAuthenticationProvider authenticationProvider() {
        DaoAuthenticationProvider authProvider = new DaoAuthenticationProvider();
        authProvider.setUserDetailsService(userDetailsService);
        authProvider.setPasswordEncoder(passwordEncoder());
        return authProvider;
    }

    @Bean
    public AuthenticationManager authenticationManager(AuthenticationConfiguration authConfig) throws Exception {
        return authConfig.getAuthenticationManager();
    }

    @Bean
    public PasswordEncoder passwordEncoder() {
        return new BCryptPasswordEncoder();
    }

    @Bean
    public SecurityFilterChain filterChain(HttpSecurity http) throws Exception {
        http
                .csrf(csrf -> csrf.disable())
                .exceptionHandling(exception -> exception.authenticationEntryPoint(unauthorizedHandler))
                .sessionManagement(session -> session.sessionCreationPolicy(SessionCreationPolicy.STATELESS))
                .authorizeHttpRequests(auth -> auth
                        // Endpoints públicos (sin autenticación)
                        .requestMatchers("/api/auth/**").permitAll()
                        .requestMatchers("/api/dev/**").permitAll()
                        .requestMatchers("/swagger-ui/**", "/v3/api-docs/**", "/swagger-ui.html").permitAll()
                        .requestMatchers("/api/servicios/**").permitAll()

                        // IMPORTANTE: Estos endpoints ESPECÍFICOS deben ir ANTES de la regla general /api/usuarios/**
                        .requestMatchers("/api/usuarios/with-cliente").permitAll()
                        .requestMatchers("/api/usuarios/with-administrador").permitAll()

                        // Endpoints solo para ADMINISTRADOR (regla general, va DESPUÉS de las específicas)
                        .requestMatchers("/api/administradores/**").hasRole("ADMINISTRADOR")
                        .requestMatchers("/api/usuarios/**").hasRole("ADMINISTRADOR")

                        // Bloqueos: ADMINISTRADOR puede crear/modificar, ALUMNO solo consultar
                        .requestMatchers("/api/bloqueos").hasAnyRole("ADMINISTRADOR", "ALUMNO")
                        .requestMatchers("/api/bloqueos/{id}").hasAnyRole("ADMINISTRADOR", "ALUMNO")
                        .requestMatchers("/api/bloqueos/admin/{id}").hasAnyRole("ADMINISTRADOR", "ALUMNO")
                        .requestMatchers("/api/bloqueos/**").hasRole("ADMINISTRADOR")

                        // Grupos: ADMINISTRADOR y ALUMNO
                        .requestMatchers("/api/grupos/**").hasAnyRole("ADMINISTRADOR", "ALUMNO")

                        // Citas: ADMINISTRADOR puede todo, clientes autenticados también
                        .requestMatchers("/api/citas/**").authenticated()

                        // Valoraciones: requiere autenticación
                        .requestMatchers("/api/valoraciones/**").authenticated()

                        // Clientes: requiere autenticación
                        .requestMatchers("/api/clientes/**").authenticated()

                        // Cualquier otra petición requiere autenticación
                        .anyRequest().authenticated()
                );

        http.authenticationProvider(authenticationProvider());
        http.addFilterBefore(authenticationJwtTokenFilter(), UsernamePasswordAuthenticationFilter.class);

        return http.build();
    }
}