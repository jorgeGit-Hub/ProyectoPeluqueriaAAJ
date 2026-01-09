using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class HorarioSemanalForm : Form
    {
        public HorarioSemanalForm()
        {
            InitializeComponent();
            ConfigurarDataGrid();
            CargarBloqueos();
        }

        private void ConfigurarDataGrid()
        {
            BloqueosDataGrid.Columns.Clear();
            BloqueosDataGrid.Columns.Add("idBloqueo", "ID");
            BloqueosDataGrid.Columns.Add("fecha", "Fecha");
            BloqueosDataGrid.Columns.Add("horaInicio", "Hora Inicio");
            BloqueosDataGrid.Columns.Add("horaFin", "Hora Fin");
            BloqueosDataGrid.Columns.Add("motivo", "Motivo");
            BloqueosDataGrid.Columns.Add("administrador", "Administrador");

            BloqueosDataGrid.Columns["idBloqueo"].Width = 50;
        }

        private async void CargarBloqueos()
        {
            try
            {
                var bloqueos = await ApiService.GetAsync<List<BloqueoHorario>>("api/bloqueos");

                BloqueosDataGrid.Rows.Clear();

                if (bloqueos == null || bloqueos.Count == 0)
                {
                    MessageBox.Show("No hay bloqueos horarios registrados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var bloqueo in bloqueos)
                {
                    string adminNombre = bloqueo.administrador?.idUsuario.ToString() ?? "N/A";

                    BloqueosDataGrid.Rows.Add(
                        bloqueo.idBloqueo,
                        bloqueo.fecha,
                        bloqueo.horaInicio,
                        bloqueo.horaFin,
                        bloqueo.motivo ?? "N/A",
                        adminNombre
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar bloqueos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*private void CrearBloqueoBtn_Click(object sender, EventArgs e)
        {
            CrearEditarBloqueoForm crearForm = new CrearEditarBloqueoForm();
            DialogResult result = crearForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                CargarBloqueos();
            }
        }

        private void EditarBtn_Click(object sender, EventArgs e)
        {
            if (BloqueosDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un bloqueo para editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idBloqueo = Convert.ToInt32(BloqueosDataGrid.SelectedRows[0].Cells["idBloqueo"].Value);

            CrearEditarBloqueoForm editarForm = new CrearEditarBloqueoForm(idBloqueo);
            DialogResult result = editarForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                CargarBloqueos();
            }
        }*/

        private async void EliminarBtn_Click(object sender, EventArgs e)
        {
            if (BloqueosDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un bloqueo para eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que quieres eliminar este bloqueo?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    int idBloqueo = Convert.ToInt32(BloqueosDataGrid.SelectedRows[0].Cells["idBloqueo"].Value);

                    bool eliminado = await ApiService.DeleteAsync($"api/bloqueos/{idBloqueo}");

                    if (eliminado)
                    {
                        MessageBox.Show("Bloqueo eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarBloqueos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar bloqueo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        private void CitasBoto_Click(object sender, EventArgs e)
        {
            CitasForm citasForm = new CitasForm();
            citasForm.Show();
            this.Hide();
        }

        private void GruposBoto_Click(object sender, EventArgs e)
        {
            GruposForm gruposForm = new GruposForm();
            gruposForm.Show();
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
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
        }
    }
}