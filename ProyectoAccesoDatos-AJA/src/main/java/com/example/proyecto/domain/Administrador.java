package com.example.proyecto.domain;

import jakarta.persistence.*;

@Entity
@Table(name = "Administrador")
public class Administrador {
    @Id
    @Column(name="id_usuario")
    private Integer idUsuario;

    @Column(name="especialidad")
    private String especialidad;

    public Integer getIdUsuario(){return idUsuario;}
    public void setIdUsuario(Integer idUsuario){this.idUsuario=idUsuario;}
    public String getEspecialidad(){return especialidad;}
    public void setEspecialidad(String especialidad){this.especialidad=especialidad;}
}