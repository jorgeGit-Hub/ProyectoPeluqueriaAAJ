package com.example.proyecto.security.payload;

import jakarta.validation.constraints.NotBlank;

public class LoginRequest {
    @NotBlank
    private String correo;

    @NotBlank
    private String contrasena;

    public String getCorreo() {
        return correo;
    }

    public void setCorreo(String correo) {
        this.correo = correo;
    }

    public String getContrasena() {
        return contrasena;
    }

    public void setContrasena(String contrasena) {
        this.contrasena = contrasena;
    }
}