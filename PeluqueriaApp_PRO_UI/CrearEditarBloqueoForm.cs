using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class CrearEditarBloqueoForm : Form
    {
        private int? idBloqueo = null;
        private bool esEdicion = false;

        public CrearEditarBloqueoForm()
        {
            InitializeComponent();
            this.Text = "Crear Nuevo Bloqueo";
            TituloLbl.Text = "Crear Nuevo Bloqueo";
            esEdicion = false;

            FechaCalendar.SetDate(DateTime.Now);
            _ = CargarAdministradoresAsync();
        }

        public CrearEditarBloqueoForm(int id)
        {
            InitializeComponent();
            this.Text = "Editar Bloqueo";
            TituloLbl.Text = "Editar Bloqueo";
            idBloqueo = id;
            esEdicion = true;

            CargarListasYDatos(id);
        }

        private async Task CargarAdministradoresAsync()
        {
            try
            {
                // Obtenemos todos los usuarios y filtramos a nivel local los que son administradores
                var usuarios = await ApiService.GetAsync<List<Usuario>>("api/usuarios");
                var administradores = usuarios?.Where(u => u.rol != null && u.rol.ToLower() == "administrador").ToList();

                AdminCombo.Items.Clear();
                if (administradores != null)
                {
                    foreach (var admin in administradores)
                    {
                        AdminCombo.Items.Add(new ComboItem($"{admin.nombre} {admin.apellidos}", admin.idUsuario));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar administradores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CargarListasYDatos(int id)
        {
            await CargarAdministradoresAsync();
            CargarDatosBloqueo(id);
        }

        private async void CargarDatosBloqueo(int id)
        {
            try
            {
                var bloqueo = await ApiService.GetAsync<BloqueoHorario>($"api/bloqueos/{id}");
                if (bloqueo == null) return;

                if (DateTime.TryParse(bloqueo.fecha, out DateTime fechaCita))
                {
                    FechaCalendar.SetDate(fechaCita);
                }

                HoraInicioTxt.Text = bloqueo.horaInicio;
                HoraFinTxt.Text = bloqueo.horaFin;
                MotivoTxt.Text = bloqueo.motivo;

                if (bloqueo.administrador != null)
                {
                    foreach (ComboItem item in AdminCombo.Items)
                    {
                        if (item.Id == bloqueo.administrador.idUsuario)
                        {
                            AdminCombo.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del bloqueo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void GuardarBtn_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(HoraInicioTxt.Text) || string.IsNullOrWhiteSpace(HoraFinTxt.Text))
            {
                MessageBox.Show("Debe especificar la Hora de Inicio y Fin del bloqueo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (AdminCombo.SelectedItem == null)
            {
                MessageBox.Show("Debes seleccionar un administrador responsable.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hInicioStr = HoraInicioTxt.Text.Trim();
            string hFinStr = HoraFinTxt.Text.Trim();

            // Adaptar a HH:mm:ss
            if (hInicioStr.Length == 5) hInicioStr += ":00";
            if (hFinStr.Length == 5) hFinStr += ":00";

            GuardarBtn.Enabled = false;
            GuardarBtn.Text = "Guardando...";

            try
            {
                int idAdminSeleccionado = ((ComboItem)AdminCombo.SelectedItem).Id;

                var bloqueoData = new BloqueoHorario
                {
                    fecha = FechaCalendar.SelectionStart.ToString("yyyy-MM-dd"),
                    horaInicio = hInicioStr,
                    horaFin = hFinStr,
                    motivo = MotivoTxt.Text.Trim(),
                    administrador = new AdministradorSimple { idUsuario = idAdminSeleccionado }
                };

                if (esEdicion && idBloqueo.HasValue)
                {
                    bloqueoData.idBloqueo = idBloqueo.Value;
                    await ApiService.PutAsync<BloqueoHorario>($"api/bloqueos/{idBloqueo.Value}", bloqueoData);
                    MessageBox.Show("Bloqueo actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await ApiService.PostAsync<BloqueoHorario>("api/bloqueos", bloqueoData);
                    MessageBox.Show("Bloqueo creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar bloqueo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                GuardarBtn.Enabled = true;
                GuardarBtn.Text = "💾 Guardar Bloqueo";
            }
        }

        private void CancelarBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estás seguro de que quieres cancelar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private class ComboItem
        {
            public string Texto { get; }
            public int Id { get; }

            public ComboItem(string texto, int id)
            {
                Texto = texto;
                Id = id;
            }

            public override string ToString() => Texto;
        }
    }
}