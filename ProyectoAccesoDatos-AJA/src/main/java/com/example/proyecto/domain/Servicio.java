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
    private String nombre; // Ej: "Manicura exprés"

    @Column(name="modulo")
    private String modulo; // Ej: "Estética de Manos y Pies"

    @Column(name="aula")
    private String aula; // Ej: "C2.3"

    @Column(name="tiempo_cliente")
    private String tiempoCliente; // Ej: "45'", "1h", "1,5h"

    @Column(name="precio")
    private BigDecimal precio; // Ej: 2.00

    @Column(name="dia_semana")
    private String diaSemana; // Ej: "Lunes", "Martes"

    @Column(name="horario")
    private String horario; // Ej: "8:50 a 10:30"

    // Relación con Grupo: Un servicio específico es impartido por un grupo específico
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

    public String getDiaSemana(){ return diaSemana; }
    public void setDiaSemana(String diaSemana){ this.diaSemana = diaSemana; }

    public String getHorario(){ return horario; }
    public void setHorario(String horario){ this.horario = horario; }

    public Grupo getGrupo(){ return grupo; }
    public void setGrupo(Grupo grupo){ this.grupo = grupo; }
}