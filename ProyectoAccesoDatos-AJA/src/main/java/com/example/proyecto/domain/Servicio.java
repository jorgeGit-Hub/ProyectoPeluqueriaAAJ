package com.example.proyecto.domain;

import jakarta.persistence.*;
import java.math.BigDecimal;
import com.fasterxml.jackson.annotation.JsonIdentityInfo;
import com.fasterxml.jackson.annotation.ObjectIdGenerators;

@JsonIdentityInfo(generator = ObjectIdGenerators.PropertyGenerator.class, property = "idServicio")
@Entity
@Table(name = "Servicio")
public class Servicio {
    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name="id_servicio")
    private Integer idServicio;
    @Column(name="nombre")
    private String nombre;
    @Column(name="descripcion")
    private String descripcion;
    @Column(name="duracion")
    private Integer duracion;
    @Column(name="precio")
    private BigDecimal precio;

    public Integer getIdServicio(){return idServicio;}
    public void setIdServicio(Integer idServicio){this.idServicio=idServicio;}
    public String getNombre(){return nombre;}
    public void setNombre(String nombre){this.nombre=nombre;}
    public String getDescripcion(){return descripcion;}
    public void setDescripcion(String descripcion){this.descripcion=descripcion;}
    public Integer getDuracion(){return duracion;}
    public void setDuracion(Integer duracion){this.duracion=duracion;}
    public BigDecimal getPrecio(){return precio;}
    public void setPrecio(BigDecimal precio){this.precio=precio;}
}
