namespace PeluqueriaApp
{
    // Clase estática para guardar la información del usuario en sesión
    public static class UserSession
    {
        public static int UserId { get; set; }
        public static string Nombre { get; set; }
        public static string Apellidos { get; set; }
        public static string Correo { get; set; }
        public static string Rol { get; set; }

        // Método para obtener el nombre completo
        public static string NombreCompleto => $"{Nombre} {Apellidos}";

        // Verificar si es administrador
        public static bool EsAdministrador => Rol?.ToLower() == "administrador";

        // Verificar si es alumno
        public static bool EsAlumno => Rol?.ToLower() == "alumno";

        // Verificar si es cliente
        public static bool EsCliente => Rol?.ToLower() == "cliente";

        // Limpiar sesión
        public static void CerrarSesion()
        {
            UserId = 0;
            Nombre = null;
            Apellidos = null;
            Correo = null;
            Rol = null;
        }
    }
}