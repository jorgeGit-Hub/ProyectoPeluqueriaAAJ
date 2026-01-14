using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class CitasForm : Form
    {
        public CitasForm()
        {
            InitializeComponent();
            ConfigurarDataGrid();
            CargarCitas();
        }

        private void ConfigurarDataGrid()
        {
            CitasDataGrid.Columns.Clear();
            CitasDataGrid.Columns.Add("idCita", "ID");
            CitasDataGrid.Columns.Add("fecha", "Fecha");
            CitasDataGrid.Columns.Add("horaInicio", "Hora Inicio");
            CitasDataGrid.Columns.Add("horaFin", "Hora Fin");
            CitasDataGrid.Columns.Add("cliente", "Cliente (ID)");
            CitasDataGrid.Columns.Add("servicio", "Servicio (ID)");
            CitasDataGrid.Columns.Add("estado", "Estado");

            CitasDataGrid.Columns["idCita"].Width = 50;
            CitasDataGrid.Columns["cliente"].Width = 100;
            CitasDataGrid.Columns["servicio"].Width = 100;
        }

        private async void CargarCitas()
        {
            try
            {
                var citas = await ApiService.GetAsync<List<Cita>>("api/citas");

                CitasDataGrid.Rows.Clear();

                if (citas == null || citas.Count == 0)
                {
                    MessageBox.Show("No hay citas registradas", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var cita in citas)
                {
                    string clienteInfo = cita.cliente != null ? $"ID: {cita.cliente.idUsuario}" : "N/A";
                    string servicioInfo = cita.servicio != null ? $"ID: {cita.servicio.idServicio}" : "N/A";

                    CitasDataGrid.Rows.Add(
                        cita.idCita,
                        cita.fecha ?? "N/A",
                        cita.horaInicio ?? "N/A",
                        cita.horaFin ?? "N/A",
                        clienteInfo,
                        servicioInfo,
                        cita.estado ?? "pendiente"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar citas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearCitaBtn_Click(object sender, EventArgs e)
        {
            CrearEditarCitaForm crearForm = new CrearEditarCitaForm();
            DialogResult result = crearForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                CargarCitas();
            }
        }

        private void EditarBtn_Click(object sender, EventArgs e)
        {
            if (CitasDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona una cita para editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idCita = Convert.ToInt32(CitasDataGrid.SelectedRows[0].Cells["idCita"].Value);

            CrearEditarCitaForm editarForm = new CrearEditarCitaForm(idCita);
            DialogResult result = editarForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                CargarCitas();
            }
        }

        private async void EliminarBtn_Click(object sender, EventArgs e)
        {
            if (CitasDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona una cita para eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que quieres eliminar esta cita?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    int idCita = Convert.ToInt32(CitasDataGrid.SelectedRows[0].Cells["idCita"].Value);

                    bool eliminado = await ApiService.DeleteAsync($"api/citas/{idCita}");

                    if (eliminado)
                    {
                        MessageBox.Show("Cita eliminada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarCitas();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar cita: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FiltroFechaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Implementar filtro por fecha si es necesario
        }

        private void IniciBoto_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm();
            homeForm.Show();
            this.Hide();
        }

        private void ServiciosBoto_Click(object sender, EventArgs e)
        {
            ServiciosForm serviciosForm = new ServiciosForm();
            serviciosForm.Show();
            this.Hide();
        }

        private void UsuariosBoto_Click(object sender, EventArgs e)
        {
            UsuariosForm usuariosForm = new UsuariosForm();
            usuariosForm.Show();
            this.Hide();
        }

        private void ClientesBoto_Click(object sender, EventArgs e)
        {
            ClientesForm clientesForm = new ClientesForm();
            clientesForm.Show();
            this.Hide();
        }

        private void GruposBoto_Click(object sender, EventArgs e)
        {
            GruposForm gruposForm = new GruposForm();
            gruposForm.Show();
            this.Hide();
        }

        private void HorarioSemanalBoto_Click(object sender, EventArgs e)
        {
            HorarioSemanalForm horarioForm = new HorarioSemanalForm();
            horarioForm.Show();
            this.Hide();
        }

        private void MiCuentaBoto_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pantalla de Mi Cuenta en desarrollo", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

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

                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
        }
    }
}