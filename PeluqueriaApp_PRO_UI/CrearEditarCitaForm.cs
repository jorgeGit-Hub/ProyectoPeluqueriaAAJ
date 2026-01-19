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

        public CrearEditarCitaForm()
        {
            InitializeComponent();
            this.Text = "Crear Nueva Cita";
            TituloLbl.Text = "Crear Nueva Cita";
            esEdicion = false;

            FechaCalendar.SetDate(DateTime.Now);
            CargarClientes();
            CargarServicios();
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

            if (string.IsNullOrWhiteSpace(HoraInicioTxt.Text) || string.IsNullOrWhiteSpace(HoraFinTxt.Text))
            {
                MessageBox.Show("La hora de inicio y fin son obligatorias", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar formato de horas
            if (!ValidarFormatoHora(HoraInicioTxt.Text) || !ValidarFormatoHora(HoraFinTxt.Text))
            {
                MessageBox.Show("El formato de hora debe ser HH:mm:ss (ejemplo: 09:00:00)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // Obtener fecha seleccionada
                DateTime fechaSeleccionada = FechaCalendar.SelectionStart;

                // Verificar horarios disponibles ANTES de crear la cita
                var horarios = await ApiService.GetAsync<List<HorarioSemanal>>(
                    $"api/horarios/servicio/{servicioSeleccionado.Value}/dia/{ObtenerDiaSemana(fechaSeleccionada)}"
                );

                if (horarios == null || horarios.Count == 0)
                {
                    string diaNombre = ObtenerNombreDia(fechaSeleccionada);
                    MessageBox.Show(
                        $"El servicio seleccionado no tiene horarios disponibles para {diaNombre}.\n\n" +
                        "Por favor, selecciona otro día o verifica los horarios del servicio.",
                        "Horario no disponible",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Validar que el horario esté dentro de los disponibles
                bool horarioValido = false;
                foreach (var horario in horarios)
                {
                    if (HoraInicioTxt.Text.CompareTo(horario.horaInicio) >= 0 &&
                        HoraFinTxt.Text.CompareTo(horario.horaFin) <= 0)
                    {
                        horarioValido = true;
                        break;
                    }
                }

                if (!horarioValido)
                {
                    string horariosDisponibles = string.Join(", ",
                        horarios.ConvertAll(h => $"{h.horaInicio} - {h.horaFin}"));

                    MessageBox.Show(
                        $"El horario seleccionado no está disponible.\n\n" +
                        $"Horarios disponibles: {horariosDisponibles}",
                        "Horario inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                var cita = new Cita
                {
                    fecha = fechaSeleccionada.ToString("yyyy-MM-dd"),
                    horaInicio = HoraInicioTxt.Text.Trim(),
                    horaFin = HoraFinTxt.Text.Trim(),
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
                        // Intentar parsear el JSON de error
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

        private bool ValidarFormatoHora(string hora)
        {
            // Validar formato HH:mm:ss
            if (string.IsNullOrWhiteSpace(hora)) return false;

            string[] partes = hora.Split(':');
            if (partes.Length != 3) return false;

            int hh, mm, ss;
            if (!int.TryParse(partes[0], out hh) || hh < 0 || hh > 23) return false;
            if (!int.TryParse(partes[1], out mm) || mm < 0 || mm > 59) return false;
            if (!int.TryParse(partes[2], out ss) || ss < 0 || ss > 59) return false;

            return true;
        }

        private string ObtenerDiaSemana(DateTime fecha)
        {
            // Convertir a español en minúsculas SIN TILDES para coincidir con el backend
            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday: return "lunes";
                case DayOfWeek.Tuesday: return "martes";
                case DayOfWeek.Wednesday: return "miercoles"; // SIN TILDE
                case DayOfWeek.Thursday: return "jueves";
                case DayOfWeek.Friday: return "viernes";
                case DayOfWeek.Saturday: return "sabado"; // SIN TILDE
                case DayOfWeek.Sunday: return "domingo";
                default: return "";
            }
        }

        private string ObtenerNombreDia(DateTime fecha)
        {
            // Versión con tildes para mostrar al usuario
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

                // Cargar horas
                HoraInicioTxt.Text = cita.horaInicio;
                HoraFinTxt.Text = cita.horaFin;

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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos de la cita: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } //

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
    }
}