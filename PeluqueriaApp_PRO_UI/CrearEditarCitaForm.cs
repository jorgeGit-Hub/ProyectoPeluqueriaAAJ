using System;
using System.Collections.Generic;
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

        public CrearEditarCitaForm()
        {
            InitializeComponent();
            this.Text = "Crear Nueva Cita";
            TituloLbl.Text = "Crear Nueva Cita";
            esEdicion = false;

            FechaCalendar.SetDate(DateTime.Now);
            CargarClientes();
            CargarServicios();

            // Eventos para actualizar horarios
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

            // Eventos para actualizar horarios
            FechaCalendar.DateChanged += FechaCalendar_DateChanged;
            ServicioCombo.SelectedIndexChanged += ServicioCombo_SelectedIndexChanged;
        }

        private async void CargarClientes()
        {
            try
            {
                var usuarios = await ApiService.GetAsync<List<Usuario>>("api/usuarios");

                ClienteCombo.Items.Clear();
                ClienteCombo.Items.Add("-- Seleccionar Cliente --");

                if (usuarios != null && usuarios.Count > 0)
                {
                    foreach (var usuario in usuarios)
                    {
                        if (usuario.rol?.ToLower() == "cliente")
                        {
                            ClienteCombo.Items.Add(new ComboItem
                            {
                                Text = $"{usuario.nombre} {usuario.apellidos} (ID: {usuario.idUsuario})",
                                Value = usuario.idUsuario
                            });
                        }
                    }
                }

                ClienteCombo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CargarServicios()
        {
            try
            {
                var servicios = await ApiService.GetAsync<List<Servicio>>("api/servicios");

                ServicioCombo.Items.Clear();
                ServicioCombo.Items.Add("-- Seleccionar Servicio --");

                if (servicios != null && servicios.Count > 0)
                {
                    foreach (var servicio in servicios)
                    {
                        string grupoInfo = servicio.grupo != null ? $"Grupo {servicio.grupo.idGrupo}" : "Sin grupo";
                        ServicioCombo.Items.Add(new ComboItem
                        {
                            Text = $"{servicio.nombre} ({grupoInfo}) - {servicio.precio}€",
                            Value = servicio.idServicio
                        });
                    }
                }

                ServicioCombo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar servicios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ServicioCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarHorariosDisponibles();
        }

        private void FechaCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            CargarHorariosDisponibles();
        }

        private async void CargarHorariosDisponibles()
        {
            // Limpiar lista de horarios
            HorariosListBox.Items.Clear();
            horariosDisponibles.Clear();

            // Validar que haya un servicio seleccionado
            if (ServicioCombo.SelectedIndex <= 0)
            {
                HorariosListBox.Items.Add("Selecciona un servicio para ver horarios disponibles");
                return;
            }

            try
            {
                var servicioSeleccionado = (ComboItem)ServicioCombo.SelectedItem;
                DateTime fechaSeleccionada = FechaCalendar.SelectionStart;
                string diaSemana = ObtenerDiaSemana(fechaSeleccionada);
                string diaNombre = ObtenerNombreDia(fechaSeleccionada);

                // Obtener horarios del servicio para ese día
                var horarios = await ApiService.GetAsync<List<HorarioSemanal>>(
                    $"api/horarios/servicio/{servicioSeleccionado.Value}/dia/{diaSemana}"
                );

                if (horarios == null || horarios.Count == 0)
                {
                    HorariosListBox.Items.Add($"⚠️  No hay horarios disponibles para {diaNombre}");
                    return;
                }

                // Obtener el servicio completo para saber la duración
                var servicio = await ApiService.GetAsync<Servicio>($"api/servicios/{servicioSeleccionado.Value}");
                int duracionMinutos = ParsearDuracion(servicio.tiempoCliente);

                // Generar slots de tiempo para cada horario
                foreach (var horario in horarios)
                {
                    GenerarSlotsDeHorario(horario, duracionMinutos, fechaSeleccionada);
                }

                // Mostrar en el ListBox
                if (horariosDisponibles.Count == 0)
                {
                    HorariosListBox.Items.Add("⚠️  No hay slots disponibles para este día");
                }
                else
                {
                    HorariosListBox.Items.Add($"✓ {horariosDisponibles.Count} horarios disponibles para {diaNombre}:");
                    HorariosListBox.Items.Add(""); // Línea en blanco

                    foreach (var slot in horariosDisponibles)
                    {
                        HorariosListBox.Items.Add(slot);
                    }
                }
            }
            catch (Exception ex)
            {
                HorariosListBox.Items.Add($"Error al cargar horarios: {ex.Message}");
            }
        }

        private void GenerarSlotsDeHorario(HorarioSemanal horario, int duracionMinutos, DateTime fecha)
        {
            try
            {
                TimeSpan horaInicio = TimeSpan.Parse(horario.horaInicio);
                TimeSpan horaFin = TimeSpan.Parse(horario.horaFin);
                TimeSpan duracion = TimeSpan.FromMinutes(duracionMinutos);

                TimeSpan horaActual = horaInicio;

                while (horaActual.Add(duracion) <= horaFin)
                {
                    TimeSpan horaFinSlot = horaActual.Add(duracion);

                    horariosDisponibles.Add(new HorarioDisponible
                    {
                        HoraInicio = horaActual.ToString(@"hh\:mm\:ss"),
                        HoraFin = horaFinSlot.ToString(@"hh\:mm\:ss"),
                        Fecha = fecha,
                        DisplayText = $"🕐 {horaActual:hh\\:mm} - {horaFinSlot:hh\\:mm}"
                    });

                    // Avanzar al siguiente slot (puede ser cada 30 min, 1 hora, etc.)
                    horaActual = horaActual.Add(duracion);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error generando slots: {ex.Message}");
            }
        }

        private int ParsearDuracion(string tiempoCliente)
        {
            if (string.IsNullOrEmpty(tiempoCliente))
                return 60; // 1 hora por defecto

            try
            {
                // Quitar espacios y convertir a minúsculas
                tiempoCliente = tiempoCliente.Trim().ToLower();

                // Casos comunes: "45'", "1h", "1,5h", "90min", etc.
                if (tiempoCliente.Contains("h"))
                {
                    // Formato: "1h", "1.5h", "1,5h"
                    string numero = tiempoCliente.Replace("h", "").Replace(",", ".").Trim();
                    double horas = double.Parse(numero, System.Globalization.CultureInfo.InvariantCulture);
                    return (int)(horas * 60);
                }
                else if (tiempoCliente.Contains("min"))
                {
                    // Formato: "45min", "90min"
                    string numero = tiempoCliente.Replace("min", "").Trim();
                    return int.Parse(numero);
                }
                else if (tiempoCliente.Contains("'"))
                {
                    // Formato: "45'", "60'"
                    string numero = tiempoCliente.Replace("'", "").Trim();
                    return int.Parse(numero);
                }
                else
                {
                    // Intentar parsear como número directo (asumiendo minutos)
                    return int.Parse(tiempoCliente);
                }
            }
            catch
            {
                return 60; // 1 hora por defecto en caso de error
            }
        }

        private async void GuardarBtn_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (ClienteCombo.SelectedIndex <= 0)
            {
                MessageBox.Show("Debes seleccionar un cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClienteCombo.Focus();
                return;
            }

            if (ServicioCombo.SelectedIndex <= 0)
            {
                MessageBox.Show("Debes seleccionar un servicio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ServicioCombo.Focus();
                return;
            }

            if (HorariosListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Debes seleccionar un horario de la lista", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                HorariosListBox.Focus();
                return;
            }

            // Obtener el horario seleccionado
            var horarioSeleccionado = HorariosListBox.SelectedItem as HorarioDisponible;
            if (horarioSeleccionado == null)
            {
                MessageBox.Show("Por favor selecciona un horario válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (EstadoCombo.SelectedItem == null)
            {
                MessageBox.Show("Debes seleccionar un estado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EstadoCombo.Focus();
                return;
            }

            GuardarBtn.Enabled = false;
            GuardarBtn.Text = "Guardando...";

            try
            {
                var clienteSeleccionado = (ComboItem)ClienteCombo.SelectedItem;
                var servicioSeleccionado = (ComboItem)ServicioCombo.SelectedItem;

                var cita = new Cita
                {
                    fecha = horarioSeleccionado.Fecha.ToString("yyyy-MM-dd"),
                    horaInicio = horarioSeleccionado.HoraInicio,
                    horaFin = horarioSeleccionado.HoraFin,
                    estado = EstadoCombo.SelectedItem.ToString().ToLower(),
                    cliente = new ClienteSimple { idUsuario = clienteSeleccionado.Value },
                    servicio = new Servicio { idServicio = servicioSeleccionado.Value }
                };

                if (esEdicion)
                {
                    cita.idCita = idCita.Value;
                    await ApiService.PutAsync<Cita>($"api/citas/{idCita.Value}", cita);
                    MessageBox.Show("Cita actualizada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await ApiService.PostAsync<Cita>("api/citas", cita);
                    MessageBox.Show("Cita creada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;

                // Extraer el mensaje de error del JSON si existe
                if (mensaje.Contains("error"))
                {
                    try
                    {
                        int startIndex = mensaje.IndexOf("{\"error\":");
                        if (startIndex >= 0)
                        {
                            string jsonPart = mensaje.Substring(startIndex);
                            var errorObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonPart);
                            if (errorObj != null && errorObj.ContainsKey("error"))
                            {
                                mensaje = errorObj["error"];
                            }
                        }
                    }
                    catch { }
                }

                MessageBox.Show($"Error al guardar:\n\n{mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                GuardarBtn.Enabled = true;
                GuardarBtn.Text = "💾 Guardar";
            }
        }

        private string ObtenerDiaSemana(DateTime fecha)
        {
            switch (fecha.DayOfWeek)
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

        private string ObtenerNombreDia(DateTime fecha)
        {
            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday: return "lunes";
                case DayOfWeek.Tuesday: return "martes";
                case DayOfWeek.Wednesday: return "miércoles";
                case DayOfWeek.Thursday: return "jueves";
                case DayOfWeek.Friday: return "viernes";
                case DayOfWeek.Saturday: return "sábado";
                case DayOfWeek.Sunday: return "domingo";
                default: return "";
            }
        }

        private async void CargarDatosCita(int id)
        {
            try
            {
                var cita = await ApiService.GetAsync<Cita>($"api/citas/{id}");

                // Cargar fecha
                if (!string.IsNullOrEmpty(cita.fecha))
                {
                    FechaCalendar.SetDate(DateTime.Parse(cita.fecha));
                }

                // Seleccionar estado
                if (!string.IsNullOrEmpty(cita.estado))
                {
                    string estadoCapitalizado = char.ToUpper(cita.estado[0]) + cita.estado.Substring(1).ToLower();
                    EstadoCombo.SelectedItem = estadoCapitalizado;
                }

                // Seleccionar cliente
                if (cita.cliente != null)
                {
                    for (int i = 1; i < ClienteCombo.Items.Count; i++)
                    {
                        var item = (ComboItem)ClienteCombo.Items[i];
                        if (item.Value == cita.cliente.idUsuario)
                        {
                            ClienteCombo.SelectedIndex = i;
                            break;
                        }
                    }
                }

                // Seleccionar servicio
                if (cita.servicio != null)
                {
                    for (int i = 1; i < ServicioCombo.Items.Count; i++)
                    {
                        var item = (ComboItem)ServicioCombo.Items[i];
                        if (item.Value == cita.servicio.idServicio)
                        {
                            ServicioCombo.SelectedIndex = i;
                            break;
                        }
                    }
                }

                // Esperar a que se carguen los horarios y seleccionar el correcto
                await System.Threading.Tasks.Task.Delay(500);

                // Buscar y seleccionar el horario correspondiente
                for (int i = 0; i < HorariosListBox.Items.Count; i++)
                {
                    var item = HorariosListBox.Items[i] as HorarioDisponible;
                    if (item != null && item.HoraInicio == cita.horaInicio && item.HoraFin == cita.horaFin)
                    {
                        HorariosListBox.SelectedIndex = i;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos de la cita: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelarBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que quieres cancelar? Se perderán los cambios no guardados.",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        // Clase auxiliar para ComboBox
        private class ComboItem
        {
            public string Text { get; set; }
            public int Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        // Clase para los horarios disponibles
        private class HorarioDisponible
        {
            public string HoraInicio { get; set; }
            public string HoraFin { get; set; }
            public DateTime Fecha { get; set; }
            public string DisplayText { get; set; }

            public override string ToString()
            {
                return DisplayText;
            }
        }
    }
}