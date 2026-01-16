package com.example.proyecto.domain;

import jakarta.persistence.*;
import java.time.LocalTime;
import com.fasterxml.jackson.annotation.JsonIdentityInfo;
import com.fasterxml.jackson.annotation.ObjectIdGenerators;

@JsonIdentityInfo(generator = ObjectIdGenerators.PropertyGenerator.class, property = "idHorario")
@Entity
@Table(name = "HorarioSemanal")
public class HorarioSemanal {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name="id_horario")
    private Integer idHorario;

    @ManyToOne
    @JoinColumn(name="id_servicio")
    private Servicio servicio;

    @Enumerated(EnumType.STRING)
    @Column(name="dia_semana")
    private DiaSemana diaSemana;

    @Column(name="hora_inicio")
    private LocalTime horaInicio;

    @Column(name="hora_fin")
    private LocalTime horaFin;

    // Enum para días de la semana
    public enum DiaSemana {
        lunes, martes, miercoles, jueves, viernes, sabado, domingo
    }

    // Constructor vacío
    public HorarioSemanal() {}

    // Getters y Setters
    public Integer getIdHorario() {
        return idHorario;
    }

    public void setIdHorario(Integer idHorario) {
        this.idHorario = idHorario;
    }

    public Servicio getServicio() {
        return servicio;
    }

    public void setServicio(Servicio servicio) {
        this.servicio = servicio;
    }

    public DiaSemana getDiaSemana() {
        return diaSemana;
    }

    public void setDiaSemana(DiaSemana diaSemana) {
        this.diaSemana = diaSemana;
    }

    public LocalTime getHoraInicio() {
        return horaInicio;
    }

    public void setHoraInicio(LocalTime horaInicio) {
        this.horaInicio = horaInicio;
    }

    public LocalTime getHoraFin() {
        return horaFin;
    }

    public void setHoraFin(LocalTime horaFin) {
        this.horaFin = horaFin;
    }
}