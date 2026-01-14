using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class CrearEditarServicioForm : Form
    {
        private int? idServicio = null;
        private bool esEdicion = false;

        public CrearEditarServicioForm()
        {
            InitializeComponent();
            this.Text = "Crear Nuevo Servicio";
            TituloLbl.Text = "Crear Nuevo Servicio";
            esEdicion = false;
            CargarGrupos();
        }

        public CrearEditarServicioForm(int id)
        {
            InitializeComponent();
            this.Text = "Editar Servicio";
            TituloLbl.Text = "Editar Servicio";
            idServicio = id;
            esEdicion = true;
            CargarGrupos();
            CargarDatosServicio(id);
        }

        private async void CargarGrupos()
        {
            try
            {
                var grupos = await ApiService.GetAsync<List<Grupo>>("api/grupos");

                GrupoCombo.Items.Clear();
                GrupoCombo.Items.Add("-- Seleccionar Grupo --");

                if (grupos != null && grupos.Count > 0)
                {
                    foreach (var grupo in grupos)
                    {
                        GrupoCombo.Items.Add(new ComboItem
                        {
                            Text = $"{grupo.curso} - {grupo.turno}",
                            Value = grupo.idGrupo
                        });
                    }
                }

                GrupoCombo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar grupos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void GuardarBtn_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(NombreTxt.Text))
            {
                MessageBox.Show("El nombre es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NombreTxt.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(ModuloTxt.Text))
            {
                MessageBox.Show("El módulo es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ModuloTxt.Focus();
                return;
            }

            if (!decimal.TryParse(PrecioTxt.Text, out decimal precio) || precio < 0)
            {
                MessageBox.Show("El precio debe ser un número válido mayor o igual a 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PrecioTxt.Focus();
                return;
            }

            if (GrupoCombo.SelectedIndex <= 0)
            {
                MessageBox.Show("Debes seleccionar un grupo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GrupoCombo.Focus();
                return;
            }

            GuardarBtn.Enabled = false;
            GuardarBtn.Text = "Guardando...";

            try
            {
                var grupoSeleccionado = (ComboItem)GrupoCombo.SelectedItem;

                var servicio = new Servicio
                {
                    nombre = NombreTxt.Text.Trim(),
                    modulo = ModuloTxt.Text.Trim(),
                    aula = AulaTxt.Text.Trim(),
                    tiempoCliente = TiempoClienteTxt.Text.Trim(),
                    precio = precio,
                    diaSemana = DiaSemanaCombo.SelectedItem?.ToString(),
                    horario = HorarioTxt.Text.Trim(),
                    grupo = new GrupoSimple { idGrupo = grupoSeleccionado.Value }
                };

                if (esEdicion)
                {
                    servicio.idServicio = idServicio.Value;
                    await ApiService.PutAsync<Servicio>($"api/servicios/{idServicio.Value}", servicio);
                    MessageBox.Show("Servicio actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await ApiService.PostAsync<Servicio>("api/servicios", servicio);
                    MessageBox.Show("Servicio creado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void CargarDatosServicio(int id)
        {
            try
            {
                var servicio = await ApiService.GetAsync<Servicio>($"api/servicios/{id}");

                NombreTxt.Text = servicio.nombre;
                ModuloTxt.Text = servicio.modulo;
                AulaTxt.Text = servicio.aula;
                TiempoClienteTxt.Text = servicio.tiempoCliente;
                PrecioTxt.Text = servicio.precio.ToString("F2");
                HorarioTxt.Text = servicio.horario;

                // Seleccionar día de la semana
                if (!string.IsNullOrEmpty(servicio.diaSemana))
                {
                    int index = DiaSemanaCombo.Items.IndexOf(servicio.diaSemana);
                    if (index >= 0)
                    {
                        DiaSemanaCombo.SelectedIndex = index;
                    }
                }

                // Seleccionar grupo
                if (servicio.grupo != null && servicio.grupo.idGrupo > 0)
                {
                    for (int i = 1; i < GrupoCombo.Items.Count; i++)
                    {
                        var item = (ComboItem)GrupoCombo.Items[i];
                        if (item.Value == servicio.grupo.idGrupo)
                        {
                            GrupoCombo.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del servicio: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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