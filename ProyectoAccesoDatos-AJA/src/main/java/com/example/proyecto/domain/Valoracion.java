package com.example.proyecto.domain;

import jakarta.persistence.*;
import java.time.LocalDate;
import com.fasterxml.jackson.annotation.JsonIdentityInfo;
import com.fasterxml.jackson.annotation.ObjectIdGenerators;

@JsonIdentityInfo(generator = ObjectIdGenerators.PropertyGenerator.class, property = "idValoracion")
@Entity
@Table(name = "Valoracion")
public class Valoracion {
    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name="id_valoracion")
    private Integer idValoracion;
    @OneToOne
    @JoinColumn(name="id_cita")
    private Cita cita;
    @Column(name="puntuacion")
    private Integer puntuacion;
    @Column(name="comentario")
    private String comentario;
    @Column(name="fecha_valoracion")
    private LocalDate fechaValoracion;

    public Integer getIdValoracion(){return idValoracion;}
    public void setIdValoracion(Integer idValoracion){this.idValoracion=idValoracion;}
    public Cita getCita(){return cita;}
    public void setCita(Cita cita){this.cita=cita;}
    public Integer getPuntuacion(){return puntuacion;}
    public void setPuntuacion(Integer puntuacion){this.puntuacion=puntuacion;}
    public String getComentario(){return comentario;}
    public void setComentario(String comentario){this.comentario=comentario;}
    public LocalDate getFechaValoracion(){return fechaValoracion;}
    public void setFechaValoracion(LocalDate fechaValoracion){this.fechaValoracion=fechaValoracion;}
}
