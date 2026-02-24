package com.example.proyecto.controller;

import com.example.proyecto.domain.Configuracion;
import com.example.proyecto.repository.ConfiguracionRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.Optional;

@CrossOrigin(origins = "*", maxAge = 3600)
@RestController
@RequestMapping("/api/configuracion")
public class ConfiguracionController {

    @Autowired
    private ConfiguracionRepository configuracionRepository;

    @GetMapping("/logo")
    public ResponseEntity<String> getLogo() {
        Optional<Configuracion> config = configuracionRepository.findByClave("LOGO_PRINCIPAL");
        if (config.isPresent()) {
            return ResponseEntity.ok(config.get().getValorBase64());
        }
        return ResponseEntity.notFound().build();
    }
}//a