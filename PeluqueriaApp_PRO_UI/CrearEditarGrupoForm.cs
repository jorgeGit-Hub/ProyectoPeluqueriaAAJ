using System;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class CrearEditarGrupoForm : Form
    {
        private int? idGrupo = null;
        private bool esEdicion = false;

        public CrearEditarGrupoForm()
        {
            InitializeComponent();
            this.Text = "Crear Nuevo Grupo";
            TituloLbl.Text = "Crear Nuevo Grupo";
            esEdicion = false;
        }

        public CrearEditarGrupoForm(int id)
        {
            InitializeComponent();
            this.Text = "Editar Grupo";
            TituloLbl.Text = "Editar Grupo";
            idGrupo = id;
            esEdicion = true;

            CargarDatosGrupo(id);
        }

        private async void GuardarBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CursoTxt.Text))
            {
                MessageBox.Show("El curso es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CursoTxt.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(EmailTxt.Text) || !EmailTxt.Text.Contains("@"))
            {
                MessageBox.Show("El email no es válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EmailTxt.Focus();
                return;
            }

            if (TurnoCombo.SelectedItem == null)
            {
                MessageBox.Show("Debes seleccionar un turno", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TurnoCombo.Focus();
                return;
            }

            if (!esEdicion && string.IsNullOrWhiteSpace(ContrasenaTxt.Text))
            {
                MessageBox.Show("La contraseña es obligatoria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ContrasenaTxt.Focus();
                return;
            }

            GuardarBtn.Enabled = false;
            GuardarBtn.Text = "Guardando...";

            try
            {
                var grupo = new Grupo
                {
                    curso = CursoTxt.Text.Trim(),
                    email = EmailTxt.Text.Trim(),
                    turno = TurnoCombo.SelectedItem.ToString().ToLower()
                };

                /*if (!esEdicion && !string.IsNullOrWhiteSpace(ContrasenaTxt.Text))
                {
                    grupo.contrasena = ContrasenaTxt.Text.Trim();
                }*/

                if (esEdicion)
                {
                    grupo.idGrupo = idGrupo.Value;
                    await ApiService.PutAsync<Grupo>($"api/grupos/{idGrupo.Value}", grupo);
                    MessageBox.Show("Grupo actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await ApiService.PostAsync<Grupo>("api/grupos", grupo);
                    MessageBox.Show("Grupo creado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void CargarDatosGrupo(int id)
        {
            try
            {
                var grupo = await ApiService.GetAsync<Grupo>($"api/grupos/{id}");

                CursoTxt.Text = grupo.curso;
                EmailTxt.Text = grupo.email;

                if (!string.IsNullOrEmpty(grupo.turno))
                {
                    string turnoCapitalizado = char.ToUpper(grupo.turno[0]) + grupo.turno.Substring(1).ToLower();
                    TurnoCombo.SelectedItem = turnoCapitalizado;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del grupo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}