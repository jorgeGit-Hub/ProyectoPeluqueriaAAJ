using System;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class CrearEditarCitaForm : Form
    {
        private int? idCita = null;
        private bool esEdicion = false;

        // Constructor para CREAR nueva cita
        public CrearEditarCitaForm()
        {
            InitializeComponent();
            this.Text = "Crear Nueva Cita";
            TituloLbl.Text = "Crear Nueva Cita";
            esEdicion = false;

            // Inicializar fecha a hoy
            FechaCalendar.SetDate(DateTime.Now);
        }

        // Constructor para EDITAR cita existente
        public CrearEditarCitaForm(int id)
        {
            InitializeComponent();
            this.Text = "Editar Cita";
            TituloLbl.Text = "Editar Cita";
            idCita = id;
            esEdicion = true;

            CargarDatosCita(id);
        }

        private async void GuardarBtn_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (ClienteIdTxt.Text == "" || !int.TryParse(ClienteIdTxt.Text, out int idCliente))
            {
                MessageBox.Show("El ID del cliente es obligatorio y debe ser un número", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClienteIdTxt.Focus();
                return;
            }

            if (ServicioIdTxt.Text == "" || !int.TryParse(ServicioIdTxt.Text, out int idServicio))
            {
                MessageBox.Show("El ID del servicio es obligatorio y debe ser un número", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ServicioIdTxt.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(HoraInicioTxt.Text) || string.IsNullOrWhiteSpace(HoraFinTxt.Text))
            {
                MessageBox.Show("La hora de inicio y fin son obligatorias", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (EstadoCombo.SelectedItem == null)
            {
                MessageBox.Show("Debes seleccionar un estado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EstadoCombo.Focus();
                return;
            }

            GuardarBtn.Enabled = false;
            GuardarBtn.Text = "Guardando...";

            try
            {
                var cita = new Cita
                {
                    fecha = FechaCalendar.SelectionStart.ToString("yyyy-MM-dd"),
                    horaInicio = HoraInicioTxt.Text.Trim(),
                    horaFin = HoraFinTxt.Text.Trim(),
                    estado = EstadoCombo.SelectedItem.ToString().ToLower(),
                    cliente = new ClienteSimple { idUsuario = idCliente },
                    servicio = new ServicioSimple { idServicio = idServicio }
                };

                // Si hay grupo, añadirlo
                if (!string.IsNullOrWhiteSpace(GrupoIdTxt.Text) && int.TryParse(GrupoIdTxt.Text, out int idGrupo))
                {
                    cita.grupo = new GrupoSimple { idGrupo = idGrupo };
                }

                if (esEdicion)
                {
                    cita.idCita = idCita.Value;
                    await ApiService.PutAsync<Cita>($"api/citas/{idCita.Value}", cita);
                    MessageBox.Show("Cita actualizada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await ApiService.PostAsync<Cita>("api/citas", cita);
                    MessageBox.Show("Cita creada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void CargarDatosCita(int id)
        {
            try
            {
                var cita = await ApiService.GetAsync<Cita>($"api/citas/{id}");

                // Cargar fecha en calendario
                if (!string.IsNullOrEmpty(cita.fecha))
                {
                    FechaCalendar.SetDate(DateTime.Parse(cita.fecha));
                }

                // Cargar datos
                ClienteIdTxt.Text = cita.cliente?.idUsuario.ToString() ?? "";
                ServicioIdTxt.Text = cita.servicio?.idServicio.ToString() ?? "";
                GrupoIdTxt.Text = cita.grupo?.idGrupo.ToString() ?? "";
                HoraInicioTxt.Text = cita.horaInicio;
                HoraFinTxt.Text = cita.horaFin;

                // Seleccionar estado
                if (!string.IsNullOrEmpty(cita.estado))
                {
                    string estadoCapitalizado = char.ToUpper(cita.estado[0]) + cita.estado.Substring(1).ToLower();
                    EstadoCombo.SelectedItem = estadoCapitalizado;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos de la cita: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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