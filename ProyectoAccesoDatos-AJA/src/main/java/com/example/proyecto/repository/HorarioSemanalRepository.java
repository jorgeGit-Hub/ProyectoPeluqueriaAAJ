package com.example.proyecto.repository;

import com.example.proyecto.domain.HorarioSemanal;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import java.util.List;

public interface HorarioSemanalRepository extends JpaRepository<HorarioSemanal, Integer> {

    // Buscar horarios por servicio
    @Query(value="SELECT * FROM horario_semanal WHERE id_servicio = :idServicio", nativeQuery = true)
    List<HorarioSemanal> findByServicio(@Param("idServicio") Integer idServicio);

    // Buscar horarios por día de la semana
    @Query(value="SELECT * FROM horario_semanal WHERE dia_semana = :dia", nativeQuery = true)
    List<HorarioSemanal> findByDiaSemana(@Param("dia") String dia);

    // Buscar horarios por servicio y día
    @Query(value="SELECT * FROM horario_semanal WHERE id_servicio = :idServicio AND dia_semana = :dia", nativeQuery = true)
    List<HorarioSemanal> findByServicioAndDia(@Param("idServicio") Integer idServicio, @Param("dia") String dia);

    // ✅ NUEVO: Buscar horarios por grupo
    @Query(value="SELECT * FROM horario_semanal WHERE id_grupo = :idGrupo", nativeQuery = true)
    List<HorarioSemanal> findByGrupo(@Param("idGrupo") Integer idGrupo);

    // ✅ NUEVO: Buscar horarios por grupo y día
    @Query(value="SELECT * FROM horario_semanal WHERE id_grupo = :idGrupo AND dia_semana = :dia", nativeQuery = true)
    List<HorarioSemanal> findByGrupoAndDia(@Param("idGrupo") Integer idGrupo, @Param("dia") String dia);

    // ✅ NUEVO: Buscar horarios por servicio, grupo y día
    @Query(value="SELECT * FROM horario_semanal WHERE id_servicio = :idServicio AND id_grupo = :idGrupo AND dia_semana = :dia", nativeQuery = true)
    List<HorarioSemanal> findByServicioGrupoAndDia(@Param("idServicio") Integer idServicio, @Param("idGrupo") Integer idGrupo, @Param("dia") String dia);
}