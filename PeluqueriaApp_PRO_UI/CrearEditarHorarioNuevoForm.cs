using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class CrearEditarHorarioNuevoForm : Form
    {
        private int? idHorario = null;
        private bool esEdicion = false;

        // Constructor para CREAR
        public CrearEditarHorarioNuevoForm()
        {
            InitializeComponent();
            this.Text = "Crear Horario Semanal";
            TituloLbl.Text = "Crear Horario Semanal";
            esEdicion = false;
            CargarServicios();
            CargarGrupos();
        }

        // Constructor para EDITAR
        public CrearEditarHorarioNuevoForm(int idHorario)
        {
            InitializeComponent();
            this.idHorario = idHorario;
            this.Text = "Editar Horario Semanal";
            TituloLbl.Text = "Editar Horario Semanal";
            esEdicion = true;
            CargarServicios();
            CargarGrupos();
            CargarDatosHorario(idHorario);
        }

        private async void CargarServicios()
        {
            try
            {
                var servicios = await ApiService.GetAsync<List<Servicio>>("api/servicios");
                ServicioCombo.Items.Clear();
                ServicioCombo.Items.Add(new ComboItem("-- Sin servicio --", 0));

                if (servicios != null)
                    foreach (var s in servicios)
                        ServicioCombo.Items.Add(new ComboItem($"{s.nombre} (ID: {s.idServicio})", s.idServicio));

                ServicioCombo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar servicios: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CargarGrupos()
        {
            try
            {
                var grupos = await ApiService.GetAsync<List<Grupo>>("api/grupos");
                GrupoCombo.Items.Clear();
                GrupoCombo.Items.Add(new ComboItem("-- Sin grupo --", 0));

                if (grupos != null)
                    foreach (var g in grupos)
                        GrupoCombo.Items.Add(new ComboItem($"{g.curso} - {g.turno} (ID: {g.idGrupo})", g.idGrupo));

                GrupoCombo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar grupos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CargarDatosHorario(int id)
        {
            try
            {
                var horario = await ApiService.GetAsync<HorarioSemanal>($"api/horarios/{id}");

                // Seleccionar día
                if (!string.IsNullOrEmpty(horario.diaSemana))
                {
                    string dia = horario.diaSemana.ToLower();
                    for (int i = 0; i < DiaCombo.Items.Count; i++)
                    {
                        if (DiaCombo.Items[i].ToString().ToLower() == dia)
                        {
                            DiaCombo.SelectedIndex = i;
                            break;
                        }
                    }
                }

                HoraInicioTxt.Text = horario.horaInicio ?? "";
                HoraFinTxt.Text = horario.horaFin ?? "";

                // Seleccionar servicio
                if (horario.servicio != null)
                {
                    for (int i = 0; i < ServicioCombo.Items.Count; i++)
                    {
                        if (ServicioCombo.Items[i] is ComboItem item && item.Id == horario.servicio.idServicio)
                        {
                            ServicioCombo.SelectedIndex = i;
                            break;
                        }
                    }
                }

                // Seleccionar grupo
                if (horario.grupo != null)
                {
                    for (int i = 0; i < GrupoCombo.Items.Count; i++)
                    {
                        if (GrupoCombo.Items[i] is ComboItem item && item.Id == horario.grupo.idGrupo)
                        {
                            GrupoCombo.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del horario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void GuardarBtn_Click(object sender, EventArgs e)
        {
            if (DiaCombo.SelectedItem == null || DiaCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Debes seleccionar un día de la semana", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(HoraInicioTxt.Text))
            {
                MessageBox.Show("La hora de inicio es obligatoria (formato HH:mm:ss)", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(HoraFinTxt.Text))
            {
                MessageBox.Show("La hora de fin es obligatoria (formato HH:mm:ss)", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            GuardarBtn.Enabled = false;
            GuardarBtn.Text = "Guardando...";

            try
            {
                // Construir objeto del horario
                int idServicioSel = (ServicioCombo.SelectedItem is ComboItem si) ? si.Id : 0;
                int idGrupoSel = (GrupoCombo.SelectedItem is ComboItem gi) ? gi.Id : 0;

                var horarioData = new
                {
                    servicio = idServicioSel > 0 ? new { idServicio = idServicioSel } : (object)null,
                    grupo = idGrupoSel > 0 ? new { idGrupo = idGrupoSel } : (object)null,
                    diaSemana = DiaCombo.SelectedItem.ToString().ToLower(),
                    horaInicio = HoraInicioTxt.Text.Trim(),
                    horaFin = HoraFinTxt.Text.Trim()
                };

                if (esEdicion)
                {
                    await ApiService.PutAsync<object>($"api/horarios/{idHorario.Value}", horarioData);
                    MessageBox.Show("Horario actualizado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await ApiService.PostAsync<object>("api/horarios", horarioData);
                    MessageBox.Show("Horario creado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                GuardarBtn.Enabled = true;
                GuardarBtn.Text = "💾 Guardar";
            }
        }

        private void CancelarBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estás seguro de que quieres cancelar?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        // Clase auxiliar para los ComboBox con ID
        private class ComboItem
        {
            public string Texto { get; }
            public int Id { get; }

            public ComboItem(string texto, int id)
            {
                Texto = texto;
                Id = id;
            }

            public override string ToString() => Texto;
        }
    }
}