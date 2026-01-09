using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    partial class LoginForm
    {
        private Panel FondoIzquierdoPanel;
        private Panel LoginPanel;
        private Label LogoLbl;
        private Label TituloLbl;
        private Label SubtituloLbl;
        private Label CorreoLbl;
        private TextBox CorreuTxt;
        private Label ContrasenaLbl;
        private TextBox ContrasenyaTxt;
        private Button IniciarSesionBoto;
        private Button RegistrarseBoto;
        private Label BienvenidaLbl;
        private Label InfoLbl;

        private void InitializeComponent()
        {
            this.FondoIzquierdoPanel = new Panel();
            this.LoginPanel = new Panel();
            this.LogoLbl = new Label();
            this.TituloLbl = new Label();
            this.SubtituloLbl = new Label();
            this.CorreoLbl = new Label();
            this.CorreuTxt = new TextBox();
            this.ContrasenaLbl = new Label();
            this.ContrasenyaTxt = new TextBox();
            this.IniciarSesionBoto = new Button();
            this.RegistrarseBoto = new Button();
            this.BienvenidaLbl = new Label();
            this.InfoLbl = new Label();

            // 
            // LoginForm
            // 
            this.ClientSize = new Size(1100, 700);
            this.Text = "Peluquería Escola - Iniciar Sesión";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(250, 245, 240);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // 
            // FondoIzquierdoPanel (Panel decorativo lateral)
            // 
            this.FondoIzquierdoPanel.Dock = DockStyle.Left;
            this.FondoIzquierdoPanel.Width = 450;
            this.FondoIzquierdoPanel.BackColor = Color.FromArgb(255, 140, 0);

            // 
            // BienvenidaLbl
            // 
            this.BienvenidaLbl.Text = "✂️\nPeluquería Escola";
            this.BienvenidaLbl.Font = new Font("Segoe UI", 28F, FontStyle.Bold, GraphicsUnit.Point);
            this.BienvenidaLbl.ForeColor = Color.White;
            this.BienvenidaLbl.AutoSize = false;
            this.BienvenidaLbl.Size = new Size(400, 150);
            this.BienvenidaLbl.Location = new Point(25, 200);
            this.BienvenidaLbl.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // InfoLbl
            // 
            this.InfoLbl.Text = "Sistema de Gestión y Reservas\nProfesional";
            this.InfoLbl.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            this.InfoLbl.ForeColor = Color.FromArgb(255, 250, 245);
            this.InfoLbl.AutoSize = false;
            this.InfoLbl.Size = new Size(400, 60);
            this.InfoLbl.Location = new Point(25, 360);
            this.InfoLbl.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // LoginPanel
            // 
            this.LoginPanel.BackColor = Color.White;
            this.LoginPanel.Size = new Size(480, 600);
            this.LoginPanel.Location = new Point(500, 50);

            // 
            // LogoLbl
            // 
            this.LogoLbl.Text = "✂";
            this.LogoLbl.Font = new Font("Segoe UI Emoji", 32F, FontStyle.Regular, GraphicsUnit.Point);
            this.LogoLbl.ForeColor = Color.FromArgb(255, 140, 0);
            this.LogoLbl.AutoSize = false;
            this.LogoLbl.TextAlign = ContentAlignment.MiddleCenter;
            this.LogoLbl.Size = new Size(480, 70);
            this.LogoLbl.Location = new Point(0, 30);

            // 
            // TituloLbl
            // 
            this.TituloLbl.Text = "Iniciar Sesión";
            this.TituloLbl.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
            this.TituloLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TituloLbl.AutoSize = false;
            this.TituloLbl.Size = new Size(480, 50);
            this.TituloLbl.Location = new Point(0, 110);
            this.TituloLbl.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // SubtituloLbl
            // 
            this.SubtituloLbl.Text = "Accede a tu cuenta";
            this.SubtituloLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.SubtituloLbl.ForeColor = Color.FromArgb(139, 90, 60);
            this.SubtituloLbl.AutoSize = false;
            this.SubtituloLbl.Size = new Size(480, 30);
            this.SubtituloLbl.Location = new Point(0, 160);
            this.SubtituloLbl.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // CorreoLbl
            // 
            this.CorreoLbl.Text = "Correo Electrónico";
            this.CorreoLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.CorreoLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.CorreoLbl.AutoSize = true;
            this.CorreoLbl.Location = new Point(60, 220);

            // 
            // CorreuTxt
            // 
            this.CorreuTxt.Name = "CorreuTxt";
            this.CorreuTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.CorreuTxt.Size = new Size(360, 32);
            this.CorreuTxt.Location = new Point(60, 250);
            this.CorreuTxt.BorderStyle = BorderStyle.FixedSingle;
            this.CorreuTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // ContrasenaLbl
            // 
            this.ContrasenaLbl.Text = "Contraseña";
            this.ContrasenaLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.ContrasenaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.ContrasenaLbl.AutoSize = true;
            this.ContrasenaLbl.Location = new Point(60, 310);

            // 
            // ContrasenyaTxt
            // 
            this.ContrasenyaTxt.Name = "ContrasenyaTxt";
            this.ContrasenyaTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.ContrasenyaTxt.Size = new Size(360, 32);
            this.ContrasenyaTxt.Location = new Point(60, 340);
            this.ContrasenyaTxt.PasswordChar = '●';
            this.ContrasenyaTxt.BorderStyle = BorderStyle.FixedSingle;
            this.ContrasenyaTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // IniciarSesionBoto
            // 
            this.IniciarSesionBoto.Name = "IniciarSesionBoto";
            this.IniciarSesionBoto.Text = "Iniciar Sesión";
            this.IniciarSesionBoto.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.IniciarSesionBoto.Size = new Size(360, 50);
            this.IniciarSesionBoto.Location = new Point(60, 410);
            this.IniciarSesionBoto.BackColor = Color.FromArgb(255, 140, 0);
            this.IniciarSesionBoto.ForeColor = Color.White;
            this.IniciarSesionBoto.FlatStyle = FlatStyle.Flat;
            this.IniciarSesionBoto.FlatAppearance.BorderSize = 0;
            this.IniciarSesionBoto.Cursor = Cursors.Hand;
            this.IniciarSesionBoto.Click += new System.EventHandler(this.IniciarSesionBoto_Click);

            // 
            // RegistrarseBoto
            // 
            this.RegistrarseBoto.Name = "RegistrarseBoto";
            this.RegistrarseBoto.Text = "¿No tienes cuenta? Regístrate";
            this.RegistrarseBoto.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.RegistrarseBoto.Size = new Size(360, 45);
            this.RegistrarseBoto.Location = new Point(60, 480);
            this.RegistrarseBoto.BackColor = Color.White;
            this.RegistrarseBoto.ForeColor = Color.FromArgb(255, 140, 0);
            this.RegistrarseBoto.FlatStyle = FlatStyle.Flat;
            this.RegistrarseBoto.FlatAppearance.BorderColor = Color.FromArgb(255, 140, 0);
            this.RegistrarseBoto.FlatAppearance.BorderSize = 2;
            this.RegistrarseBoto.Cursor = Cursors.Hand;
            this.RegistrarseBoto.Click += new System.EventHandler(this.RegistrarseBoto_Click);

            // 
            // Add controls to FondoIzquierdoPanel
            // 
            this.FondoIzquierdoPanel.Controls.Add(this.BienvenidaLbl);
            this.FondoIzquierdoPanel.Controls.Add(this.InfoLbl);

            // 
            // Add controls to LoginPanel
            // 
            this.LoginPanel.Controls.Add(this.LogoLbl);
            this.LoginPanel.Controls.Add(this.TituloLbl);
            this.LoginPanel.Controls.Add(this.SubtituloLbl);
            this.LoginPanel.Controls.Add(this.CorreoLbl);
            this.LoginPanel.Controls.Add(this.CorreuTxt);
            this.LoginPanel.Controls.Add(this.ContrasenaLbl);
            this.LoginPanel.Controls.Add(this.ContrasenyaTxt);
            this.LoginPanel.Controls.Add(this.IniciarSesionBoto);
            this.LoginPanel.Controls.Add(this.RegistrarseBoto);

            // 
            // Add controls to Form
            // 
            this.Controls.Add(this.LoginPanel);
            this.Controls.Add(this.FondoIzquierdoPanel);
        }
    }
}