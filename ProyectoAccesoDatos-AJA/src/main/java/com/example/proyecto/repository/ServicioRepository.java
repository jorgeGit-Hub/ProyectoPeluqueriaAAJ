package com.example.proyecto.repository;

import com.example.proyecto.domain.Servicio;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import java.util.List;

public interface ServicioRepository extends JpaRepository<Servicio, Integer> {

    // NUEVO: Buscar servicios disponibles en un día específico (Ej: "Lunes")
    // Útil para que el cliente filtre: "¿Qué puedo hacerme hoy?"
    @Query(value="SELECT * FROM Servicio WHERE dia_semana = :dia", nativeQuery = true)
    List<Servicio> findByDiaSemana(@Param("dia") String dia);

    // NUEVO: Buscar servicios impartidos por un grupo específico
    // Útil para la gestión académica: "¿Qué ofrece el grupo 2GME?"
    @Query(value="SELECT * FROM Servicio WHERE id_grupo = :idGrupo", nativeQuery = true)
    List<Servicio> findByGrupo(@Param("idGrupo") Integer idGrupo);

    // NUEVO: Combinación de Día y Grupo
    @Query(value="SELECT * FROM Servicio WHERE dia_semana = :dia AND id_grupo = :idGrupo", nativeQuery = true)
    List<Servicio> findByDiaAndGrupo(@Param("dia") String dia, @Param("idGrupo") Integer idGrupo);

    // --- Métodos existentes preservados (compatibles con la nueva entidad) ---

    @Query(value="SELECT * FROM Servicio WHERE precio <= ?1", nativeQuery = true)
    List<Servicio> findAffordable(double maxPrice);

    @Query(value="SELECT nombre, precio FROM Servicio ORDER BY precio DESC LIMIT 5", nativeQuery = true)
    List<Object[]> top5Expensive();

    @Query(value="SELECT * FROM Servicio WHERE LOWER(nombre) LIKE LOWER(CONCAT('%', :texto, '%'))", nativeQuery = true)
    List<Servicio> findByNombreContaining(@Param("texto") String texto);

    @Query(value="SELECT * FROM Servicio WHERE LOWER(nombre) LIKE LOWER(CONCAT(:texto, '%'))", nativeQuery = true)
    List<Servicio> findByNombreStartingWith(@Param("texto") String texto);
}