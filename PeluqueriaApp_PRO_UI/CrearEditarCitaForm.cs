using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class CrearEditarCitaForm : Form
    {
        private int? idCita = null;
        private bool esEdicion = false;
        private List<HorarioDisponible> horariosDisponibles = new List<HorarioDisponible>();

        // Guardamos el idHorario del slot base seleccionado para enviarlo al backend
        private int idHorarioSeleccionado = 0;

        public CrearEditarCitaForm()
        {
            InitializeComponent();
            this.Text = "Crear Nueva Cita";
            TituloLbl.Text = "Crear Nueva Cita";
            esEdicion = false;

            FechaCalendar.SetDate(DateTime.Now);
            EstadoCombo.SelectedIndex = 0; // pendiente por defecto

            CargarClientes();
            CargarServicios();

            FechaCalendar.DateChanged += FechaCalendar_DateChanged;
            ServicioCombo.SelectedIndexChanged += ServicioCombo_SelectedIndexChanged;
        }

        public CrearEditarCitaForm(int id)
        {
            InitializeComponent();
            this.Text = "Editar Cita";
            TituloLbl.Text = "Editar Cita";
            idCita = id;
            esEdicion = true;

            CargarClientes();
            CargarServicios();
            CargarDatosCita(id);

            FechaCalendar.DateChanged += FechaCalendar_DateChanged;
            ServicioCombo.SelectedIndexChanged += ServicioCombo_SelectedIndexChanged;
        }

        private async void CargarClientes()
        {
            try
            {
                var clientes = await ApiService.GetAsync<List<ClienteCompleto>>("api/clientes/completos");
                ClienteCombo.Items.Clear();
                if (clientes != null)
                {
                    foreach (var c in clientes)
                    {
                        ClienteCombo.Items.Add(new ComboItem { Text = $"{c.nombre} {c.apellidos}", Value = c.idUsuario });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}");
            }
        }

        private async void CargarServicios()
        {
            try
            {
                var servicios = await ApiService.GetAsync<List<Servicio>>("api/servicios");
                ServicioCombo.Items.Clear();
                if (servicios != null)
                {
                    foreach (var s in servicios)
                    {
                        ServicioCombo.Items.Add(new ComboItem { Text = s.nombre, Value = s.idServicio });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar servicios: {ex.Message}");
            }
        }

        private void FechaCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            CargarHorariosDisponibles();
        }

        private void ServicioCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarHorariosDisponibles();
        }

        private async void CargarHorariosDisponibles()
        {
            if (ServicioCombo.SelectedItem == null) return;

            int idServ = ((ComboItem)ServicioCombo.SelectedItem).Value;
            DateTime fechaSelec = FechaCalendar.SelectionStart;
            string diaSemana = TraducirDia(fechaSelec.DayOfWeek);

            try
            {
                var horarios = await ApiService.GetAsync<List<HorarioSemanal>>($"api/horarios/servicio/{idServ}");
                var horariosDelDia = horarios?.Where(h => h.diaSemana?.ToLower() == diaSemana).ToList();

                HorariosListBox.Items.Clear();
                horariosDisponibles.Clear();

                if (horariosDelDia != null)
                {
                    foreach (var h in horariosDelDia)
                    {
                        // ✅ FIX: GetValueOrDefault() para extraer el número del int? de forma segura
                        var hd = new HorarioDisponible
                        {
                            IdHorario = h.idHorario.GetValueOrDefault(),
                            HoraInicio = h.horaInicio,
                            HoraFin = h.horaFin,
                            DisplayText = $"{h.horaInicio} a {h.horaFin} (Grupo ID: {h.grupo?.idGrupo})"
                        };
                        HorariosListBox.Items.Add(hd);
                        horariosDisponibles.Add(hd);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar horarios: {ex.Message}");
            }
        }

        // AUTOCOMPLETAR HORAS: Cuando selecciona un bloque horario base, ponemos sus horas en los campos
        private void HorariosListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HorariosListBox.SelectedItem is HorarioDisponible seleccionado)
            {
                idHorarioSeleccionado = seleccionado.IdHorario;

                // Rellenar las horas si están vacías, para ayudar al usuario
                if (string.IsNullOrWhiteSpace(HoraInicioTxt.Text))
                    HoraInicioTxt.Text = seleccionado.HoraInicio;

                if (string.IsNullOrWhiteSpace(HoraFinTxt.Text))
                    HoraFinTxt.Text = seleccionado.HoraFin;
            }
        }

        private string TraducirDia(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday: return "lunes";
                case DayOfWeek.Tuesday: return "martes";
                case DayOfWeek.Wednesday: return "miercoles";
                case DayOfWeek.Thursday: return "jueves";
                case DayOfWeek.Friday: return "viernes";
                case DayOfWeek.Saturday: return "sabado";
                case DayOfWeek.Sunday: return "domingo";
                default: return "";
            }
        }

        private async void GuardarBtn_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (ClienteCombo.SelectedItem == null)
            {
                MessageBox.Show("Debes seleccionar un cliente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ServicioCombo.SelectedItem == null)
            {
                MessageBox.Show("Debes seleccionar un servicio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (idHorarioSeleccionado == 0)
            {
                MessageBox.Show("Debes seleccionar un turno semanal base de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(HoraInicioTxt.Text) || string.IsNullOrWhiteSpace(HoraFinTxt.Text))
            {
                MessageBox.Show("Debe especificar la Hora de Inicio y Fin de la cita.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hInicioStr = HoraInicioTxt.Text.Trim();
            string hFinStr = HoraFinTxt.Text.Trim();

            // Aseguramos formato HH:mm:ss requerido por el backend (LocalTime)
            if (hInicioStr.Length == 5) hInicioStr += ":00";
            if (hFinStr.Length == 5) hFinStr += ":00";

            GuardarBtn.Enabled = false;
            GuardarBtn.Text = "Guardando...";

            try
            {
                var citaPayload = new Cita
                {
                    fecha = FechaCalendar.SelectionStart.ToString("yyyy-MM-dd"),
                    horaInicio = hInicioStr,
                    horaFin = hFinStr,
                    estado = EstadoCombo.SelectedItem?.ToString() ?? "pendiente",
                    cliente = new ClienteSimple { idUsuario = ((ComboItem)ClienteCombo.SelectedItem).Value },
                    horarioSemanal = new HorarioSemanal { idHorario = idHorarioSeleccionado }
                };

                if (esEdicion && idCita.HasValue)
                {
                    await ApiService.PutAsync<Cita>($"api/citas/{idCita.Value}", citaPayload);
                    MessageBox.Show("Cita actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await ApiService.PostAsync<Cita>("api/citas", citaPayload);
                    MessageBox.Show("Cita creada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar cita: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                GuardarBtn.Enabled = true;
                GuardarBtn.Text = "💾 Guardar Cita";
            }
        }

        private async void CargarDatosCita(int id)
        {
            try
            {
                var cita = await ApiService.GetAsync<Cita>($"api/citas/{id}");
                if (cita == null) return;

                if (DateTime.TryParse(cita.fecha, out DateTime fechaCita))
                {
                    FechaCalendar.SetDate(fechaCita);
                }

                // Cargar horas
                HoraInicioTxt.Text = cita.horaInicio;
                HoraFinTxt.Text = cita.horaFin;

                if (cita.cliente != null)
                {
                    foreach (ComboItem item in ClienteCombo.Items)
                    {
                        if (item.Value == cita.cliente.idUsuario)
                        {
                            ClienteCombo.SelectedItem = item;
                            break;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(cita.estado))
                {
                    foreach (string item in EstadoCombo.Items)
                    {
                        if (item.ToLower() == cita.estado.ToLower())
                        {
                            EstadoCombo.SelectedItem = item;
                            break;
                        }
                    }
                }

                // ✅ FIX: Comprobación segura y uso del .Value para el int nullable
                if (cita.horarioSemanal != null && cita.horarioSemanal.idHorario != null && cita.horarioSemanal.idHorario != 0)
                {
                    idHorarioSeleccionado = cita.horarioSemanal.idHorario.Value;

                    var horario = await ApiService.GetAsync<HorarioSemanal>($"api/horarios/{idHorarioSeleccionado}");
                    if (horario != null && horario.servicio != null)
                    {
                        foreach (ComboItem item in ServicioCombo.Items)
                        {
                            if (item.Value == horario.servicio.idServicio)
                            {
                                ServicioCombo.SelectedItem = item;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos de la cita: {ex.Message}");
            }
        }

        private void CancelarBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Clases auxiliares
        private class ComboItem
        {
            public string Text { get; set; }
            public int Value { get; set; }
            public override string ToString() => Text;
        }

        private class HorarioDisponible
        {
            public int IdHorario { get; set; }
            public string HoraInicio { get; set; }
            public string HoraFin { get; set; }
            public string DisplayText { get; set; }
            public override string ToString() => DisplayText;
        }
    }
}