using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class ValoracionesForm : Form
    {
        private List<Valoracion> listaOriginal = new List<Valoracion>();
        private Dictionary<int, string> dictUsuarios = new Dictionary<int, string>();

        public ValoracionesForm()
        {
            InitializeComponent();

            this.Load += ValoracionesForm_Load;

            ConfigurarDataGrid();
            CargarValoraciones();
        }

        private void ConfigurarDataGrid()
        {
            ValoracionesDataGrid.Columns.Clear();
            ValoracionesDataGrid.Columns.Add("fecha", "Fecha Reseña");
            ValoracionesDataGrid.Columns.Add("cita", "Info de Cita");
            ValoracionesDataGrid.Columns.Add("puntuacion", "Estrellas");
            ValoracionesDataGrid.Columns.Add("comentario", "Comentario");

            ValoracionesDataGrid.Columns.Add("idValoracion", "ID Oculto");
            ValoracionesDataGrid.Columns["idValoracion"].Visible = false;

            // Igualamos estilos de anchos personalizados como en CitasForm
            ValoracionesDataGrid.Columns["cita"].Width = 200;
            ValoracionesDataGrid.Columns["comentario"].Width = 350;
        }

        private async void CargarValoraciones()
        {
            try
            {
                var valoraciones = await ApiService.GetAsync<List<Valoracion>>("api/valoraciones");
                var usuarios = await ApiService.GetAsync<List<Usuario>>("api/usuarios");

                dictUsuarios = usuarios?.ToDictionary(u => u.idUsuario, u => $"{u.nombre} {u.apellidos}") ?? new Dictionary<int, string>();
                listaOriginal = valoraciones ?? new List<Valoracion>();

                ActualizarTabla(listaOriginal);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar valoraciones: {ex.Message}");
            }
        }

        private void ActualizarTabla(List<Valoracion> lista)
        {
            ValoracionesDataGrid.Rows.Clear();
            foreach (var v in lista)
            {
                string estrellas = new string('⭐', v.puntuacion);
                string cliente = (v.cita?.cliente != null && dictUsuarios.ContainsKey(v.cita.cliente.idUsuario))
                                 ? dictUsuarios[v.cita.cliente.idUsuario] : "Desconocido";

                ValoracionesDataGrid.Rows.Add(
                    v.fechaValoracion,
                    $"Cita {v.cita?.idCita} - {cliente}",
                    estrellas,
                    v.comentario,
                    v.idValoracion
                );
            }
        }

        private void FiltroPuntuacionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = FiltroPuntuacionCombo.SelectedIndex;
            if (index == 0) ActualizarTabla(listaOriginal);
            else if (index == 1) ActualizarTabla(listaOriginal.Where(v => v.puntuacion == 5).ToList());
            else if (index == 2) ActualizarTabla(listaOriginal.Where(v => v.puntuacion >= 4).ToList());
            else if (index == 3) ActualizarTabla(listaOriginal.Where(v => v.puntuacion >= 3).ToList());
        }

        private void CrearValoracionBtn_Click(object sender, EventArgs e)
        {
            if (new CrearEditarValoracionForm().ShowDialog() == DialogResult.OK) CargarValoraciones();
        }

        private void EditarBtn_Click(object sender, EventArgs e)
        {
            if (ValoracionesDataGrid.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(ValoracionesDataGrid.SelectedRows[0].Cells["idValoracion"].Value);
                if (new CrearEditarValoracionForm(id).ShowDialog() == DialogResult.OK) CargarValoraciones();
            }
        }

        private async void EliminarBtn_Click(object sender, EventArgs e)
        {
            if (ValoracionesDataGrid.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("¿Eliminar valoración?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(ValoracionesDataGrid.SelectedRows[0].Cells["idValoracion"].Value);
                    await ApiService.DeleteAsync($"api/valoraciones/{id}");
                    CargarValoraciones();
                }
            }
        }

        // Navegación lateral idéntica a CitasForm
        private void IniciBoto_Click(object sender, EventArgs e) { new HomeForm().Show(); this.Hide(); }
        private void ServiciosBoto_Click(object sender, EventArgs e) { new ServiciosForm().Show(); this.Hide(); }
        private void UsuariosBoto_Click(object sender, EventArgs e) { new UsuariosForm().Show(); this.Hide(); }
        private void ClientesBoto_Click(object sender, EventArgs e) { new ClientesForm().Show(); this.Hide(); }
        private void CitasBoto_Click(object sender, EventArgs e) { new CitasForm().Show(); this.Hide(); }
        private void GruposBoto_Click(object sender, EventArgs e) { new GruposForm().Show(); this.Hide(); }
        private void HorarioForm_Click(object sender, EventArgs e) { new HorarioForm().Show(); this.Hide(); }
        private void HorarioSemanalBoto_Click(object sender, EventArgs e) { new HorarioSemanalForm().Show(); this.Hide(); }
        private void MiCuentaBoto_Click(object sender, EventArgs e)
        {
            MiCuentaForm form = new MiCuentaForm();
            form.Show();
            this.Hide();
        }
        private void TancarSessioBoto_Click(object sender, EventArgs e)
        {
            ApiService.ClearAuthToken();
            UserSession.CerrarSesion();
            new LoginForm().Show();
            this.Close();
        }

        private void ValoracionesForm_Load(object sender, EventArgs e)
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