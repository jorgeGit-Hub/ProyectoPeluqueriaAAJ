using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    partial class CrearEditarBloqueoForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel FormPanel;
        private Label TituloLbl;
        private Label FechaLbl;
        private MonthCalendar FechaCalendar;
        private Label HoraInicioLbl;
        private TextBox HoraInicioTxt;
        private Label HoraFinLbl;
        private TextBox HoraFinTxt;
        private Label MotivoLbl;
        private TextBox MotivoTxt;
        private Label AdminLbl;
        private ComboBox AdminCombo;
        private Button GuardarBtn;
        private Button CancelarBtn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.FormPanel = new Panel();
            this.TituloLbl = new Label();
            this.FechaLbl = new Label();
            this.FechaCalendar = new MonthCalendar();
            this.HoraInicioLbl = new Label();
            this.HoraInicioTxt = new TextBox();
            this.HoraFinLbl = new Label();
            this.HoraFinTxt = new TextBox();
            this.MotivoLbl = new Label();
            this.MotivoTxt = new TextBox();
            this.AdminLbl = new Label();
            this.AdminCombo = new ComboBox();
            this.GuardarBtn = new Button();
            this.CancelarBtn = new Button();

            this.ClientSize = new Size(700, 650);
            this.Text = "Gestión de Bloqueo Horario";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(250, 245, 240);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.FormPanel.BackColor = Color.White;
            this.FormPanel.Location = new Point(20, 20);
            this.FormPanel.Size = new Size(650, 600);
            this.FormPanel.BorderStyle = BorderStyle.FixedSingle;

            this.TituloLbl.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.TituloLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TituloLbl.Location = new Point(20, 20);
            this.TituloLbl.Size = new Size(610, 40);
            this.TituloLbl.TextAlign = ContentAlignment.MiddleCenter;

            // Calendario
            this.FechaLbl.Text = "Fecha del Bloqueo:";
            this.FechaLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.FechaLbl.Location = new Point(40, 80);
            this.FechaLbl.Size = new Size(250, 25);

            this.FechaCalendar.Location = new Point(40, 110);
            this.FechaCalendar.MaxSelectionCount = 1;

            // Hora Inicio
            this.HoraInicioLbl.Text = "Hora Inicio (HH:mm):";
            this.HoraInicioLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.HoraInicioLbl.Location = new Point(340, 80);
            this.HoraInicioLbl.Size = new Size(250, 25);

            this.HoraInicioTxt.Font = new Font("Segoe UI", 11F);
            this.HoraInicioTxt.Location = new Point(340, 110);
            this.HoraInicioTxt.Size = new Size(250, 32);

            // Hora Fin
            this.HoraFinLbl.Text = "Hora Fin (HH:mm):";
            this.HoraFinLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.HoraFinLbl.Location = new Point(340, 160);
            this.HoraFinLbl.Size = new Size(250, 25);

            this.HoraFinTxt.Font = new Font("Segoe UI", 11F);
            this.HoraFinTxt.Location = new Point(340, 190);
            this.HoraFinTxt.Size = new Size(250, 32);

            // Administrador Responsable
            this.AdminLbl.Text = "Administrador Responsable:";
            this.AdminLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.AdminLbl.Location = new Point(340, 240);
            this.AdminLbl.Size = new Size(250, 25);

            this.AdminCombo.Font = new Font("Segoe UI", 11F);
            this.AdminCombo.Location = new Point(340, 270);
            this.AdminCombo.Size = new Size(250, 33);
            this.AdminCombo.DropDownStyle = ComboBoxStyle.DropDownList;

            // Motivo
            this.MotivoLbl.Text = "Motivo del Bloqueo:";
            this.MotivoLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.MotivoLbl.Location = new Point(40, 300);
            this.MotivoLbl.Size = new Size(550, 25);

            this.MotivoTxt.Font = new Font("Segoe UI", 11F);
            this.MotivoTxt.Location = new Point(40, 330);
            this.MotivoTxt.Size = new Size(550, 100);
            this.MotivoTxt.Multiline = true;

            // Guardar
            this.GuardarBtn.Text = "💾 Guardar Bloqueo";
            this.GuardarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.GuardarBtn.Size = new Size(250, 50);
            this.GuardarBtn.Location = new Point(40, 480);
            this.GuardarBtn.BackColor = Color.FromArgb(255, 140, 0);
            this.GuardarBtn.ForeColor = Color.White;
            this.GuardarBtn.FlatStyle = FlatStyle.Flat;
            this.GuardarBtn.FlatAppearance.BorderSize = 0;
            this.GuardarBtn.Cursor = Cursors.Hand;
            this.GuardarBtn.Click += new System.EventHandler(this.GuardarBtn_Click);

            // Cancelar
            this.CancelarBtn.Text = "❌ Cancelar";
            this.CancelarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.CancelarBtn.Size = new Size(250, 50);
            this.CancelarBtn.Location = new Point(340, 480);
            this.CancelarBtn.BackColor = Color.FromArgb(139, 90, 60);
            this.CancelarBtn.ForeColor = Color.White;
            this.CancelarBtn.FlatStyle = FlatStyle.Flat;
            this.CancelarBtn.FlatAppearance.BorderSize = 0;
            this.CancelarBtn.Cursor = Cursors.Hand;
            this.CancelarBtn.Click += new System.EventHandler(this.CancelarBtn_Click);

            this.FormPanel.Controls.Add(this.TituloLbl);
            this.FormPanel.Controls.Add(this.FechaLbl);
            this.FormPanel.Controls.Add(this.FechaCalendar);
            this.FormPanel.Controls.Add(this.HoraInicioLbl);
            this.FormPanel.Controls.Add(this.HoraInicioTxt);
            this.FormPanel.Controls.Add(this.HoraFinLbl);
            this.FormPanel.Controls.Add(this.HoraFinTxt);
            this.FormPanel.Controls.Add(this.AdminLbl);
            this.FormPanel.Controls.Add(this.AdminCombo);
            this.FormPanel.Controls.Add(this.MotivoLbl);
            this.FormPanel.Controls.Add(this.MotivoTxt);
            this.FormPanel.Controls.Add(this.GuardarBtn);
            this.FormPanel.Controls.Add(this.CancelarBtn);

            this.Controls.Add(this.FormPanel);
        }
    }
}