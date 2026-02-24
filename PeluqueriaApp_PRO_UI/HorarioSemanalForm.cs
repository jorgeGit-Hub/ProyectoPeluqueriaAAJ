using System;
using System.Collections.Generic;
using System.Linq; // ✅ IMPORTANTÍSIMO: Necesario para los Diccionarios
using System.Windows.Forms;
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

            // ❌ ELIMINADA la columna visible de idBloqueo
            BloqueosDataGrid.Columns.Add("fecha", "Fecha");
            BloqueosDataGrid.Columns.Add("horaInicio", "Hora Inicio");
            BloqueosDataGrid.Columns.Add("horaFin", "Hora Fin");
            BloqueosDataGrid.Columns.Add("motivo", "Motivo");
            BloqueosDataGrid.Columns.Add("administrador", "Administrador"); // ✅ Mostrará el nombre

            // ✅ NUEVO: Columna oculta para guardar el ID del bloqueo
            BloqueosDataGrid.Columns.Add("idBloqueo", "ID Oculto");
            BloqueosDataGrid.Columns["idBloqueo"].Visible = false;

            BloqueosDataGrid.Columns["motivo"].Width = 200;
            BloqueosDataGrid.Columns["administrador"].Width = 180;
        }

        private async void CargarBloqueos()
        {
            try
            {
                // ✅ Descargamos Bloqueos y Usuarios al mismo tiempo
                var bloqueos = await ApiService.GetAsync<List<BloqueoHorario>>("api/bloqueos");
                var usuarios = await ApiService.GetAsync<List<Usuario>>("api/usuarios");

                // ✅ Creamos el diccionario para buscar el nombre del administrador rapidísimo
                var dictUsuarios = usuarios?.ToDictionary(u => u.idUsuario, u => $"{u.nombre} {u.apellidos}") ?? new Dictionary<int, string>();

                BloqueosDataGrid.Rows.Clear();

                if (bloqueos == null || bloqueos.Count == 0)
                {
                    MessageBox.Show("No hay bloqueos registrados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var b in bloqueos)
                {
                    // Buscar el nombre cruzando el ID
                    string adminNombre = "Desconocido";
                    if (b.administrador != null && dictUsuarios.ContainsKey(b.administrador.idUsuario))
                    {
                        adminNombre = dictUsuarios[b.administrador.idUsuario];
                    }

                    BloqueosDataGrid.Rows.Add(
                        b.fecha ?? "N/A",
                        b.horaInicio ?? "N/A",
                        b.horaFin ?? "N/A",
                        b.motivo ?? "N/A",
                        adminNombre,
                        b.idBloqueo // ✅ Guardamos el ID en la celda oculta
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar bloqueos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- BOTONES DE ACCIÓN ---

        /* NOTA: Asegúrate de tener un form llamado CrearEditarBloqueoForm si vas a usar esta lógica. 
           Si el nombre de tu formulario es distinto, cámbialo aquí abajo. */

        private void CrearBloqueoBtn_Click(object sender, EventArgs e)
        {
             CrearEditarBloqueoForm crearForm = new CrearEditarBloqueoForm();
             if (crearForm.ShowDialog() == DialogResult.OK) CargarBloqueos();
        }

        private void EditarBtn_Click(object sender, EventArgs e)
        {
            if (BloqueosDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un bloqueo para editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Usar el ID de la columna oculta
            int idBloqueo = Convert.ToInt32(BloqueosDataGrid.SelectedRows[0].Cells["idBloqueo"].Value);

             CrearEditarBloqueoForm editarForm = new CrearEditarBloqueoForm(idBloqueo);
             if (editarForm.ShowDialog() == DialogResult.OK) CargarBloqueos();
        }

        private async void EliminarBtn_Click(object sender, EventArgs e)
        {
            if (BloqueosDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un bloqueo para eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Estás seguro de que quieres eliminar este bloqueo?", "Confirmar Eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    // ✅ Usar el ID de la columna oculta
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

        // --- NAVEGACIÓN LATERAL ---
        private void IniciBoto_Click(object sender, EventArgs e) { new HomeForm().Show(); this.Hide(); }
        private void ServiciosBoto_Click(object sender, EventArgs e) { new ServiciosForm().Show(); this.Hide(); }
        private void UsuariosBoto_Click(object sender, EventArgs e) { new UsuariosForm().Show(); this.Hide(); }
        private void ClientesBoto_Click(object sender, EventArgs e) { new ClientesForm().Show(); this.Hide(); }
        private void CitasBoto_Click(object sender, EventArgs e) { new CitasForm().Show(); this.Hide(); }
        private void GruposBoto_Click(object sender, EventArgs e) { new GruposForm().Show(); this.Hide(); }
        private void HorarioForm_Click(object sender, EventArgs e) { new HorarioForm().Show(); this.Hide(); }

        private void HorarioSemanalBoto_Click(object sender, EventArgs e)
        {
            // Ya estamos en esta pantalla
        }

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
    }
}