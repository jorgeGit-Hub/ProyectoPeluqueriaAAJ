package com.example.proyecto.repository;

import com.example.proyecto.domain.PasswordResetToken;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import java.time.LocalDateTime;
import java.util.Optional;

public interface PasswordResetTokenRepository extends JpaRepository<PasswordResetToken, Long> {

    // Buscar el token más reciente por correo y PIN que no haya sido usado
    @Query("SELECT t FROM PasswordResetToken t WHERE t.correo = :correo AND t.pin = :pin AND t.usado = false ORDER BY t.fechaCreacion DESC")
    Optional<PasswordResetToken> findByCorreoAndPinAndUsadoFalse(@Param("correo") String correo, @Param("pin") String pin);

    // Buscar todos los tokens no usados de un correo
    @Query("SELECT t FROM PasswordResetToken t WHERE t.correo = :correo AND t.usado = false")
    Optional<PasswordResetToken> findByCorreoAndUsadoFalse(@Param("correo") String correo);

    // Eliminar tokens expirados (para limpieza periódica)
    @Query("DELETE FROM PasswordResetToken t WHERE t.fechaExpiracion < :now")
    void deleteExpiredTokens(@Param("now") LocalDateTime now);
}