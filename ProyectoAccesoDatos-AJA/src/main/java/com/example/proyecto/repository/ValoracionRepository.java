package com.example.proyecto.repository;

import com.example.proyecto.domain.Valoracion;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import java.util.List;
import java.util.Optional;

public interface ValoracionRepository extends JpaRepository<Valoracion, Integer> {

    @Query(value="SELECT * FROM Valoracion WHERE puntuacion >= ?1", nativeQuery = true)
    List<Valoracion> findByMinPuntuacion(int min);

    // ✅ NUEVO: Verificar si ya existe una valoración para una cita concreta
    @Query(value="SELECT * FROM Valoracion WHERE id_cita = :idCita LIMIT 1", nativeQuery = true)
    Optional<Valoracion> findByCitaId(@Param("idCita") int idCita);
}