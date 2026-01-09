using System;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void IniciarSesionBoto_Click(object sender, EventArgs e)
        {
            string correo = CorreuTxt.Text.Trim();
            string contrasena = ContrasenyaTxt.Text.Trim();

            // Validación básica
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor, completa todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Deshabilitar botón mientras se procesa
            IniciarSesionBoto.Enabled = false;
            IniciarSesionBoto.Text = "Iniciando sesión...";

            try
            {
                // Limpiar token anterior
                ApiService.ClearAuthToken();

                // Crear objeto de login
                var loginRequest = new LoginRequest
                {
                    correo = correo,
                    contrasena = contrasena
                };

                // Llamar al API sin token (es login público)
                var response = await ApiService.PostAsync<LoginResponse>("api/auth/signin", loginRequest, sendToken: false);

                System.Diagnostics.Debug.WriteLine("=== RESPUESTA DEL SERVIDOR ===");
                System.Diagnostics.Debug.WriteLine($"Response es null: {response == null}");

                if (response != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Token es null o vacío: {string.IsNullOrEmpty(response.token)}");
                    System.Diagnostics.Debug.WriteLine($"ID: {response.id}");
                    System.Diagnostics.Debug.WriteLine($"Nombre: {response.nombre}");
                    System.Diagnostics.Debug.WriteLine($"Rol: {response.rol}");
                    if (!string.IsNullOrEmpty(response.token))
                    {
                        System.Diagnostics.Debug.WriteLine($"Token (primeros 50): {response.token.Substring(0, Math.Min(50, response.token.Length))}");
                    }
                }

                // Verificar que recibimos token
                if (response == null || string.IsNullOrEmpty(response.token))
                {
                    MessageBox.Show("Error: No se recibió respuesta válida del servidor.\n\nRevisa la ventana Output para más detalles.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"Token recibido correctamente");

                // Guardar el token JWT
                ApiService.SetAuthToken(response.token);

                // Verificar que se guardó
                System.Diagnostics.Debug.WriteLine($"Token guardado verificado: {!string.IsNullOrEmpty(ApiService.Token)}");

                // Guardar información del usuario
                UserSession.UserId = response.id;
                UserSession.Nombre = response.nombre;
                UserSession.Apellidos = response.apellidos;
                UserSession.Correo = response.correo;
                UserSession.Rol = response.rol;

                MessageBox.Show($"¡Bienvenido/a {response.nombre}!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Redirigir al Home
                HomeForm homeForm = new HomeForm();
                homeForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Error completo: {ex}");
            }
            finally
            {
                // Rehabilitar botón
                IniciarSesionBoto.Enabled = true;
                IniciarSesionBoto.Text = "Iniciar Sesión";
            }
        }

        private void RegistrarseBoto_Click(object sender, EventArgs e)
        {
            RegistroForm registroForm = new RegistroForm();
            registroForm.Show();
            this.Hide();
        }
    }
}