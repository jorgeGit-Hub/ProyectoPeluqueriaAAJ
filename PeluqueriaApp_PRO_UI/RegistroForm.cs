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
            /*EspecialidadLbl.Visible = false;
            EspecialidadTxt.Visible = false;*/
        }

        private void MostrarCamposAdministrador()
        {
            /*EspecialidadLbl.Visible = true;
            EspecialidadTxt.Visible = true;*/
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
                // Crear objeto de registro
                var signupData = new SignupRequest
                {
                    nombre = NombreTxt.Text.Trim(),
                    apellidos = ApellidosTxt.Text.Trim(),
                    correo = CorreoTxt.Text.Trim(),
                    contrasena = ContrasenaTxt.Text.Trim(),
                    rol = rolSeleccionado
                };

                // Agregar campos específicos solo si es cliente
                if (rolSeleccionado == "cliente")
                {
                    signupData.telefono = TelefonoTxt.Text.Trim();
                    signupData.direccion = DireccionTxt.Text.Trim();
                    signupData.alergenos = AlergenosTxt.Text.Trim();
                    signupData.observaciones = ObservacionesTxt.Text.Trim();
                }

                // Llamar al API de signup (el backend maneja la creación según el rol)
                var resultado = await ApiService.PostAsync<MessageResponse>("api/auth/signup", signupData);

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

        private void VolverLoginBtn_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}