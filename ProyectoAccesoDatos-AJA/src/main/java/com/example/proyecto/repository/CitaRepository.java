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

    @Query(value="SELECT * FROM Cita WHERE id_horario_semanal = :idHorario AND fecha >= :fromDate", nativeQuery = true)
    List<Cita> findByHorarioAndFromDate(@Param("idHorario") int idHorario, @Param("fromDate") LocalDate fromDate);

    // ✅ NUEVO: Buscar citas de un horario en una fecha exacta (para cálculo de capacidad/solapamientos)
    @Query(value="SELECT * FROM Cita WHERE id_horario_semanal = :idHorario AND fecha = :fecha", nativeQuery = true)
    List<Cita> findByHorarioAndFecha(@Param("idHorario") int idHorario, @Param("fecha") LocalDate fecha);

    @Query(value="SELECT * FROM Cita WHERE id_cliente = :idCliente ORDER BY fecha DESC", nativeQuery = true)
    List<Cita> findByClienteId(@Param("idCliente") int idCliente);

    // ✅ NUEVO: Buscar citas de un cliente en una fecha exacta (para evitar que se solapen sus propias citas)
    @Query(value="SELECT * FROM Cita WHERE id_cliente = :idCliente AND fecha = :fecha", nativeQuery = true)
    List<Cita> findByClienteAndFecha(@Param("idCliente") int idCliente, @Param("fecha") LocalDate fecha);

    // ✅ MODIFICADO: Ahora ordena por la hora de la cita real en lugar de la del horario
    @Query(value="SELECT c.* FROM Cita c " +
            "JOIN horario_semanal h ON c.id_horario_semanal = h.id_horario " +
            "WHERE h.id_grupo = :idGrupo AND c.fecha = :fecha " +
            "ORDER BY c.hora_inicio ASC", nativeQuery = true)
    List<Cita> findByGrupoAndFecha(@Param("idGrupo") int idGrupo, @Param("fecha") LocalDate fecha);

    @Query(value="SELECT c.* FROM Cita c " +
            "JOIN horario_semanal h ON c.id_horario_semanal = h.id_horario " +
            "WHERE h.id_servicio = :idServicio AND c.fecha >= :fromDate", nativeQuery = true)
    List<Cita> findByServicioAndFromDate(@Param("idServicio") int idServicio, @Param("fromDate") LocalDate fromDate);
}