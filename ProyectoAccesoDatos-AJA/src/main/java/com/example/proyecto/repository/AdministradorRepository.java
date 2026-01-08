package com.example.proyecto.repository;

import com.example.proyecto.domain.Administrador;
import org.springframework.data.jpa.repository.JpaRepository;

public interface AdministradorRepository extends JpaRepository<Administrador, Integer> {
}
