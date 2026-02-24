using Newtonsoft.Json;
using PeluqueriaApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

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

    // ========== SERVICIO ==========

    public class Servicio
    {
        public int idServicio { get; set; }
        public string nombre { get; set; }
        public string modulo { get; set; }
        public string aula { get; set; }
        public string tiempoCliente { get; set; }
        public decimal precio { get; set; }
        public string imagen { get; set; }
    }

    public class ServicioSimple
    {
        public int idServicio { get; set; }
    }

    // ========== CONVERTIDOR PARA ServicioSimple ==========

    public class ServicioSimpleConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ServicioSimple);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            if (reader.TokenType == JsonToken.Integer)
                return new ServicioSimple { idServicio = Convert.ToInt32(reader.Value) };

            if (reader.TokenType == JsonToken.StartObject)
            {
                var servicioSimple = new ServicioSimple();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.EndObject) break;
                    if (reader.TokenType == JsonToken.PropertyName)
                    {
                        string propertyName = reader.Value.ToString();
                        reader.Read();
                        if (propertyName == "idServicio" && reader.TokenType == JsonToken.Integer)
                            servicioSimple.idServicio = Convert.ToInt32(reader.Value);
                        else
                            reader.Skip();
                    }
                }
                return servicioSimple;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null) { writer.WriteNull(); return; }
            var s = (ServicioSimple)value;
            writer.WriteStartObject();
            writer.WritePropertyName("idServicio");
            writer.WriteValue(s.idServicio);
            writer.WriteEndObject();
        }
    }

    // ========== GRUPO ==========

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

    // ========== CONVERTIDOR PARA GrupoSimple ==========

    public class GrupoConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(GrupoSimple);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            if (reader.TokenType == JsonToken.Integer)
                return new GrupoSimple { idGrupo = Convert.ToInt32(reader.Value) };

            if (reader.TokenType == JsonToken.StartObject)
            {
                var grupo = new GrupoSimple();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.EndObject) break;
                    if (reader.TokenType == JsonToken.PropertyName)
                    {
                        string propertyName = reader.Value.ToString();
                        reader.Read();
                        if (propertyName == "idGrupo" && reader.TokenType == JsonToken.Integer)
                            grupo.idGrupo = Convert.ToInt32(reader.Value);
                        else
                            reader.Skip();
                    }
                }
                return grupo;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null) { writer.WriteNull(); return; }
            var g = (GrupoSimple)value;
            writer.WriteStartObject();
            writer.WritePropertyName("idGrupo");
            writer.WriteValue(g.idGrupo);
            writer.WriteEndObject();
        }
    }

    // ========== CONVERTIDOR PARA HorarioSemanal ==========
    // Necesario porque @JsonIdentityInfo en Java puede serializar HorarioSemanal
    // como un entero (solo el ID) cuando ya fue referenciado anteriormente en el JSON.

    public class HorarioSemanalConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(HorarioSemanal);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            // Cuando @JsonIdentityInfo ya serializó el objeto antes,
            // las siguientes referencias son solo el ID como entero
            if (reader.TokenType == JsonToken.Integer)
            {
                return new HorarioSemanal
                {
                    idHorario = Convert.ToInt32(reader.Value)
                };
            }

            if (reader.TokenType == JsonToken.StartObject)
            {
                var horario = new HorarioSemanal();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.EndObject) break;
                    if (reader.TokenType == JsonToken.PropertyName)
                    {
                        string prop = reader.Value.ToString();
                        reader.Read();

                        switch (prop)
                        {
                            case "idHorario":
                                horario.idHorario = Convert.ToInt32(reader.Value);
                                break;
                            case "diaSemana":
                                horario.diaSemana = reader.Value?.ToString();
                                break;
                            case "horaInicio":
                                horario.horaInicio = reader.Value?.ToString();
                                break;
                            case "horaFin":
                                horario.horaFin = reader.Value?.ToString();
                                break;
                            case "servicio":
                                var servicioConverter = new ServicioSimpleConverter();
                                horario.servicio = (ServicioSimple)servicioConverter.ReadJson(reader, typeof(ServicioSimple), null, serializer);
                                break;
                            case "grupo":
                                var grupoConverter = new GrupoConverter();
                                horario.grupo = (GrupoSimple)grupoConverter.ReadJson(reader, typeof(GrupoSimple), null, serializer);
                                break;
                            default:
                                reader.Skip();
                                break;
                        }
                    }
                }
                return horario;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null) { writer.WriteNull(); return; }
            var h = (HorarioSemanal)value;
            writer.WriteStartObject();
            writer.WritePropertyName("idHorario");
            writer.WriteValue(h.idHorario);
            writer.WriteEndObject();
        }
    }

    // ========== HORARIO SEMANAL ==========
    public class HorarioSemanal
    {
        // ✅ AÑADIMOS EL '?' para hacerlo nullable y que lo ignore al crear uno nuevo
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? idHorario { get; set; }

        [JsonConverter(typeof(ServicioSimpleConverter))]
        public ServicioSimple servicio { get; set; }

        [JsonConverter(typeof(GrupoConverter))]
        public GrupoSimple grupo { get; set; }

        public string diaSemana { get; set; }
        public string horaInicio { get; set; }
        public string horaFin { get; set; }
    }

    // ========== CITA ==========
    // horarioSemanal ahora usa HorarioSemanalConverter para manejar referencias por ID

    public class Cita
    {
        public int idCita { get; set; }
        public string fecha { get; set; }

        // ✅ NUEVO: Vuelven los campos de hora específica de la cita
        public string horaInicio { get; set; }
        public string horaFin { get; set; }

        public string estado { get; set; }
        public ClienteSimple cliente { get; set; }

        [JsonConverter(typeof(HorarioSemanalConverter))]
        public HorarioSemanal horarioSemanal { get; set; }
    }

    public class ClienteSimple
    {
        public int idUsuario { get; set; }
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

    // ========== ADMINISTRADOR ==========

    public class Administrador
    {
        public int idUsuario { get; set; }
        public string especialidad { get; set; }
    }

    public class AdministradorSimple
    {
        public int idUsuario { get; set; }
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

    // ========== VALORACIÓN ==========

    public class Valoracion
    {
        // ✅ AÑADIDO: Nullable e Ignore para que Spring Boot permita crear nuevas
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? idValoracion { get; set; }

        public Cita cita { get; set; }
        public int puntuacion { get; set; }
        public string comentario { get; set; }
        public string fechaValoracion { get; set; }
    }
}