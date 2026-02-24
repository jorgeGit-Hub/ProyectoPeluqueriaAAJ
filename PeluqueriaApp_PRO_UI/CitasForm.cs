using System;
using System.Collections.Generic;
using System.Linq; // ✅ IMPORTANTE: Necesario para usar ToDictionary y cruzar datos
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
            this.Load += CitasForm_Load;
            ConfigurarDataGrid();
            CargarCitas();
        }

        private void ConfigurarDataGrid()
        {
            CitasDataGrid.Columns.Clear();
            // ❌ ELIMINADA la columna visible de idCita
            CitasDataGrid.Columns.Add("fecha", "Fecha");
            CitasDataGrid.Columns.Add("horaInicio", "Hora Inicio");
            CitasDataGrid.Columns.Add("horaFin", "Hora Fin");
            CitasDataGrid.Columns.Add("cliente", "Cliente"); // ✅ Ya no dice "(ID)"
            CitasDataGrid.Columns.Add("servicio", "Servicio");
            CitasDataGrid.Columns.Add("estado", "Estado");

            // ✅ NUEVO: Agregamos el ID de la cita pero de forma OCULTA 
            // para que los botones de Editar y Eliminar sigan sabiendo qué cita es.
            CitasDataGrid.Columns.Add("idCita", "ID Oculto");
            CitasDataGrid.Columns["idCita"].Visible = false;

            CitasDataGrid.Columns["cliente"].Width = 150;
            CitasDataGrid.Columns["servicio"].Width = 250;
        }

        private async void CargarCitas()
        {
            try
            {
                // ✅ Obtenemos Citas, Usuarios y Servicios al mismo tiempo
                var citas = await ApiService.GetAsync<List<Cita>>("api/citas");
                var usuarios = await ApiService.GetAsync<List<Usuario>>("api/usuarios");
                var servicios = await ApiService.GetAsync<List<Servicio>>("api/servicios");

                // ✅ Creamos "Diccionarios" para buscar el nombre por ID instantáneamente
                var dictUsuarios = usuarios?.ToDictionary(u => u.idUsuario, u => $"{u.nombre} {u.apellidos}") ?? new Dictionary<int, string>();
                var dictServicios = servicios?.ToDictionary(s => s.idServicio, s => s.nombre) ?? new Dictionary<int, string>();

                CitasDataGrid.Rows.Clear();

                if (citas == null || citas.Count == 0)
                {
                    MessageBox.Show("No hay citas registradas", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var cita in citas)
                {
                    // 1. Obtener Nombre del Cliente cruzando el ID
                    string clienteNombre = "Desconocido";
                    if (cita.cliente != null && dictUsuarios.ContainsKey(cita.cliente.idUsuario))
                    {
                        clienteNombre = dictUsuarios[cita.cliente.idUsuario];
                    }

                    // 2. Obtener Nombre del Servicio cruzando el ID
                    string servicioNombre = "Desconocido";
                    if (cita.horarioSemanal?.servicio != null && dictServicios.ContainsKey(cita.horarioSemanal.servicio.idServicio))
                    {
                        servicioNombre = dictServicios[cita.horarioSemanal.servicio.idServicio];
                    }

                    // 3. Obtener las horas propias de la cita
                    string horaInicio = cita.horaInicio ?? "N/A";
                    string horaFin = cita.horaFin ?? "N/A";

                    CitasDataGrid.Rows.Add(
                        cita.fecha ?? "N/A",
                        horaInicio,
                        horaFin,
                        clienteNombre,
                        servicioNombre,
                        cita.estado ?? "pendiente",
                        cita.idCita // ✅ Se guarda en la columna oculta
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

            // ✅ Sigue funcionando porque saca el ID de la columna oculta
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
                    // ✅ Sigue funcionando porque saca el ID de la columna oculta
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

        private void HorarioForm_Click(object sender, EventArgs e)
        {
            HorarioForm horarioForm = new HorarioForm();
            horarioForm.Show();
            this.Hide();
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

        private void CitasForm_Load(object sender, EventArgs e)
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