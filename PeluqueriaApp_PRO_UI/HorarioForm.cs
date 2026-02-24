using System;
using System.Collections.Generic;
using System.Linq; // ✅ Necesario para cruzar datos con diccionarios
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class HorarioForm : Form
    {
        public HorarioForm()
        {
            InitializeComponent();
            this.Load += HorarioForm_Load;
            ConfigurarDataGrid();
            CargarHorarios();
        }

        private void ConfigurarDataGrid()
        {
            HorariosDataGrid.Columns.Clear();
            // ❌ ELIMINADA la columna visible de idHorario
            HorariosDataGrid.Columns.Add("diaSemana", "Día");
            HorariosDataGrid.Columns.Add("horaInicio", "Hora Inicio");
            HorariosDataGrid.Columns.Add("horaFin", "Hora Fin");
            HorariosDataGrid.Columns.Add("servicio", "Servicio"); // ✅ Ya no dice "(ID)"
            HorariosDataGrid.Columns.Add("grupo", "Grupo");       // ✅ Ya no dice "(ID)"

            // ✅ NUEVO: Columna oculta para guardar el ID del horario (necesaria para editar/eliminar)
            HorariosDataGrid.Columns.Add("idHorario", "ID Oculto");
            HorariosDataGrid.Columns["idHorario"].Visible = false;

            HorariosDataGrid.Columns["servicio"].Width = 200;
            HorariosDataGrid.Columns["grupo"].Width = 200;
        }

        private async void CargarHorarios()
        {
            try
            {
                // ✅ Obtenemos Horarios, Servicios y Grupos al mismo tiempo
                var horarios = await ApiService.GetAsync<List<HorarioSemanal>>("api/horarios");
                var servicios = await ApiService.GetAsync<List<Servicio>>("api/servicios");
                var grupos = await ApiService.GetAsync<List<Grupo>>("api/grupos");

                // ✅ Creamos diccionarios para buscar los nombres por ID instantáneamente
                var dictServicios = servicios?.ToDictionary(s => s.idServicio, s => s.nombre) ?? new Dictionary<int, string>();

                // Para el grupo, uniremos el Curso y el Turno para que quede más claro
                var dictGrupos = grupos?.ToDictionary(g => g.idGrupo, g => $"{g.curso} ({g.turno})") ?? new Dictionary<int, string>();

                HorariosDataGrid.Rows.Clear();

                if (horarios == null || horarios.Count == 0)
                {
                    MessageBox.Show("No hay horarios registrados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var h in horarios)
                {
                    // 1. Obtener Nombre del Servicio
                    string nombreServicio = "Desconocido";
                    if (h.servicio != null && dictServicios.ContainsKey(h.servicio.idServicio))
                    {
                        nombreServicio = dictServicios[h.servicio.idServicio];
                    }

                    // 2. Obtener Nombre del Grupo (Curso + Turno)
                    string nombreGrupo = "Desconocido";
                    if (h.grupo != null && dictGrupos.ContainsKey(h.grupo.idGrupo))
                    {
                        nombreGrupo = dictGrupos[h.grupo.idGrupo];
                    }

                    HorariosDataGrid.Rows.Add(
                        h.diaSemana ?? "N/A",
                        h.horaInicio ?? "N/A",
                        h.horaFin ?? "N/A",
                        nombreServicio,
                        nombreGrupo,
                        h.idHorario // ✅ Se guarda el ID en la columna oculta
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar horarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearHorarioBtn_Click(object sender, EventArgs e)
        {
            // Nota: Este formulario usaba un constructor que requería un idServicio y nombre.
            // Si quieres crearlo de manera general, asegúrate de que CrearEditarHorarioNuevoForm se use aquí:
            CrearEditarHorarioNuevoForm crearForm = new CrearEditarHorarioNuevoForm();
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
                MessageBox.Show("Por favor, selecciona un horario para editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Saca el ID del horario de la columna oculta
            int idHorario = Convert.ToInt32(HorariosDataGrid.SelectedRows[0].Cells["idHorario"].Value);

            CrearEditarHorarioNuevoForm editarForm = new CrearEditarHorarioNuevoForm(idHorario);
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
                MessageBox.Show("Por favor, selecciona un horario de la tabla para eliminarlo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que quieres eliminar este horario semanal?\n\nNota: Si existen citas reservadas en este horario, no podrás eliminarlo por seguridad.",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                // Deshabilitamos el botón temporalmente para evitar dobles clics
                EliminarBtn.Enabled = false;

                try
                {
                    // Sacamos el ID de la columna que ocultamos previamente
                    int idHorario = Convert.ToInt32(HorariosDataGrid.SelectedRows[0].Cells["idHorario"].Value);

                    bool eliminado = await ApiService.DeleteAsync($"api/horarios/{idHorario}");

                    if (eliminado)
                    {
                        MessageBox.Show("Horario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarHorarios(); // Refrescamos la tabla
                    }
                }
                catch (Exception ex)
                {
                    // Si el backend lanza un error de "ConstraintViolation" suele ser por las citas asociadas
                    if (ex.Message.Contains("ConstraintViolation") || ex.Message.Contains("foreign key constraint") || ex.Message.Contains("500"))
                    {
                        MessageBox.Show("No se puede eliminar este horario porque hay CITAS asociadas a él.\n\nDebes cancelar o eliminar primero las citas de este turno antes de borrar el horario base.",
                            "Acción Bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Error al eliminar horario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                finally
                {
                    EliminarBtn.Enabled = true;
                }
            }
        }

        private void IniciBoto_Click(object sender, EventArgs e) { new HomeForm().Show(); this.Hide(); }
        private void ServiciosBoto_Click(object sender, EventArgs e) { new ServiciosForm().Show(); this.Hide(); }
        private void UsuariosBoto_Click(object sender, EventArgs e) { new UsuariosForm().Show(); this.Hide(); }
        private void ClientesBoto_Click(object sender, EventArgs e) { new ClientesForm().Show(); this.Hide(); }
        private void CitasBoto_Click(object sender, EventArgs e) { new CitasForm().Show(); this.Hide(); }
        private void GruposBoto_Click(object sender, EventArgs e) { new GruposForm().Show(); this.Hide(); }
        private void HorarioSemanalBoto_Click(object sender, EventArgs e) { new HorarioSemanalForm().Show(); this.Hide(); }

        private void ValoracionForm_Click(object sender, EventArgs e)
        {
            ValoracionesForm valoracionform = new ValoracionesForm();
            valoracionform.Show();
            this.Hide();
        }

        private void HorarioForm_Click(object sender, EventArgs e)
        {
            // Ya estamos en esta pantalla
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

        private void HorarioForm_Load(object sender, EventArgs e)
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