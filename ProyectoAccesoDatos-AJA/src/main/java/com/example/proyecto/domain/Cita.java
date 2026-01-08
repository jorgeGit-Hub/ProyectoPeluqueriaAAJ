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
    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name="id_cita")
    private Integer idCita;
    private LocalDate fecha;
    @Column(name="hora_inicio")
    private LocalTime horaInicio;
    @Column(name="hora_fin")
    private LocalTime horaFin;
    @Enumerated(EnumType.STRING)
    private Estado estado;
    public enum Estado { pendiente, realizada, cancelada }
    @ManyToOne
    @JoinColumn(name="id_cliente")
    private Cliente cliente;
    @ManyToOne
    @JoinColumn(name="id_grupo")
    private Grupo grupo;
    @ManyToOne
    @JoinColumn(name="id_servicio")
    private Servicio servicio;

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
    public Grupo getGrupo(){return grupo;}
    public void setGrupo(Grupo grupo){this.grupo=grupo;}
    public Servicio getServicio(){return servicio;}
    public void setServicio(Servicio servicio){this.servicio=servicio;}
}
