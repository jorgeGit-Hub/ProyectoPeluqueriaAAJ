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
                    fecha = FechaCalendar.SelectionStart.ToString("yyyy-MM-dd"),
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
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                GuardarBtn.Enabled = true;
                GuardarBtn.Text = "💾 Guardar";
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
    }
}