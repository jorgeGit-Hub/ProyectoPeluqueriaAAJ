using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    partial class HomeForm
    {
        private Panel LateralPanel;
        private Panel CapcaleraPanel;
        private Panel ContenidoPanel;
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
        private Label TitolPaginaLbl;
        private Label SubtitolPaginaLbl;
        private Panel TarjetaServiciosPanel;
        private Panel TarjetaUsuariosPanel;
        private Panel TarjetaCitasPanel;
        private Panel TarjetaCuentaPanel;

        private void InitializeComponent()
        {
            LateralPanel = new Panel();
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
            ContenidoPanel = new Panel();
            TitolPaginaLbl = new Label();
            SubtitolPaginaLbl = new Label();
            TarjetaServiciosPanel = new Panel();
            iconoServicios = new Label();
            tituloServicios = new Label();
            descripcionServicios = new Label();
            TarjetaUsuariosPanel = new Panel();
            iconoUsuarios = new Label();
            tituloUsuarios = new Label();
            descripcionUsuarios = new Label();
            TarjetaCitasPanel = new Panel();
            TarjetaCuentaPanel = new Panel();
            iconoCuenta = new Label();
            tituloCuenta = new Label();
            descripcionCuenta = new Label();
            pbLogo = new PictureBox();
            LateralPanel.SuspendLayout();
            CapcaleraPanel.SuspendLayout();
            ContenidoPanel.SuspendLayout();
            TarjetaServiciosPanel.SuspendLayout();
            TarjetaUsuariosPanel.SuspendLayout();
            TarjetaCuentaPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
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
            LateralPanel.TabIndex = 2;
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
            IniciBoto.BackColor = Color.FromArgb(255, 140, 0);
            IniciBoto.Cursor = Cursors.Hand;
            IniciBoto.FlatAppearance.BorderSize = 0;
            IniciBoto.FlatStyle = FlatStyle.Flat;
            IniciBoto.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            IniciBoto.ForeColor = Color.White;
            IniciBoto.Location = new Point(10, 130);
            IniciBoto.Name = "IniciBoto";
            IniciBoto.Padding = new Padding(20, 0, 0, 0);
            IniciBoto.Size = new Size(240, 45);
            IniciBoto.TabIndex = 1;
            IniciBoto.Text = "🏠  Inicio";
            IniciBoto.TextAlign = ContentAlignment.MiddleLeft;
            IniciBoto.UseVisualStyleBackColor = false;
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
            MiCuentaBoto.BackColor = Color.FromArgb(45, 35, 30);
            MiCuentaBoto.Cursor = Cursors.Hand;
            MiCuentaBoto.FlatAppearance.BorderSize = 0;
            MiCuentaBoto.FlatStyle = FlatStyle.Flat;
            MiCuentaBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            MiCuentaBoto.ForeColor = Color.FromArgb(200, 200, 200);
            MiCuentaBoto.Location = new Point(10, 580);
            MiCuentaBoto.Name = "MiCuentaBoto";
            MiCuentaBoto.Padding = new Padding(20, 0, 0, 0);
            MiCuentaBoto.Size = new Size(240, 45);
            MiCuentaBoto.TabIndex = 10;
            MiCuentaBoto.Text = "⚙️  Mi Cuenta";
            MiCuentaBoto.TextAlign = ContentAlignment.MiddleLeft;
            MiCuentaBoto.UseVisualStyleBackColor = false;
            MiCuentaBoto.Click += MiCuentaBoto_Click;
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
            CapcaleraPanel.TabIndex = 1;
            // 
            // TitolAppLbl
            // 
            TitolAppLbl.AutoSize = true;
            TitolAppLbl.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            TitolAppLbl.ForeColor = Color.FromArgb(45, 35, 30);
            TitolAppLbl.Location = new Point(30, 25);
            TitolAppLbl.Name = "TitolAppLbl";
            TitolAppLbl.Size = new Size(264, 30);
            TitolAppLbl.TabIndex = 0;
            TitolAppLbl.Text = "Panel de Administración";
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
            // ContenidoPanel
            // 
            ContenidoPanel.AutoScroll = true;
            ContenidoPanel.BackColor = Color.FromArgb(250, 245, 240);
            ContenidoPanel.Controls.Add(TitolPaginaLbl);
            ContenidoPanel.Controls.Add(SubtitolPaginaLbl);
            ContenidoPanel.Controls.Add(TarjetaServiciosPanel);
            ContenidoPanel.Controls.Add(TarjetaUsuariosPanel);
            ContenidoPanel.Controls.Add(TarjetaCitasPanel);
            ContenidoPanel.Controls.Add(TarjetaCuentaPanel);
            ContenidoPanel.Location = new Point(260, 80);
            ContenidoPanel.Name = "ContenidoPanel";
            ContenidoPanel.Size = new Size(1140, 720);
            ContenidoPanel.TabIndex = 0;
            // 
            // TitolPaginaLbl
            // 
            TitolPaginaLbl.AutoSize = true;
            TitolPaginaLbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            TitolPaginaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            TitolPaginaLbl.Location = new Point(40, 30);
            TitolPaginaLbl.Name = "TitolPaginaLbl";
            TitolPaginaLbl.Size = new Size(443, 37);
            TitolPaginaLbl.TabIndex = 0;
            TitolPaginaLbl.Text = "Bienvenido al Sistema de Gestión";
            // 
            // SubtitolPaginaLbl
            // 
            SubtitolPaginaLbl.AutoSize = true;
            SubtitolPaginaLbl.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            SubtitolPaginaLbl.ForeColor = Color.FromArgb(139, 90, 60);
            SubtitolPaginaLbl.Location = new Point(40, 70);
            SubtitolPaginaLbl.Name = "SubtitolPaginaLbl";
            SubtitolPaginaLbl.Size = new Size(271, 21);
            SubtitolPaginaLbl.TabIndex = 1;
            SubtitolPaginaLbl.Text = "Selecciona una opción para comenzar";
            // 
            // TarjetaServiciosPanel
            // 
            TarjetaServiciosPanel.BackColor = Color.White;
            TarjetaServiciosPanel.Controls.Add(iconoServicios);
            TarjetaServiciosPanel.Controls.Add(tituloServicios);
            TarjetaServiciosPanel.Controls.Add(descripcionServicios);
            TarjetaServiciosPanel.Cursor = Cursors.Hand;
            TarjetaServiciosPanel.Location = new Point(40, 140);
            TarjetaServiciosPanel.Name = "TarjetaServiciosPanel";
            TarjetaServiciosPanel.Size = new Size(320, 200);
            TarjetaServiciosPanel.TabIndex = 2;
            TarjetaServiciosPanel.Click += ServiciosBoto_Click;
            // 
            // iconoServicios
            // 
            iconoServicios.Font = new Font("Segoe UI Emoji", 40F, FontStyle.Regular, GraphicsUnit.Point);
            iconoServicios.ForeColor = Color.FromArgb(255, 140, 0);
            iconoServicios.Location = new Point(0, 20);
            iconoServicios.Name = "iconoServicios";
            iconoServicios.Size = new Size(320, 80);
            iconoServicios.TabIndex = 0;
            iconoServicios.Text = "✂️";
            iconoServicios.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tituloServicios
            // 
            tituloServicios.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            tituloServicios.ForeColor = Color.FromArgb(45, 35, 30);
            tituloServicios.Location = new Point(0, 100);
            tituloServicios.Name = "tituloServicios";
            tituloServicios.Size = new Size(320, 40);
            tituloServicios.TabIndex = 1;
            tituloServicios.Text = "Servicios";
            tituloServicios.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // descripcionServicios
            // 
            descripcionServicios.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            descripcionServicios.ForeColor = Color.FromArgb(139, 90, 60);
            descripcionServicios.Location = new Point(0, 140);
            descripcionServicios.Name = "descripcionServicios";
            descripcionServicios.Size = new Size(320, 30);
            descripcionServicios.TabIndex = 2;
            descripcionServicios.Text = "Gestionar servicios de peluquería";
            descripcionServicios.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TarjetaUsuariosPanel
            // 
            TarjetaUsuariosPanel.BackColor = Color.White;
            TarjetaUsuariosPanel.Controls.Add(iconoUsuarios);
            TarjetaUsuariosPanel.Controls.Add(tituloUsuarios);
            TarjetaUsuariosPanel.Controls.Add(descripcionUsuarios);
            TarjetaUsuariosPanel.Cursor = Cursors.Hand;
            TarjetaUsuariosPanel.Location = new Point(390, 140);
            TarjetaUsuariosPanel.Name = "TarjetaUsuariosPanel";
            TarjetaUsuariosPanel.Size = new Size(320, 200);
            TarjetaUsuariosPanel.TabIndex = 3;
            TarjetaUsuariosPanel.Click += UsuariosBoto_Click;
            // 
            // iconoUsuarios
            // 
            iconoUsuarios.Font = new Font("Segoe UI Emoji", 40F, FontStyle.Regular, GraphicsUnit.Point);
            iconoUsuarios.ForeColor = Color.FromArgb(255, 140, 0);
            iconoUsuarios.Location = new Point(0, 20);
            iconoUsuarios.Name = "iconoUsuarios";
            iconoUsuarios.Size = new Size(320, 80);
            iconoUsuarios.TabIndex = 0;
            iconoUsuarios.Text = "👥";
            iconoUsuarios.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tituloUsuarios
            // 
            tituloUsuarios.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            tituloUsuarios.ForeColor = Color.FromArgb(45, 35, 30);
            tituloUsuarios.Location = new Point(0, 100);
            tituloUsuarios.Name = "tituloUsuarios";
            tituloUsuarios.Size = new Size(320, 40);
            tituloUsuarios.TabIndex = 1;
            tituloUsuarios.Text = "Usuarios";
            tituloUsuarios.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // descripcionUsuarios
            // 
            descripcionUsuarios.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            descripcionUsuarios.ForeColor = Color.FromArgb(139, 90, 60);
            descripcionUsuarios.Location = new Point(0, 140);
            descripcionUsuarios.Name = "descripcionUsuarios";
            descripcionUsuarios.Size = new Size(320, 30);
            descripcionUsuarios.TabIndex = 2;
            descripcionUsuarios.Text = "Administrar usuarios del sistema";
            descripcionUsuarios.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TarjetaCitasPanel
            // 
            TarjetaCitasPanel.Location = new Point(0, 0);
            TarjetaCitasPanel.Name = "TarjetaCitasPanel";
            TarjetaCitasPanel.Size = new Size(200, 100);
            TarjetaCitasPanel.TabIndex = 4;
            // 
            // TarjetaCuentaPanel
            // 
            TarjetaCuentaPanel.BackColor = Color.White;
            TarjetaCuentaPanel.Controls.Add(iconoCuenta);
            TarjetaCuentaPanel.Controls.Add(tituloCuenta);
            TarjetaCuentaPanel.Controls.Add(descripcionCuenta);
            TarjetaCuentaPanel.Cursor = Cursors.Hand;
            TarjetaCuentaPanel.Location = new Point(740, 140);
            TarjetaCuentaPanel.Name = "TarjetaCuentaPanel";
            TarjetaCuentaPanel.Size = new Size(320, 200);
            TarjetaCuentaPanel.TabIndex = 5;
            TarjetaCuentaPanel.Click += MiCuentaBoto_Click;
            // 
            // iconoCuenta
            // 
            iconoCuenta.Font = new Font("Segoe UI Emoji", 40F, FontStyle.Regular, GraphicsUnit.Point);
            iconoCuenta.ForeColor = Color.FromArgb(255, 140, 0);
            iconoCuenta.Location = new Point(0, 20);
            iconoCuenta.Name = "iconoCuenta";
            iconoCuenta.Size = new Size(320, 80);
            iconoCuenta.TabIndex = 0;
            iconoCuenta.Text = "⚙️";
            iconoCuenta.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tituloCuenta
            // 
            tituloCuenta.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            tituloCuenta.ForeColor = Color.FromArgb(45, 35, 30);
            tituloCuenta.Location = new Point(0, 100);
            tituloCuenta.Name = "tituloCuenta";
            tituloCuenta.Size = new Size(320, 40);
            tituloCuenta.TabIndex = 1;
            tituloCuenta.Text = "Mi Cuenta";
            tituloCuenta.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // descripcionCuenta
            // 
            descripcionCuenta.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            descripcionCuenta.ForeColor = Color.FromArgb(139, 90, 60);
            descripcionCuenta.Location = new Point(0, 140);
            descripcionCuenta.Name = "descripcionCuenta";
            descripcionCuenta.Size = new Size(320, 30);
            descripcionCuenta.TabIndex = 2;
            descripcionCuenta.Text = "Ver y editar información personal";
            descripcionCuenta.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pbLogo
            // 
            pbLogo.Location = new Point(78, 5);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(100, 50);
            pbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pbLogo.TabIndex = 12;
            pbLogo.TabStop = false;
            // 
            // HomeForm
            // 
            BackColor = Color.FromArgb(250, 245, 240);
            ClientSize = new Size(1370, 749);
            Controls.Add(ContenidoPanel);
            Controls.Add(CapcaleraPanel);
            Controls.Add(LateralPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "HomeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Peluquería Bernat Sarriá - Inicio";
            LateralPanel.ResumeLayout(false);
            CapcaleraPanel.ResumeLayout(false);
            CapcaleraPanel.PerformLayout();
            ContenidoPanel.ResumeLayout(false);
            ContenidoPanel.PerformLayout();
            TarjetaServiciosPanel.ResumeLayout(false);
            TarjetaUsuariosPanel.ResumeLayout(false);
            TarjetaCuentaPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            ResumeLayout(false);
        }

        private PictureBox pbLogo;
        private Label iconoServicios;
        private Label tituloServicios;
        private Label descripcionServicios;
        private Label iconoUsuarios;
        private Label tituloUsuarios;
        private Label descripcionUsuarios;
        private Label iconoCuenta;
        private Label tituloCuenta;
        private Label descripcionCuenta;
    }
}