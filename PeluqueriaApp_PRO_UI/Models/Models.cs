using System;
using System.Collections.Generic;

namespace PeluqueriaApp.Models
{
    // ========== AUTENTICACIÓN ==========

    public class LoginRequest
    {
        public string correo { get; set; }
        public string contrasena { get; set; }
    }

    public class LoginResponse
    {
        // CORREGIDO: Jackson usa el nombre del getter en el backend
        // El getter es getAccessToken() así que el JSON tiene "accessToken"
        [Newtonsoft.Json.JsonProperty("accessToken")]
        public string token { get; set; }

        [Newtonsoft.Json.JsonProperty("tokenType")]
        public string type { get; set; }

        public int id { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public string rol { get; set; }
    }

    public class SignupRequest
    {
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
        public string rol { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string alergenos { get; set; }
        public string observaciones { get; set; }
    }

    public class MessageResponse
    {
        public string message { get; set; }
    }

    // ========== SERVICIO ==========

    public class Servicio
    {
        public int idServicio { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int duracion { get; set; }
        public decimal precio { get; set; }
    }

    // ========== USUARIO ==========

    public class Usuario
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public string rol { get; set; }
    }

    // ========== CLIENTE ==========

    public class Cliente
    {
        public int idUsuario { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string alergenos { get; set; }
        public string observaciones { get; set; }
    }

    public class ClienteCompleto
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string alergenos { get; set; }
        public string observaciones { get; set; }
    }

    // ========== CITA ==========

    public class Cita
    {
        public int idCita { get; set; }
        public string fecha { get; set; }
        public string horaInicio { get; set; }
        public string horaFin { get; set; }
        public string estado { get; set; }
        public ClienteSimple cliente { get; set; }
        public GrupoSimple grupo { get; set; }
        public ServicioSimple servicio { get; set; }
    }

    public class ClienteSimple
    {
        public int idUsuario { get; set; }
    }

    public class GrupoSimple
    {
        public int idGrupo { get; set; }
    }

    public class ServicioSimple
    {
        public int idServicio { get; set; }
    }

    // ========== GRUPO ==========

    public class Grupo
    {
        public int idGrupo { get; set; }
        public string curso { get; set; }
        public string email { get; set; }
        public string turno { get; set; }
    }

    // ========== VALORACIÓN ==========

    public class Valoracion
    {
        public int idValoracion { get; set; }
        public Cita cita { get; set; }
        public int puntuacion { get; set; }
        public string comentario { get; set; }
        public string fechaValoracion { get; set; }
    }

    // ========== BLOQUEO HORARIO ==========

    public class BloqueoHorario
    {
        public int idBloqueo { get; set; }
        public string fecha { get; set; }
        public string horaInicio { get; set; }
        public string horaFin { get; set; }
        public string motivo { get; set; }
        public AdministradorSimple administrador { get; set; }
    }

    public class AdministradorSimple
    {
        public int idUsuario { get; set; }
    }

    // ========== ADMINISTRADOR ==========

    public class Administrador
    {
        public int idUsuario { get; set; }
        public string especialidad { get; set; }
    }
}