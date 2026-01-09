using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class GruposForm : Form
    {
        public GruposForm()
        {
            InitializeComponent();
            ConfigurarDataGrid();
            CargarGrupos();
        }

        private void ConfigurarDataGrid()
        {
            GruposDataGrid.Columns.Clear();
            GruposDataGrid.Columns.Add("idGrupo", "ID");
            GruposDataGrid.Columns.Add("curso", "Curso");
            GruposDataGrid.Columns.Add("email", "Email");
            GruposDataGrid.Columns.Add("turno", "Turno");

            GruposDataGrid.Columns["idGrupo"].Width = 50;
        }

        private async void CargarGrupos()
        {
            try
            {
                var grupos = await ApiService.GetAsync<List<Grupo>>("api/grupos");

                GruposDataGrid.Rows.Clear();

                if (grupos == null || grupos.Count == 0)
                {
                    MessageBox.Show("No hay grupos registrados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var grupo in grupos)
                {
                    GruposDataGrid.Rows.Add(
                        grupo.idGrupo,
                        grupo.curso ?? "N/A",
                        grupo.email ?? "N/A",
                        grupo.turno ?? "N/A"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar grupos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BuscarBtn_Click(object sender, EventArgs e)
        {
            string textoBusqueda = BuscarGruposTxt.Text.Trim();

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                CargarGrupos();
                return;
            }

            try
            {
                var grupos = await ApiService.GetAsync<List<Grupo>>("api/grupos");

                GruposDataGrid.Rows.Clear();

                if (grupos == null || grupos.Count == 0)
                {
                    MessageBox.Show("No se encontraron grupos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var grupo in grupos)
                {
                    bool coincide = false;

                    if (grupo.curso?.ToLower().Contains(textoBusqueda.ToLower()) == true ||
                        grupo.email?.ToLower().Contains(textoBusqueda.ToLower()) == true ||
                        grupo.turno?.ToLower().Contains(textoBusqueda.ToLower()) == true)
                    {
                        coincide = true;
                    }

                    if (coincide)
                    {
                        GruposDataGrid.Rows.Add(
                            grupo.idGrupo,
                            grupo.curso ?? "N/A",
                            grupo.email ?? "N/A",
                            grupo.turno ?? "N/A"
                        );
                    }
                }

                if (GruposDataGrid.Rows.Count == 0)
                {
                    MessageBox.Show($"No se encontraron grupos con '{textoBusqueda}'", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar grupos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearGrupoBtn_Click(object sender, EventArgs e)
        {
            CrearEditarGrupoForm crearForm = new CrearEditarGrupoForm();
            DialogResult result = crearForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                CargarGrupos();
            }
        }

        private void EditarBtn_Click(object sender, EventArgs e)
        {
            if (GruposDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un grupo para editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idGrupo = Convert.ToInt32(GruposDataGrid.SelectedRows[0].Cells["idGrupo"].Value);

            CrearEditarGrupoForm editarForm = new CrearEditarGrupoForm(idGrupo);
            DialogResult result = editarForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                CargarGrupos();
            }
        }

        private async void EliminarBtn_Click(object sender, EventArgs e)
        {
            if (GruposDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un grupo para eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que quieres eliminar este grupo?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    int idGrupo = Convert.ToInt32(GruposDataGrid.SelectedRows[0].Cells["idGrupo"].Value);

                    bool eliminado = await ApiService.DeleteAsync($"api/grupos/{idGrupo}");

                    if (eliminado)
                    {
                        MessageBox.Show("Grupo eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarGrupos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar grupo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
        }
    }
}