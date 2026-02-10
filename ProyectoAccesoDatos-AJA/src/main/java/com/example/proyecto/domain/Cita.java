package com.example.proyecto.domain;

import jakarta.persistence.*;
import java.time.LocalDate;
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

    // ❌ ELIMINADOS: hora_inicio, hora_fin, id_servicio
    // ✅ NUEVO: Relación con HorarioSemanal que ya contiene esa info

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

    public Estado getEstado(){return estado;}
    public void setEstado(Estado estado){this.estado=estado;}

    public Cliente getCliente(){return cliente;}
    public void setCliente(Cliente cliente){this.cliente=cliente;}

    // ✅ NUEVO: Getter y Setter para HorarioSemanal
    public HorarioSemanal getHorarioSemanal(){return horarioSemanal;}
    public void setHorarioSemanal(HorarioSemanal horarioSemanal){this.horarioSemanal=horarioSemanal;}

    // ❌ ELIMINADOS: getServicio, setServicio, getHoraInicio, setHoraInicio, getHoraFin, setHoraFin
}