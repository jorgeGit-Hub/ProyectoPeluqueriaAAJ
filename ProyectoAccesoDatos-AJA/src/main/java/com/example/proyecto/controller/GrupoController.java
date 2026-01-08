package com.example.proyecto.controller;

import com.example.proyecto.domain.Grupo;
import com.example.proyecto.repository.GrupoRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/grupos")
public class GrupoController {

    @Autowired
    private GrupoRepository repo;

    @GetMapping
    public List<Grupo> all() {
        return repo.findAll();
    }

    @GetMapping("/{id}")
    public ResponseEntity<Grupo> get(@PathVariable int id) {
        return repo.findById(id)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

    @GetMapping("/turno/{turno}")
    public List<Grupo> byTurno(@PathVariable String turno) {
        return repo.findByTurno(turno);
    }

    @PostMapping
    public Grupo create(@RequestBody Grupo g) {
        return repo.save(g);
    }

    @PutMapping("/{id}")
    public ResponseEntity<Grupo> update(@PathVariable int id, @RequestBody Grupo g) {
        if (!repo.existsById(id)) {
            return ResponseEntity.notFound().build();
        }
        g.setIdGrupo(id);
        return ResponseEntity.ok(repo.save(g));
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> delete(@PathVariable int id) {
        if (!repo.existsById(id)) {
            return ResponseEntity.notFound().build();
        }
        repo.deleteById(id);
        return ResponseEntity.noContent().build();
    }
}