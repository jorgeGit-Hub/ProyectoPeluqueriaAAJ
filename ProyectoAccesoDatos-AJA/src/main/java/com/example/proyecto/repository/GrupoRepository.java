package com.example.proyecto.repository;

import com.example.proyecto.domain.Grupo;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

import java.util.List;

public interface GrupoRepository extends JpaRepository<Grupo, Integer> {
    @Query(value="SELECT * FROM Grupo WHERE turno = ?1", nativeQuery = true)
    List<Grupo> findByTurno(String turno);
}
