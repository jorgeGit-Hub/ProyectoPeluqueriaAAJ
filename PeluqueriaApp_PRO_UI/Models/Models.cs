using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        [JsonProperty("accessToken")]
        public string token { get; set; }

        [JsonProperty("tokenType")]
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

    // ========== SERVICIO (SIN diaSemana ni horario) ==========

    public class Servicio
    {
        public int idServicio { get; set; }
        public string nombre { get; set; }
        public string modulo { get; set; }
        public string aula { get; set; }
        public string tiempoCliente { get; set; }
        public decimal precio { get; set; }
        public string imagen { get; set; }

        // CORRECCIÓN: Usar un convertidor personalizado para manejar tanto int como objeto
        [JsonConverter(typeof(GrupoConverter))]
        public GrupoSimple grupo { get; set; }
    }

    // ========== CONVERTIDOR PERSONALIZADO PARA SERVICIO ==========
    public class ServicioConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Servicio);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            // CASO 1: Si la API devuelve un número (ID), creamos un objeto Servicio solo con el ID
            if (reader.TokenType == JsonToken.Integer)
            {
                int idServicio = Convert.ToInt32(reader.Value);
                return new Servicio { idServicio = idServicio, nombre = "Desconocido" }; // Ponemos nombre por defecto para que no falle la UI
            }

            // CASO 2: Si la API devuelve un objeto completo, deserializamos normalmente
            if (reader.TokenType == JsonToken.StartObject)
            {
                var servicio = new Servicio();
                serializer.Populate(reader, servicio); // Usamos Populate para llenar el objeto
                return servicio;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Esto maneja cómo se envía de vuelta a la API
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var servicio = (Servicio)value;
            // Generalmente al enviar, queremos enviar el objeto completo o según requiera tu API
            // Si tu API al guardar prefiere solo el ID, podrías cambiar esto, 
            // pero por defecto serializamos el objeto completo:
            serializer.Serialize(writer, value);
        }
    }

    // ========== CONVERTIDOR PERSONALIZADO PARA GRUPO ==========
    public class GrupoConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(GrupoSimple);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            // Si es un número (int), crear un GrupoSimple solo con el ID
            if (reader.TokenType == JsonToken.Integer)
            {
                int idGrupo = Convert.ToInt32(reader.Value);
                return new GrupoSimple { idGrupo = idGrupo };
            }

            // Si es un objeto, deserializarlo normalmente
            if (reader.TokenType == JsonToken.StartObject)
            {
                var grupo = new GrupoSimple();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.EndObject)
                        break;

                    if (reader.TokenType == JsonToken.PropertyName)
                    {
                        string propertyName = reader.Value.ToString();
                        reader.Read();

                        if (propertyName == "idGrupo" && reader.TokenType == JsonToken.Integer)
                        {
                            grupo.idGrupo = Convert.ToInt32(reader.Value);
                        }
                    }
                }
                return grupo;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var grupo = (GrupoSimple)value;
            writer.WriteStartObject();
            writer.WritePropertyName("idGrupo");
            writer.WriteValue(grupo.idGrupo);
            writer.WriteEndObject();
        }
    }

    // ========== HORARIO SEMANAL (NUEVO) ==========

    public class HorarioSemanal
    {
        public int idHorario { get; set; }
        public ServicioSimple servicio { get; set; }
        public string diaSemana { get; set; }
        public string horaInicio { get; set; }
        public string horaFin { get; set; }
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

    // ========== CITA (SIN GRUPO) ==========

    public class Cita
    {
        public int idCita { get; set; }
        public string fecha { get; set; }
        public string horaInicio { get; set; }
        public string horaFin { get; set; }
        public string estado { get; set; }
        public ClienteSimple cliente { get; set; }

        [JsonConverter(typeof(ServicioConverter))]
        public Servicio servicio { get; set; }

        //  CAMBIO: Ahora acepta Servicio completo en lugar de ServicioSimple
        
    }

    public class ClienteSimple
    {
        public int idUsuario { get; set; }
    }

    public class ServicioSimple
    {
        public int idServicio { get; set; }
    }

    // ========== GRUPO (CON cantAlumnos) ==========

    public class Grupo
    {
        public int idGrupo { get; set; }
        public string curso { get; set; }
        public string email { get; set; }
        public string turno { get; set; }
        public int? cantAlumnos { get; set; }
    }

    public class GrupoSimple
    {
        public int idGrupo { get; set; }
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