using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    partial class CrearEditarHorarioNuevoForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel FormPanel;
        private Label TituloLbl;
        private Label DiaLbl;
        private ComboBox DiaCombo;
        private Label HoraInicioLbl;
        private TextBox HoraInicioTxt;
        private Label HoraFinLbl;
        private TextBox HoraFinTxt;
        private Label ServicioLbl;
        private ComboBox ServicioCombo;
        private Label GrupoLbl;
        private ComboBox GrupoCombo;
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
            this.DiaLbl = new Label();
            this.DiaCombo = new ComboBox();
            this.HoraInicioLbl = new Label();
            this.HoraInicioTxt = new TextBox();
            this.HoraFinLbl = new Label();
            this.HoraFinTxt = new TextBox();
            this.ServicioLbl = new Label();
            this.ServicioCombo = new ComboBox();
            this.GrupoLbl = new Label();
            this.GrupoCombo = new ComboBox();
            this.GuardarBtn = new Button();
            this.CancelarBtn = new Button();

            this.FormPanel.SuspendLayout();
            this.SuspendLayout();

            // CrearEditarHorarioNuevoForm
            this.ClientSize = new Size(580, 560);
            this.Text = "Horario Semanal";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(250, 245, 240);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // FormPanel
            this.FormPanel.BackColor = Color.White;
            this.FormPanel.Location = new Point(30, 20);
            this.FormPanel.Size = new Size(520, 510);
            this.FormPanel.BorderStyle = BorderStyle.FixedSingle;

            // TituloLbl
            this.TituloLbl.Text = "Crear Horario Semanal";
            this.TituloLbl.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            this.TituloLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TituloLbl.Location = new Point(20, 20);
            this.TituloLbl.Size = new Size(480, 35);
            this.TituloLbl.TextAlign = ContentAlignment.MiddleCenter;

            // DiaLbl
            this.DiaLbl.Text = "Día de la Semana:";
            this.DiaLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.DiaLbl.Location = new Point(30, 75);
            this.DiaLbl.Size = new Size(460, 22);

            // DiaCombo
            this.DiaCombo.Location = new Point(30, 97);
            this.DiaCombo.Size = new Size(460, 30);
            this.DiaCombo.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.DiaCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.DiaCombo.Items.AddRange(new object[] {
                "lunes", "martes", "miercoles", "jueves", "viernes", "sabado", "domingo"
            });

            // HoraInicioLbl
            this.HoraInicioLbl.Text = "Hora Inicio (HH:mm:ss):";
            this.HoraInicioLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.HoraInicioLbl.Location = new Point(30, 148);
            this.HoraInicioLbl.Size = new Size(210, 22);

            // HoraInicioTxt
            this.HoraInicioTxt.Location = new Point(30, 170);
            this.HoraInicioTxt.Size = new Size(210, 30);
            this.HoraInicioTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.HoraInicioTxt.PlaceholderText = "09:00:00";

            // HoraFinLbl
            this.HoraFinLbl.Text = "Hora Fin (HH:mm:ss):";
            this.HoraFinLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.HoraFinLbl.Location = new Point(280, 148);
            this.HoraFinLbl.Size = new Size(210, 22);

            // HoraFinTxt
            this.HoraFinTxt.Location = new Point(280, 170);
            this.HoraFinTxt.Size = new Size(210, 30);
            this.HoraFinTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.HoraFinTxt.PlaceholderText = "14:00:00";

            // ServicioLbl
            this.ServicioLbl.Text = "Servicio:";
            this.ServicioLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.ServicioLbl.Location = new Point(30, 225);
            this.ServicioLbl.Size = new Size(460, 22);

            // ServicioCombo
            this.ServicioCombo.Location = new Point(30, 247);
            this.ServicioCombo.Size = new Size(460, 30);
            this.ServicioCombo.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.ServicioCombo.DropDownStyle = ComboBoxStyle.DropDownList;

            // GrupoLbl
            this.GrupoLbl.Text = "Grupo:";
            this.GrupoLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.GrupoLbl.Location = new Point(30, 300);
            this.GrupoLbl.Size = new Size(460, 22);

            // GrupoCombo
            this.GrupoCombo.Location = new Point(30, 322);
            this.GrupoCombo.Size = new Size(460, 30);
            this.GrupoCombo.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.GrupoCombo.DropDownStyle = ComboBoxStyle.DropDownList;

            // GuardarBtn
            this.GuardarBtn.Text = "💾 Guardar";
            this.GuardarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.GuardarBtn.Size = new Size(200, 50);
            this.GuardarBtn.Location = new Point(30, 420);
            this.GuardarBtn.BackColor = Color.FromArgb(255, 140, 0);
            this.GuardarBtn.ForeColor = Color.White;
            this.GuardarBtn.FlatStyle = FlatStyle.Flat;
            this.GuardarBtn.FlatAppearance.BorderSize = 0;
            this.GuardarBtn.Cursor = Cursors.Hand;
            this.GuardarBtn.Click += new System.EventHandler(this.GuardarBtn_Click);

            // CancelarBtn
            this.CancelarBtn.Text = "❌ Cancelar";
            this.CancelarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.CancelarBtn.Size = new Size(200, 50);
            this.CancelarBtn.Location = new Point(290, 420);
            this.CancelarBtn.BackColor = Color.FromArgb(139, 90, 60);
            this.CancelarBtn.ForeColor = Color.White;
            this.CancelarBtn.FlatStyle = FlatStyle.Flat;
            this.CancelarBtn.FlatAppearance.BorderSize = 0;
            this.CancelarBtn.Cursor = Cursors.Hand;
            this.CancelarBtn.Click += new System.EventHandler(this.CancelarBtn_Click);

            // Add controls to FormPanel
            this.FormPanel.Controls.Add(this.TituloLbl);
            this.FormPanel.Controls.Add(this.DiaLbl);
            this.FormPanel.Controls.Add(this.DiaCombo);
            this.FormPanel.Controls.Add(this.HoraInicioLbl);
            this.FormPanel.Controls.Add(this.HoraInicioTxt);
            this.FormPanel.Controls.Add(this.HoraFinLbl);
            this.FormPanel.Controls.Add(this.HoraFinTxt);
            this.FormPanel.Controls.Add(this.ServicioLbl);
            this.FormPanel.Controls.Add(this.ServicioCombo);
            this.FormPanel.Controls.Add(this.GrupoLbl);
            this.FormPanel.Controls.Add(this.GrupoCombo);
            this.FormPanel.Controls.Add(this.GuardarBtn);
            this.FormPanel.Controls.Add(this.CancelarBtn);

            this.Controls.Add(this.FormPanel);

            this.FormPanel.ResumeLayout(false);
            this.FormPanel.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}