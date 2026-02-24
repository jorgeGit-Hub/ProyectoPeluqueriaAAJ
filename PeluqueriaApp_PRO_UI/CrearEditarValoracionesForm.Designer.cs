using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    partial class CrearEditarValoracionForm
    {
        private Panel FormPanel;
        private Label TituloLbl;
        private Label CitaLbl;
        private ComboBox CitaCombo;
        private Label PuntuacionLbl;
        private NumericUpDown PuntuacionNum;
        private Label ComentarioLbl;
        private TextBox ComentarioTxt;
        private Button GuardarBtn;
        private Button CancelarBtn;

        private void InitializeComponent()
        {
            this.FormPanel = new Panel();
            this.TituloLbl = new Label();
            this.CitaLbl = new Label();
            this.CitaCombo = new ComboBox();
            this.PuntuacionLbl = new Label();
            this.PuntuacionNum = new NumericUpDown();
            this.ComentarioLbl = new Label();
            this.ComentarioTxt = new TextBox();
            this.GuardarBtn = new Button();
            this.CancelarBtn = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.PuntuacionNum)).BeginInit();

            this.ClientSize = new Size(600, 500);
            this.Text = "Gestión de Valoración";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(250, 245, 240);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            this.FormPanel.BackColor = Color.White;
            this.FormPanel.Location = new Point(20, 20);
            this.FormPanel.Size = new Size(550, 450);
            this.FormPanel.BorderStyle = BorderStyle.FixedSingle;

            this.TituloLbl.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.TituloLbl.Location = new Point(20, 20);
            this.TituloLbl.Size = new Size(500, 40);
            this.TituloLbl.TextAlign = ContentAlignment.MiddleCenter;

            this.CitaLbl.Text = "Selecciona la Cita (Sólo realizadas):";
            this.CitaLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.CitaLbl.Location = new Point(40, 80);
            this.CitaLbl.Size = new Size(300, 25);

            this.CitaCombo.Font = new Font("Segoe UI", 11F);
            this.CitaCombo.Location = new Point(40, 110);
            this.CitaCombo.Size = new Size(470, 33);
            this.CitaCombo.DropDownStyle = ComboBoxStyle.DropDownList;

            this.PuntuacionLbl.Text = "Puntuación (1 a 5 estrellas):";
            this.PuntuacionLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.PuntuacionLbl.Location = new Point(40, 160);
            this.PuntuacionLbl.Size = new Size(300, 25);

            this.PuntuacionNum.Font = new Font("Segoe UI", 12F);
            this.PuntuacionNum.Location = new Point(40, 190);
            this.PuntuacionNum.Size = new Size(100, 34);
            this.PuntuacionNum.Minimum = 1;
            this.PuntuacionNum.Maximum = 5;
            this.PuntuacionNum.Value = 5;

            this.ComentarioLbl.Text = "Comentario:";
            this.ComentarioLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.ComentarioLbl.Location = new Point(40, 240);
            this.ComentarioLbl.Size = new Size(300, 25);

            this.ComentarioTxt.Font = new Font("Segoe UI", 11F);
            this.ComentarioTxt.Location = new Point(40, 270);
            this.ComentarioTxt.Size = new Size(470, 80);
            this.ComentarioTxt.Multiline = true;

            this.GuardarBtn.Text = "💾 Guardar";
            this.GuardarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.GuardarBtn.Size = new Size(200, 50);
            this.GuardarBtn.Location = new Point(40, 370);
            this.GuardarBtn.BackColor = Color.FromArgb(255, 140, 0);
            this.GuardarBtn.ForeColor = Color.White;
            this.GuardarBtn.FlatStyle = FlatStyle.Flat;
            this.GuardarBtn.Cursor = Cursors.Hand;
            this.GuardarBtn.Click += new System.EventHandler(this.GuardarBtn_Click);

            this.CancelarBtn.Text = "❌ Cancelar";
            this.CancelarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.CancelarBtn.Size = new Size(200, 50);
            this.CancelarBtn.Location = new Point(310, 370);
            this.CancelarBtn.BackColor = Color.FromArgb(139, 90, 60);
            this.CancelarBtn.ForeColor = Color.White;
            this.CancelarBtn.FlatStyle = FlatStyle.Flat;
            this.CancelarBtn.Cursor = Cursors.Hand;
            this.CancelarBtn.Click += new System.EventHandler(this.CancelarBtn_Click);

            this.FormPanel.Controls.Add(this.TituloLbl);
            this.FormPanel.Controls.Add(this.CitaLbl);
            this.FormPanel.Controls.Add(this.CitaCombo);
            this.FormPanel.Controls.Add(this.PuntuacionLbl);
            this.FormPanel.Controls.Add(this.PuntuacionNum);
            this.FormPanel.Controls.Add(this.ComentarioLbl);
            this.FormPanel.Controls.Add(this.ComentarioTxt);
            this.FormPanel.Controls.Add(this.GuardarBtn);
            this.FormPanel.Controls.Add(this.CancelarBtn);

            this.Controls.Add(this.FormPanel);
            ((System.ComponentModel.ISupportInitialize)(this.PuntuacionNum)).EndInit();
        }
    }
}