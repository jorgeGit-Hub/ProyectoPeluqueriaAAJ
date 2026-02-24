using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class GruposForm : Form
    {
        private List<Grupo> listaGruposOriginal = new List<Grupo>();

        public GruposForm()
        {
            InitializeComponent();
            this.Load += GruposForm_Load;
            ConfigurarDataGrid();
            CargarGrupos();
        }

        private void ConfigurarDataGrid()
        {
            GruposDataGrid.Columns.Clear();
            GruposDataGrid.Columns.Add("idGrupo", "ID");
            GruposDataGrid.Columns.Add("curso", "Curso");
            GruposDataGrid.Columns.Add("turno", "Turno");
            GruposDataGrid.Columns.Add("cantAlumnos", "Nº Alumnos");

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
                return;

            foreach (var grupo in grupos)
            {
                string turnoDisplay = !string.IsNullOrEmpty(grupo.turno)
                    ? char.ToUpper(grupo.turno[0]) + grupo.turno.Substring(1).ToLower()
                    : "N/A";

                GruposDataGrid.Rows.Add(
                    grupo.idGrupo,
                    grupo.curso ?? "",
                    turnoDisplay,
                    grupo.cantAlumnos?.ToString() ?? "0"
                );
            }
        }

        private void BuscarBtn_Click(object sender, EventArgs e)
        {
            string textoBusqueda = BuscarGruposTxt.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                MostrarGruposEnGrid(listaGruposOriginal);
                return;
            }

            var gruposFiltrados = listaGruposOriginal.Where(g =>
                (g.curso != null && g.curso.ToLower().Contains(textoBusqueda)) ||
                (g.turno != null && g.turno.ToLower().Contains(textoBusqueda))
            ).ToList();

            MostrarGruposEnGrid(gruposFiltrados);

            if (gruposFiltrados.Count == 0)
                MessageBox.Show($"No se encontraron grupos con '{textoBusqueda}'",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CrearGrupoBtn_Click(object sender, EventArgs e)
        {
            CrearEditarGrupoForm crearForm = new CrearEditarGrupoForm();
            if (crearForm.ShowDialog() == DialogResult.OK)
                CargarGrupos();
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
            if (editarForm.ShowDialog() == DialogResult.OK)
                CargarGrupos();
        }

        private async void EliminarBtn_Click(object sender, EventArgs e)
        {
            if (GruposDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un grupo para eliminar",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Estás seguro de que quieres eliminar este grupo?\nEsto podría afectar a los servicios asociados.",
                "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
        private void IniciBoto_Click(object sender, EventArgs e) { new HomeForm().Show(); this.Hide(); }
        private void ServiciosBoto_Click(object sender, EventArgs e) { new ServiciosForm().Show(); this.Hide(); }
        private void UsuariosBoto_Click(object sender, EventArgs e) { new UsuariosForm().Show(); this.Hide(); }
        private void ClientesBoto_Click(object sender, EventArgs e) { new ClientesForm().Show(); this.Hide(); }
        private void CitasBoto_Click(object sender, EventArgs e) { new CitasForm().Show(); this.Hide(); }
        private void HorarioForm_Click(object sender, EventArgs e) { new HorarioForm().Show(); this.Hide(); }
        private void HorarioSemanalBoto_Click(object sender, EventArgs e) { new HorarioSemanalForm().Show(); this.Hide(); }

        private void ValoracionForm_Click(object sender, EventArgs e)
        {
            ValoracionesForm valoracionform = new ValoracionesForm();
            valoracionform.Show();
            this.Hide();
        }
        private void MiCuentaBoto_Click(object sender, EventArgs e)
        {
            MiCuentaForm form = new MiCuentaForm();
            form.Show();
            this.Hide();
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

        private void GruposForm_Load(object sender, EventArgs e)
        {
            // Cargar el logo desde la memoria global de la app
            if (UserSession.LogoApp != null)
            {
                pbLogo.Image = UserSession.LogoApp;
            }

            // (Aquí debajo dejas el resto de código que ya tuvieras en este Load, si había algo)
        }
    }
}