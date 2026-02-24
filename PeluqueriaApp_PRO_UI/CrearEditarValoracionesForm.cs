using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class CrearEditarValoracionForm : Form
    {
        private int? idValoracion = null;
        private bool esEdicion = false;

        public CrearEditarValoracionForm()
        {
            InitializeComponent();
            this.Text = "Crear Valoración";
            TituloLbl.Text = "Nueva Valoración";
            esEdicion = false;
            _ = CargarCitasRealizadas();
        }

        public CrearEditarValoracionForm(int id)
        {
            InitializeComponent();
            this.Text = "Editar Valoración";
            TituloLbl.Text = "Editar Valoración";
            idValoracion = id;
            esEdicion = true;
            CargarListasYDatos(id);
        }

        private async Task CargarCitasRealizadas()
        {
            try
            {
                var citas = await ApiService.GetAsync<List<Cita>>("api/citas");
                var usuarios = await ApiService.GetAsync<List<Usuario>>("api/usuarios");
                var dictUsuarios = usuarios?.ToDictionary(u => u.idUsuario, u => $"{u.nombre} {u.apellidos}") ?? new Dictionary<int, string>();

                CitaCombo.Items.Clear();

                if (citas != null)
                {
                    // ✅ FILTRO CRÍTICO: El backend exige que la cita esté REALIZADA
                    var citasRealizadas = citas.Where(c => c.estado != null && c.estado.ToLower() == "realizada").ToList();

                    foreach (var c in citasRealizadas)
                    {
                        string nomCliente = dictUsuarios.ContainsKey(c.cliente.idUsuario) ? dictUsuarios[c.cliente.idUsuario] : "Desconocido";
                        string texto = $"Cita {c.idCita} - {c.fecha} - {nomCliente}";
                        CitaCombo.Items.Add(new ComboItem(texto, c.idCita));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar citas: {ex.Message}");
            }
        }

        private async void CargarListasYDatos(int id)
        {
            await CargarCitasRealizadas();

            try
            {
                var valoracion = await ApiService.GetAsync<Valoracion>($"api/valoraciones/{id}");
                if (valoracion == null) return;

                PuntuacionNum.Value = valoracion.puntuacion;
                ComentarioTxt.Text = valoracion.comentario;

                if (valoracion.cita != null)
                {
                    foreach (ComboItem item in CitaCombo.Items)
                    {
                        if (item.Id == valoracion.cita.idCita)
                        {
                            CitaCombo.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar valoración: {ex.Message}");
            }
        }

        private async void GuardarBtn_Click(object sender, EventArgs e)
        {
            if (CitaCombo.SelectedItem == null)
            {
                MessageBox.Show("Debes seleccionar una cita realizada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GuardarBtn.Enabled = false;
            GuardarBtn.Text = "Guardando...";

            try
            {
                var payload = new Valoracion
                {
                    cita = new Cita { idCita = ((ComboItem)CitaCombo.SelectedItem).Id },
                    puntuacion = (int)PuntuacionNum.Value,
                    comentario = ComentarioTxt.Text.Trim(),
                    fechaValoracion = DateTime.Now.ToString("yyyy-MM-dd") // El backend pide fecha, mandamos la de hoy
                };

                if (esEdicion && idValoracion.HasValue)
                {
                    payload.idValoracion = idValoracion.Value;
                    await ApiService.PutAsync<Valoracion>($"api/valoraciones/{idValoracion.Value}", payload);
                    MessageBox.Show("Valoración actualizada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await ApiService.PostAsync<Valoracion>("api/valoraciones", payload);
                    MessageBox.Show("Valoración creada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n\nNota: Es posible que esta cita ya tenga una valoración registrada.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                GuardarBtn.Enabled = true;
                GuardarBtn.Text = "💾 Guardar";
            }
        }

        private void CancelarBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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