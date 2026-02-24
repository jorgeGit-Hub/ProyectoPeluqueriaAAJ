using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class CrearEditarHorarioNuevoForm : Form
    {
        private int? idHorario = null;
        private bool esEdicion = false;

        public CrearEditarHorarioNuevoForm()
        {
            InitializeComponent();
            this.Text = "Crear Horario Semanal";
            TituloLbl.Text = "Crear Horario Semanal";
            esEdicion = false;

            _ = CargarListasAsync();
        }

        public CrearEditarHorarioNuevoForm(int idHorario)
        {
            InitializeComponent();
            this.idHorario = idHorario;
            this.Text = "Editar Horario Semanal";
            TituloLbl.Text = "Editar Horario Semanal";
            esEdicion = true;

            CargarListasYDatos(idHorario);
        }

        private async Task CargarListasAsync()
        {
            try
            {
                var servicios = await ApiService.GetAsync<List<Servicio>>("api/servicios");
                ServicioCombo.Items.Clear();
                if (servicios != null)
                {
                    foreach (var s in servicios) ServicioCombo.Items.Add(new ComboItem(s.nombre, s.idServicio));
                }

                var grupos = await ApiService.GetAsync<List<Grupo>>("api/grupos");
                GrupoCombo.Items.Clear();
                if (grupos != null)
                {
                    foreach (var g in grupos) GrupoCombo.Items.Add(new ComboItem($"{g.curso} ({g.turno})", g.idGrupo));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar listas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CargarListasYDatos(int id)
        {
            await CargarListasAsync();
            CargarDatosHorario(id);
        }

        private async void CargarDatosHorario(int id)
        {
            try
            {
                var horario = await ApiService.GetAsync<HorarioSemanal>($"api/horarios/{id}");
                if (horario == null) return;

                if (!string.IsNullOrEmpty(horario.diaSemana))
                {
                    foreach (string item in DiaCombo.Items)
                    {
                        if (item.ToLower() == horario.diaSemana.ToLower())
                        {
                            DiaCombo.SelectedItem = item;
                            break;
                        }
                    }
                }

                HoraInicioTxt.Text = horario.horaInicio;
                HoraFinTxt.Text = horario.horaFin;

                if (horario.servicio != null)
                {
                    foreach (ComboItem item in ServicioCombo.Items)
                    {
                        if (item.Id == horario.servicio.idServicio)
                        {
                            ServicioCombo.SelectedItem = item;
                            break;
                        }
                    }
                }

                if (horario.grupo != null)
                {
                    foreach (ComboItem item in GrupoCombo.Items)
                    {
                        if (item.Id == horario.grupo.idGrupo)
                        {
                            GrupoCombo.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void GuardarBtn_Click(object sender, EventArgs e)
        {
            // 🚨 ESCUDO 1: Comprobación estricta del Token
            if (string.IsNullOrEmpty(ApiService.Token))
            {
                MessageBox.Show("❌ Tu sesión de seguridad (Token) está vacía o se ha perdido.\n\nPor favor, cierra sesión en el menú lateral, vuelve a iniciar sesión e inténtalo de nuevo.", "Sesión Inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DiaCombo.SelectedItem == null || string.IsNullOrWhiteSpace(HoraInicioTxt.Text) || string.IsNullOrWhiteSpace(HoraFinTxt.Text) || ServicioCombo.SelectedItem == null || GrupoCombo.SelectedItem == null)
            {
                MessageBox.Show("Por favor, rellena todos los campos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hInicioStr = HoraInicioTxt.Text.Trim();
            string hFinStr = HoraFinTxt.Text.Trim();

            // Formatear la hora de forma segura para Spring Boot
            if (hInicioStr.Length == 5) hInicioStr += ":00";
            if (hFinStr.Length == 5) hFinStr += ":00";

            GuardarBtn.Enabled = false;
            GuardarBtn.Text = "Guardando...";

            try
            {
                int idServicioSeleccionado = ((ComboItem)ServicioCombo.SelectedItem).Id;
                int idGrupoSeleccionado = ((ComboItem)GrupoCombo.SelectedItem).Id;

                // 🚨 ESCUDO 2: JSON Blindado
                // Usamos un objeto anónimo puro para asegurarnos de que los JsonConverters no rompen el envío
                var horarioData = new
                {
                    diaSemana = DiaCombo.SelectedItem.ToString().ToLower(), // Lo enviamos en minúscula
                    horaInicio = hInicioStr,
                    horaFin = hFinStr,
                    servicio = new { idServicio = idServicioSeleccionado },
                    grupo = new { idGrupo = idGrupoSeleccionado }
                };

                if (esEdicion && idHorario.HasValue)
                {
                    await ApiService.PutAsync<object>($"api/horarios/{idHorario.Value}", horarioData);
                    MessageBox.Show("Horario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await ApiService.PostAsync<object>("api/horarios", horarioData);
                    MessageBox.Show("Horario creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el horario:\n\n{ex.Message}", "Error del Servidor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                GuardarBtn.Enabled = true;
                GuardarBtn.Text = "💾 Guardar";
            }
        }

        private void CancelarBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que quieres cancelar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private class ComboItem
        {
            public string Texto { get; }
            public int Id { get; }
            public ComboItem(string texto, int id) { Texto = texto; Id = id; }
            public override string ToString() => Texto;
        }
    }
}