package com.example.proyecto.domain;

import jakarta.persistence.*;
import java.time.LocalDate;
import java.time.LocalTime;
import com.fasterxml.jackson.annotation.JsonIdentityInfo;
import com.fasterxml.jackson.annotation.ObjectIdGenerators;

@JsonIdentityInfo(generator = ObjectIdGenerators.PropertyGenerator.class, property = "idCita")
@Entity
@Table(name = "Cita")
public class Cita {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name="id_cita")
    private Integer idCita;

    @Column(name="fecha")
    private LocalDate fecha;

    // ✅ Vuelven los campos de hora específica para la cita
    @Column(name="hora_inicio")
    private LocalTime horaInicio;

    @Column(name="hora_fin")
    private LocalTime horaFin;

    @ManyToOne
    @JoinColumn(name="id_horario_semanal")
    private HorarioSemanal horarioSemanal;

    @Enumerated(EnumType.STRING)
    @Column(name="estado")
    private Estado estado;

    public enum Estado { pendiente, realizada, cancelada }

    @ManyToOne
    @JoinColumn(name="id_cliente")
    private Cliente cliente;

    // Constructores
    public Cita() {}

    // Getters y Setters
    public Integer getIdCita(){return idCita;}
    public void setIdCita(Integer idCita){this.idCita=idCita;}

    public LocalDate getFecha(){return fecha;}
    public void setFecha(LocalDate fecha){this.fecha=fecha;}

    public LocalTime getHoraInicio(){return horaInicio;}
    public void setHoraInicio(LocalTime horaInicio){this.horaInicio=horaInicio;}

    public LocalTime getHoraFin(){return horaFin;}
    public void setHoraFin(LocalTime horaFin){this.horaFin=horaFin;}

    public Estado getEstado(){return estado;}
    public void setEstado(Estado estado){this.estado=estado;}

    public Cliente getCliente(){return cliente;}
    public void setCliente(Cliente cliente){this.cliente=cliente;}

    public HorarioSemanal getHorarioSemanal(){return horarioSemanal;}
    public void setHorarioSemanal(HorarioSemanal horarioSemanal){this.horarioSemanal=horarioSemanal;}
}