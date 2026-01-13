using System;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class RegistroForm : Form
    {
        public RegistroForm()
        {
            InitializeComponent();

            // Configurar evento para cambio de rol
            RolCombo.SelectedIndexChanged += RolCombo_SelectedIndexChanged;

            // Mostrar campos iniciales según el rol por defecto
            ActualizarCamposSegunRol();
        }

        private void RolCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarCamposSegunRol();
        }

        private void ActualizarCamposSegunRol()
        {
            string rolSeleccionado = RolCombo.SelectedItem?.ToString()?.ToLower() ?? "cliente";

            // Ocultar todos los campos específicos primero
            OcultarCamposCliente();
            OcultarCamposAdministrador();

            // Mostrar campos según el rol seleccionado
            switch (rolSeleccionado)
            {
                case "cliente":
                    MostrarCamposCliente();
                    break;
                case "administrador":
                    MostrarCamposAdministrador();
                    break;
                case "alumno":
                    // Alumno solo necesita los campos básicos
                    break;
            }
        }

        private void OcultarCamposCliente()
        {
            TelefonoLbl.Visible = false;
            TelefonoTxt.Visible = false;
            DireccionLbl.Visible = false;
            DireccionTxt.Visible = false;
            AlergenosLbl.Visible = false;
            AlergenosTxt.Visible = false;
            ObservacionesLbl.Visible = false;
            ObservacionesTxt.Visible = false;
        }

        private void MostrarCamposCliente()
        {
            TelefonoLbl.Visible = true;
            TelefonoTxt.Visible = true;
            DireccionLbl.Visible = true;
            DireccionTxt.Visible = true;
            AlergenosLbl.Visible = true;
            AlergenosTxt.Visible = true;
            ObservacionesLbl.Visible = true;
            ObservacionesTxt.Visible = true;
        }

        private void OcultarCamposAdministrador()
        {
            EspecialidadLbl.Visible = false;
            EspecialidadTxt.Visible = false;
        }

        private void MostrarCamposAdministrador()
        {
            EspecialidadLbl.Visible = true;
            EspecialidadTxt.Visible = true;
        }

        private async void RegistrarseBtn_Click(object sender, EventArgs e)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(NombreTxt.Text))
            {
                MessageBox.Show("El nombre es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NombreTxt.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(ApellidosTxt.Text))
            {
                MessageBox.Show("Los apellidos son obligatorios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ApellidosTxt.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(CorreoTxt.Text) || !CorreoTxt.Text.Contains("@"))
            {
                MessageBox.Show("El correo electrónico no es válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CorreoTxt.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(ContrasenaTxt.Text))
            {
                MessageBox.Show("La contraseña es obligatoria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ContrasenaTxt.Focus();
                return;
            }

            if (ContrasenaTxt.Text.Length < 6)
            {
                MessageBox.Show("La contraseña debe tener al menos 6 caracteres", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ContrasenaTxt.Focus();
                return;
            }

            if (ContrasenaTxt.Text != ConfirmarContrasenaTxt.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ConfirmarContrasenaTxt.Focus();
                return;
            }

            if (RolCombo.SelectedItem == null)
            {
                MessageBox.Show("Debes seleccionar un rol", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RolCombo.Focus();
                return;
            }

            string rolSeleccionado = RolCombo.SelectedItem.ToString().ToLower();

            // Deshabilitar botón mientras se procesa
            RegistrarseBtn.Enabled = false;
            RegistrarseBtn.Text = "Registrando...";

            try
            {
                // Según el rol, usar el endpoint correspondiente
                if (rolSeleccionado == "cliente")
                {
                    await RegistrarCliente();
                }
                else if (rolSeleccionado == "administrador")
                {
                    await RegistrarAdministrador();
                }
                else if (rolSeleccionado == "alumno")
                {
                    await RegistrarAlumno();
                }

                MessageBox.Show("¡Registro exitoso! Ya puedes iniciar sesión con tu cuenta.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Volver al login
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrarse: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Rehabilitar botón
                RegistrarseBtn.Enabled = true;
                RegistrarseBtn.Text = "Registrarse";
            }
        }

        private async System.Threading.Tasks.Task RegistrarCliente()
        {
            // Crear el objeto payload para el endpoint /with-cliente
            var payload = new
            {
                usuario = new
                {
                    nombre = NombreTxt.Text.Trim(),
                    apellidos = ApellidosTxt.Text.Trim(),
                    correo = CorreoTxt.Text.Trim(),
                    contrasena = ContrasenaTxt.Text.Trim(),
                    rol = "cliente"
                },
                cliente = new
                {
                    telefono = TelefonoTxt.Text.Trim(),
                    direccion = DireccionTxt.Text.Trim(),
                    alergenos = AlergenosTxt.Text.Trim(),
                    observaciones = ObservacionesTxt.Text.Trim()
                }
            };

            // Llamar al endpoint /api/usuarios/with-cliente
            var resultado = await ApiService.PostAsync<object>("api/usuarios/with-cliente", payload, sendToken: false);
        }

        private async System.Threading.Tasks.Task RegistrarAdministrador()
        {
            // Crear el objeto payload para el endpoint /with-administrador
            var payload = new
            {
                usuario = new
                {
                    nombre = NombreTxt.Text.Trim(),
                    apellidos = ApellidosTxt.Text.Trim(),
                    correo = CorreoTxt.Text.Trim(),
                    contrasena = ContrasenaTxt.Text.Trim(),
                    rol = "administrador"
                },
                administrador = new
                {
                    especialidad = EspecialidadTxt.Text.Trim()
                }
            };

            // Llamar al endpoint /api/usuarios/with-administrador
            var resultado = await ApiService.PostAsync<object>("api/usuarios/with-administrador", payload, sendToken: false);
        }

        private async System.Threading.Tasks.Task RegistrarAlumno()
        {
            // Para alumno, usar el endpoint de signup básico del AuthController
            var signupData = new SignupRequest
            {
                nombre = NombreTxt.Text.Trim(),
                apellidos = ApellidosTxt.Text.Trim(),
                correo = CorreoTxt.Text.Trim(),
                contrasena = ContrasenaTxt.Text.Trim(),
                rol = "alumno"
            };

            var resultado = await ApiService.PostAsync<MessageResponse>("api/auth/signup", signupData, sendToken: false);
        }

        private void VolverLoginBtn_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}