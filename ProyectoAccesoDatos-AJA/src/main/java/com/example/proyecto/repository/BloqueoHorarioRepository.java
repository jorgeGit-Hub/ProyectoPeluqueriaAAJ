package com.example.proyecto.repository;

import com.example.proyecto.domain.BloqueoHorario;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import java.util.List;

public interface BloqueoHorarioRepository extends JpaRepository<BloqueoHorario, Integer> {
    @Query(value="SELECT * FROM BloqueoHorario WHERE id_administrador = ?1", nativeQuery = true)
    List<BloqueoHorario> findByAdmin(int idAdmin);
}
