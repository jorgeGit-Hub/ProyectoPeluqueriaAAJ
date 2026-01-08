package com.example.proyecto.repository;

import com.example.proyecto.domain.Usuario;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import java.util.List;

public interface UsuarioRepository extends JpaRepository<Usuario, Integer> {
    @Query(value="SELECT * FROM Usuario WHERE correo = :correo", nativeQuery = true)
    Usuario findByEmail(@Param("correo") String correo);

    @Query(value="SELECT * FROM Usuario WHERE rol = 'cliente'", nativeQuery = true)
    List<Usuario> findAllClientes();

    @Query(value="SELECT COUNT(*) FROM Usuario WHERE rol = 'alumno'", nativeQuery = true)
    int countAlumnos();

    // Método adicional para verificar existencia por correo
    @Query(value="SELECT COUNT(*) > 0 FROM Usuario WHERE correo = :correo", nativeQuery = true)
    boolean existsByCorreo(@Param("correo") String correo);
}