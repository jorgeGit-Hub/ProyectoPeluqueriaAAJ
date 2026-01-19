using Newtonsoft.Json;
using PeluqueriaApp.Services;
using System;
using System.Collections.Generic;
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

    // Opción 2: ServicioConverter que hace fetch cuando recibe solo un ID
    public class ServicioConverter : JsonConverter
    {
        // Cache estático para evitar múltiples peticiones del mismo servicio
        private static Dictionary<int, Servicio> serviciosCache = new Dictionary<int, Servicio>();

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

            // CASO 1: Si la API devuelve un número (ID), buscamos el servicio completo
            if (reader.TokenType == JsonToken.Integer)
            {
                int idServicio = Convert.ToInt32(reader.Value);

                // Verificar si ya lo tenemos en caché
                if (serviciosCache.ContainsKey(idServicio))
                {
                    return serviciosCache[idServicio];
                }

                // Si no está en caché, hacer una petición síncrona al backend
                try
                {
                    var servicio = BuscarServicioPorId(idServicio);
                    if (servicio != null)
                    {
                        serviciosCache[idServicio] = servicio;
                        return servicio;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error al buscar servicio {idServicio}: {ex.Message}");
                }

                // Si falla, devolver objeto mínimo
                return new Servicio { idServicio = idServicio, nombre = "Desconocido" };
            }

            // CASO 2: Si la API devuelve un objeto completo, deserializamos manualmente
            if (reader.TokenType == JsonToken.StartObject)
            {
                var servicio = new Servicio();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.EndObject)
                    {
                        break;
                    }

                    if (reader.TokenType == JsonToken.PropertyName)
                    {
                        string propertyName = reader.Value.ToString();
                        reader.Read();

                        switch (propertyName)
                        {
                            case "idServicio":
                                if (reader.TokenType == JsonToken.Integer)
                                {
                                    servicio.idServicio = Convert.ToInt32(reader.Value);
                                }
                                break;

                            case "nombre":
                                if (reader.TokenType == JsonToken.String)
                                {
                                    servicio.nombre = reader.Value.ToString();
                                }
                                break;

                            case "modulo":
                                if (reader.TokenType == JsonToken.String)
                                {
                                    servicio.modulo = reader.Value.ToString();
                                }
                                break;

                            case "aula":
                                if (reader.TokenType == JsonToken.String)
                                {
                                    servicio.aula = reader.Value.ToString();
                                }
                                break;

                            case "tiempoCliente":
                                if (reader.TokenType == JsonToken.String)
                                {
                                    servicio.tiempoCliente = reader.Value.ToString();
                                }
                                break;

                            case "precio":
                                if (reader.TokenType == JsonToken.Float || reader.TokenType == JsonToken.Integer)
                                {
                                    servicio.precio = Convert.ToDecimal(reader.Value);
                                }
                                break;

                            case "imagen":
                                if (reader.TokenType == JsonToken.String)
                                {
                                    servicio.imagen = reader.Value.ToString();
                                }
                                break;

                            case "grupo":
                                if (reader.TokenType == JsonToken.StartObject || reader.TokenType == JsonToken.Integer)
                                {
                                    var grupoConverter = new GrupoConverter();
                                    servicio.grupo = (GrupoSimple)grupoConverter.ReadJson(reader, typeof(GrupoSimple), null, serializer);
                                }
                                break;

                            default:
                                reader.Skip();
                                break;
                        }
                    }
                }

                // Guardar en caché
                if (servicio.idServicio > 0)
                {
                    serviciosCache[servicio.idServicio] = servicio;
                }

                return servicio;
            }

            return null;
        }

        // Método auxiliar para buscar un servicio por ID (síncrono)
        private Servicio BuscarServicioPorId(int idServicio)
        {
            try
            {
                var url = $"http://localhost:8090/api/servicios/{idServicio}";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Accept = "application/json";

                if (!string.IsNullOrEmpty(ApiService.Token))
                {
                    request.Headers.Add("Authorization", $"Bearer {ApiService.Token}");
                }

                using (var response = (HttpWebResponse)request.GetResponse())
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string jsonResponse = streamReader.ReadToEnd();

                    // Deserializar sin usar el converter (para evitar recursión)
                    var settings = new JsonSerializerSettings();
                    settings.Converters.Clear(); // No usar converters personalizados aquí

                    return JsonConvert.DeserializeObject<Servicio>(jsonResponse, settings);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en BuscarServicioPorId: {ex.Message}");
                return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var servicio = (Servicio)value;

            writer.WriteStartObject();
            writer.WritePropertyName("idServicio");
            writer.WriteValue(servicio.idServicio);
            writer.WriteEndObject();
        }
    }

    // ========== CONVERTIDOR PERSONALIZADO PARA SERVICIO SIMPLE ==========
    public class ServicioSimpleConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ServicioSimple);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            // CASO 1: Si la API devuelve un número (ID)
            if (reader.TokenType == JsonToken.Integer)
            {
                int idServicio = Convert.ToInt32(reader.Value);
                return new ServicioSimple { idServicio = idServicio };
            }

            // CASO 2: Si la API devuelve un objeto completo, solo tomamos el ID
            if (reader.TokenType == JsonToken.StartObject)
            {
                var servicioSimple = new ServicioSimple();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.EndObject)
                    {
                        break;
                    }

                    if (reader.TokenType == JsonToken.PropertyName)
                    {
                        string propertyName = reader.Value.ToString();
                        reader.Read();

                        if (propertyName == "idServicio" && reader.TokenType == JsonToken.Integer)
                        {
                            servicioSimple.idServicio = Convert.ToInt32(reader.Value);
                        }
                        else
                        {
                            // Saltar otras propiedades que no necesitamos
                            reader.Skip();
                        }
                    }
                }

                return servicioSimple;
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

            var servicioSimple = (ServicioSimple)value;

            writer.WriteStartObject();
            writer.WritePropertyName("idServicio");
            writer.WriteValue(servicioSimple.idServicio);
            writer.WriteEndObject();
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

    // ========== HORARIO SEMANAL (ACTUALIZADO) ==========

    public class HorarioSemanal
    {
        public int idHorario { get; set; }

        [JsonConverter(typeof(ServicioSimpleConverter))]
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