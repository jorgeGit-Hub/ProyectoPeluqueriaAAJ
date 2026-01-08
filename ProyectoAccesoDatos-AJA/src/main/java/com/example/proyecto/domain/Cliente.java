package com.example.proyecto.domain;

import jakarta.persistence.*;

@Entity
@Table(name = "Cliente")
public class Cliente {
    @Id
    @Column(name="id_usuario")
    private Integer idUsuario;

    @Column(name="telefono")
    private String telefono;

    @Column(name="direccion")
    private String direccion;

    @Column(name="alergenos")
    private String alergenos;

    @Column(name="observaciones")
    private String observaciones;

    // Getters y Setters
    public Integer getIdUsuario(){return idUsuario;}
    public void setIdUsuario(Integer idUsuario){this.idUsuario=idUsuario;}
    public String getTelefono(){return telefono;}
    public void setTelefono(String telefono){this.telefono=telefono;}
    public String getDireccion(){return direccion;}
    public void setDireccion(String direccion){this.direccion=direccion;}
    public String getAlergenos(){return alergenos;}
    public void setAlergenos(String alergenos){this.alergenos=alergenos;}
    public String getObservaciones(){return observaciones;}
    public void setObservaciones(String observaciones){this.observaciones=observaciones;}
}