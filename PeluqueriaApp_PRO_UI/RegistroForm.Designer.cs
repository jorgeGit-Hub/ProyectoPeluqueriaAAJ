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

        // Campos específicos para ADMINISTRADOR
        private Label EspecialidadLbl;
        private TextBox EspecialidadTxt;

        private Button RegistrarseBtn;
        private Button VolverLoginBtn;
        private Label BienvenidaLbl;
        private Label InfoLbl;

        private void InitializeComponent()
        {
            FondoIzquierdoPanel = new Panel();
            BienvenidaLbl = new Label();
            InfoLbl = new Label();
            RegistroPanel = new Panel();
            LogoLbl = new Label();
            TituloLbl = new Label();
            SubtituloLbl = new Label();
            NombreLbl = new Label();
            NombreTxt = new TextBox();
            ApellidosLbl = new Label();
            ApellidosTxt = new TextBox();
            CorreoLbl = new Label();
            CorreoTxt = new TextBox();
            ContrasenaLbl = new Label();
            ContrasenaTxt = new TextBox();
            ConfirmarContrasenaLbl = new Label();
            ConfirmarContrasenaTxt = new TextBox();
            RolLbl = new Label();
            RolCombo = new ComboBox();
            TelefonoLbl = new Label();
            TelefonoTxt = new TextBox();
            DireccionLbl = new Label();
            DireccionTxt = new TextBox();
            AlergenosLbl = new Label();
            AlergenosTxt = new TextBox();
            ObservacionesLbl = new Label();
            ObservacionesTxt = new TextBox();
            EspecialidadLbl = new Label();
            EspecialidadTxt = new TextBox();
            RegistrarseBtn = new Button();
            VolverLoginBtn = new Button();
            pbLogo = new PictureBox();
            FondoIzquierdoPanel.SuspendLayout();
            RegistroPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            SuspendLayout();
            // 
            // FondoIzquierdoPanel
            // 
            FondoIzquierdoPanel.BackColor = Color.FromArgb(255, 140, 0);
            FondoIzquierdoPanel.Controls.Add(pbLogo);
            FondoIzquierdoPanel.Controls.Add(BienvenidaLbl);
            FondoIzquierdoPanel.Controls.Add(InfoLbl);
            FondoIzquierdoPanel.Dock = DockStyle.Left;
            FondoIzquierdoPanel.Location = new Point(0, 0);
            FondoIzquierdoPanel.Name = "FondoIzquierdoPanel";
            FondoIzquierdoPanel.Size = new Size(400, 749);
            FondoIzquierdoPanel.TabIndex = 1;
            // 
            // BienvenidaLbl
            // 
            BienvenidaLbl.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
            BienvenidaLbl.ForeColor = Color.White;
            BienvenidaLbl.Location = new Point(25, 250);
            BienvenidaLbl.Name = "BienvenidaLbl";
            BienvenidaLbl.Size = new Size(350, 120);
            BienvenidaLbl.TabIndex = 0;
            BienvenidaLbl.Text = "\nPeluquería Bernat Sarriá";
            BienvenidaLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // InfoLbl
            // 
            InfoLbl.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            InfoLbl.ForeColor = Color.FromArgb(255, 250, 245);
            InfoLbl.Location = new Point(25, 380);
            InfoLbl.Name = "InfoLbl";
            InfoLbl.Size = new Size(350, 80);
            InfoLbl.TabIndex = 1;
            InfoLbl.Text = "Crea tu cuenta\ny empieza a disfrutar\nde nuestros servicios";
            InfoLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RegistroPanel
            // 
            RegistroPanel.AutoScroll = true;
            RegistroPanel.BackColor = Color.White;
            RegistroPanel.Controls.Add(LogoLbl);
            RegistroPanel.Controls.Add(TituloLbl);
            RegistroPanel.Controls.Add(SubtituloLbl);
            RegistroPanel.Controls.Add(NombreLbl);
            RegistroPanel.Controls.Add(NombreTxt);
            RegistroPanel.Controls.Add(ApellidosLbl);
            RegistroPanel.Controls.Add(ApellidosTxt);
            RegistroPanel.Controls.Add(CorreoLbl);
            RegistroPanel.Controls.Add(CorreoTxt);
            RegistroPanel.Controls.Add(ContrasenaLbl);
            RegistroPanel.Controls.Add(ContrasenaTxt);
            RegistroPanel.Controls.Add(ConfirmarContrasenaLbl);
            RegistroPanel.Controls.Add(ConfirmarContrasenaTxt);
            RegistroPanel.Controls.Add(RolLbl);
            RegistroPanel.Controls.Add(RolCombo);
            RegistroPanel.Controls.Add(TelefonoLbl);
            RegistroPanel.Controls.Add(TelefonoTxt);
            RegistroPanel.Controls.Add(DireccionLbl);
            RegistroPanel.Controls.Add(DireccionTxt);
            RegistroPanel.Controls.Add(AlergenosLbl);
            RegistroPanel.Controls.Add(AlergenosTxt);
            RegistroPanel.Controls.Add(ObservacionesLbl);
            RegistroPanel.Controls.Add(ObservacionesTxt);
            RegistroPanel.Controls.Add(EspecialidadLbl);
            RegistroPanel.Controls.Add(EspecialidadTxt);
            RegistroPanel.Controls.Add(RegistrarseBtn);
            RegistroPanel.Controls.Add(VolverLoginBtn);
            RegistroPanel.Location = new Point(450, 25);
            RegistroPanel.Name = "RegistroPanel";
            RegistroPanel.Size = new Size(700, 750);
            RegistroPanel.TabIndex = 0;
            // 
            // LogoLbl
            // 
            LogoLbl.Font = new Font("Segoe UI Emoji", 28F, FontStyle.Regular, GraphicsUnit.Point);
            LogoLbl.ForeColor = Color.FromArgb(255, 140, 0);
            LogoLbl.Location = new Point(0, 20);
            LogoLbl.Name = "LogoLbl";
            LogoLbl.Size = new Size(700, 60);
            LogoLbl.TabIndex = 0;
            LogoLbl.Text = "✂";
            LogoLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TituloLbl
            // 
            TituloLbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            TituloLbl.ForeColor = Color.FromArgb(45, 35, 30);
            TituloLbl.Location = new Point(0, 85);
            TituloLbl.Name = "TituloLbl";
            TituloLbl.Size = new Size(700, 40);
            TituloLbl.TabIndex = 1;
            TituloLbl.Text = "Crear Cuenta";
            TituloLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SubtituloLbl
            // 
            SubtituloLbl.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            SubtituloLbl.ForeColor = Color.FromArgb(139, 90, 60);
            SubtituloLbl.Location = new Point(0, 125);
            SubtituloLbl.Name = "SubtituloLbl";
            SubtituloLbl.Size = new Size(700, 25);
            SubtituloLbl.TabIndex = 2;
            SubtituloLbl.Text = "Completa el formulario para registrarte";
            SubtituloLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // NombreLbl
            // 
            NombreLbl.AutoSize = true;
            NombreLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            NombreLbl.ForeColor = Color.FromArgb(45, 35, 30);
            NombreLbl.Location = new Point(50, 170);
            NombreLbl.Name = "NombreLbl";
            NombreLbl.Size = new Size(61, 15);
            NombreLbl.TabIndex = 3;
            NombreLbl.Text = "Nombre *";
            // 
            // NombreTxt
            // 
            NombreTxt.BackColor = Color.FromArgb(250, 245, 240);
            NombreTxt.BorderStyle = BorderStyle.FixedSingle;
            NombreTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            NombreTxt.Location = new Point(50, 170);
            NombreTxt.Name = "NombreTxt";
            NombreTxt.Size = new Size(280, 25);
            NombreTxt.TabIndex = 4;
            // 
            // ApellidosLbl
            // 
            ApellidosLbl.AutoSize = true;
            ApellidosLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            ApellidosLbl.ForeColor = Color.FromArgb(45, 35, 30);
            ApellidosLbl.Location = new Point(370, 170);
            ApellidosLbl.Name = "ApellidosLbl";
            ApellidosLbl.Size = new Size(65, 15);
            ApellidosLbl.TabIndex = 5;
            ApellidosLbl.Text = "Apellidos *";
            // 
            // ApellidosTxt
            // 
            ApellidosTxt.BackColor = Color.FromArgb(250, 245, 240);
            ApellidosTxt.BorderStyle = BorderStyle.FixedSingle;
            ApellidosTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            ApellidosTxt.Location = new Point(370, 170);
            ApellidosTxt.Name = "ApellidosTxt";
            ApellidosTxt.Size = new Size(280, 25);
            ApellidosTxt.TabIndex = 6;
            // 
            // CorreoLbl
            // 
            CorreoLbl.AutoSize = true;
            CorreoLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            CorreoLbl.ForeColor = Color.FromArgb(45, 35, 30);
            CorreoLbl.Location = new Point(50, 170);
            CorreoLbl.Name = "CorreoLbl";
            CorreoLbl.Size = new Size(118, 15);
            CorreoLbl.TabIndex = 7;
            CorreoLbl.Text = "Correo Electrónico *";
            // 
            // CorreoTxt
            // 
            CorreoTxt.BackColor = Color.FromArgb(250, 245, 240);
            CorreoTxt.BorderStyle = BorderStyle.FixedSingle;
            CorreoTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            CorreoTxt.Location = new Point(50, 170);
            CorreoTxt.Name = "CorreoTxt";
            CorreoTxt.Size = new Size(600, 25);
            CorreoTxt.TabIndex = 8;
            // 
            // ContrasenaLbl
            // 
            ContrasenaLbl.AutoSize = true;
            ContrasenaLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            ContrasenaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            ContrasenaLbl.Location = new Point(50, 170);
            ContrasenaLbl.Name = "ContrasenaLbl";
            ContrasenaLbl.Size = new Size(77, 15);
            ContrasenaLbl.TabIndex = 9;
            ContrasenaLbl.Text = "Contraseña *";
            // 
            // ContrasenaTxt
            // 
            ContrasenaTxt.BackColor = Color.FromArgb(250, 245, 240);
            ContrasenaTxt.BorderStyle = BorderStyle.FixedSingle;
            ContrasenaTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            ContrasenaTxt.Location = new Point(50, 170);
            ContrasenaTxt.Name = "ContrasenaTxt";
            ContrasenaTxt.PasswordChar = '●';
            ContrasenaTxt.Size = new Size(280, 25);
            ContrasenaTxt.TabIndex = 10;
            // 
            // ConfirmarContrasenaLbl
            // 
            ConfirmarContrasenaLbl.AutoSize = true;
            ConfirmarContrasenaLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            ConfirmarContrasenaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            ConfirmarContrasenaLbl.Location = new Point(370, 170);
            ConfirmarContrasenaLbl.Name = "ConfirmarContrasenaLbl";
            ConfirmarContrasenaLbl.Size = new Size(136, 15);
            ConfirmarContrasenaLbl.TabIndex = 11;
            ConfirmarContrasenaLbl.Text = "Confirmar Contraseña *";
            // 
            // ConfirmarContrasenaTxt
            // 
            ConfirmarContrasenaTxt.BackColor = Color.FromArgb(250, 245, 240);
            ConfirmarContrasenaTxt.BorderStyle = BorderStyle.FixedSingle;
            ConfirmarContrasenaTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            ConfirmarContrasenaTxt.Location = new Point(370, 170);
            ConfirmarContrasenaTxt.Name = "ConfirmarContrasenaTxt";
            ConfirmarContrasenaTxt.PasswordChar = '●';
            ConfirmarContrasenaTxt.Size = new Size(280, 25);
            ConfirmarContrasenaTxt.TabIndex = 12;
            // 
            // RolLbl
            // 
            RolLbl.AutoSize = true;
            RolLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            RolLbl.ForeColor = Color.FromArgb(45, 35, 30);
            RolLbl.Location = new Point(50, 170);
            RolLbl.Name = "RolLbl";
            RolLbl.Size = new Size(33, 15);
            RolLbl.TabIndex = 13;
            RolLbl.Text = "Rol *";
            // 
            // RolCombo
            // 
            RolCombo.BackColor = Color.FromArgb(250, 245, 240);
            RolCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            RolCombo.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            RolCombo.Items.AddRange(new object[] { "Administrador", "Alumno", "Cliente" });
            RolCombo.Location = new Point(50, 170);
            RolCombo.Name = "RolCombo";
            RolCombo.Size = new Size(280, 25);
            RolCombo.TabIndex = 14;
            // 
            // TelefonoLbl
            // 
            TelefonoLbl.AutoSize = true;
            TelefonoLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            TelefonoLbl.ForeColor = Color.FromArgb(45, 35, 30);
            TelefonoLbl.Location = new Point(50, 170);
            TelefonoLbl.Name = "TelefonoLbl";
            TelefonoLbl.Size = new Size(56, 15);
            TelefonoLbl.TabIndex = 15;
            TelefonoLbl.Text = "Teléfono";
            // 
            // TelefonoTxt
            // 
            TelefonoTxt.BackColor = Color.FromArgb(250, 245, 240);
            TelefonoTxt.BorderStyle = BorderStyle.FixedSingle;
            TelefonoTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            TelefonoTxt.Location = new Point(50, 170);
            TelefonoTxt.Name = "TelefonoTxt";
            TelefonoTxt.Size = new Size(280, 25);
            TelefonoTxt.TabIndex = 16;
            // 
            // DireccionLbl
            // 
            DireccionLbl.AutoSize = true;
            DireccionLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            DireccionLbl.ForeColor = Color.FromArgb(45, 35, 30);
            DireccionLbl.Location = new Point(370, 170);
            DireccionLbl.Name = "DireccionLbl";
            DireccionLbl.Size = new Size(60, 15);
            DireccionLbl.TabIndex = 17;
            DireccionLbl.Text = "Dirección";
            // 
            // DireccionTxt
            // 
            DireccionTxt.BackColor = Color.FromArgb(250, 245, 240);
            DireccionTxt.BorderStyle = BorderStyle.FixedSingle;
            DireccionTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            DireccionTxt.Location = new Point(370, 170);
            DireccionTxt.Name = "DireccionTxt";
            DireccionTxt.Size = new Size(280, 25);
            DireccionTxt.TabIndex = 18;
            // 
            // AlergenosLbl
            // 
            AlergenosLbl.AutoSize = true;
            AlergenosLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            AlergenosLbl.ForeColor = Color.FromArgb(45, 35, 30);
            AlergenosLbl.Location = new Point(50, 170);
            AlergenosLbl.Name = "AlergenosLbl";
            AlergenosLbl.Size = new Size(63, 15);
            AlergenosLbl.TabIndex = 19;
            AlergenosLbl.Text = "Alérgenos";
            // 
            // AlergenosTxt
            // 
            AlergenosTxt.BackColor = Color.FromArgb(250, 245, 240);
            AlergenosTxt.BorderStyle = BorderStyle.FixedSingle;
            AlergenosTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            AlergenosTxt.Location = new Point(50, 170);
            AlergenosTxt.Name = "AlergenosTxt";
            AlergenosTxt.Size = new Size(600, 25);
            AlergenosTxt.TabIndex = 20;
            // 
            // ObservacionesLbl
            // 
            ObservacionesLbl.AutoSize = true;
            ObservacionesLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            ObservacionesLbl.ForeColor = Color.FromArgb(45, 35, 30);
            ObservacionesLbl.Location = new Point(50, 170);
            ObservacionesLbl.Name = "ObservacionesLbl";
            ObservacionesLbl.Size = new Size(88, 15);
            ObservacionesLbl.TabIndex = 21;
            ObservacionesLbl.Text = "Observaciones";
            // 
            // ObservacionesTxt
            // 
            ObservacionesTxt.BackColor = Color.FromArgb(250, 245, 240);
            ObservacionesTxt.BorderStyle = BorderStyle.FixedSingle;
            ObservacionesTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            ObservacionesTxt.Location = new Point(50, 170);
            ObservacionesTxt.Name = "ObservacionesTxt";
            ObservacionesTxt.Size = new Size(600, 25);
            ObservacionesTxt.TabIndex = 22;
            // 
            // EspecialidadLbl
            // 
            EspecialidadLbl.AutoSize = true;
            EspecialidadLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            EspecialidadLbl.ForeColor = Color.FromArgb(45, 35, 30);
            EspecialidadLbl.Location = new Point(50, 170);
            EspecialidadLbl.Name = "EspecialidadLbl";
            EspecialidadLbl.Size = new Size(73, 15);
            EspecialidadLbl.TabIndex = 23;
            EspecialidadLbl.Text = "Especialidad";
            EspecialidadLbl.Visible = false;
            // 
            // EspecialidadTxt
            // 
            EspecialidadTxt.BackColor = Color.FromArgb(250, 245, 240);
            EspecialidadTxt.BorderStyle = BorderStyle.FixedSingle;
            EspecialidadTxt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            EspecialidadTxt.Location = new Point(50, 170);
            EspecialidadTxt.Name = "EspecialidadTxt";
            EspecialidadTxt.Size = new Size(600, 25);
            EspecialidadTxt.TabIndex = 24;
            EspecialidadTxt.Visible = false;
            // 
            // RegistrarseBtn
            // 
            RegistrarseBtn.BackColor = Color.FromArgb(255, 140, 0);
            RegistrarseBtn.Cursor = Cursors.Hand;
            RegistrarseBtn.FlatAppearance.BorderSize = 0;
            RegistrarseBtn.FlatStyle = FlatStyle.Flat;
            RegistrarseBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            RegistrarseBtn.ForeColor = Color.White;
            RegistrarseBtn.Location = new Point(50, 170);
            RegistrarseBtn.Name = "RegistrarseBtn";
            RegistrarseBtn.Size = new Size(600, 45);
            RegistrarseBtn.TabIndex = 25;
            RegistrarseBtn.Text = "Registrarse";
            RegistrarseBtn.UseVisualStyleBackColor = false;
            RegistrarseBtn.Click += RegistrarseBtn_Click;
            // 
            // VolverLoginBtn
            // 
            VolverLoginBtn.BackColor = Color.White;
            VolverLoginBtn.Cursor = Cursors.Hand;
            VolverLoginBtn.FlatAppearance.BorderColor = Color.FromArgb(255, 140, 0);
            VolverLoginBtn.FlatAppearance.BorderSize = 2;
            VolverLoginBtn.FlatStyle = FlatStyle.Flat;
            VolverLoginBtn.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            VolverLoginBtn.ForeColor = Color.FromArgb(255, 140, 0);
            VolverLoginBtn.Location = new Point(50, 170);
            VolverLoginBtn.Name = "VolverLoginBtn";
            VolverLoginBtn.Size = new Size(600, 40);
            VolverLoginBtn.TabIndex = 26;
            VolverLoginBtn.Text = "¿Ya tienes cuenta? Inicia sesión";
            VolverLoginBtn.UseVisualStyleBackColor = false;
            VolverLoginBtn.Click += VolverLoginBtn_Click;
            // 
            // pbLogo
            // 
            pbLogo.Location = new Point(86, 77);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(219, 186);
            pbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pbLogo.TabIndex = 2;
            pbLogo.TabStop = false;
            // 
            // RegistroForm
            // 
            BackColor = Color.FromArgb(250, 245, 240);
            ClientSize = new Size(1200, 749);
            Controls.Add(RegistroPanel);
            Controls.Add(FondoIzquierdoPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "RegistroForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Peluquería Escola - Registro";
            FondoIzquierdoPanel.ResumeLayout(false);
            RegistroPanel.ResumeLayout(false);
            RegistroPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            ResumeLayout(false);
        }

        private PictureBox pbLogo;
    }
}