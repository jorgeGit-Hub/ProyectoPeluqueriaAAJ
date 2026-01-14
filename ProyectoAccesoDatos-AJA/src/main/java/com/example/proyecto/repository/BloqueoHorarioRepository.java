package com.example.proyecto.repository;

import com.example.proyecto.domain.BloqueoHorario;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import java.util.List;
import java.time.LocalDate;

public interface BloqueoHorarioRepository extends JpaRepository<BloqueoHorario, Integer> {

    @Query(value="SELECT * FROM bloqueo_horario WHERE id_administrador = ?1", nativeQuery = true)
    List<BloqueoHorario> findByAdmin(int idAdmin);

    @Query(value="SELECT * FROM bloqueo_horario WHERE fecha = :fecha", nativeQuery = true)
    List<BloqueoHorario> findByFecha(@Param("fecha") LocalDate fecha);
}