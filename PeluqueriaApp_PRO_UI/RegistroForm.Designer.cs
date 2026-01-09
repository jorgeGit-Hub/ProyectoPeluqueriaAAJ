using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    partial class RegistroForm
    {
        private Panel FondoIzquierdoPanel;
        private Panel RegistroPanel;
        private Label LogoLbl;
        private Label TituloLbl;
        private Label SubtituloLbl;
        private Label NombreLbl;
        private TextBox NombreTxt;
        private Label ApellidosLbl;
        private TextBox ApellidosTxt;
        private Label CorreoLbl;
        private TextBox CorreoTxt;
        private Label ContrasenaLbl;
        private TextBox ContrasenaTxt;
        private Label ConfirmarContrasenaLbl;
        private TextBox ConfirmarContrasenaTxt;
        private Label RolLbl;
        private ComboBox RolCombo;

        // Campos específicos para CLIENTE
        private Label TelefonoLbl;
        private TextBox TelefonoTxt;
        private Label DireccionLbl;
        private TextBox DireccionTxt;
        private Label AlergenosLbl;
        private TextBox AlergenosTxt;
        private Label ObservacionesLbl;
        private TextBox ObservacionesTxt;

        private Button RegistrarseBtn;
        private Button VolverLoginBtn;
        private Label BienvenidaLbl;
        private Label InfoLbl;

        private void InitializeComponent()
        {
            this.FondoIzquierdoPanel = new Panel();
            this.RegistroPanel = new Panel();
            this.LogoLbl = new Label();
            this.TituloLbl = new Label();
            this.SubtituloLbl = new Label();
            this.NombreLbl = new Label();
            this.NombreTxt = new TextBox();
            this.ApellidosLbl = new Label();
            this.ApellidosTxt = new TextBox();
            this.CorreoLbl = new Label();
            this.CorreoTxt = new TextBox();
            this.ContrasenaLbl = new Label();
            this.ContrasenaTxt = new TextBox();
            this.ConfirmarContrasenaLbl = new Label();
            this.ConfirmarContrasenaTxt = new TextBox();
            this.RolLbl = new Label();
            this.RolCombo = new ComboBox();

            // Campos de cliente
            this.TelefonoLbl = new Label();
            this.TelefonoTxt = new TextBox();
            this.DireccionLbl = new Label();
            this.DireccionTxt = new TextBox();
            this.AlergenosLbl = new Label();
            this.AlergenosTxt = new TextBox();
            this.ObservacionesLbl = new Label();
            this.ObservacionesTxt = new TextBox();

            this.RegistrarseBtn = new Button();
            this.VolverLoginBtn = new Button();
            this.BienvenidaLbl = new Label();
            this.InfoLbl = new Label();

            // 
            // RegistroForm
            // 
            this.ClientSize = new Size(1200, 800);
            this.Text = "Peluquería Escola - Registro";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(250, 245, 240);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // 
            // FondoIzquierdoPanel
            // 
            this.FondoIzquierdoPanel.Dock = DockStyle.Left;
            this.FondoIzquierdoPanel.Width = 400;
            this.FondoIzquierdoPanel.BackColor = Color.FromArgb(255, 140, 0);

            // 
            // BienvenidaLbl
            // 
            this.BienvenidaLbl.Text = "✂️\nPeluquería Escola";
            this.BienvenidaLbl.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
            this.BienvenidaLbl.ForeColor = Color.White;
            this.BienvenidaLbl.AutoSize = false;
            this.BienvenidaLbl.Size = new Size(350, 120);
            this.BienvenidaLbl.Location = new Point(25, 250);
            this.BienvenidaLbl.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // InfoLbl
            // 
            this.InfoLbl.Text = "Crea tu cuenta\ny empieza a disfrutar\nde nuestros servicios";
            this.InfoLbl.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            this.InfoLbl.ForeColor = Color.FromArgb(255, 250, 245);
            this.InfoLbl.AutoSize = false;
            this.InfoLbl.Size = new Size(350, 80);
            this.InfoLbl.Location = new Point(25, 380);
            this.InfoLbl.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // RegistroPanel
            // 
            this.RegistroPanel.BackColor = Color.White;
            this.RegistroPanel.Size = new Size(700, 750);
            this.RegistroPanel.Location = new Point(450, 25);
            this.RegistroPanel.AutoScroll = true;

            // 
            // LogoLbl
            // 
            this.LogoLbl.Text = "✂";
            this.LogoLbl.Font = new Font("Segoe UI Emoji", 28F, FontStyle.Regular, GraphicsUnit.Point);
            this.LogoLbl.ForeColor = Color.FromArgb(255, 140, 0);
            this.LogoLbl.AutoSize = false;
            this.LogoLbl.TextAlign = ContentAlignment.MiddleCenter;
            this.LogoLbl.Size = new Size(700, 60);
            this.LogoLbl.Location = new Point(0, 20);

            // 
            // TituloLbl
            // 
            this.TituloLbl.Text = "Crear Cuenta";
            this.TituloLbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            this.TituloLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TituloLbl.AutoSize = false;
            this.TituloLbl.Size = new Size(700, 40);
            this.TituloLbl.Location = new Point(0, 85);
            this.TituloLbl.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // SubtituloLbl
            // 
            this.SubtituloLbl.Text = "Completa el formulario para registrarte";
            this.SubtituloLbl.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.SubtituloLbl.ForeColor = Color.FromArgb(139, 90, 60);
            this.SubtituloLbl.AutoSize = false;
            this.SubtituloLbl.Size = new Size(700, 25);
            this.SubtituloLbl.Location = new Point(0, 125);
            this.SubtituloLbl.TextAlign = ContentAlignment.MiddleCenter;

            // Configurar campos en dos columnas
            int columna1X = 50;
            int columna2X = 370;
            int inicioY = 170;
            int espacioY = 60;

            // ========== CAMPOS BÁSICOS (SIEMPRE VISIBLES) ==========

            // 
            // NombreLbl y NombreTxt
            // 
            this.NombreLbl.Text = "Nombre *";
            this.NombreLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.NombreLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.NombreLbl.AutoSize = true;
            this.NombreLbl.Location = new Point(columna1X, inicioY);

            this.NombreTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.NombreTxt.Size = new Size(280, 27);
            this.NombreTxt.Location = new Point(columna1X, inicioY + 20);
            this.NombreTxt.BorderStyle = BorderStyle.FixedSingle;
            this.NombreTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // ApellidosLbl y ApellidosTxt
            // 
            this.ApellidosLbl.Text = "Apellidos *";
            this.ApellidosLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.ApellidosLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.ApellidosLbl.AutoSize = true;
            this.ApellidosLbl.Location = new Point(columna2X, inicioY);

            this.ApellidosTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.ApellidosTxt.Size = new Size(280, 27);
            this.ApellidosTxt.Location = new Point(columna2X, inicioY + 20);
            this.ApellidosTxt.BorderStyle = BorderStyle.FixedSingle;
            this.ApellidosTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // CorreoLbl y CorreoTxt
            // 
            this.CorreoLbl.Text = "Correo Electrónico *";
            this.CorreoLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.CorreoLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.CorreoLbl.AutoSize = true;
            this.CorreoLbl.Location = new Point(columna1X, inicioY + espacioY);

            this.CorreoTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.CorreoTxt.Size = new Size(600, 27);
            this.CorreoTxt.Location = new Point(columna1X, inicioY + espacioY + 20);
            this.CorreoTxt.BorderStyle = BorderStyle.FixedSingle;
            this.CorreoTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // ContrasenaLbl y ContrasenaTxt
            // 
            this.ContrasenaLbl.Text = "Contraseña *";
            this.ContrasenaLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.ContrasenaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.ContrasenaLbl.AutoSize = true;
            this.ContrasenaLbl.Location = new Point(columna1X, inicioY + espacioY * 2);

            this.ContrasenaTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.ContrasenaTxt.Size = new Size(280, 27);
            this.ContrasenaTxt.Location = new Point(columna1X, inicioY + espacioY * 2 + 20);
            this.ContrasenaTxt.PasswordChar = '●';
            this.ContrasenaTxt.BorderStyle = BorderStyle.FixedSingle;
            this.ContrasenaTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // ConfirmarContrasenaLbl y ConfirmarContrasenaTxt
            // 
            this.ConfirmarContrasenaLbl.Text = "Confirmar Contraseña *";
            this.ConfirmarContrasenaLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.ConfirmarContrasenaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.ConfirmarContrasenaLbl.AutoSize = true;
            this.ConfirmarContrasenaLbl.Location = new Point(columna2X, inicioY + espacioY * 2);

            this.ConfirmarContrasenaTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.ConfirmarContrasenaTxt.Size = new Size(280, 27);
            this.ConfirmarContrasenaTxt.Location = new Point(columna2X, inicioY + espacioY * 2 + 20);
            this.ConfirmarContrasenaTxt.PasswordChar = '●';
            this.ConfirmarContrasenaTxt.BorderStyle = BorderStyle.FixedSingle;
            this.ConfirmarContrasenaTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // RolLbl y RolCombo
            // 
            this.RolLbl.Text = "Rol *";
            this.RolLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.RolLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.RolLbl.AutoSize = true;
            this.RolLbl.Location = new Point(columna1X, inicioY + espacioY * 3);

            this.RolCombo.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.RolCombo.Size = new Size(280, 27);
            this.RolCombo.Location = new Point(columna1X, inicioY + espacioY * 3 + 20);
            this.RolCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.RolCombo.BackColor = Color.FromArgb(250, 245, 240);
            this.RolCombo.Items.AddRange(new object[] { "Administrador", "Alumno", "Cliente" });
            this.RolCombo.SelectedIndex = 2; // Cliente por defecto

            // ========== CAMPOS ESPECÍFICOS DE CLIENTE ==========

            // 
            // TelefonoLbl y TelefonoTxt
            // 
            this.TelefonoLbl.Text = "Teléfono";
            this.TelefonoLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.TelefonoLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TelefonoLbl.AutoSize = true;
            this.TelefonoLbl.Location = new Point(columna1X, inicioY + espacioY * 4);

            this.TelefonoTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.TelefonoTxt.Size = new Size(280, 27);
            this.TelefonoTxt.Location = new Point(columna1X, inicioY + espacioY * 4 + 20);
            this.TelefonoTxt.BorderStyle = BorderStyle.FixedSingle;
            this.TelefonoTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // DireccionLbl y DireccionTxt
            // 
            this.DireccionLbl.Text = "Dirección";
            this.DireccionLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.DireccionLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.DireccionLbl.AutoSize = true;
            this.DireccionLbl.Location = new Point(columna2X, inicioY + espacioY * 4);

            this.DireccionTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.DireccionTxt.Size = new Size(280, 27);
            this.DireccionTxt.Location = new Point(columna2X, inicioY + espacioY * 4 + 20);
            this.DireccionTxt.BorderStyle = BorderStyle.FixedSingle;
            this.DireccionTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // AlergenosLbl y AlergenosTxt
            // 
            this.AlergenosLbl.Text = "Alérgenos";
            this.AlergenosLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.AlergenosLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.AlergenosLbl.AutoSize = true;
            this.AlergenosLbl.Location = new Point(columna1X, inicioY + espacioY * 5);

            this.AlergenosTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.AlergenosTxt.Size = new Size(600, 27);
            this.AlergenosTxt.Location = new Point(columna1X, inicioY + espacioY * 5 + 20);
            this.AlergenosTxt.BorderStyle = BorderStyle.FixedSingle;
            this.AlergenosTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // ObservacionesLbl y ObservacionesTxt
            // 
            this.ObservacionesLbl.Text = "Observaciones";
            this.ObservacionesLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.ObservacionesLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.ObservacionesLbl.AutoSize = true;
            this.ObservacionesLbl.Location = new Point(columna1X, inicioY + espacioY * 6);

            this.ObservacionesTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.ObservacionesTxt.Size = new Size(600, 27);
            this.ObservacionesTxt.Location = new Point(columna1X, inicioY + espacioY * 6 + 20);
            this.ObservacionesTxt.BorderStyle = BorderStyle.FixedSingle;
            this.ObservacionesTxt.BackColor = Color.FromArgb(250, 245, 240);

            // ========== BOTONES ==========

            // 
            // RegistrarseBtn
            // 
            this.RegistrarseBtn.Text = "Registrarse";
            this.RegistrarseBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.RegistrarseBtn.Size = new Size(600, 45);
            this.RegistrarseBtn.Location = new Point(50, inicioY + espacioY * 7 + 20);
            this.RegistrarseBtn.BackColor = Color.FromArgb(255, 140, 0);
            this.RegistrarseBtn.ForeColor = Color.White;
            this.RegistrarseBtn.FlatStyle = FlatStyle.Flat;
            this.RegistrarseBtn.FlatAppearance.BorderSize = 0;
            this.RegistrarseBtn.Cursor = Cursors.Hand;
            this.RegistrarseBtn.Click += new System.EventHandler(this.RegistrarseBtn_Click);

            // 
            // VolverLoginBtn
            // 
            this.VolverLoginBtn.Text = "¿Ya tienes cuenta? Inicia sesión";
            this.VolverLoginBtn.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.VolverLoginBtn.Size = new Size(600, 40);
            this.VolverLoginBtn.Location = new Point(50, inicioY + espacioY * 7 + 75);
            this.VolverLoginBtn.BackColor = Color.White;
            this.VolverLoginBtn.ForeColor = Color.FromArgb(255, 140, 0);
            this.VolverLoginBtn.FlatStyle = FlatStyle.Flat;
            this.VolverLoginBtn.FlatAppearance.BorderColor = Color.FromArgb(255, 140, 0);
            this.VolverLoginBtn.FlatAppearance.BorderSize = 2;
            this.VolverLoginBtn.Cursor = Cursors.Hand;
            this.VolverLoginBtn.Click += new System.EventHandler(this.VolverLoginBtn_Click);

            // 
            // Add controls to FondoIzquierdoPanel
            // 
            this.FondoIzquierdoPanel.Controls.Add(this.BienvenidaLbl);
            this.FondoIzquierdoPanel.Controls.Add(this.InfoLbl);

            // 
            // Add controls to RegistroPanel
            // 
            this.RegistroPanel.Controls.Add(this.LogoLbl);
            this.RegistroPanel.Controls.Add(this.TituloLbl);
            this.RegistroPanel.Controls.Add(this.SubtituloLbl);
            this.RegistroPanel.Controls.Add(this.NombreLbl);
            this.RegistroPanel.Controls.Add(this.NombreTxt);
            this.RegistroPanel.Controls.Add(this.ApellidosLbl);
            this.RegistroPanel.Controls.Add(this.ApellidosTxt);
            this.RegistroPanel.Controls.Add(this.CorreoLbl);
            this.RegistroPanel.Controls.Add(this.CorreoTxt);
            this.RegistroPanel.Controls.Add(this.ContrasenaLbl);
            this.RegistroPanel.Controls.Add(this.ContrasenaTxt);
            this.RegistroPanel.Controls.Add(this.ConfirmarContrasenaLbl);
            this.RegistroPanel.Controls.Add(this.ConfirmarContrasenaTxt);
            this.RegistroPanel.Controls.Add(this.RolLbl);
            this.RegistroPanel.Controls.Add(this.RolCombo);

            // Campos de cliente
            this.RegistroPanel.Controls.Add(this.TelefonoLbl);
            this.RegistroPanel.Controls.Add(this.TelefonoTxt);
            this.RegistroPanel.Controls.Add(this.DireccionLbl);
            this.RegistroPanel.Controls.Add(this.DireccionTxt);
            this.RegistroPanel.Controls.Add(this.AlergenosLbl);
            this.RegistroPanel.Controls.Add(this.AlergenosTxt);
            this.RegistroPanel.Controls.Add(this.ObservacionesLbl);
            this.RegistroPanel.Controls.Add(this.ObservacionesTxt);

            this.RegistroPanel.Controls.Add(this.RegistrarseBtn);
            this.RegistroPanel.Controls.Add(this.VolverLoginBtn);

            // 
            // Add controls to Form
            // 
            this.Controls.Add(this.RegistroPanel);
            this.Controls.Add(this.FondoIzquierdoPanel);
        }
    }
}