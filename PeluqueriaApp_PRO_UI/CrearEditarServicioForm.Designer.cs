using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    // CAMBIO IMPORTANTE: El nombre de la clase ahora es CrearEditarServicioForm
    partial class CrearEditarServicioForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Declaración de controles necesarios para la lógica
        private Panel FormPanel;
        private Label TituloLbl;

        private Label NombreLbl;
        private TextBox NombreTxt;

        private Label ModuloLbl;
        private TextBox ModuloTxt;

        private Label AulaLbl;
        private TextBox AulaTxt;

        private Label TiempoClienteLbl;
        private TextBox TiempoClienteTxt;

        private Label PrecioLbl;
        private TextBox PrecioTxt;

        private Label GrupoLbl;
        private ComboBox GrupoCombo;

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
            this.NombreLbl = new Label();
            this.NombreTxt = new TextBox();
            this.ModuloLbl = new Label();
            this.ModuloTxt = new TextBox();
            this.AulaLbl = new Label();
            this.AulaTxt = new TextBox();
            this.TiempoClienteLbl = new Label();
            this.TiempoClienteTxt = new TextBox();
            this.PrecioLbl = new Label();
            this.PrecioTxt = new TextBox();
            this.GrupoLbl = new Label();
            this.GrupoCombo = new ComboBox();
            this.GuardarBtn = new Button();
            this.CancelarBtn = new Button();

            this.FormPanel.SuspendLayout();
            this.SuspendLayout();

            // 
            // CrearEditarServicioForm
            // 
            this.ClientSize = new Size(600, 650);
            this.Text = "Gestión de Servicio";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(250, 245, 240);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // 
            // FormPanel
            // 
            this.FormPanel.BackColor = Color.White;
            this.FormPanel.Location = new Point(50, 30);
            this.FormPanel.Size = new Size(500, 580);
            this.FormPanel.TabIndex = 0;
            // CORRECCIÓN: Usamos una propiedad estándar en lugar de lambda para evitar error del designer
            this.FormPanel.BorderStyle = BorderStyle.FixedSingle;

            // 
            // TituloLbl
            // 
            this.TituloLbl.Text = "Crear/Editar Servicio";
            this.TituloLbl.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            this.TituloLbl.ForeColor = Color.FromArgb(50, 50, 50);
            this.TituloLbl.Location = new Point(20, 20);
            this.TituloLbl.Size = new Size(460, 40);
            this.TituloLbl.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // NombreLbl
            // 
            this.NombreLbl.Text = "Nombre del Servicio:";
            this.NombreLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.NombreLbl.Location = new Point(40, 80);
            this.NombreLbl.Size = new Size(420, 25);

            // 
            // NombreTxt
            // 
            this.NombreTxt.Location = new Point(40, 105);
            this.NombreTxt.Size = new Size(420, 30);
            this.NombreTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);

            // 
            // ModuloLbl
            // 
            this.ModuloLbl.Text = "Módulo:";
            this.ModuloLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.ModuloLbl.Location = new Point(40, 145);
            this.ModuloLbl.Size = new Size(200, 25);

            // 
            // ModuloTxt
            // 
            this.ModuloTxt.Location = new Point(40, 170);
            this.ModuloTxt.Size = new Size(200, 30);
            this.ModuloTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);

            // 
            // AulaLbl
            // 
            this.AulaLbl.Text = "Aula:";
            this.AulaLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.AulaLbl.Location = new Point(260, 145);
            this.AulaLbl.Size = new Size(200, 25);

            // 
            // AulaTxt
            // 
            this.AulaTxt.Location = new Point(260, 170);
            this.AulaTxt.Size = new Size(200, 30);
            this.AulaTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);

            // 
            // TiempoClienteLbl
            // 
            this.TiempoClienteLbl.Text = "Tiempo Estimado (ej: 45', 1h):";
            this.TiempoClienteLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.TiempoClienteLbl.Location = new Point(40, 210);
            this.TiempoClienteLbl.Size = new Size(200, 25);

            // 
            // TiempoClienteTxt
            // 
            this.TiempoClienteTxt.Location = new Point(40, 235);
            this.TiempoClienteTxt.Size = new Size(200, 30);
            this.TiempoClienteTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);

            // 
            // PrecioLbl
            // 
            this.PrecioLbl.Text = "Precio (€):";
            this.PrecioLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.PrecioLbl.Location = new Point(260, 210);
            this.PrecioLbl.Size = new Size(200, 25);

            // 
            // PrecioTxt
            // 
            this.PrecioTxt.Location = new Point(260, 235);
            this.PrecioTxt.Size = new Size(200, 30);
            this.PrecioTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);

            // 
            // GrupoLbl
            // 
            this.GrupoLbl.Text = "Grupo Asignado:";
            this.GrupoLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.GrupoLbl.Location = new Point(40, 275);
            this.GrupoLbl.Size = new Size(420, 25);

            // 
            // GrupoCombo
            // 
            this.GrupoCombo.Location = new Point(40, 300);
            this.GrupoCombo.Size = new Size(420, 30);
            this.GrupoCombo.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.GrupoCombo.DropDownStyle = ComboBoxStyle.DropDownList;

            // 
            // GuardarBtn
            // 
            this.GuardarBtn.Text = "💾 Guardar";
            this.GuardarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.GuardarBtn.Size = new Size(180, 50);
            this.GuardarBtn.Location = new Point(40, 480);
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
            this.CancelarBtn.Size = new Size(180, 50);
            this.CancelarBtn.Location = new Point(280, 480);
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
            this.FormPanel.Controls.Add(this.NombreLbl);
            this.FormPanel.Controls.Add(this.NombreTxt);
            this.FormPanel.Controls.Add(this.ModuloLbl);
            this.FormPanel.Controls.Add(this.ModuloTxt);
            this.FormPanel.Controls.Add(this.AulaLbl);
            this.FormPanel.Controls.Add(this.AulaTxt);
            this.FormPanel.Controls.Add(this.TiempoClienteLbl);
            this.FormPanel.Controls.Add(this.TiempoClienteTxt);
            this.FormPanel.Controls.Add(this.PrecioLbl);
            this.FormPanel.Controls.Add(this.PrecioTxt);
            this.FormPanel.Controls.Add(this.GrupoLbl);
            this.FormPanel.Controls.Add(this.GrupoCombo);
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