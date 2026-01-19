using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    partial class CrearEditarHorarioForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Declaración de los controles que faltan
        private Panel FormPanel;
        private Label TituloLbl;

        private Label DiaLbl;
        private ComboBox DiaCombo;

        private Label HoraInicioLbl;
        private TextBox HoraInicioTxt;

        private Label HoraFinLbl;
        private TextBox HoraFinTxt;

        private Button GuardarBtn;
        private Button CancelarBtn;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
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
            this.GuardarBtn = new Button();
            this.CancelarBtn = new Button();

            this.FormPanel.SuspendLayout();
            this.SuspendLayout();

            // 
            // CrearEditarHorarioForm
            // 
            this.ClientSize = new Size(550, 450);
            this.Text = "Gestión de Horario";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(250, 245, 240);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // 
            // FormPanel
            // 
            this.FormPanel.BackColor = Color.White;
            this.FormPanel.Location = new Point(40, 30);
            this.FormPanel.Size = new Size(470, 380);
            this.FormPanel.TabIndex = 0;
            // CORRECCIÓN: Eliminada la lambda que causaba el error
            this.FormPanel.BorderStyle = BorderStyle.FixedSingle;

            // 
            // TituloLbl
            // 
            this.TituloLbl.Text = "Crear/Editar Horario";
            this.TituloLbl.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            this.TituloLbl.ForeColor = Color.FromArgb(50, 50, 50);
            this.TituloLbl.Location = new Point(20, 20);
            this.TituloLbl.Size = new Size(430, 35);
            this.TituloLbl.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // DiaLbl
            // 
            this.DiaLbl.Text = "Día de la Semana:";
            this.DiaLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.DiaLbl.Location = new Point(40, 80);
            this.DiaLbl.Size = new Size(390, 25);

            // 
            // DiaCombo
            // 
            this.DiaCombo.Location = new Point(40, 105);
            this.DiaCombo.Size = new Size(390, 30);
            this.DiaCombo.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.DiaCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.DiaCombo.Items.AddRange(new object[] {
                "lunes",
                "martes",
                "miercoles",
                "jueves",
                "viernes",
                "sabado",
                "domingo"
            });

            // 
            // HoraInicioLbl
            // 
            this.HoraInicioLbl.Text = "Hora Inicio (HH:mm:ss):";
            this.HoraInicioLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.HoraInicioLbl.Location = new Point(40, 160);
            this.HoraInicioLbl.Size = new Size(180, 25);

            // 
            // HoraInicioTxt
            // 
            this.HoraInicioTxt.Location = new Point(40, 185);
            this.HoraInicioTxt.Size = new Size(180, 30);
            this.HoraInicioTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.HoraInicioTxt.PlaceholderText = "09:00:00";

            // 
            // HoraFinLbl
            // 
            this.HoraFinLbl.Text = "Hora Fin (HH:mm:ss):";
            this.HoraFinLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.HoraFinLbl.Location = new Point(250, 160);
            this.HoraFinLbl.Size = new Size(180, 25);

            // 
            // HoraFinTxt
            // 
            this.HoraFinTxt.Location = new Point(250, 185);
            this.HoraFinTxt.Size = new Size(180, 30);
            this.HoraFinTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.HoraFinTxt.PlaceholderText = "14:00:00";

            // 
            // GuardarBtn
            // 
            this.GuardarBtn.Text = "💾 Guardar";
            this.GuardarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.GuardarBtn.Size = new Size(170, 50);
            this.GuardarBtn.Location = new Point(40, 280);
            this.GuardarBtn.BackColor = Color.FromArgb(255, 140, 0);
            this.GuardarBtn.ForeColor = Color.White;
            this.GuardarBtn.FlatStyle = FlatStyle.Flat;
            this.GuardarBtn.FlatAppearance.BorderSize = 0;
            this.GuardarBtn.Cursor = Cursors.Hand;
            this.GuardarBtn.Click += new System.EventHandler(this.GuardarBtn_Click);

            // 
            // CancelarBtn
            // 
            this.CancelarBtn.Text = "❌ Cancelar";
            this.CancelarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.CancelarBtn.Size = new Size(170, 50);
            this.CancelarBtn.Location = new Point(260, 280);
            this.CancelarBtn.BackColor = Color.FromArgb(139, 90, 60);
            this.CancelarBtn.ForeColor = Color.White;
            this.CancelarBtn.FlatStyle = FlatStyle.Flat;
            this.CancelarBtn.FlatAppearance.BorderSize = 0;
            this.CancelarBtn.Cursor = Cursors.Hand;
            this.CancelarBtn.Click += new System.EventHandler(this.CancelarBtn_Click);

            // 
            // Add controls to FormPanel
            // 
            this.FormPanel.Controls.Add(this.TituloLbl);
            this.FormPanel.Controls.Add(this.DiaLbl);
            this.FormPanel.Controls.Add(this.DiaCombo);
            this.FormPanel.Controls.Add(this.HoraInicioLbl);
            this.FormPanel.Controls.Add(this.HoraInicioTxt);
            this.FormPanel.Controls.Add(this.HoraFinLbl);
            this.FormPanel.Controls.Add(this.HoraFinTxt);
            this.FormPanel.Controls.Add(this.GuardarBtn);
            this.FormPanel.Controls.Add(this.CancelarBtn);

            this.Controls.Add(this.FormPanel);

            this.FormPanel.ResumeLayout(false);
            this.FormPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
    }
}