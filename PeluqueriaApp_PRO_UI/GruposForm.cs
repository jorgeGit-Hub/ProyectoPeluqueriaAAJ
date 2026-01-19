using System;
using System.Collections.Generic;
using System.Linq; // Necesario para la búsqueda local
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class GruposForm : Form
    {
        // Lista para mantener los grupos en memoria y poder filtrar localmente
        private List<Grupo> listaGruposOriginal = new List<Grupo>();

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
            // Nuevo campo sincronizado con el Backend
            GruposDataGrid.Columns.Add("cantAlumnos", "Nº Alumnos");

            // Ajuste de anchos
            GruposDataGrid.Columns["idGrupo"].Width = 50;
            GruposDataGrid.Columns["cantAlumnos"].Width = 100;
            GruposDataGrid.Columns["turno"].Width = 100;
            GruposDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private async void CargarGrupos()
        {
            try
            {
                var grupos = await ApiService.GetAsync<List<Grupo>>("api/grupos");

                // Guardamos la lista original para las búsquedas
                listaGruposOriginal = grupos ?? new List<Grupo>();

                MostrarGruposEnGrid(listaGruposOriginal);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar grupos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarGruposEnGrid(List<Grupo> grupos)
        {
            GruposDataGrid.Rows.Clear();

            if (grupos == null || grupos.Count == 0)
            {
                return;
            }

            foreach (var grupo in grupos)
            {
                // Capitalizar turno si existe
                string turnoDisplay = !string.IsNullOrEmpty(grupo.turno)
                    ? char.ToUpper(grupo.turno[0]) + grupo.turno.Substring(1).ToLower()
                    : "N/A";

                GruposDataGrid.Rows.Add(
                    grupo.idGrupo,
                    grupo.curso ?? "",
                    grupo.email ?? "",
                    turnoDisplay,
                    grupo.cantAlumnos?.ToString() ?? "0" // Mostrar 0 si es null
                );
            }
        }

        // --- FUNCIONALIDADES CRUD (Que faltaban) ---

        private void BuscarBtn_Click(object sender, EventArgs e)
        {
            string textoBusqueda = BuscarGruposTxt.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                MostrarGruposEnGrid(listaGruposOriginal);
                return;
            }

            // Filtrado local (ya que el backend no tiene endpoint de búsqueda por texto general)
            var gruposFiltrados = listaGruposOriginal.Where(g =>
                (g.curso != null && g.curso.ToLower().Contains(textoBusqueda)) ||
                (g.email != null && g.email.ToLower().Contains(textoBusqueda)) ||
                (g.turno != null && g.turno.ToLower().Contains(textoBusqueda))
            ).ToList();

            MostrarGruposEnGrid(gruposFiltrados);

            if (gruposFiltrados.Count == 0)
            {
                MessageBox.Show($"No se encontraron grupos con '{textoBusqueda}'",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Por favor, selecciona un grupo para editar",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Por favor, selecciona un grupo para eliminar",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmación de seguridad
            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que quieres eliminar este grupo?\nEsto podría afectar a los servicios asociados.",
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
                        MessageBox.Show("Grupo eliminado correctamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarGrupos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar grupo: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- NAVEGACIÓN ---

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
            MessageBox.Show("Pantalla de Mi Cuenta en desarrollo", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
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