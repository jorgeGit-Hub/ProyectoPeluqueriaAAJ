package com.example.proyecto.repository;

import com.example.proyecto.domain.Cliente;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import java.util.List;

public interface ClienteRepository extends JpaRepository<Cliente, Integer> {

    // Opción 1: Native Query corregida
    @Query(value="SELECT * FROM Cliente WHERE telefono LIKE CONCAT('%', :fragment, '%')", nativeQuery = true)
    List<Cliente> findByTelefonoLike(@Param("fragment") String fragment);

    // Query adicional: buscar por dirección
    @Query(value="SELECT * FROM Cliente WHERE direccion LIKE CONCAT('%', :fragment, '%')", nativeQuery = true)
    List<Cliente> findByDireccionLike(@Param("fragment") String fragment);

    // Query adicional: buscar por alérgenos
    @Query(value="SELECT * FROM Cliente WHERE alergenos LIKE CONCAT('%', :alergeno, '%')", nativeQuery = true)
    List<Cliente> findByAlergenosContaining(@Param("alergeno") String alergeno);
}