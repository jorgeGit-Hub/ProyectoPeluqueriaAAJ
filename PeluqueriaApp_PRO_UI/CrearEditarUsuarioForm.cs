using System;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class CrearEditarUsuarioForm : Form
    {
        private int? idUsuario = null;
        private bool esEdicion = false;

        // Constructor para CREAR nuevo usuario
        public CrearEditarUsuarioForm()
        {
            InitializeComponent();
            this.Text = "Crear Nuevo Usuario";
            TituloLbl.Text = "Crear Nuevo Usuario";
            esEdicion = false;
        }

        // Constructor para EDITAR usuario existente
        public CrearEditarUsuarioForm(int id)
        {
            InitializeComponent();
            this.Text = "Editar Usuario";
            TituloLbl.Text = "Editar Usuario";
            idUsuario = id;
            esEdicion = true;

            // Ocultar campo contraseña en edición
            ContrasenaLbl.Visible = false;
            ContrasenaTxt.Visible = false;

            CargarDatosUsuario(id);
        }

        private async void GuardarBtn_Click(object sender, EventArgs e)
        {
            // Validaciones
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

            if (!esEdicion && string.IsNullOrWhiteSpace(ContrasenaTxt.Text))
            {
                MessageBox.Show("La contraseña es obligatoria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ContrasenaTxt.Focus();
                return;
            }

            if (!esEdicion && ContrasenaTxt.Text.Length < 6)
            {
                MessageBox.Show("La contraseña debe tener al menos 6 caracteres", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ContrasenaTxt.Focus();
                return;
            }

            if (RolCombo.SelectedItem == null)
            {
                MessageBox.Show("Debes seleccionar un rol", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RolCombo.Focus();
                return;
            }

            // Deshabilitar botón mientras se procesa
            GuardarBtn.Enabled = false;
            GuardarBtn.Text = "Guardando...";

            try
            {
                if (esEdicion)
                {
                    // EDITAR usuario existente
                    var usuarioData = new Usuario
                    {
                        idUsuario = idUsuario.Value,
                        nombre = NombreTxt.Text.Trim(),
                        apellidos = ApellidosTxt.Text.Trim(),
                        correo = CorreoTxt.Text.Trim(),
                        rol = RolCombo.SelectedItem.ToString().ToLower()
                    };

                    var resultado = await ApiService.PutAsync<Usuario>($"api/usuarios/{idUsuario.Value}", usuarioData);

                    MessageBox.Show("Usuario actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // CREAR nuevo usuario
                    var signupData = new SignupRequest
                    {
                        nombre = NombreTxt.Text.Trim(),
                        apellidos = ApellidosTxt.Text.Trim(),
                        correo = CorreoTxt.Text.Trim(),
                        contrasena = ContrasenaTxt.Text.Trim(),
                        rol = RolCombo.SelectedItem.ToString().ToLower()
                    };

                    var resultado = await ApiService.PostAsync<MessageResponse>("api/auth/signup", signupData);

                    MessageBox.Show("Usuario creado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                GuardarBtn.Enabled = true;
                GuardarBtn.Text = "💾 Guardar";
            }
        }

        private async void CargarDatosUsuario(int id)
        {
            try
            {
                var usuario = await ApiService.GetAsync<Usuario>($"api/usuarios/{id}");

                NombreTxt.Text = usuario.nombre;
                ApellidosTxt.Text = usuario.apellidos;
                CorreoTxt.Text = usuario.correo;

                // Seleccionar el rol en el combo
                string rolCapitalizado = char.ToUpper(usuario.rol[0]) + usuario.rol.Substring(1).ToLower();
                RolCombo.SelectedItem = rolCapitalizado;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelarBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que quieres cancelar? Se perderán los cambios no guardados.",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}