package com.example.proyecto.repository;

import com.example.proyecto.domain.Cita;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import java.util.List;
import java.time.LocalDate;

public interface CitaRepository extends JpaRepository<Cita, Integer> {

    // Buscar citas por fecha
    @Query(value="SELECT * FROM Cita WHERE fecha = :fecha", nativeQuery = true)
    List<Cita> findByFecha(@Param("fecha") LocalDate fecha);

    // ✅ MODIFICADO: Buscar citas por horario semanal (antes era por servicio)
    @Query(value="SELECT * FROM Cita WHERE id_horario_semanal = :idHorario AND fecha >= :fromDate", nativeQuery = true)
    List<Cita> findByHorarioAndFromDate(@Param("idHorario") int idHorario, @Param("fromDate") LocalDate fromDate);

    // Buscar citas de un cliente
    @Query(value="SELECT * FROM Cita WHERE id_cliente = :idCliente ORDER BY fecha DESC", nativeQuery = true)
    List<Cita> findByClienteId(@Param("idCliente") int idCliente);

    // ✅ MODIFICADO: Buscar citas por grupo y fecha (usando join con horario_semanal)
    @Query(value="SELECT c.* FROM Cita c " +
            "JOIN horario_semanal h ON c.id_horario_semanal = h.id_horario " +
            "WHERE h.id_grupo = :idGrupo AND c.fecha = :fecha " +
            "ORDER BY h.hora_inicio ASC", nativeQuery = true)
    List<Cita> findByGrupoAndFecha(@Param("idGrupo") int idGrupo, @Param("fecha") LocalDate fecha);

    // ✅ NUEVO: Buscar citas por servicio y fecha (útil para validaciones)
    @Query(value="SELECT c.* FROM Cita c " +
            "JOIN horario_semanal h ON c.id_horario_semanal = h.id_horario " +
            "WHERE h.id_servicio = :idServicio AND c.fecha >= :fromDate", nativeQuery = true)
    List<Cita> findByServicioAndFromDate(@Param("idServicio") int idServicio, @Param("fromDate") LocalDate fromDate);
}