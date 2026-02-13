package com.example.proyecto.controller;

import com.example.proyecto.domain.Servicio;
import com.example.proyecto.repository.ServicioRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/servicios")
public class ServicioController {

    @Autowired
    private ServicioRepository repo;

    @GetMapping
    public List<Servicio> all() {
        return repo.findAll();
    }

    @GetMapping("/{id}")
    public ResponseEntity<Servicio> get(@PathVariable int id) {
        return repo.findById(id)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

    // ❌ ELIMINADO: byGrupo - ahora se consulta a través de /api/horarios/grupo/{id}

    @GetMapping("/baratos/{max}")
    public List<Servicio> baratos(@PathVariable double max) {
        return repo.findAffordable(max);
    }

    @GetMapping("/top5exp")
    public List<Object[]> top5() {
        return repo.top5Expensive();
    }

    @GetMapping("/buscar/{texto}")
    public List<Servicio> buscarPorNombre(@PathVariable String texto) {
        return repo.findByNombreContaining(texto);
    }

    @GetMapping("/buscar/empieza/{texto}")
    public List<Servicio> buscarPorInicio(@PathVariable String texto) {
        return repo.findByNombreStartingWith(texto);
    }

    @PostMapping
    public Servicio create(@RequestBody Servicio s) {
        return repo.save(s);
    }

    @PutMapping("/{id}")
    public ResponseEntity<Servicio> update(@PathVariable int id, @RequestBody Servicio s) {
        if (!repo.existsById(id)) {
            return ResponseEntity.notFound().build();
        }
        s.setIdServicio(id);
        return ResponseEntity.ok(repo.save(s));
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