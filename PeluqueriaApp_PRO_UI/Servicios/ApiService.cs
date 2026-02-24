using System;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PeluqueriaApp.Services
{
    public class ApiService
    {
        private const string BASE_URL = "http://localhost:8090";

        // Token JWT que se guarda después del login
        public static string Token { get; set; }

        // Configurar el token de autenticación
        public static void SetAuthToken(string token)
        {
            Token = token;
            System.Diagnostics.Debug.WriteLine($"Token configurado: {token?.Substring(0, Math.Min(20, token?.Length ?? 0))}...");
        }

        // Limpiar el token (al cerrar sesión)
        public static void ClearAuthToken()
        {
            Token = null;
            System.Diagnostics.Debug.WriteLine("Token eliminado");
        }

        // Obtener Logo Base64 en texto plano (sin Json)
        public static async Task<string> ObtenerLogoBase64Async()
        {
            return await Task.Run(() =>
            {
                try
                {
                    // Usamos la variable BASE_URL que ya tienes definida en tu archivo
                    var url = $"{BASE_URL}/api/configuracion/logo";
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";

                    // No mandamos token porque configuramos esta ruta como pública en Spring Boot

                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        using (var streamReader = new StreamReader(response.GetResponseStream()))
                        {
                            // Devolvemos el string gigante en Base64 tal cual llega
                            return streamReader.ReadToEnd();
                        }
                    }
                }
                catch (WebException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error web al obtener el logo: {ex.Message}");
                    return null;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error al obtener el logo: {ex.Message}");
                    return null;
                }
            });
        }

        // GET - Obtener datos
        public static async Task<T> GetAsync<T>(string endpoint)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var url = $"{BASE_URL}/{endpoint}";
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.ContentType = "application/json";
                    request.Accept = "application/json";

                    // Añadir el token de autorización
                    if (!string.IsNullOrEmpty(Token))
                    {
                        request.Headers.Add("Authorization", $"Bearer {Token}");
                    }

                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        using (var streamReader = new StreamReader(response.GetResponseStream()))
                        {
                            string responseBody = streamReader.ReadToEnd();
                            System.Diagnostics.Debug.WriteLine("=== RESPUESTA JSON CRUDA POST ===");
                            System.Diagnostics.Debug.WriteLine(responseBody);
                            System.Diagnostics.Debug.WriteLine("=== FIN JSON ===");
                            return JsonConvert.DeserializeObject<T>(responseBody);
                        }
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Response != null)
                    {
                        using (var streamReader = new StreamReader(ex.Response.GetResponseStream()))
                        {
                            string errorBody = streamReader.ReadToEnd();
                            var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                            throw new Exception($"Error {statusCode}: {errorBody}");
                        }
                    }
                    throw new Exception($"Error al conectar con la API: {ex.Message}");
                }
            });
        }

        // POST - Enviar datos
        public static async Task<T> PostAsync<T>(string endpoint, object data, bool sendToken = true)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var url = $"{BASE_URL}/{endpoint}";
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    string jsonData = JsonConvert.SerializeObject(data);

                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.Accept = "application/json";

                    // Solo añadir token si sendToken es true
                    if (sendToken && !string.IsNullOrEmpty(Token))
                    {
                        request.Headers.Add("Authorization", $"Bearer {Token}");
                    }

                    // Escribir el body
                    byte[] byteArray = Encoding.UTF8.GetBytes(jsonData);
                    request.ContentLength = byteArray.Length;

                    using (Stream dataStream = request.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                    }

                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        using (var streamReader = new StreamReader(response.GetResponseStream()))
                        {
                            string responseBody = streamReader.ReadToEnd();
                            return JsonConvert.DeserializeObject<T>(responseBody);
                        }
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Response != null)
                    {
                        using (var streamReader = new StreamReader(ex.Response.GetResponseStream()))
                        {
                            string errorBody = streamReader.ReadToEnd();
                            var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                            throw new Exception($"Error {statusCode}: {errorBody}");
                        }
                    }
                    throw new Exception($"Error al conectar con la API: {ex.Message}");
                }
            });
        }

        // PUT - Actualizar datos
        public static async Task<T> PutAsync<T>(string endpoint, object data)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var url = $"{BASE_URL}/{endpoint}";
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    string jsonData = JsonConvert.SerializeObject(data);

                    request.Method = "PUT";
                    request.ContentType = "application/json";
                    request.Accept = "application/json";

                    if (!string.IsNullOrEmpty(Token))
                    {
                        request.Headers.Add("Authorization", $"Bearer {Token}");
                    }

                    byte[] byteArray = Encoding.UTF8.GetBytes(jsonData);
                    request.ContentLength = byteArray.Length;

                    using (Stream dataStream = request.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                    }

                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        using (var streamReader = new StreamReader(response.GetResponseStream()))
                        {
                            string responseBody = streamReader.ReadToEnd();
                            return JsonConvert.DeserializeObject<T>(responseBody);
                        }
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Response != null)
                    {
                        using (var streamReader = new StreamReader(ex.Response.GetResponseStream()))
                        {
                            string errorBody = streamReader.ReadToEnd();
                            throw new Exception($"Error {((HttpWebResponse)ex.Response).StatusCode}: {errorBody}");
                        }
                    }
                    throw new Exception($"Error al conectar con la API: {ex.Message}");
                }
            });
        }

        // DELETE - Eliminar datos
        public static async Task<bool> DeleteAsync(string endpoint)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var url = $"{BASE_URL}/{endpoint}";
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "DELETE";
                    request.ContentType = "application/json";
                    request.Accept = "application/json";

                    if (!string.IsNullOrEmpty(Token))
                    {
                        request.Headers.Add("Authorization", $"Bearer {Token}");
                    }

                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusCode == HttpStatusCode.OK ||
                               response.StatusCode == HttpStatusCode.NoContent;
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Response != null)
                    {
                        using (var streamReader = new StreamReader(ex.Response.GetResponseStream()))
                        {
                            string errorBody = streamReader.ReadToEnd();
                            throw new Exception($"Error al eliminar: {errorBody}");
                        }
                    }
                    throw new Exception($"Error al conectar con la API: {ex.Message}");
                }
            });
        }
    }
}