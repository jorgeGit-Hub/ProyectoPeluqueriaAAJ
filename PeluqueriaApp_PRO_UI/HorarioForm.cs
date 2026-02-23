using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class HorarioForm : Form
    {
        public HorarioForm()
        {
            InitializeComponent();
            ConfigurarDataGrid();
            CargarHorarios();
        }

        private void ConfigurarDataGrid()
        {
            HorariosDataGrid.Columns.Clear();
            HorariosDataGrid.Columns.Add("idHorario", "ID");
            HorariosDataGrid.Columns.Add("diaSemana", "Día");
            HorariosDataGrid.Columns.Add("horaInicio", "Hora Inicio");
            HorariosDataGrid.Columns.Add("horaFin", "Hora Fin");
            HorariosDataGrid.Columns.Add("servicio", "Servicio (ID)");
            HorariosDataGrid.Columns.Add("grupo", "Grupo (ID)");

            HorariosDataGrid.Columns["idHorario"].Width = 50;
        }

        private async void CargarHorarios()
        {
            try
            {
                var horarios = await ApiService.GetAsync<List<HorarioSemanal>>("api/horarios");

                HorariosDataGrid.Rows.Clear();

                if (horarios == null || horarios.Count == 0)
                {
                    MessageBox.Show("No hay horarios registrados", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var horario in horarios)
                {
                    string dia = horario.diaSemana ?? "N/A";
                    if (dia != "N/A")
                        dia = char.ToUpper(dia[0]) + dia.Substring(1).ToLower();

                    string servicioInfo = horario.servicio != null
                        ? $"ID: {horario.servicio.idServicio}"
                        : "N/A";

                    string grupoInfo = horario.grupo != null
                        ? $"ID: {horario.grupo.idGrupo}"
                        : "N/A";

                    HorariosDataGrid.Rows.Add(
                        horario.idHorario,
                        dia,
                        horario.horaInicio ?? "N/A",
                        horario.horaFin ?? "N/A",
                        servicioInfo,
                        grupoInfo
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar horarios: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearHorarioBtn_Click(object sender, EventArgs e)
        {
            CrearEditarHorarioNuevoForm crearForm = new CrearEditarHorarioNuevoForm();
            if (crearForm.ShowDialog() == DialogResult.OK)
                CargarHorarios();
        }

        private void EditarBtn_Click(object sender, EventArgs e)
        {
            if (HorariosDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un horario para editar",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idHorario = Convert.ToInt32(HorariosDataGrid.SelectedRows[0].Cells["idHorario"].Value);
            CrearEditarHorarioNuevoForm editarForm = new CrearEditarHorarioNuevoForm(idHorario);
            if (editarForm.ShowDialog() == DialogResult.OK)
                CargarHorarios();
        }

        private async void EliminarBtn_Click(object sender, EventArgs e)
        {
            if (HorariosDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un horario para eliminar",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Estás seguro de que quieres eliminar este horario?",
                "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    int idHorario = Convert.ToInt32(HorariosDataGrid.SelectedRows[0].Cells["idHorario"].Value);
                    bool eliminado = await ApiService.DeleteAsync($"api/horarios/{idHorario}");

                    if (eliminado)
                    {
                        MessageBox.Show("Horario eliminado correctamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarHorarios();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar horario: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void IniciBoto_Click(object sender, EventArgs e) { new HomeForm().Show(); this.Hide(); }
        private void ServiciosBoto_Click(object sender, EventArgs e) { new ServiciosForm().Show(); this.Hide(); }
        private void UsuariosBoto_Click(object sender, EventArgs e) { new UsuariosForm().Show(); this.Hide(); }
        private void ClientesBoto_Click(object sender, EventArgs e) { new ClientesForm().Show(); this.Hide(); }
        private void CitasBoto_Click(object sender, EventArgs e) { new CitasForm().Show(); this.Hide(); }
        private void GruposBoto_Click(object sender, EventArgs e) { new GruposForm().Show(); this.Hide(); }
        private void HorarioSemanalBoto_Click(object sender, EventArgs e) { new HorarioSemanalForm().Show(); this.Hide(); }

        private void HorarioForm_Click(object sender, EventArgs e)
        {
            HorarioForm horarioForm = new HorarioForm();
            horarioForm.Show();
            this.Hide();
        }
        private void MiCuentaBoto_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pantalla de Mi Cuenta en desarrollo", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TancarSessioBoto_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estás seguro de que quieres cerrar sesión?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ApiService.ClearAuthToken();
                UserSession.CerrarSesion();
                new LoginForm().Show();
                this.Close();
            }
        }
    }
}