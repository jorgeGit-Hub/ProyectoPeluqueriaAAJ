using PeluqueriaApp.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    public partial class MiCuentaForm : Form
    {
        // Variable para controlar si estamos editando o no
        private bool modoEdicion = false;

        public MiCuentaForm()
        {
            InitializeComponent();
            this.Load += MiCuentaForm_Load;
            CargarDatosUsuario();
        }

        private void CargarDatosUsuario()
        {
            // Saludo superior
            BienvenidaLbl.Text = $"Bienvenido/a, {UserSession.Nombre}";

            // Rellenar las cajas de texto con los datos de sesión
            NombreTxt.Text = UserSession.Nombre;
            ApellidosTxt.Text = UserSession.Apellidos;
            CorreoTxt.Text = UserSession.Correo;

            // Ponemos la primera letra del rol en mayúscula
            if (!string.IsNullOrEmpty(UserSession.Rol))
            {
                RolTxt.Text = char.ToUpper(UserSession.Rol[0]) + UserSession.Rol.Substring(1).ToLower();
            }
        }

        // ==============================================================
        // LÓGICA PARA EDITAR DATOS DEL USUARIO
        // ==============================================================
        private async void GuardarCambiosBtn_Click(object sender, EventArgs e)
        {
            if (!modoEdicion)
            {
                // 1. ENTRAMOS EN MODO EDICIÓN
                modoEdicion = true;

                // Desbloqueamos los campos
                NombreTxt.ReadOnly = false;
                ApellidosTxt.ReadOnly = false;
                CorreoTxt.ReadOnly = false;

                // Fondo blanco para que parezcan editables
                NombreTxt.BackColor = Color.White;
                ApellidosTxt.BackColor = Color.White;
                CorreoTxt.BackColor = Color.White;

                // Cambiamos el botón
                GuardarCambiosBtn.Text = "💾 Confirmar Cambios";
                GuardarCambiosBtn.BackColor = Color.FromArgb(34, 139, 34); // Verde oscuro
            }
            else
            {
                // 2. GUARDAMOS LOS CAMBIOS EN LA BD
                if (string.IsNullOrWhiteSpace(NombreTxt.Text) ||
                    string.IsNullOrWhiteSpace(ApellidosTxt.Text) ||
                    string.IsNullOrWhiteSpace(CorreoTxt.Text))
                {
                    MessageBox.Show("Los campos Nombre, Apellidos y Correo no pueden estar vacíos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                GuardarCambiosBtn.Text = "Guardando...";
                GuardarCambiosBtn.Enabled = false;

                try
                {
                    UserSession.EnsureTokenInApiService();

                    // Datos a actualizar
                    var payload = new Dictionary<string, object>
                    {
                        { "nombre", NombreTxt.Text.Trim() },
                        { "apellidos", ApellidosTxt.Text.Trim() },
                        { "correo", CorreoTxt.Text.Trim() }
                    };

                    await ApiService.PutAsync<object>($"api/usuarios/{UserSession.UserId}", payload);

                    // Actualizamos la sesión local
                    UserSession.Nombre = NombreTxt.Text.Trim();
                    UserSession.Apellidos = ApellidosTxt.Text.Trim();
                    UserSession.Correo = CorreoTxt.Text.Trim();

                    BienvenidaLbl.Text = $"Bienvenido/a, {UserSession.Nombre}";

                    MessageBox.Show("¡Tus datos de perfil se han actualizado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Volvemos a bloquear los campos
                    modoEdicion = false;
                    NombreTxt.ReadOnly = true;
                    ApellidosTxt.ReadOnly = true;
                    CorreoTxt.ReadOnly = true;

                    NombreTxt.BackColor = Color.FromArgb(245, 245, 245);
                    ApellidosTxt.BackColor = Color.FromArgb(245, 245, 245);
                    CorreoTxt.BackColor = Color.FromArgb(245, 245, 245);

                    GuardarCambiosBtn.Text = "✏️ Editar Perfil";
                    GuardarCambiosBtn.BackColor = Color.FromArgb(139, 90, 60);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    GuardarCambiosBtn.Enabled = true;
                    if (modoEdicion) GuardarCambiosBtn.Text = "💾 Confirmar Cambios";
                    else GuardarCambiosBtn.Text = "✏️ Editar Perfil";
                }
            }
        }

        // ==============================================================
        // LÓGICA PARA CAMBIAR LA CONTRASEÑA
        // ==============================================================

        // Muestra u oculta los campos de texto de las contraseñas
        private void AccionDesarrollo_Click(object sender, EventArgs e)
        {
            bool mostrarCampos = !NuevaContrasenaTxt.Visible;

            NuevaContrasenaLbl.Visible = mostrarCampos;
            NuevaContrasenaTxt.Visible = mostrarCampos;
            ConfirmarContrasenaLbl.Visible = mostrarCampos;
            ConfirmarContrasenaTxt.Visible = mostrarCampos;
            GuardarContrasenaBtn.Visible = mostrarCampos;

            if (!mostrarCampos)
            {
                NuevaContrasenaTxt.Clear();
                ConfirmarContrasenaTxt.Clear();
            }
        }

        // Envía la nueva contraseña a la BD
        private async void GuardarContrasenaBtn_Click(object sender, EventArgs e)
        {
            string nueva = NuevaContrasenaTxt.Text;
            string confirmacion = ConfirmarContrasenaTxt.Text;

            if (string.IsNullOrWhiteSpace(nueva) || string.IsNullOrWhiteSpace(confirmacion))
            {
                MessageBox.Show("Por favor, rellena ambos campos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nueva != confirmacion)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            GuardarContrasenaBtn.Text = "Guardando...";
            GuardarContrasenaBtn.Enabled = false;

            try
            {
                UserSession.EnsureTokenInApiService();

                var payload = new Dictionary<string, object>
                {
                    { "contrasena", nueva }
                };

                await ApiService.PutAsync<object>($"api/usuarios/{UserSession.UserId}", payload);

                MessageBox.Show("¡Contraseña actualizada correctamente en la base de datos!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Ocultamos los campos
                AccionDesarrollo_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la contraseña: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                GuardarContrasenaBtn.Text = "✓ Guardar Nueva Contraseña";
                GuardarContrasenaBtn.Enabled = true;
            }
        }

        // ==============================================================
        // NAVEGACIÓN DEL MENÚ LATERAL
        // ==============================================================

        private void IniciBoto_Click(object sender, EventArgs e) { new HomeForm().Show(); this.Hide(); }
        private void ServiciosBoto_Click(object sender, EventArgs e) { new ServiciosForm().Show(); this.Hide(); }
        private void UsuariosBoto_Click(object sender, EventArgs e) { new UsuariosForm().Show(); this.Hide(); }
        private void ClientesBoto_Click(object sender, EventArgs e) { new ClientesForm().Show(); this.Hide(); }
        private void CitasBoto_Click(object sender, EventArgs e) { new CitasForm().Show(); this.Hide(); }
        private void GruposBoto_Click(object sender, EventArgs e) { new GruposForm().Show(); this.Hide(); }
        private void HorarioForm_Click(object sender, EventArgs e) { new HorarioForm().Show(); this.Hide(); }
        private void HorarioSemanalBoto_Click(object sender, EventArgs e) { new HorarioSemanalForm().Show(); this.Hide(); }
        private void ValoracionForm_Click(object sender, EventArgs e) { new ValoracionesForm().Show(); this.Hide(); }

        private void TancarSessioBoto_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que quieres cerrar sesión?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                ApiService.ClearAuthToken();
                UserSession.CerrarSesion();
                new LoginForm().Show();
                this.Close();
            }
        }

        private void MiCuentaForm_Load(object sender, EventArgs e)
        {
            // Cargar el logo desde la memoria global de la app
            if (UserSession.LogoApp != null)
            {
                pbLogo.Image = UserSession.LogoApp;
            }

            // (Aquí debajo dejas el resto de código que ya tuvieras en este Load, si había algo)
        }
    }
}