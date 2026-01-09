using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    partial class CrearEditarGrupoForm
    {
        private Label TituloLbl;
        private Label CursoLbl;
        private TextBox CursoTxt;
        private Label EmailLbl;
        private TextBox EmailTxt;
        private Label ContrasenaLbl;
        private TextBox ContrasenaTxt;
        private Label TurnoLbl;
        private ComboBox TurnoCombo;
        private Button GuardarBtn;
        private Button CancelarBtn;
        private Panel FormPanel;

        private void InitializeComponent()
        {
            this.FormPanel = new Panel();
            this.TituloLbl = new Label();
            this.CursoLbl = new Label();
            this.CursoTxt = new TextBox();
            this.EmailLbl = new Label();
            this.EmailTxt = new TextBox();
            this.ContrasenaLbl = new Label();
            this.ContrasenaTxt = new TextBox();
            this.TurnoLbl = new Label();
            this.TurnoCombo = new ComboBox();
            this.GuardarBtn = new Button();
            this.CancelarBtn = new Button();

            // 
            // CrearEditarGrupoForm
            // 
            this.ClientSize = new Size(600, 500);
            this.Text = "Gestión de Grupo";
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
            this.FormPanel.Size = new Size(500, 440);

            // 
            // TituloLbl
            // 
            this.TituloLbl.Text = "Crear Nuevo Grupo";
            this.TituloLbl.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            this.TituloLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TituloLbl.AutoSize = false;
            this.TituloLbl.Size = new Size(500, 50);
            this.TituloLbl.TextAlign = ContentAlignment.MiddleCenter;
            this.TituloLbl.Location = new Point(0, 20);

            // 
            // CursoLbl
            // 
            this.CursoLbl.Text = "Curso *";
            this.CursoLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.CursoLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.CursoLbl.AutoSize = true;
            this.CursoLbl.Location = new Point(50, 90);

            // 
            // CursoTxt
            // 
            this.CursoTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.CursoTxt.Size = new Size(400, 32);
            this.CursoTxt.Location = new Point(50, 115);
            this.CursoTxt.BorderStyle = BorderStyle.FixedSingle;
            this.CursoTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // EmailLbl
            // 
            this.EmailLbl.Text = "Email *";
            this.EmailLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.EmailLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.EmailLbl.AutoSize = true;
            this.EmailLbl.Location = new Point(50, 160);

            // 
            // EmailTxt
            // 
            this.EmailTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.EmailTxt.Size = new Size(400, 32);
            this.EmailTxt.Location = new Point(50, 185);
            this.EmailTxt.BorderStyle = BorderStyle.FixedSingle;
            this.EmailTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // ContrasenaLbl
            // 
            this.ContrasenaLbl.Text = "Contraseña *";
            this.ContrasenaLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.ContrasenaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.ContrasenaLbl.AutoSize = true;
            this.ContrasenaLbl.Location = new Point(50, 230);

            // 
            // ContrasenaTxt
            // 
            this.ContrasenaTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.ContrasenaTxt.Size = new Size(400, 32);
            this.ContrasenaTxt.Location = new Point(50, 255);
            this.ContrasenaTxt.BorderStyle = BorderStyle.FixedSingle;
            this.ContrasenaTxt.BackColor = Color.FromArgb(250, 245, 240);
            this.ContrasenaTxt.PasswordChar = '●';

            // 
            // TurnoLbl
            // 
            this.TurnoLbl.Text = "Turno *";
            this.TurnoLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.TurnoLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TurnoLbl.AutoSize = true;
            this.TurnoLbl.Location = new Point(50, 300);

            // 
            // TurnoCombo
            // 
            this.TurnoCombo.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.TurnoCombo.Size = new Size(400, 32);
            this.TurnoCombo.Location = new Point(50, 325);
            this.TurnoCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.TurnoCombo.BackColor = Color.FromArgb(250, 245, 240);
            this.TurnoCombo.Items.AddRange(new object[] { "Manana", "Tarde" });
            this.TurnoCombo.SelectedIndex = 0;

            // 
            // GuardarBtn
            // 
            this.GuardarBtn.Text = "💾 Guardar";
            this.GuardarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.GuardarBtn.Size = new Size(180, 50);
            this.GuardarBtn.Location = new Point(80, 380);
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
            this.CancelarBtn.Location = new Point(280, 380);
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
            this.FormPanel.Controls.Add(this.CursoLbl);
            this.FormPanel.Controls.Add(this.CursoTxt);
            this.FormPanel.Controls.Add(this.EmailLbl);
            this.FormPanel.Controls.Add(this.EmailTxt);
            this.FormPanel.Controls.Add(this.ContrasenaLbl);
            this.FormPanel.Controls.Add(this.ContrasenaTxt);
            this.FormPanel.Controls.Add(this.TurnoLbl);
            this.FormPanel.Controls.Add(this.TurnoCombo);
            this.FormPanel.Controls.Add(this.GuardarBtn);
            this.FormPanel.Controls.Add(this.CancelarBtn);

            // 
            // Add controls to Form
            // 
            this.Controls.Add(this.FormPanel);
        }
    }
}