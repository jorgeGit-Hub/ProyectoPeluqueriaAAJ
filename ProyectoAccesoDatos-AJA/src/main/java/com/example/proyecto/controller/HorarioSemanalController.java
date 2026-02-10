package com.example.proyecto.controller;

import com.example.proyecto.domain.HorarioSemanal;
import com.example.proyecto.repository.HorarioSemanalRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/horarios")
public class HorarioSemanalController {

    @Autowired
    private HorarioSemanalRepository repo;

    // Todos pueden consultar horarios
    @GetMapping
    public List<HorarioSemanal> all() {
        return repo.findAll();
    }

    @GetMapping("/{id}")
    public ResponseEntity<HorarioSemanal> get(@PathVariable int id) {
        return repo.findById(id)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

    // Buscar horarios de un servicio específico
    @GetMapping("/servicio/{idServicio}")
    public List<HorarioSemanal> byServicio(@PathVariable int idServicio) {
        return repo.findByServicio(idServicio);
    }

    // Buscar horarios por día de la semana
    @GetMapping("/dia/{dia}")
    public List<HorarioSemanal> byDia(@PathVariable String dia) {
        return repo.findByDiaSemana(dia);
    }

    // Buscar horarios por servicio y día
    @GetMapping("/servicio/{idServicio}/dia/{dia}")
    public List<HorarioSemanal> byServicioAndDia(@PathVariable int idServicio, @PathVariable String dia) {
        return repo.findByServicioAndDia(idServicio, dia);
    }

    // ✅ NUEVO: Buscar horarios por grupo
    @GetMapping("/grupo/{idGrupo}")
    public List<HorarioSemanal> byGrupo(@PathVariable int idGrupo) {
        return repo.findByGrupo(idGrupo);
    }

    // ✅ NUEVO: Buscar horarios por grupo y día
    @GetMapping("/grupo/{idGrupo}/dia/{dia}")
    public List<HorarioSemanal> byGrupoAndDia(@PathVariable int idGrupo, @PathVariable String dia) {
        return repo.findByGrupoAndDia(idGrupo, dia);
    }

    // Solo administradores pueden crear horarios
    @PreAuthorize("hasRole('ADMINISTRADOR')")
    @PostMapping
    public HorarioSemanal create(@RequestBody HorarioSemanal h) {
        return repo.save(h);
    }

    // Solo administradores pueden modificar horarios
    @PreAuthorize("hasRole('ADMINISTRADOR')")
    @PutMapping("/{id}")
    public ResponseEntity<HorarioSemanal> update(@PathVariable int id, @RequestBody HorarioSemanal h) {
        if (!repo.existsById(id)) {
            return ResponseEntity.notFound().build();
        }
        h.setIdHorario(id);
        return ResponseEntity.ok(repo.save(h));
    }

    // Solo administradores pueden eliminar horarios
    @PreAuthorize("hasRole('ADMINISTRADOR')")
    @DeleteMapping("/{id}")
    public ResponseEntity<Void> delete(@PathVariable int id) {
        if (!repo.existsById(id)) {
            return ResponseEntity.notFound().build();
        }
        repo.deleteById(id);
        return ResponseEntity.noContent().build();
    }
}