using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class ServiciosForm : Form
    {
        private List<Servicio> servicios = new List<Servicio>();

        public ServiciosForm()
        {
            InitializeComponent();
            ConfigurarDataGrid();
            CargarServicios();
        }

        private void ConfigurarDataGrid()
        {
            ServiciosDataGrid.Columns.Clear();
            ServiciosDataGrid.Columns.Add("idServicio", "ID");
            ServiciosDataGrid.Columns.Add("nombre", "Nombre");
            ServiciosDataGrid.Columns.Add("modulo", "Módulo");
            ServiciosDataGrid.Columns.Add("aula", "Aula");
            ServiciosDataGrid.Columns.Add("tiempoCliente", "Tiempo");
            ServiciosDataGrid.Columns.Add("precio", "Precio (€)");
            // ✅ ELIMINADA columna "grupo" (la relación grupo ahora está en HorarioSemanal)

            ServiciosDataGrid.Columns["idServicio"].Width = 50;
            ServiciosDataGrid.Columns["aula"].Width = 80;
            ServiciosDataGrid.Columns["tiempoCliente"].Width = 80;
            ServiciosDataGrid.Columns["precio"].Width = 80;
        }

        private async void CargarServicios()
        {
            try
            {
                ServiciosDataGrid.Rows.Clear();

                servicios = await ApiService.GetAsync<List<Servicio>>("api/servicios");

                if (servicios == null || servicios.Count == 0)
                {
                    MessageBox.Show("No hay servicios disponibles", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var servicio in servicios)
                {
                    // ✅ ACTUALIZADO: Sin grupoInfo, Servicio ya no tiene grupo
                    ServiciosDataGrid.Rows.Add(
                        servicio.idServicio,
                        servicio.nombre ?? "",
                        servicio.modulo ?? "N/A",
                        servicio.aula ?? "N/A",
                        servicio.tiempoCliente ?? "N/A",
                        servicio.precio.ToString("F2")
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar servicios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BuscarBtn_Click(object sender, EventArgs e)
        {
            string textoBusqueda = BuscarServiciosTxt.Text.Trim();

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                CargarServicios();
                return;
            }

            try
            {
                servicios = await ApiService.GetAsync<List<Servicio>>($"api/servicios/buscar/{textoBusqueda}");

                ServiciosDataGrid.Rows.Clear();

                if (servicios == null || servicios.Count == 0)
                {
                    MessageBox.Show($"No se encontraron servicios que contengan '{textoBusqueda}'", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var servicio in servicios)
                {
                    ServiciosDataGrid.Rows.Add(
                        servicio.idServicio,
                        servicio.nombre ?? "",
                        servicio.modulo ?? "N/A",
                        servicio.aula ?? "N/A",
                        servicio.tiempoCliente ?? "N/A",
                        servicio.precio.ToString("F2")
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar servicios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearServicioBtn_Click(object sender, EventArgs e)
        {
            CrearEditarServicioForm crearForm = new CrearEditarServicioForm();
            DialogResult result = crearForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                CargarServicios();
            }
        }

        private void EditarBtn_Click(object sender, EventArgs e)
        {
            if (ServiciosDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un servicio para editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idServicio = Convert.ToInt32(ServiciosDataGrid.SelectedRows[0].Cells["idServicio"].Value);

            CrearEditarServicioForm editarForm = new CrearEditarServicioForm(idServicio);
            DialogResult result = editarForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                CargarServicios();
            }
        }

        private async void EliminarBtn_Click(object sender, EventArgs e)
        {
            if (ServiciosDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un servicio para eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que quieres eliminar este servicio?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    int idServicio = Convert.ToInt32(ServiciosDataGrid.SelectedRows[0].Cells["idServicio"].Value);

                    bool eliminado = await ApiService.DeleteAsync($"api/servicios/{idServicio}");

                    if (eliminado)
                    {
                        MessageBox.Show("Servicio eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarServicios();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar servicio: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void VerHorariosBtn_Click(object sender, EventArgs e)
        {
            if (ServiciosDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un servicio para ver sus horarios", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idServicio = Convert.ToInt32(ServiciosDataGrid.SelectedRows[0].Cells["idServicio"].Value);
            string nombreServicio = ServiciosDataGrid.SelectedRows[0].Cells["nombre"].Value.ToString();

            HorariosServicioForm horariosForm = new HorariosServicioForm(idServicio, nombreServicio);
            horariosForm.ShowDialog();
        }

        private void IniciBoto_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm();
            homeForm.Show();
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

        private void HorarioSemanalBoto_Click(object sender, EventArgs e)
        {
            HorarioSemanalForm horarioForm = new HorarioSemanalForm();
            horarioForm.Show();
            this.Hide();
        }

        private void HorarioForm_Click(object sender, EventArgs e)
        {
            HorarioForm horarioForm = new HorarioForm();
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