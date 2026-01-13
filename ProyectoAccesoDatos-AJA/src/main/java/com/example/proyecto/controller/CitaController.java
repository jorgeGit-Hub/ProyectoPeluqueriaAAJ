package com.example.proyecto.controller;

import com.example.proyecto.domain.BloqueoHorario;
import com.example.proyecto.domain.Cita;
import com.example.proyecto.domain.Servicio;
import com.example.proyecto.repository.BloqueoHorarioRepository;
import com.example.proyecto.repository.CitaRepository;
import com.example.proyecto.repository.ClienteRepository;
import com.example.proyecto.repository.ServicioRepository;
import com.example.proyecto.services.UserDetailsImpl;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.bind.annotation.*;

import java.time.LocalDate;
import java.time.format.TextStyle; // Necesario para el nombre del día
import java.util.HashMap;
import java.util.List; // Necesario para las listas
import java.util.Locale;
import java.util.Map;

@RestController
@RequestMapping("/api/citas")
public class CitaController {

    @Autowired
    private CitaRepository repo;

    @Autowired
    private ClienteRepository clienteRepository;

    @Autowired
    private ServicioRepository servicioRepository;

    @Autowired
    private BloqueoHorarioRepository bloqueoRepository;

    // --- CONSULTAS ---

    @GetMapping
    public List<Cita> all() {
        return repo.findAll();
    }

    @GetMapping("/{id}")
    public ResponseEntity<Cita> get(@PathVariable int id) {
        return repo.findById(id)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

    @GetMapping("/mis-citas")
    public ResponseEntity<List<Cita>> misCitas() {
        Authentication auth = SecurityContextHolder.getContext().getAuthentication();
        UserDetailsImpl userDetails = (UserDetailsImpl) auth.getPrincipal();
        return ResponseEntity.ok(repo.findByClienteId(userDetails.getId()));
    }

    @GetMapping("/agenda-grupo/{idGrupo}/{fecha}")
    public List<Cita> agendaGrupo(@PathVariable int idGrupo, @PathVariable String fecha) {
        return repo.findByGrupoAndFecha(idGrupo, LocalDate.parse(fecha));
    }

    // --- GESTIÓN ---

    @PostMapping
    public ResponseEntity<?> create(@RequestBody Cita c) {
        try {
            Authentication auth = SecurityContextHolder.getContext().getAuthentication();
            UserDetailsImpl userDetails = (UserDetailsImpl) auth.getPrincipal();

            // 1. Validar Servicio
            if (c.getServicio() == null || c.getServicio().getIdServicio() == null) {
                return ResponseEntity.badRequest().body(createError("Debe especificar un servicio"));
            }
            Servicio servicioCompleto = servicioRepository.findById(c.getServicio().getIdServicio()).orElse(null);
            if (servicioCompleto == null) {
                return ResponseEntity.badRequest().body(createError("El servicio no existe"));
            }

            // 2. Validar Día
            if (!validarDiaServicio(c.getFecha(), servicioCompleto.getDiaSemana())) {
                return ResponseEntity.badRequest().body(createError("Este servicio solo es los: " + servicioCompleto.getDiaSemana()));
            }

            // 3. Validar Bloqueo
            if (existeBloqueo(c)) {
                return ResponseEntity.badRequest().body(createError("Horario bloqueado por el centro."));
            }

            c.setServicio(servicioCompleto);

            // 4. Validar Cliente/Permisos
            boolean isAdmin = auth.getAuthorities().stream().anyMatch(a -> a.getAuthority().equals("ROLE_ADMINISTRADOR"));
            if (!isAdmin) {
                if (c.getCliente() == null || c.getCliente().getIdUsuario() == null) {
                    return ResponseEntity.badRequest().body(createError("Falta el cliente"));
                }
                if (!c.getCliente().getIdUsuario().equals(userDetails.getId())) {
                    return ResponseEntity.status(403).body(createError("No puedes crear citas para otros"));
                }
            }

            return ResponseEntity.ok(repo.save(c));

        } catch (Exception e) {
            return ResponseEntity.badRequest().body(createError("Error: " + e.getMessage()));
        }
    }

    @PutMapping("/{id}")
    public ResponseEntity<?> update(@PathVariable int id, @RequestBody Cita c) {
        try {
            if (!repo.existsById(id)) return ResponseEntity.notFound().build();

            if (c.getServicio() != null && c.getFecha() != null) {
                Servicio s = servicioRepository.findById(c.getServicio().getIdServicio()).orElse(null);
                if (s != null && !validarDiaServicio(c.getFecha(), s.getDiaSemana())) {
                    return ResponseEntity.badRequest().body(createError("Día incorrecto"));
                }
                if (existeBloqueo(c)) {
                    return ResponseEntity.badRequest().body(createError("Conflicto con bloqueo horario"));
                }
            }

            c.setIdCita(id);
            return ResponseEntity.ok(repo.save(c));
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(createError("Error: " + e.getMessage()));
        }
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<?> delete(@PathVariable int id) {
        if (!repo.existsById(id)) return ResponseEntity.notFound().build();
        repo.deleteById(id);
        return ResponseEntity.noContent().build();
    }

    // --- AUXILIARES ---

    private Map<String, String> createError(String mensaje) {
        Map<String, String> map = new HashMap<>();
        map.put("error", mensaje);
        return map;
    }

    private boolean validarDiaServicio(LocalDate fecha, String diasPermitidos) {
        if (diasPermitidos == null) return true;
        String dia = fecha.getDayOfWeek().getDisplayName(TextStyle.FULL, new Locale("es", "ES")).toLowerCase();
        return diasPermitidos.toLowerCase().contains(dia);
    }

    private boolean existeBloqueo(Cita c) {
        List<BloqueoHorario> bloqueos = bloqueoRepository.findByFecha(c.getFecha());
        for (BloqueoHorario b : bloqueos) {
            if (c.getHoraInicio().isBefore(b.getHoraFin()) && c.getHoraFin().isAfter(b.getHoraInicio())) {
                return true;
            }
        }
        return false;
    }
}