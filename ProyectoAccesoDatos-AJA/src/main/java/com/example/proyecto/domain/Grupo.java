package com.example.proyecto.domain;

import jakarta.persistence.*;
import com.fasterxml.jackson.annotation.JsonIdentityInfo;
import com.fasterxml.jackson.annotation.ObjectIdGenerators;

@JsonIdentityInfo(generator = ObjectIdGenerators.PropertyGenerator.class, property = "idGrupo")
@Entity
@Table(name = "Grupo")
public class Grupo {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name="id_grupo")
    private Integer idGrupo;

    @Column(name="curso")
    private String curso;

    @Column(name="email")
    private String email;

    // CORREGIDO: nombre de columna sin caracteres especiales
    @Column(name="contrasena")
    private String contrasena;

    @Enumerated(EnumType.STRING)
    private Turno turno;

    // CORREGIDO: valores del enum sin caracteres especiales
    public enum Turno { manana, tarde }

    public Integer getIdGrupo(){return idGrupo;}
    public void setIdGrupo(Integer idGrupo){this.idGrupo=idGrupo;}
    public String getCurso(){return curso;}
    public void setCurso(String curso){this.curso=curso;}
    public String getEmail(){return email;}
    public void setEmail(String email){this.email=email;}
    public String getContrasena(){return contrasena;}
    public void setContrasena(String contrasena){this.contrasena=contrasena;}
    public Turno getTurno(){return turno;}
    public void setTurno(Turno turno){this.turno=turno;}
}