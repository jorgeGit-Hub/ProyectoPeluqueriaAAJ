package com.example.proyecto.repository;

import com.example.proyecto.domain.Cita;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import java.util.List;
import java.time.LocalDate;

public interface CitaRepository extends JpaRepository<Cita, Integer> {

    @Query(value="SELECT * FROM Cita WHERE fecha = :fecha", nativeQuery = true)
    List<Cita> findByFecha(@Param("fecha") LocalDate fecha);

    @Query(value="SELECT * FROM Cita WHERE id_servicio = :idServicio AND fecha >= :fromDate", nativeQuery = true)
    List<Cita> findByServicioAndFromDate(@Param("idServicio") int idServicio, @Param("fromDate") LocalDate fromDate);

    @Query(value="SELECT * FROM Cita WHERE id_cliente = :idCliente ORDER BY fecha DESC, hora_inicio DESC", nativeQuery = true)
    List<Cita> findByClienteId(@Param("idCliente") int idCliente);

    @Query(value="SELECT c.* FROM Cita c JOIN Servicio s ON c.id_servicio = s.id_servicio WHERE s.id_grupo = :idGrupo AND c.fecha = :fecha ORDER BY c.hora_inicio ASC", nativeQuery = true)
    List<Cita> findByGrupoAndFecha(@Param("idGrupo") int idGrupo, @Param("fecha") LocalDate fecha);
}