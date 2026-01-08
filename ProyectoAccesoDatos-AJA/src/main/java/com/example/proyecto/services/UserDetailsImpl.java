package com.example.proyecto.services;

import com.example.proyecto.domain.Usuario;
import com.fasterxml.jackson.annotation.JsonIgnore;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;

import java.util.Collection;
import java.util.List;
import java.util.Objects;

public class UserDetailsImpl implements UserDetails {
    //hace que los guardados no causen errores
    private static final long serialVersionUID = 1L;

    private Integer id;
    private String nombre;
    private String apellidos;
    private String correo;

    @JsonIgnore
    private String contrasena;

    //da la autoridad dependiendo del rol
    private Collection<? extends GrantedAuthority> authorities;

    public UserDetailsImpl(Integer id, String nombre, String apellidos, String correo, String contrasena,
                           Collection<? extends GrantedAuthority> authorities) {
        this.id = id;
        this.nombre = nombre;
        this.apellidos = apellidos;
        this.correo = correo;
        this.contrasena = contrasena;
        this.authorities = authorities;
    }

    public static UserDetailsImpl build(Usuario usuario) {
        // Convertir el rol del usuario en una autoridad de Spring Security
        GrantedAuthority authority = new SimpleGrantedAuthority("ROLE_" + usuario.getRol().name().toUpperCase());

        return new UserDetailsImpl(
                usuario.getIdUsuario(),
                usuario.getNombre(),
                usuario.getApellidos(),
                usuario.getCorreo(),
                usuario.getContrasena(),
                List.of(authority)
        );
    }

    @Override
    public Collection<? extends GrantedAuthority> getAuthorities() {
        return authorities;
    }

    public Integer getId() {
        return id;
    }

    public String getNombre() {
        return nombre;
    }

    public String getApellidos() {
        return apellidos;
    }

    public String getCorreo() {
        return correo;
    }

    @Override
    public String getPassword() {
        return contrasena;
    }

    @Override
    public String getUsername() {
        return correo;
    }

    @Override
    public boolean isAccountNonExpired() {
        return true;
    }

    @Override
    public boolean isAccountNonLocked() {
        return true;
    }

    @Override
    public boolean isCredentialsNonExpired() {
        return true;
    }

    @Override
    public boolean isEnabled() {
        return true;
    }

    // convierte el objeto pasasdo en un objeto user para que
    // userDetailImpl pueda coger el ID
    @Override
    public boolean equals(Object o) {
        if (this == o)
            return true;
        if (o == null || getClass() != o.getClass())
            return false;
        UserDetailsImpl user = (UserDetailsImpl) o;
        return Objects.equals(id, user.id);
    }
}