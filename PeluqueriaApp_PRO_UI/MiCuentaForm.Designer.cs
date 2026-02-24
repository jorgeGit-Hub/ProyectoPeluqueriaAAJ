using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    partial class MiCuentaForm
    {
        private Panel LateralPanel;
        private Panel CapcaleraPanel;
        private Label LogoLbl;
        private Label TitolAppLbl;
        private Label BienvenidaLbl;
        private Button IniciBoto;
        private Button ServiciosBoto;
        private Button UsuariosBoto;
        private Button ClientesBoto;
        private Button CitasBoto;
        private Button GruposBoto;
        private Button HorarioSemanalBoto;
        private Button HorarioBoto;
        private Button ValoracionesBoto;
        private Button MiCuentaBoto;
        private Button TancarSessioBoto;

        // Controles específicos de Mi Cuenta
        private Label TitolPaginaLbl;
        private Panel TarjetaPerfilPanel;
        private Label TituloDatosLbl;
        private Label NombreLbl;
        private TextBox NombreTxt;
        private Label ApellidosLbl;
        private TextBox ApellidosTxt;
        private Label CorreoLbl;
        private TextBox CorreoTxt;
        private Label RolLbl;
        private TextBox RolTxt;
        private Button GuardarCambiosBtn;
        private Button CambiarPasswordBtn;

        // Controles para cambiar contraseña
        private Label NuevaContrasenaLbl;
        private TextBox NuevaContrasenaTxt;
        private Label ConfirmarContrasenaLbl;
        private TextBox ConfirmarContrasenaTxt;
        private Button GuardarContrasenaBtn;

        private void InitializeComponent()
        {
            LateralPanel = new Panel();
            pbLogo = new PictureBox();
            LogoLbl = new Label();
            IniciBoto = new Button();
            ServiciosBoto = new Button();
            UsuariosBoto = new Button();
            ClientesBoto = new Button();
            CitasBoto = new Button();
            GruposBoto = new Button();
            HorarioBoto = new Button();
            HorarioSemanalBoto = new Button();
            ValoracionesBoto = new Button();
            MiCuentaBoto = new Button();
            TancarSessioBoto = new Button();
            CapcaleraPanel = new Panel();
            TitolAppLbl = new Label();
            BienvenidaLbl = new Label();
            TitolPaginaLbl = new Label();
            TarjetaPerfilPanel = new Panel();
            TituloDatosLbl = new Label();
            NombreLbl = new Label();
            NombreTxt = new TextBox();
            ApellidosLbl = new Label();
            ApellidosTxt = new TextBox();
            CorreoLbl = new Label();
            CorreoTxt = new TextBox();
            RolLbl = new Label();
            RolTxt = new TextBox();
            NuevaContrasenaLbl = new Label();
            NuevaContrasenaTxt = new TextBox();
            ConfirmarContrasenaLbl = new Label();
            ConfirmarContrasenaTxt = new TextBox();
            GuardarCambiosBtn = new Button();
            CambiarPasswordBtn = new Button();
            GuardarContrasenaBtn = new Button();
            LateralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            CapcaleraPanel.SuspendLayout();
            TarjetaPerfilPanel.SuspendLayout();
            SuspendLayout();
            // 
            // LateralPanel
            // 
            LateralPanel.BackColor = Color.FromArgb(45, 35, 30);
            LateralPanel.Controls.Add(pbLogo);
            LateralPanel.Controls.Add(LogoLbl);
            LateralPanel.Controls.Add(IniciBoto);
            LateralPanel.Controls.Add(ServiciosBoto);
            LateralPanel.Controls.Add(UsuariosBoto);
            LateralPanel.Controls.Add(ClientesBoto);
            LateralPanel.Controls.Add(CitasBoto);
            LateralPanel.Controls.Add(GruposBoto);
            LateralPanel.Controls.Add(HorarioBoto);
            LateralPanel.Controls.Add(HorarioSemanalBoto);
            LateralPanel.Controls.Add(ValoracionesBoto);
            LateralPanel.Controls.Add(MiCuentaBoto);
            LateralPanel.Controls.Add(TancarSessioBoto);
            LateralPanel.Dock = DockStyle.Left;
            LateralPanel.Location = new Point(0, 0);
            LateralPanel.Name = "LateralPanel";
            LateralPanel.Size = new Size(260, 749);
            LateralPanel.TabIndex = 6;
            // 
            // pbLogo
            // 
            pbLogo.Location = new Point(80, 5);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(100, 50);
            pbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pbLogo.TabIndex = 12;
            pbLogo.TabStop = false;
            // 
            // LogoLbl
            // 
            LogoLbl.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            LogoLbl.ForeColor = Color.FromArgb(255, 140, 0);
            LogoLbl.Location = new Point(0, 20);
            LogoLbl.Name = "LogoLbl";
            LogoLbl.Size = new Size(260, 100);
            LogoLbl.TabIndex = 0;
            LogoLbl.Text = "\nPeluquería\nBernat Sarriá";
            LogoLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // IniciBoto
            // 
            IniciBoto.BackColor = Color.FromArgb(45, 35, 30);
            IniciBoto.Cursor = Cursors.Hand;
            IniciBoto.FlatAppearance.BorderSize = 0;
            IniciBoto.FlatStyle = FlatStyle.Flat;
            IniciBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            IniciBoto.ForeColor = Color.FromArgb(200, 200, 200);
            IniciBoto.Location = new Point(10, 130);
            IniciBoto.Name = "IniciBoto";
            IniciBoto.Padding = new Padding(20, 0, 0, 0);
            IniciBoto.Size = new Size(240, 45);
            IniciBoto.TabIndex = 1;
            IniciBoto.Text = "🏠  Inicio";
            IniciBoto.TextAlign = ContentAlignment.MiddleLeft;
            IniciBoto.UseVisualStyleBackColor = false;
            IniciBoto.Click += IniciBoto_Click;
            // 
            // ServiciosBoto
            // 
            ServiciosBoto.BackColor = Color.FromArgb(45, 35, 30);
            ServiciosBoto.Cursor = Cursors.Hand;
            ServiciosBoto.FlatAppearance.BorderSize = 0;
            ServiciosBoto.FlatStyle = FlatStyle.Flat;
            ServiciosBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            ServiciosBoto.ForeColor = Color.FromArgb(200, 200, 200);
            ServiciosBoto.Location = new Point(10, 180);
            ServiciosBoto.Name = "ServiciosBoto";
            ServiciosBoto.Padding = new Padding(20, 0, 0, 0);
            ServiciosBoto.Size = new Size(240, 45);
            ServiciosBoto.TabIndex = 2;
            ServiciosBoto.Text = "✂️  Servicios";
            ServiciosBoto.TextAlign = ContentAlignment.MiddleLeft;
            ServiciosBoto.UseVisualStyleBackColor = false;
            ServiciosBoto.Click += ServiciosBoto_Click;
            // 
            // UsuariosBoto
            // 
            UsuariosBoto.BackColor = Color.FromArgb(45, 35, 30);
            UsuariosBoto.Cursor = Cursors.Hand;
            UsuariosBoto.FlatAppearance.BorderSize = 0;
            UsuariosBoto.FlatStyle = FlatStyle.Flat;
            UsuariosBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            UsuariosBoto.ForeColor = Color.FromArgb(200, 200, 200);
            UsuariosBoto.Location = new Point(10, 230);
            UsuariosBoto.Name = "UsuariosBoto";
            UsuariosBoto.Padding = new Padding(20, 0, 0, 0);
            UsuariosBoto.Size = new Size(240, 45);
            UsuariosBoto.TabIndex = 3;
            UsuariosBoto.Text = "👥  Usuarios";
            UsuariosBoto.TextAlign = ContentAlignment.MiddleLeft;
            UsuariosBoto.UseVisualStyleBackColor = false;
            UsuariosBoto.Click += UsuariosBoto_Click;
            // 
            // ClientesBoto
            // 
            ClientesBoto.BackColor = Color.FromArgb(45, 35, 30);
            ClientesBoto.Cursor = Cursors.Hand;
            ClientesBoto.FlatAppearance.BorderSize = 0;
            ClientesBoto.FlatStyle = FlatStyle.Flat;
            ClientesBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            ClientesBoto.ForeColor = Color.FromArgb(200, 200, 200);
            ClientesBoto.Location = new Point(10, 280);
            ClientesBoto.Name = "ClientesBoto";
            ClientesBoto.Padding = new Padding(20, 0, 0, 0);
            ClientesBoto.Size = new Size(240, 45);
            ClientesBoto.TabIndex = 4;
            ClientesBoto.Text = "👤  Clientes";
            ClientesBoto.TextAlign = ContentAlignment.MiddleLeft;
            ClientesBoto.UseVisualStyleBackColor = false;
            ClientesBoto.Click += ClientesBoto_Click;
            // 
            // CitasBoto
            // 
            CitasBoto.BackColor = Color.FromArgb(45, 35, 30);
            CitasBoto.Cursor = Cursors.Hand;
            CitasBoto.FlatAppearance.BorderSize = 0;
            CitasBoto.FlatStyle = FlatStyle.Flat;
            CitasBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            CitasBoto.ForeColor = Color.FromArgb(200, 200, 200);
            CitasBoto.Location = new Point(10, 330);
            CitasBoto.Name = "CitasBoto";
            CitasBoto.Padding = new Padding(20, 0, 0, 0);
            CitasBoto.Size = new Size(240, 45);
            CitasBoto.TabIndex = 5;
            CitasBoto.Text = "📅  Citas";
            CitasBoto.TextAlign = ContentAlignment.MiddleLeft;
            CitasBoto.UseVisualStyleBackColor = false;
            CitasBoto.Click += CitasBoto_Click;
            // 
            // GruposBoto
            // 
            GruposBoto.BackColor = Color.FromArgb(45, 35, 30);
            GruposBoto.Cursor = Cursors.Hand;
            GruposBoto.FlatAppearance.BorderSize = 0;
            GruposBoto.FlatStyle = FlatStyle.Flat;
            GruposBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            GruposBoto.ForeColor = Color.FromArgb(200, 200, 200);
            GruposBoto.Location = new Point(10, 380);
            GruposBoto.Name = "GruposBoto";
            GruposBoto.Padding = new Padding(20, 0, 0, 0);
            GruposBoto.Size = new Size(240, 45);
            GruposBoto.TabIndex = 6;
            GruposBoto.Text = "👨‍👩‍👧‍👦  Grupos";
            GruposBoto.TextAlign = ContentAlignment.MiddleLeft;
            GruposBoto.UseVisualStyleBackColor = false;
            GruposBoto.Click += GruposBoto_Click;
            // 
            // HorarioBoto
            // 
            HorarioBoto.BackColor = Color.FromArgb(45, 35, 30);
            HorarioBoto.Cursor = Cursors.Hand;
            HorarioBoto.FlatAppearance.BorderSize = 0;
            HorarioBoto.FlatStyle = FlatStyle.Flat;
            HorarioBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            HorarioBoto.ForeColor = Color.FromArgb(200, 200, 200);
            HorarioBoto.Location = new Point(10, 430);
            HorarioBoto.Name = "HorarioBoto";
            HorarioBoto.Padding = new Padding(20, 0, 0, 0);
            HorarioBoto.Size = new Size(240, 45);
            HorarioBoto.TabIndex = 7;
            HorarioBoto.Text = "🗓️  Horario Semanal";
            HorarioBoto.TextAlign = ContentAlignment.MiddleLeft;
            HorarioBoto.UseVisualStyleBackColor = false;
            HorarioBoto.Click += HorarioForm_Click;
            // 
            // HorarioSemanalBoto
            // 
            HorarioSemanalBoto.BackColor = Color.FromArgb(45, 35, 30);
            HorarioSemanalBoto.Cursor = Cursors.Hand;
            HorarioSemanalBoto.FlatAppearance.BorderSize = 0;
            HorarioSemanalBoto.FlatStyle = FlatStyle.Flat;
            HorarioSemanalBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            HorarioSemanalBoto.ForeColor = Color.FromArgb(200, 200, 200);
            HorarioSemanalBoto.Location = new Point(10, 480);
            HorarioSemanalBoto.Name = "HorarioSemanalBoto";
            HorarioSemanalBoto.Padding = new Padding(20, 0, 0, 0);
            HorarioSemanalBoto.Size = new Size(240, 45);
            HorarioSemanalBoto.TabIndex = 8;
            HorarioSemanalBoto.Text = "🕐  Bloqueo Horario";
            HorarioSemanalBoto.TextAlign = ContentAlignment.MiddleLeft;
            HorarioSemanalBoto.UseVisualStyleBackColor = false;
            HorarioSemanalBoto.Click += HorarioSemanalBoto_Click;
            // 
            // ValoracionesBoto
            // 
            ValoracionesBoto.BackColor = Color.FromArgb(45, 35, 30);
            ValoracionesBoto.Cursor = Cursors.Hand;
            ValoracionesBoto.FlatAppearance.BorderSize = 0;
            ValoracionesBoto.FlatStyle = FlatStyle.Flat;
            ValoracionesBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            ValoracionesBoto.ForeColor = Color.FromArgb(200, 200, 200);
            ValoracionesBoto.Location = new Point(10, 530);
            ValoracionesBoto.Name = "ValoracionesBoto";
            ValoracionesBoto.Padding = new Padding(20, 0, 0, 0);
            ValoracionesBoto.Size = new Size(240, 45);
            ValoracionesBoto.TabIndex = 9;
            ValoracionesBoto.Text = "⭐  Valoraciones";
            ValoracionesBoto.TextAlign = ContentAlignment.MiddleLeft;
            ValoracionesBoto.UseVisualStyleBackColor = false;
            ValoracionesBoto.Click += ValoracionForm_Click;
            // 
            // MiCuentaBoto
            // 
            MiCuentaBoto.BackColor = Color.FromArgb(255, 140, 0);
            MiCuentaBoto.Cursor = Cursors.Hand;
            MiCuentaBoto.FlatAppearance.BorderSize = 0;
            MiCuentaBoto.FlatStyle = FlatStyle.Flat;
            MiCuentaBoto.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            MiCuentaBoto.ForeColor = Color.White;
            MiCuentaBoto.Location = new Point(10, 580);
            MiCuentaBoto.Name = "MiCuentaBoto";
            MiCuentaBoto.Padding = new Padding(20, 0, 0, 0);
            MiCuentaBoto.Size = new Size(240, 45);
            MiCuentaBoto.TabIndex = 10;
            MiCuentaBoto.Text = "⚙️  Mi Cuenta";
            MiCuentaBoto.TextAlign = ContentAlignment.MiddleLeft;
            MiCuentaBoto.UseVisualStyleBackColor = false;
            // 
            // TancarSessioBoto
            // 
            TancarSessioBoto.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            TancarSessioBoto.BackColor = Color.FromArgb(45, 35, 30);
            TancarSessioBoto.Cursor = Cursors.Hand;
            TancarSessioBoto.FlatAppearance.BorderSize = 0;
            TancarSessioBoto.FlatStyle = FlatStyle.Flat;
            TancarSessioBoto.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            TancarSessioBoto.ForeColor = Color.FromArgb(150, 150, 150);
            TancarSessioBoto.Location = new Point(10, 1369);
            TancarSessioBoto.Name = "TancarSessioBoto";
            TancarSessioBoto.Padding = new Padding(20, 0, 0, 0);
            TancarSessioBoto.Size = new Size(240, 45);
            TancarSessioBoto.TabIndex = 11;
            TancarSessioBoto.Text = "🚪  Cerrar Sesión";
            TancarSessioBoto.TextAlign = ContentAlignment.MiddleLeft;
            TancarSessioBoto.UseVisualStyleBackColor = false;
            TancarSessioBoto.Click += TancarSessioBoto_Click;
            // 
            // CapcaleraPanel
            // 
            CapcaleraPanel.BackColor = Color.White;
            CapcaleraPanel.Controls.Add(TitolAppLbl);
            CapcaleraPanel.Controls.Add(BienvenidaLbl);
            CapcaleraPanel.Dock = DockStyle.Top;
            CapcaleraPanel.Location = new Point(260, 0);
            CapcaleraPanel.Name = "CapcaleraPanel";
            CapcaleraPanel.Size = new Size(1110, 80);
            CapcaleraPanel.TabIndex = 5;
            // 
            // TitolAppLbl
            // 
            TitolAppLbl.AutoSize = true;
            TitolAppLbl.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            TitolAppLbl.ForeColor = Color.FromArgb(45, 35, 30);
            TitolAppLbl.Location = new Point(30, 25);
            TitolAppLbl.Name = "TitolAppLbl";
            TitolAppLbl.Size = new Size(101, 30);
            TitolAppLbl.TabIndex = 0;
            TitolAppLbl.Text = "Mi Perfil";
            // 
            // BienvenidaLbl
            // 
            BienvenidaLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BienvenidaLbl.AutoSize = true;
            BienvenidaLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            BienvenidaLbl.ForeColor = Color.FromArgb(139, 90, 60);
            BienvenidaLbl.Location = new Point(1960, 30);
            BienvenidaLbl.Name = "BienvenidaLbl";
            BienvenidaLbl.Size = new Size(88, 19);
            BienvenidaLbl.TabIndex = 1;
            BienvenidaLbl.Text = "Bienvenido/a";
            // 
            // TitolPaginaLbl
            // 
            TitolPaginaLbl.AutoSize = true;
            TitolPaginaLbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            TitolPaginaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            TitolPaginaLbl.Location = new Point(290, 110);
            TitolPaginaLbl.Name = "TitolPaginaLbl";
            TitolPaginaLbl.Size = new Size(262, 37);
            TitolPaginaLbl.TabIndex = 4;
            TitolPaginaLbl.Text = "Datos de tu Cuenta";
            // 
            // TarjetaPerfilPanel
            // 
            TarjetaPerfilPanel.BackColor = Color.White;
            TarjetaPerfilPanel.BorderStyle = BorderStyle.FixedSingle;
            TarjetaPerfilPanel.Controls.Add(TituloDatosLbl);
            TarjetaPerfilPanel.Controls.Add(NombreLbl);
            TarjetaPerfilPanel.Controls.Add(NombreTxt);
            TarjetaPerfilPanel.Controls.Add(ApellidosLbl);
            TarjetaPerfilPanel.Controls.Add(ApellidosTxt);
            TarjetaPerfilPanel.Controls.Add(CorreoLbl);
            TarjetaPerfilPanel.Controls.Add(CorreoTxt);
            TarjetaPerfilPanel.Controls.Add(RolLbl);
            TarjetaPerfilPanel.Controls.Add(RolTxt);
            TarjetaPerfilPanel.Controls.Add(NuevaContrasenaLbl);
            TarjetaPerfilPanel.Controls.Add(NuevaContrasenaTxt);
            TarjetaPerfilPanel.Controls.Add(ConfirmarContrasenaLbl);
            TarjetaPerfilPanel.Controls.Add(ConfirmarContrasenaTxt);
            TarjetaPerfilPanel.Location = new Point(290, 170);
            TarjetaPerfilPanel.Name = "TarjetaPerfilPanel";
            TarjetaPerfilPanel.Size = new Size(600, 580);
            TarjetaPerfilPanel.TabIndex = 3;
            // 
            // TituloDatosLbl
            // 
            TituloDatosLbl.AutoSize = true;
            TituloDatosLbl.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            TituloDatosLbl.ForeColor = Color.FromArgb(255, 140, 0);
            TituloDatosLbl.Location = new Point(30, 30);
            TituloDatosLbl.Name = "TituloDatosLbl";
            TituloDatosLbl.Size = new Size(203, 25);
            TituloDatosLbl.TabIndex = 0;
            TituloDatosLbl.Text = "Información Personal";
            // 
            // NombreLbl
            // 
            NombreLbl.AutoSize = true;
            NombreLbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            NombreLbl.ForeColor = Color.FromArgb(45, 35, 30);
            NombreLbl.Location = new Point(30, 80);
            NombreLbl.Name = "NombreLbl";
            NombreLbl.Size = new Size(71, 20);
            NombreLbl.TabIndex = 1;
            NombreLbl.Text = "Nombre:";
            // 
            // NombreTxt
            // 
            NombreTxt.BackColor = Color.FromArgb(245, 245, 245);
            NombreTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            NombreTxt.Location = new Point(30, 110);
            NombreTxt.Name = "NombreTxt";
            NombreTxt.ReadOnly = true;
            NombreTxt.Size = new Size(530, 27);
            NombreTxt.TabIndex = 2;
            // 
            // ApellidosLbl
            // 
            ApellidosLbl.AutoSize = true;
            ApellidosLbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            ApellidosLbl.ForeColor = Color.FromArgb(45, 35, 30);
            ApellidosLbl.Location = new Point(30, 160);
            ApellidosLbl.Name = "ApellidosLbl";
            ApellidosLbl.Size = new Size(78, 20);
            ApellidosLbl.TabIndex = 3;
            ApellidosLbl.Text = "Apellidos:";
            // 
            // ApellidosTxt
            // 
            ApellidosTxt.BackColor = Color.FromArgb(245, 245, 245);
            ApellidosTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            ApellidosTxt.Location = new Point(30, 190);
            ApellidosTxt.Name = "ApellidosTxt";
            ApellidosTxt.ReadOnly = true;
            ApellidosTxt.Size = new Size(530, 27);
            ApellidosTxt.TabIndex = 4;
            // 
            // CorreoLbl
            // 
            CorreoLbl.AutoSize = true;
            CorreoLbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            CorreoLbl.ForeColor = Color.FromArgb(45, 35, 30);
            CorreoLbl.Location = new Point(30, 240);
            CorreoLbl.Name = "CorreoLbl";
            CorreoLbl.Size = new Size(141, 20);
            CorreoLbl.TabIndex = 5;
            CorreoLbl.Text = "Correo Electrónico:";
            // 
            // CorreoTxt
            // 
            CorreoTxt.BackColor = Color.FromArgb(245, 245, 245);
            CorreoTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            CorreoTxt.Location = new Point(30, 270);
            CorreoTxt.Name = "CorreoTxt";
            CorreoTxt.ReadOnly = true;
            CorreoTxt.Size = new Size(530, 27);
            CorreoTxt.TabIndex = 6;
            // 
            // RolLbl
            // 
            RolLbl.AutoSize = true;
            RolLbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            RolLbl.ForeColor = Color.FromArgb(45, 35, 30);
            RolLbl.Location = new Point(30, 320);
            RolLbl.Name = "RolLbl";
            RolLbl.Size = new Size(132, 20);
            RolLbl.TabIndex = 7;
            RolLbl.Text = "Rol en el Sistema:";
            // 
            // RolTxt
            // 
            RolTxt.BackColor = Color.FromArgb(245, 245, 245);
            RolTxt.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            RolTxt.ForeColor = Color.FromArgb(139, 90, 60);
            RolTxt.Location = new Point(30, 350);
            RolTxt.Name = "RolTxt";
            RolTxt.ReadOnly = true;
            RolTxt.Size = new Size(530, 27);
            RolTxt.TabIndex = 8;
            // 
            // NuevaContrasenaLbl
            // 
            NuevaContrasenaLbl.AutoSize = true;
            NuevaContrasenaLbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            NuevaContrasenaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            NuevaContrasenaLbl.Location = new Point(30, 400);
            NuevaContrasenaLbl.Name = "NuevaContrasenaLbl";
            NuevaContrasenaLbl.Size = new Size(141, 20);
            NuevaContrasenaLbl.TabIndex = 9;
            NuevaContrasenaLbl.Text = "Nueva Contraseña:";
            NuevaContrasenaLbl.Visible = false;
            // 
            // NuevaContrasenaTxt
            // 
            NuevaContrasenaTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            NuevaContrasenaTxt.Location = new Point(30, 430);
            NuevaContrasenaTxt.Name = "NuevaContrasenaTxt";
            NuevaContrasenaTxt.PasswordChar = '*';
            NuevaContrasenaTxt.Size = new Size(530, 27);
            NuevaContrasenaTxt.TabIndex = 10;
            NuevaContrasenaTxt.Visible = false;
            // 
            // ConfirmarContrasenaLbl
            // 
            ConfirmarContrasenaLbl.AutoSize = true;
            ConfirmarContrasenaLbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            ConfirmarContrasenaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            ConfirmarContrasenaLbl.Location = new Point(30, 480);
            ConfirmarContrasenaLbl.Name = "ConfirmarContrasenaLbl";
            ConfirmarContrasenaLbl.Size = new Size(167, 20);
            ConfirmarContrasenaLbl.TabIndex = 11;
            ConfirmarContrasenaLbl.Text = "Confirmar Contraseña:";
            ConfirmarContrasenaLbl.Visible = false;
            // 
            // ConfirmarContrasenaTxt
            // 
            ConfirmarContrasenaTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            ConfirmarContrasenaTxt.Location = new Point(30, 510);
            ConfirmarContrasenaTxt.Name = "ConfirmarContrasenaTxt";
            ConfirmarContrasenaTxt.PasswordChar = '*';
            ConfirmarContrasenaTxt.Size = new Size(530, 27);
            ConfirmarContrasenaTxt.TabIndex = 12;
            ConfirmarContrasenaTxt.Visible = false;
            // 
            // GuardarCambiosBtn
            // 
            GuardarCambiosBtn.BackColor = Color.FromArgb(139, 90, 60);
            GuardarCambiosBtn.Cursor = Cursors.Hand;
            GuardarCambiosBtn.FlatAppearance.BorderSize = 0;
            GuardarCambiosBtn.FlatStyle = FlatStyle.Flat;
            GuardarCambiosBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            GuardarCambiosBtn.ForeColor = Color.White;
            GuardarCambiosBtn.Location = new Point(920, 170);
            GuardarCambiosBtn.Name = "GuardarCambiosBtn";
            GuardarCambiosBtn.Size = new Size(200, 45);
            GuardarCambiosBtn.TabIndex = 0;
            GuardarCambiosBtn.Text = "✏️ Editar Perfil";
            GuardarCambiosBtn.UseVisualStyleBackColor = false;
            GuardarCambiosBtn.Click += GuardarCambiosBtn_Click;
            // 
            // CambiarPasswordBtn
            // 
            CambiarPasswordBtn.BackColor = Color.FromArgb(70, 130, 180);
            CambiarPasswordBtn.Cursor = Cursors.Hand;
            CambiarPasswordBtn.FlatAppearance.BorderSize = 0;
            CambiarPasswordBtn.FlatStyle = FlatStyle.Flat;
            CambiarPasswordBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            CambiarPasswordBtn.ForeColor = Color.White;
            CambiarPasswordBtn.Location = new Point(920, 230);
            CambiarPasswordBtn.Name = "CambiarPasswordBtn";
            CambiarPasswordBtn.Size = new Size(220, 45);
            CambiarPasswordBtn.TabIndex = 1;
            CambiarPasswordBtn.Text = "🔑 Cambiar Contraseña";
            CambiarPasswordBtn.UseVisualStyleBackColor = false;
            CambiarPasswordBtn.Click += AccionDesarrollo_Click;
            // 
            // GuardarContrasenaBtn
            // 
            GuardarContrasenaBtn.BackColor = Color.FromArgb(34, 139, 34);
            GuardarContrasenaBtn.Cursor = Cursors.Hand;
            GuardarContrasenaBtn.FlatAppearance.BorderSize = 0;
            GuardarContrasenaBtn.FlatStyle = FlatStyle.Flat;
            GuardarContrasenaBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            GuardarContrasenaBtn.ForeColor = Color.White;
            GuardarContrasenaBtn.Location = new Point(920, 290);
            GuardarContrasenaBtn.Name = "GuardarContrasenaBtn";
            GuardarContrasenaBtn.Size = new Size(250, 45);
            GuardarContrasenaBtn.TabIndex = 2;
            GuardarContrasenaBtn.Text = "✓ Guardar Nueva Contraseña";
            GuardarContrasenaBtn.UseVisualStyleBackColor = false;
            GuardarContrasenaBtn.Visible = false;
            GuardarContrasenaBtn.Click += GuardarContrasenaBtn_Click;
            // 
            // MiCuentaForm
            // 
            BackColor = Color.FromArgb(250, 245, 240);
            ClientSize = new Size(1370, 749);
            Controls.Add(GuardarCambiosBtn);
            Controls.Add(CambiarPasswordBtn);
            Controls.Add(GuardarContrasenaBtn);
            Controls.Add(TarjetaPerfilPanel);
            Controls.Add(TitolPaginaLbl);
            Controls.Add(CapcaleraPanel);
            Controls.Add(LateralPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MiCuentaForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Peluquería Escola - Mi Cuenta";
            LateralPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            CapcaleraPanel.ResumeLayout(false);
            CapcaleraPanel.PerformLayout();
            TarjetaPerfilPanel.ResumeLayout(false);
            TarjetaPerfilPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private PictureBox pbLogo;
    }
}