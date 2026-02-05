package com.example.proyecto.domain;

import jakarta.persistence.*;
import java.time.LocalDateTime;

@Entity
@Table(name = "password_reset_token")
public class PasswordResetToken {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id")
    private Long id;

    @Column(name = "correo", nullable = false)
    private String correo;

    @Column(name = "pin", nullable = false)
    private String pin;

    @Column(name = "fecha_expiracion", nullable = false)
    private LocalDateTime fechaExpiracion;

    @Column(name = "usado", nullable = false)
    private boolean usado = false;

    @Column(name = "fecha_creacion", nullable = false)
    private LocalDateTime fechaCreacion;

    // Constructor vacío
    public PasswordResetToken() {
        this.fechaCreacion = LocalDateTime.now();
    }

    // Constructor con parámetros
    public PasswordResetToken(String correo, String pin) {
        this.correo = correo;
        this.pin = pin;
        this.fechaCreacion = LocalDateTime.now();
        // El PIN expira en 15 minutos
        this.fechaExpiracion = LocalDateTime.now().plusMinutes(15);
        this.usado = false;
    }

    // Getters y Setters
    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getCorreo() {
        return correo;
    }

    public void setCorreo(String correo) {
        this.correo = correo;
    }

    public String getPin() {
        return pin;
    }

    public void setPin(String pin) {
        this.pin = pin;
    }

    public LocalDateTime getFechaExpiracion() {
        return fechaExpiracion;
    }

    public void setFechaExpiracion(LocalDateTime fechaExpiracion) {
        this.fechaExpiracion = fechaExpiracion;
    }

    public boolean isUsado() {
        return usado;
    }

    public void setUsado(boolean usado) {
        this.usado = usado;
    }

    public LocalDateTime getFechaCreacion() {
        return fechaCreacion;
    }

    public void setFechaCreacion(LocalDateTime fechaCreacion) {
        this.fechaCreacion = fechaCreacion;
    }

    // Método para verificar si el token ha expirado
    public boolean isExpired() {
        return LocalDateTime.now().isAfter(this.fechaExpiracion);
    }
}