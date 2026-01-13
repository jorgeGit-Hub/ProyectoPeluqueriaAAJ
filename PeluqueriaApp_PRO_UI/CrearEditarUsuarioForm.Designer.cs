using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    partial class CrearEditarUsuarioForm
    {
        private Label TituloLbl;
        private Label NombreLbl;
        private TextBox NombreTxt;
        private Label ApellidosLbl;
        private TextBox ApellidosTxt;
        private Label CorreoLbl;
        private TextBox CorreoTxt;
        private Label ContrasenaLbl;
        private TextBox ContrasenaTxt;
        private Label RolLbl;
        private ComboBox RolCombo;
        private Label EspecialidadLbl;
        private TextBox EspecialidadTxt;
        private Button GuardarBtn;
        private Button CancelarBtn;
        private Panel FormPanel;

        private void InitializeComponent()
        {
            this.FormPanel = new Panel();
            this.TituloLbl = new Label();
            this.NombreLbl = new Label();
            this.NombreTxt = new TextBox();
            this.ApellidosLbl = new Label();
            this.ApellidosTxt = new TextBox();
            this.CorreoLbl = new Label();
            this.CorreoTxt = new TextBox();
            this.ContrasenaLbl = new Label();
            this.ContrasenaTxt = new TextBox();
            this.RolLbl = new Label();
            this.RolCombo = new ComboBox();
            this.EspecialidadLbl = new Label();
            this.EspecialidadTxt = new TextBox();
            this.GuardarBtn = new Button();
            this.CancelarBtn = new Button();

            // 
            // CrearEditarUsuarioForm
            // 
            this.ClientSize = new Size(600, 650);
            this.Text = "Gestión de Usuario";
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

            // 
            // TituloLbl
            // 
            this.TituloLbl.Text = "Crear Nuevo Usuario";
            this.TituloLbl.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            this.TituloLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TituloLbl.AutoSize = false;
            this.TituloLbl.Size = new Size(500, 50);
            this.TituloLbl.TextAlign = ContentAlignment.MiddleCenter;
            this.TituloLbl.Location = new Point(0, 20);

            // 
            // NombreLbl
            // 
            this.NombreLbl.Text = "Nombre *";
            this.NombreLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.NombreLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.NombreLbl.AutoSize = true;
            this.NombreLbl.Location = new Point(50, 90);

            // 
            // NombreTxt
            // 
            this.NombreTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.NombreTxt.Size = new Size(400, 32);
            this.NombreTxt.Location = new Point(50, 115);
            this.NombreTxt.BorderStyle = BorderStyle.FixedSingle;
            this.NombreTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // ApellidosLbl
            // 
            this.ApellidosLbl.Text = "Apellidos *";
            this.ApellidosLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.ApellidosLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.ApellidosLbl.AutoSize = true;
            this.ApellidosLbl.Location = new Point(50, 160);

            // 
            // ApellidosTxt
            // 
            this.ApellidosTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.ApellidosTxt.Size = new Size(400, 32);
            this.ApellidosTxt.Location = new Point(50, 185);
            this.ApellidosTxt.BorderStyle = BorderStyle.FixedSingle;
            this.ApellidosTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // CorreoLbl
            // 
            this.CorreoLbl.Text = "Correo Electrónico *";
            this.CorreoLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.CorreoLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.CorreoLbl.AutoSize = true;
            this.CorreoLbl.Location = new Point(50, 230);

            // 
            // CorreoTxt
            // 
            this.CorreoTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.CorreoTxt.Size = new Size(400, 32);
            this.CorreoTxt.Location = new Point(50, 255);
            this.CorreoTxt.BorderStyle = BorderStyle.FixedSingle;
            this.CorreoTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // ContrasenaLbl
            // 
            this.ContrasenaLbl.Text = "Contraseña *";
            this.ContrasenaLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.ContrasenaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.ContrasenaLbl.AutoSize = true;
            this.ContrasenaLbl.Location = new Point(50, 300);

            // 
            // ContrasenaTxt
            // 
            this.ContrasenaTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.ContrasenaTxt.Size = new Size(400, 32);
            this.ContrasenaTxt.Location = new Point(50, 325);
            this.ContrasenaTxt.BorderStyle = BorderStyle.FixedSingle;
            this.ContrasenaTxt.BackColor = Color.FromArgb(250, 245, 240);
            this.ContrasenaTxt.PasswordChar = '●';

            // 
            // RolLbl
            // 
            this.RolLbl.Text = "Rol *";
            this.RolLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.RolLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.RolLbl.AutoSize = true;
            this.RolLbl.Location = new Point(50, 370);

            // 
            // RolCombo
            // 
            this.RolCombo.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.RolCombo.Size = new Size(400, 32);
            this.RolCombo.Location = new Point(50, 395);
            this.RolCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.RolCombo.BackColor = Color.FromArgb(250, 245, 240);
            this.RolCombo.Items.AddRange(new object[] { "Administrador", "Alumno", "Cliente" });
            this.RolCombo.SelectedIndex = 1; // Alumno por defecto

            // 
            // EspecialidadLbl
            // 
            this.EspecialidadLbl.Text = "Especialidad *";
            this.EspecialidadLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.EspecialidadLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.EspecialidadLbl.AutoSize = true;
            this.EspecialidadLbl.Location = new Point(50, 440);
            this.EspecialidadLbl.Visible = false;

            // 
            // EspecialidadTxt
            // 
            this.EspecialidadTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.EspecialidadTxt.Size = new Size(400, 32);
            this.EspecialidadTxt.Location = new Point(50, 465);
            this.EspecialidadTxt.BorderStyle = BorderStyle.FixedSingle;
            this.EspecialidadTxt.BackColor = Color.FromArgb(250, 245, 240);
            this.EspecialidadTxt.Visible = false;

            // 
            // GuardarBtn
            // 
            this.GuardarBtn.Text = "💾 Guardar";
            this.GuardarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.GuardarBtn.Size = new Size(180, 50);
            this.GuardarBtn.Location = new Point(80, 520);
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
            this.CancelarBtn.Location = new Point(280, 520);
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
            this.FormPanel.Controls.Add(this.ApellidosLbl);
            this.FormPanel.Controls.Add(this.ApellidosTxt);
            this.FormPanel.Controls.Add(this.CorreoLbl);
            this.FormPanel.Controls.Add(this.CorreoTxt);
            this.FormPanel.Controls.Add(this.ContrasenaLbl);
            this.FormPanel.Controls.Add(this.ContrasenaTxt);
            this.FormPanel.Controls.Add(this.RolLbl);
            this.FormPanel.Controls.Add(this.RolCombo);
            this.FormPanel.Controls.Add(this.EspecialidadLbl);
            this.FormPanel.Controls.Add(this.EspecialidadTxt);
            this.FormPanel.Controls.Add(this.GuardarBtn);
            this.FormPanel.Controls.Add(this.CancelarBtn);

            // 
            // Add controls to Form
            // 
            this.Controls.Add(this.FormPanel);
        }
    }
}