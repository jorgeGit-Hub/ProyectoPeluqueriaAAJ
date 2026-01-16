package com.example.proyecto.repository;

import com.example.proyecto.domain.Servicio;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import java.util.List;

public interface ServicioRepository extends JpaRepository<Servicio, Integer> {

    // ELIMINADO: findByDiaSemana (ahora se hace a través de HorarioSemanal)

    // Buscar servicios impartidos por un grupo específico
    @Query(value="SELECT * FROM Servicio WHERE id_grupo = :idGrupo", nativeQuery = true)
    List<Servicio> findByGrupo(@Param("idGrupo") Integer idGrupo);

    // ELIMINADO: findByDiaAndGrupo (ya no aplica)

    @Query(value="SELECT * FROM Servicio WHERE precio <= ?1", nativeQuery = true)
    List<Servicio> findAffordable(double maxPrice);

    @Query(value="SELECT nombre, precio FROM Servicio ORDER BY precio DESC LIMIT 5", nativeQuery = true)
    List<Object[]> top5Expensive();

    @Query(value="SELECT * FROM Servicio WHERE LOWER(nombre) LIKE LOWER(CONCAT('%', :texto, '%'))", nativeQuery = true)
    List<Servicio> findByNombreContaining(@Param("texto") String texto);

    @Query(value="SELECT * FROM Servicio WHERE LOWER(nombre) LIKE LOWER(CONCAT(:texto, '%'))", nativeQuery = true)
    List<Servicio> findByNombreStartingWith(@Param("texto") String texto);
}