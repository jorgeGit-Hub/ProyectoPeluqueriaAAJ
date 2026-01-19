using System;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class CrearEditarHorarioForm : Form
    {
        private int idServicio;
        private string nombreServicio;
        private int? idHorario = null;
        private bool esEdicion = false;

        // Constructor para CREAR
        public CrearEditarHorarioForm(int idServicio, string nombreServicio)
        {
            InitializeComponent();
            this.idServicio = idServicio;
            this.nombreServicio = nombreServicio;
            this.Text = $"Crear Horario - {nombreServicio}";
            TituloLbl.Text = $"Crear Horario para {nombreServicio}";
            esEdicion = false;
        }

        // Constructor para EDITAR
        public CrearEditarHorarioForm(int idServicio, string nombreServicio, int idHorario)
        {
            InitializeComponent();
            this.idServicio = idServicio;
            this.nombreServicio = nombreServicio;
            this.idHorario = idHorario;
            this.Text = $"Editar Horario - {nombreServicio}";
            TituloLbl.Text = $"Editar Horario de {nombreServicio}";
            esEdicion = true;

            CargarDatosHorario(idHorario);
        }

        private async void CargarDatosHorario(int id)
        {
            try
            {
                var horario = await ApiService.GetAsync<HorarioSemanal>($"api/horarios/{id}");

                // Seleccionar día
                if (!string.IsNullOrEmpty(horario.diaSemana))
                {
                    string diaCapitalizado = char.ToUpper(horario.diaSemana[0]) + horario.diaSemana.Substring(1).ToLower();
                    DiaCombo.SelectedItem = diaCapitalizado;
                }

                // Cargar horas
                HoraInicioTxt.Text = horario.horaInicio;
                HoraFinTxt.Text = horario.horaFin;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del horario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void GuardarBtn_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (DiaCombo.SelectedItem == null)
            {
                MessageBox.Show("Debes seleccionar un día de la semana", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DiaCombo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(HoraInicioTxt.Text))
            {
                MessageBox.Show("La hora de inicio es obligatoria", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                HoraInicioTxt.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(HoraFinTxt.Text))
            {
                MessageBox.Show("La hora de fin es obligatoria", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                HoraFinTxt.Focus();
                return;
            }

            GuardarBtn.Enabled = false;
            GuardarBtn.Text = "Guardando...";

            try
            {
                var horario = new
                {
                    servicio = new { idServicio = idServicio },
                    diaSemana = DiaCombo.SelectedItem.ToString().ToLower(),
                    horaInicio = HoraInicioTxt.Text.Trim(),
                    horaFin = HoraFinTxt.Text.Trim()
                };

                if (esEdicion)
                {
                    await ApiService.PutAsync<object>($"api/horarios/{idHorario.Value}", horario);
                    MessageBox.Show("Horario actualizado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await ApiService.PostAsync<object>("api/horarios", horario);
                    MessageBox.Show("Horario creado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                GuardarBtn.Enabled = true;
                GuardarBtn.Text = "💾 Guardar";
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