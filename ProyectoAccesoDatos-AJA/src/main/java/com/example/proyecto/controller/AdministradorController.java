package com.example.proyecto.controller;

import com.example.proyecto.domain.Administrador;
import com.example.proyecto.repository.AdministradorRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/administradores")
public class AdministradorController {

    @Autowired
    private AdministradorRepository repo;

    @GetMapping
    public List<Administrador> all() {
        return repo.findAll();
    }

    @GetMapping("/{id}")
    public ResponseEntity<Administrador> get(@PathVariable int id) {
        return repo.findById(id)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

    @PostMapping
    public Administrador create(@RequestBody Administrador a) {
        return repo.save(a);
    }

    @PutMapping("/{id}")
    public ResponseEntity<Administrador> update(@PathVariable int id, @RequestBody Administrador a) {
        if (!repo.existsById(id)) {
            return ResponseEntity.notFound().build();
        }
        a.setIdUsuario(id);
        return ResponseEntity.ok(repo.save(a));
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