package com.example.proyecto.repository;

import com.example.proyecto.domain.Cita;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import java.util.List;
import java.time.LocalDate;

public interface CitaRepository extends JpaRepository<Cita, Integer> {
    @Query(value="SELECT * FROM Cita WHERE fecha = ?1", nativeQuery = true)
    List<Cita> findByFecha(LocalDate fecha);

    @Query(value="SELECT * FROM Cita WHERE id_servicio = ?1 AND fecha >= ?2", nativeQuery = true)
    List<Cita> findByServicioAndFromDate(int idServicio, LocalDate fromDate);
}
