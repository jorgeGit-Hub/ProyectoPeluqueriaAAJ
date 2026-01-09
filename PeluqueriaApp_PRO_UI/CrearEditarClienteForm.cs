using System;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class CrearEditarClienteForm : Form
    {
        private int? idCliente = null; // null = crear, con valor = editar
        private bool esEdicion = false;

        // Constructor para CREAR nuevo cliente
        public CrearEditarClienteForm()
        {
            InitializeComponent();
            this.Text = "Crear Nuevo Cliente";
            TituloLbl.Text = "Crear Nuevo Cliente";
            esEdicion = false;
        }

        // Constructor para EDITAR cliente existente
        public CrearEditarClienteForm(int id)
        {
            InitializeComponent();
            this.Text = "Editar Cliente";
            TituloLbl.Text = "Editar Cliente";
            idCliente = id;
            esEdicion = true;

            // Ocultar campo contraseña en edición
            ContrasenaLbl.Visible = false;
            ContrasenaTxt.Visible = false;

            CargarDatosCliente(id);
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

            // Deshabilitar botón mientras se procesa
            GuardarBtn.Enabled = false;
            GuardarBtn.Text = "Guardando...";

            try
            {
                if (esEdicion)
                {
                    // EDITAR cliente existente
                    var clienteData = new Cliente
                    {
                        idUsuario = idCliente.Value,
                        telefono = TelefonoTxt.Text.Trim(),
                        direccion = DireccionTxt.Text.Trim(),
                        alergenos = AlergenosTxt.Text.Trim(),
                        observaciones = ObservacionesTxt.Text.Trim()
                    };

                    var resultado = await ApiService.PutAsync<Cliente>($"api/clientes/{idCliente.Value}", clienteData);

                    MessageBox.Show("Cliente actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // CREAR nuevo cliente
                    var signupData = new SignupRequest
                    {
                        nombre = NombreTxt.Text.Trim(),
                        apellidos = ApellidosTxt.Text.Trim(),
                        correo = CorreoTxt.Text.Trim(),
                        contrasena = ContrasenaTxt.Text.Trim(),
                        rol = "cliente",
                        telefono = TelefonoTxt.Text.Trim(),
                        direccion = DireccionTxt.Text.Trim(),
                        alergenos = AlergenosTxt.Text.Trim(),
                        observaciones = ObservacionesTxt.Text.Trim()
                    };

                    var resultado = await ApiService.PostAsync<MessageResponse>("api/auth/signup", signupData);

                    MessageBox.Show("Cliente creado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void CargarDatosCliente(int id)
        {
            try
            {
                // Cargar datos del usuario
                var usuario = await ApiService.GetAsync<Usuario>($"api/usuarios/{id}");

                // Cargar datos del cliente
                var cliente = await ApiService.GetAsync<Cliente>($"api/clientes/{id}");

                // Llenar los campos
                NombreTxt.Text = usuario.nombre;
                ApellidosTxt.Text = usuario.apellidos;
                CorreoTxt.Text = usuario.correo;
                TelefonoTxt.Text = cliente.telefono;
                DireccionTxt.Text = cliente.direccion;
                AlergenosTxt.Text = cliente.alergenos;
                ObservacionesTxt.Text = cliente.observaciones;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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