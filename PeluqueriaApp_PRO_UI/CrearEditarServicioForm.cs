using System;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class CrearEditarServicioForm : Form
    {
        private int? idServicio = null;
        private bool esEdicion = false;

        // Constructor para CREAR nuevo servicio
        public CrearEditarServicioForm()
        {
            InitializeComponent();
            this.Text = "Crear Nuevo Servicio";
            TituloLbl.Text = "Crear Nuevo Servicio";
            esEdicion = false;
        }

        // Constructor para EDITAR servicio existente
        public CrearEditarServicioForm(int id)
        {
            InitializeComponent();
            this.Text = "Editar Servicio";
            TituloLbl.Text = "Editar Servicio";
            idServicio = id;
            esEdicion = true;

            CargarDatosServicio(id);
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

            if (!int.TryParse(DuracionTxt.Text, out int duracion) || duracion <= 0)
            {
                MessageBox.Show("La duración debe ser un número positivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DuracionTxt.Focus();
                return;
            }

            if (!decimal.TryParse(PrecioTxt.Text, out decimal precio) || precio <= 0)
            {
                MessageBox.Show("El precio debe ser un número positivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PrecioTxt.Focus();
                return;
            }

            // Deshabilitar botón mientras se procesa
            GuardarBtn.Enabled = false;
            GuardarBtn.Text = "Guardando...";

            try
            {
                var servicio = new Servicio
                {
                    nombre = NombreTxt.Text.Trim(),
                    descripcion = DescripcionTxt.Text.Trim(),
                    duracion = duracion,
                    precio = precio
                };

                if (esEdicion)
                {
                    servicio.idServicio = idServicio.Value;
                    await ApiService.PutAsync<Servicio>($"api/servicios/{idServicio.Value}", servicio);
                    MessageBox.Show("Servicio actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await ApiService.PostAsync<Servicio>("api/servicios", servicio);
                    MessageBox.Show("Servicio creado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void CargarDatosServicio(int id)
        {
            try
            {
                var servicio = await ApiService.GetAsync<Servicio>($"api/servicios/{id}");

                NombreTxt.Text = servicio.nombre;
                DescripcionTxt.Text = servicio.descripcion;
                DuracionTxt.Text = servicio.duracion.ToString();
                PrecioTxt.Text = servicio.precio.ToString("F2");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del servicio: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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