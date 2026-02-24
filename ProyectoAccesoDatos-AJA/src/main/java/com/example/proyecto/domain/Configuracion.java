package com.example.proyecto.domain;

import jakarta.persistence.*;
import com.fasterxml.jackson.annotation.JsonIdentityInfo;
import com.fasterxml.jackson.annotation.ObjectIdGenerators;


@Entity
@Table(name = "configuracion")
public class Configuracion {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer idConfiguracion;

    private String clave;

    @Lob
    @Column(columnDefinition = "LONGTEXT")
    private String valorBase64;

    // Getters y Setters...
    public String getValorBase64() { return valorBase64; }
    public void setValorBase64(String valorBase64) { this.valorBase64 = valorBase64; }
}