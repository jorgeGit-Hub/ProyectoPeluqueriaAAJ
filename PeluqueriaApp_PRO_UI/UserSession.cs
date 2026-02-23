using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public static class UserSession
    {
        public static int UserId { get; set; }
        public static string Nombre { get; set; }
        public static string Apellidos { get; set; }
        public static string Correo { get; set; }
        public static string Rol { get; set; }

        // ✅ NUEVO: Guardar el token aquí también
        public static string Token { get; set; }

        public static string NombreCompleto => $"{Nombre} {Apellidos}";

        public static bool EsAdministrador => Rol?.ToLower() == "administrador";
        public static bool EsAlumno => Rol?.ToLower() == "alumno";
        public static bool EsCliente => Rol?.ToLower() == "cliente";

        // ✅ NUEVO: Si ApiService perdió el token, lo restauramos desde aquí
        public static void EnsureTokenInApiService()
        {
            if (!string.IsNullOrEmpty(Token) && string.IsNullOrEmpty(ApiService.Token))
            {
                ApiService.SetAuthToken(Token);
            }
        }

        public static void CerrarSesion()
        {
            UserId = 0;
            Nombre = null;
            Apellidos = null;
            Correo = null;
            Rol = null;
            Token = null; // ✅ Limpiar también el token
        }
    }
}