package com.example.proyecto.domain;

import jakarta.persistence.*;
import com.fasterxml.jackson.annotation.JsonIdentityInfo;
import com.fasterxml.jackson.annotation.ObjectIdGenerators;

@JsonIdentityInfo(generator = ObjectIdGenerators.PropertyGenerator.class, property = "idUsuario")
@Entity
@Table(name = "Usuario")
public class Usuario {
    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name="id_usuario")
    private Integer idUsuario;
    @Column(name="nombre")
    private String nombre;
    @Column(name="apellidos")
    private String apellidos;
    @Column(name="correo")
    private String correo;
    @Column(name="contrasena")
    private String contrasena;
    @Enumerated(EnumType.STRING)
    private Rol rol;


    public void setPassword(String password) {
        this.contrasena = password;
    }

    public enum Rol { administrador, alumno, cliente }

    public Integer getIdUsuario(){return idUsuario;}
    public void setIdUsuario(Integer idUsuario){this.idUsuario = idUsuario;}
    public String getNombre(){return nombre;}
    public void setNombre(String nombre){this.nombre = nombre;}
    public String getApellidos(){return apellidos;}
    public void setApellidos(String apellidos){this.apellidos = apellidos;}
    public String getCorreo(){return correo;}
    public void setCorreo(String email){this.correo = email;}
    public String getContrasena(){return contrasena;}
    public void setContrasena(String contrasena){this.contrasena = contrasena;}
    public Rol getRol(){return rol;}
    public void setRol(Rol rol){this.rol = rol;}
}
