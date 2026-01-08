package com.example.proyecto.repository;

import com.example.proyecto.domain.Valoracion;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import java.util.List;

public interface ValoracionRepository extends JpaRepository<Valoracion, Integer> {
    @Query(value="SELECT * FROM Valoracion WHERE puntuacion >= ?1", nativeQuery = true)
    List<Valoracion> findByMinPuntuacion(int min);
}
