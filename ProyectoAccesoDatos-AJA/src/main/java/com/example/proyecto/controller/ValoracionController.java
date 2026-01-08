package com.example.proyecto.controller;

import com.example.proyecto.domain.Valoracion;
import com.example.proyecto.repository.ValoracionRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/valoraciones")
public class ValoracionController {

    @Autowired
    private ValoracionRepository repo;

    @GetMapping
    public List<Valoracion> all() {
        return repo.findAll();
    }

    @GetMapping("/{id}")
    public ResponseEntity<Valoracion> get(@PathVariable int id) {
        return repo.findById(id)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

    @GetMapping("/min/{p}")
    public List<Valoracion> min(@PathVariable int p) {
        return repo.findByMinPuntuacion(p);
    }

    @PostMapping
    public Valoracion create(@RequestBody Valoracion v) {
        return repo.save(v);
    }

    @PutMapping("/{id}")
    public ResponseEntity<Valoracion> update(@PathVariable int id, @RequestBody Valoracion v) {
        if (!repo.existsById(id)) {
            return ResponseEntity.notFound().build();
        }
        v.setIdValoracion(id);
        return ResponseEntity.ok(repo.save(v));
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