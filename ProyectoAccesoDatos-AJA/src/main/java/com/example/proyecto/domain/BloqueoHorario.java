package com.example.proyecto.domain;

import jakarta.persistence.*;
import java.time.LocalDate;
import java.time.LocalTime;
import com.fasterxml.jackson.annotation.JsonIdentityInfo;
import com.fasterxml.jackson.annotation.ObjectIdGenerators;

@JsonIdentityInfo(generator = ObjectIdGenerators.PropertyGenerator.class, property = "idBloqueo")
@Entity
@Table(name = "BloqueoHorario")
public class BloqueoHorario {
    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name="id_bloqueo")
    private Integer idBloqueo;
    @Column(name="fecha")
    private LocalDate fecha;
    @Column(name="hora_inicio")
    private LocalTime horaInicio;
    @Column(name="hora_fin")
    private LocalTime horaFin;
    @Column(name="motivo")
    private String motivo;
    @ManyToOne
    @JoinColumn(name="id_administrador")
    private Administrador administrador;

    public Integer getIdBloqueo(){return idBloqueo;}
    public void setIdBloqueo(Integer idBloqueo){this.idBloqueo=idBloqueo;}
    public LocalDate getFecha(){return fecha;}
    public void setFecha(LocalDate fecha){this.fecha=fecha;}
    public LocalTime getHoraInicio(){return horaInicio;}
    public void setHoraInicio(LocalTime horaInicio){this.horaInicio=horaInicio;}
    public LocalTime getHoraFin(){return horaFin;}
    public void setHoraFin(LocalTime horaFin){this.horaFin=horaFin;}
    public String getMotivo(){return motivo;}
    public void setMotivo(String motivo){this.motivo=motivo;}
    public Administrador getAdministrador(){return administrador;}
    public void setAdministrador(Administrador administrador){this.administrador=administrador;}
}
