package com.example.proyecto.domain;

import jakarta.persistence.AttributeConverter;
import jakarta.persistence.Converter;

/**
 * Convertidor personalizado para el enum Rol que acepta valores en minúsculas o mayúsculas
 * desde la base de datos y los convierte al enum correcto.
 */
@Converter(autoApply = true)
public class RolConverter implements AttributeConverter<Usuario.Rol, String> {

    /**
     * Convierte el enum a String para guardarlo en la base de datos (siempre en MAYÚSCULAS)
     */
    @Override
    public String convertToDatabaseColumn(Usuario.Rol rol) {
        if (rol == null) {
            return null;
        }
        return rol.name(); // Guarda en MAYÚSCULAS
    }

    /**
     * Convierte el String de la base de datos al enum Rol
     * Acepta tanto minúsculas como mayúsculas
     */
    @Override
    public Usuario.Rol convertToEntityAttribute(String dbData) {
        if (dbData == null || dbData.trim().isEmpty()) {
            return null;
        }

        try {
            // Convierte a mayúsculas antes de buscar el enum
            return Usuario.Rol.valueOf(dbData.toUpperCase());
        } catch (IllegalArgumentException e) {
            // Si no encuentra el rol, lanza una excepción más descriptiva
            throw new IllegalArgumentException(
                    "Rol inválido en la base de datos: '" + dbData + "'. " +
                            "Valores válidos: ADMINISTRADOR, ALUMNO, CLIENTE"
            );
        }
    }
}