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

            // Configurar evento para cambio de rol
            RolCombo.SelectedIndexChanged += RolCombo_SelectedIndexChanged;

            // Mostrar campos iniciales según el rol por defecto
            ActualizarCamposSegunRol();
        }

        // Constructor para EDITAR usuario existente
        public CrearEditarUsuarioForm(int id)
        {
            InitializeComponent();
            this.Text = "Editar Usuario";
            TituloLbl.Text = "Editar Usuario";
            idUsuario = id;
            esEdicion = true;

            // Configurar evento para cambio de rol
            RolCombo.SelectedIndexChanged += RolCombo_SelectedIndexChanged;

            // Ocultar campo contraseña en edición
            ContrasenaLbl.Visible = false;
            ContrasenaTxt.Visible = false;

            CargarDatosUsuario(id);
        }

        private void RolCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarCamposSegunRol();
        }

        private void ActualizarCamposSegunRol()
        {
            string rolSeleccionado = RolCombo.SelectedItem?.ToString()?.ToLower() ?? "alumno";

            // Ocultar todos los campos específicos primero
            EspecialidadLbl.Visible = false;
            EspecialidadTxt.Visible = false;

            // Mostrar campos según el rol seleccionado
            if (rolSeleccionado == "administrador")
            {
                EspecialidadLbl.Visible = true;
                EspecialidadTxt.Visible = true;
            }
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

            string rolSeleccionado = RolCombo.SelectedItem.ToString().ToLower();

            // Validación específica para administrador
            if (rolSeleccionado == "administrador" && string.IsNullOrWhiteSpace(EspecialidadTxt.Text))
            {
                MessageBox.Show("La especialidad es obligatoria para administradores", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EspecialidadTxt.Focus();
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
                        rol = rolSeleccionado
                    };

                    await ApiService.PutAsync<Usuario>($"api/usuarios/{idUsuario.Value}", usuarioData);

                    // Si es administrador, actualizar también la especialidad
                    if (rolSeleccionado == "administrador")
                    {
                        var adminData = new Administrador
                        {
                            idUsuario = idUsuario.Value,
                            especialidad = EspecialidadTxt.Text.Trim()
                        };

                        try
                        {
                            await ApiService.PutAsync<Administrador>($"api/administradores/{idUsuario.Value}", adminData);
                        }
                        catch
                        {
                            // Si no existe el administrador, intentar crearlo
                            // (esto podría ser necesario si se cambió el rol a administrador)
                        }
                    }

                    MessageBox.Show("Usuario actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // CREAR nuevo usuario
                    if (rolSeleccionado == "administrador")
                    {
                        // Crear administrador con el endpoint especial
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

                        await ApiService.PostAsync<object>("api/usuarios/with-administrador", payload, sendToken: false);
                    }
                    else
                    {
                        // Crear usuario normal (alumno o cliente)
                        var signupData = new SignupRequest
                        {
                            nombre = NombreTxt.Text.Trim(),
                            apellidos = ApellidosTxt.Text.Trim(),
                            correo = CorreoTxt.Text.Trim(),
                            contrasena = ContrasenaTxt.Text.Trim(),
                            rol = rolSeleccionado
                        };

                        await ApiService.PostAsync<MessageResponse>("api/auth/signup", signupData, sendToken: false);
                    }

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

                // Si es administrador, cargar la especialidad
                if (usuario.rol.ToLower() == "administrador")
                {
                    try
                    {
                        var admin = await ApiService.GetAsync<Administrador>($"api/administradores/{id}");
                        EspecialidadTxt.Text = admin.especialidad;
                    }
                    catch
                    {
                        // Si no se puede cargar la especialidad, dejarla vacía
                    }
                }
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