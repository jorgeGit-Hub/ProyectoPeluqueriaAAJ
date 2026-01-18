package com.example.proyecto.domain;

import jakarta.persistence.*;
import java.math.BigDecimal;
import com.fasterxml.jackson.annotation.JsonIdentityInfo;
import com.fasterxml.jackson.annotation.ObjectIdGenerators;

@JsonIdentityInfo(generator = ObjectIdGenerators.PropertyGenerator.class, property = "idServicio")
@Entity
@Table(name = "Servicio")
public class Servicio {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name="id_servicio")
    private Integer idServicio;

    @Column(name="nombre")
    private String nombre;

    @Column(name="modulo")
    private String modulo;

    @Column(name="aula")
    private String aula;

    @Column(name="tiempo_cliente")
    private String tiempoCliente; // Ej: "45'", "1h", "1,5h"

    @Column(name="precio")
    private BigDecimal precio;

    @Column(name="imagen")
    private String imagen; // URL de la imagen



    // ELIMINADOS: diaSemana y horario (ahora están en HorarioSemanal)

    @ManyToOne
    @JoinColumn(name="id_grupo")
    private Grupo grupo;

    // Constructor vacío
    public Servicio() {}

    // Getters y Setters
    public Integer getIdServicio(){ return idServicio; }
    public void setIdServicio(Integer idServicio){ this.idServicio = idServicio; }

    public String getNombre(){ return nombre; }
    public void setNombre(String nombre){ this.nombre = nombre; }

    public String getModulo(){ return modulo; }
    public void setModulo(String modulo){ this.modulo = modulo; }

    public String getAula(){ return aula; }
    public void setAula(String aula){ this.aula = aula; }

    public String getTiempoCliente(){ return tiempoCliente; }
    public void setTiempoCliente(String tiempoCliente){ this.tiempoCliente = tiempoCliente; }

    public BigDecimal getPrecio(){ return precio; }
    public void setPrecio(BigDecimal precio){ this.precio = precio; }

    public Grupo getGrupo(){ return grupo; }
    public void setGrupo(Grupo grupo){ this.grupo = grupo; }


    public String getImagen() { return imagen; }
    public void setImagen(String imagen) { this.imagen = imagen; }
}