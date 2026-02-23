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

            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor, completa todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            IniciarSesionBoto.Enabled = false;
            IniciarSesionBoto.Text = "Iniciando sesión...";

            try
            {
                ApiService.ClearAuthToken();

                var loginRequest = new LoginRequest
                {
                    correo = correo,
                    contrasena = contrasena
                };

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

                if (response == null || string.IsNullOrEmpty(response.token))
                {
                    MessageBox.Show("Error: No se recibió respuesta válida del servidor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                System.Diagnostics.Debug.WriteLine("Token recibido correctamente");

                // ✅ Guardar token en ApiService
                ApiService.SetAuthToken(response.token);

                // ✅ Guardar token también en UserSession como respaldo
                UserSession.Token = response.token;

                // Guardar información del usuario en sesión
                UserSession.UserId = response.id;
                UserSession.Nombre = response.nombre;
                UserSession.Apellidos = response.apellidos;
                UserSession.Correo = response.correo;
                UserSession.Rol = response.rol;

                System.Diagnostics.Debug.WriteLine($"Token guardado verificado: {!string.IsNullOrEmpty(ApiService.Token)}");

                MessageBox.Show($"¡Bienvenido/a {response.nombre}!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

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