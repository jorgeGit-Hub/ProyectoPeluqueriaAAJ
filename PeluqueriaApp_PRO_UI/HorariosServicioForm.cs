using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class HorariosServicioForm : Form
    {
        private int idServicio;
        private string nombreServicio;

        public HorariosServicioForm(int idServicio, string nombreServicio)
        {
            InitializeComponent();
            this.idServicio = idServicio;
            this.nombreServicio = nombreServicio;
            this.Text = $"Horarios de {nombreServicio}";
            TituloLbl.Text = $"Horarios de {nombreServicio}";
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

            HorariosDataGrid.Columns["idHorario"].Width = 50;
            HorariosDataGrid.Columns["diaSemana"].Width = 150;
        }

        private async void CargarHorarios()
        {
            try
            {
                var horarios = await ApiService.GetAsync<List<HorarioSemanal>>($"api/horarios/servicio/{idServicio}");

                HorariosDataGrid.Rows.Clear();

                if (horarios == null || horarios.Count == 0)
                {
                    MessageBox.Show("Este servicio no tiene horarios configurados", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var horario in horarios)
                {
                    HorariosDataGrid.Rows.Add(
                        horario.idHorario,
                        CapitalizarDia(horario.diaSemana),
                        horario.horaInicio ?? "N/A",
                        horario.horaFin ?? "N/A"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar horarios: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CapitalizarDia(string dia)
        {
            if (string.IsNullOrEmpty(dia)) return "N/A";
            return char.ToUpper(dia[0]) + dia.Substring(1).ToLower();
        }

        private void CrearHorarioBtn_Click(object sender, EventArgs e)
        {
            CrearEditarHorarioForm crearForm = new CrearEditarHorarioForm(idServicio, nombreServicio);
            DialogResult result = crearForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                CargarHorarios();
            }
        }

        private void EditarBtn_Click(object sender, EventArgs e)
        {
            if (HorariosDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un horario para editar", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idHorario = Convert.ToInt32(HorariosDataGrid.SelectedRows[0].Cells["idHorario"].Value);

            CrearEditarHorarioForm editarForm = new CrearEditarHorarioForm(idServicio, nombreServicio, idHorario);
            DialogResult result = editarForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                CargarHorarios();
            }
        }

        private async void EliminarBtn_Click(object sender, EventArgs e)
        {
            if (HorariosDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un horario para eliminar", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que quieres eliminar este horario?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
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

        private void CerrarBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}