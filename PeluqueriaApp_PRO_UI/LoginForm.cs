using System;
using System.Drawing;
using System.IO;
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
            // Conectamos el evento Load
            this.Load += LoginForm_Load;
        }

        private async void LoginForm_Load(object sender, EventArgs e)
        {
            // 1. Verificamos si el logo ya está descargado
            if (UserSession.LogoApp == null)
            {
                // 2. Llamamos al backend a por el texto en Base64
                string base64String = await ApiService.ObtenerLogoBase64Async();

                if (!string.IsNullOrEmpty(base64String))
                {
                    try
                    {
                        // 3. Convertimos a Imagen y la guardamos en la sesión global
                        byte[] imageBytes = Convert.FromBase64String(base64String);
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            UserSession.LogoApp = Image.FromStream(ms);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error procesando el logo: " + ex.Message);
                    }
                }
            }

            // 4. Mostramos el logo en los dos huecos del Login
            if (UserSession.LogoApp != null)
            {
                pbLogoBienvenida.Image = UserSession.LogoApp;
                pbLogoLogin.Image = UserSession.LogoApp;
            }
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

                if (response == null || string.IsNullOrEmpty(response.token))
                {
                    MessageBox.Show("Error: No se recibió respuesta válida del servidor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Guardar token y sesión
                ApiService.SetAuthToken(response.token);
                UserSession.Token = response.token;
                UserSession.UserId = response.id;
                UserSession.Nombre = response.nombre;
                UserSession.Apellidos = response.apellidos;
                UserSession.Correo = response.correo;
                UserSession.Rol = response.rol;

                MessageBox.Show($"¡Bienvenido/a {response.nombre}!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                HomeForm homeForm = new HomeForm();
                homeForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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