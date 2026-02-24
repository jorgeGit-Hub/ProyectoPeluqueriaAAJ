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
            this.LateralPanel = new Panel();
            this.CapcaleraPanel = new Panel();
            this.ContenidoPanel = new Panel();
            this.LogoLbl = new Label();
            this.TitolAppLbl = new Label();
            this.BienvenidaLbl = new Label();
            this.IniciBoto = new Button();
            this.ServiciosBoto = new Button();
            this.UsuariosBoto = new Button();
            this.ClientesBoto = new Button();
            this.CitasBoto = new Button();
            this.GruposBoto = new Button();
            this.HorarioSemanalBoto = new Button();
            this.HorarioBoto = new Button();
            this.ValoracionesBoto = new Button();
            this.MiCuentaBoto = new Button();
            this.TancarSessioBoto = new Button();
            this.TitolPaginaLbl = new Label();
            this.SubtitolPaginaLbl = new Label();
            this.TarjetaServiciosPanel = new Panel();
            this.TarjetaUsuariosPanel = new Panel();
            this.TarjetaCitasPanel = new Panel();
            this.TarjetaCuentaPanel = new Panel();

            // 
            // HomeForm
            // 
            this.ClientSize = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Peluquería Escola - Inicio";
            this.BackColor = Color.FromArgb(250, 245, 240);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // 
            // LateralPanel
            // 
            this.LateralPanel.Dock = DockStyle.Left;
            this.LateralPanel.Width = 260;
            this.LateralPanel.BackColor = Color.FromArgb(45, 35, 30);

            // 
            // LogoLbl
            // 
            this.LogoLbl.Text = "✂️\nPeluquería\nEscola";
            this.LogoLbl.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            this.LogoLbl.ForeColor = Color.FromArgb(255, 140, 0);
            this.LogoLbl.AutoSize = false;
            this.LogoLbl.TextAlign = ContentAlignment.MiddleCenter;
            this.LogoLbl.Size = new Size(260, 100);
            this.LogoLbl.Location = new Point(0, 20);

            // 
            // IniciBoto (ACTIVO)
            // 
            this.IniciBoto.Text = "🏠  Inicio";
            this.IniciBoto.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.IniciBoto.ForeColor = Color.White;
            this.IniciBoto.BackColor = Color.FromArgb(255, 140, 0);
            this.IniciBoto.FlatStyle = FlatStyle.Flat;
            this.IniciBoto.FlatAppearance.BorderSize = 0;
            this.IniciBoto.Size = new Size(240, 45);
            this.IniciBoto.Location = new Point(10, 130);
            this.IniciBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.IniciBoto.Padding = new Padding(20, 0, 0, 0);
            this.IniciBoto.Cursor = Cursors.Hand;

            // 
            // ServiciosBoto
            // 
            this.ServiciosBoto.Text = "✂️  Servicios";
            this.ServiciosBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.ServiciosBoto.ForeColor = Color.FromArgb(200, 200, 200);
            this.ServiciosBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.ServiciosBoto.FlatStyle = FlatStyle.Flat;
            this.ServiciosBoto.FlatAppearance.BorderSize = 0;
            this.ServiciosBoto.Size = new Size(240, 45);
            this.ServiciosBoto.Location = new Point(10, 180);
            this.ServiciosBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.ServiciosBoto.Padding = new Padding(20, 0, 0, 0);
            this.ServiciosBoto.Cursor = Cursors.Hand;
            this.ServiciosBoto.Click += new System.EventHandler(this.ServiciosBoto_Click);

            // 
            // UsuariosBoto
            // 
            this.UsuariosBoto.Text = "👥  Usuarios";
            this.UsuariosBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.UsuariosBoto.ForeColor = Color.FromArgb(200, 200, 200);
            this.UsuariosBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.UsuariosBoto.FlatStyle = FlatStyle.Flat;
            this.UsuariosBoto.FlatAppearance.BorderSize = 0;
            this.UsuariosBoto.Size = new Size(240, 45);
            this.UsuariosBoto.Location = new Point(10, 230);
            this.UsuariosBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.UsuariosBoto.Padding = new Padding(20, 0, 0, 0);
            this.UsuariosBoto.Cursor = Cursors.Hand;
            this.UsuariosBoto.Click += new System.EventHandler(this.UsuariosBoto_Click);

            // 
            // ClientesBoto
            // 
            this.ClientesBoto.Text = "👤  Clientes";
            this.ClientesBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.ClientesBoto.ForeColor = Color.FromArgb(200, 200, 200);
            this.ClientesBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.ClientesBoto.FlatStyle = FlatStyle.Flat;
            this.ClientesBoto.FlatAppearance.BorderSize = 0;
            this.ClientesBoto.Size = new Size(240, 45);
            this.ClientesBoto.Location = new Point(10, 280);
            this.ClientesBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.ClientesBoto.Padding = new Padding(20, 0, 0, 0);
            this.ClientesBoto.Cursor = Cursors.Hand;
            this.ClientesBoto.Click += new System.EventHandler(this.ClientesBoto_Click);

            // 
            // CitasBoto
            // 
            this.CitasBoto.Text = "📅  Citas";
            this.CitasBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.CitasBoto.ForeColor = Color.FromArgb(200, 200, 200);
            this.CitasBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.CitasBoto.FlatStyle = FlatStyle.Flat;
            this.CitasBoto.FlatAppearance.BorderSize = 0;
            this.CitasBoto.Size = new Size(240, 45);
            this.CitasBoto.Location = new Point(10, 330);
            this.CitasBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.CitasBoto.Padding = new Padding(20, 0, 0, 0);
            this.CitasBoto.Cursor = Cursors.Hand;
            this.CitasBoto.Click += new System.EventHandler(this.CitasBoto_Click);

            // 
            // GruposBoto
            // 
            this.GruposBoto.Text = "👨‍👩‍👧‍👦  Grupos";
            this.GruposBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.GruposBoto.ForeColor = Color.FromArgb(200, 200, 200);
            this.GruposBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.GruposBoto.FlatStyle = FlatStyle.Flat;
            this.GruposBoto.FlatAppearance.BorderSize = 0;
            this.GruposBoto.Size = new Size(240, 45);
            this.GruposBoto.Location = new Point(10, 380);
            this.GruposBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.GruposBoto.Padding = new Padding(20, 0, 0, 0);
            this.GruposBoto.Cursor = Cursors.Hand;
            this.GruposBoto.Click += new System.EventHandler(this.GruposBoto_Click);

            // 
            // HorarioBoto
            // 
            this.HorarioBoto.Text = "🗓️  Horario Semanal";
            this.HorarioBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.HorarioBoto.ForeColor = Color.FromArgb(200, 200, 200);
            this.HorarioBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.HorarioBoto.FlatStyle = FlatStyle.Flat;
            this.HorarioBoto.FlatAppearance.BorderSize = 0;
            this.HorarioBoto.Size = new Size(240, 45);
            this.HorarioBoto.Location = new Point(10, 430);
            this.HorarioBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.HorarioBoto.Padding = new Padding(20, 0, 0, 0);
            this.HorarioBoto.Cursor = Cursors.Hand;
            this.HorarioBoto.Click += new System.EventHandler(this.HorarioForm_Click);

            // 
            // HorarioSemanalBoto
            // 
            this.HorarioSemanalBoto.Text = "🕐  Bloqueo Horario";
            this.HorarioSemanalBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.HorarioSemanalBoto.ForeColor = Color.FromArgb(200, 200, 200);
            this.HorarioSemanalBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.HorarioSemanalBoto.FlatStyle = FlatStyle.Flat;
            this.HorarioSemanalBoto.FlatAppearance.BorderSize = 0;
            this.HorarioSemanalBoto.Size = new Size(240, 45);
            this.HorarioSemanalBoto.Location = new Point(10, 480);
            this.HorarioSemanalBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.HorarioSemanalBoto.Padding = new Padding(20, 0, 0, 0);
            this.HorarioSemanalBoto.Cursor = Cursors.Hand;
            this.HorarioSemanalBoto.Click += new System.EventHandler(this.HorarioSemanalBoto_Click);

            // 
            // ValoracionesBoto
            // 
            this.ValoracionesBoto.Text = "⭐  Valoraciones";
            this.ValoracionesBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.ValoracionesBoto.ForeColor = Color.FromArgb(200, 200, 200);
            this.ValoracionesBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.ValoracionesBoto.FlatStyle = FlatStyle.Flat;
            this.ValoracionesBoto.FlatAppearance.BorderSize = 0;
            this.ValoracionesBoto.Size = new Size(240, 45);
            this.ValoracionesBoto.Location = new Point(10, 530);
            this.ValoracionesBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.ValoracionesBoto.Padding = new Padding(20, 0, 0, 0);
            this.ValoracionesBoto.Cursor = Cursors.Hand;
            this.ValoracionesBoto.Click += new System.EventHandler(this.ValoracionForm_Click);

            // 
            // MiCuentaBoto
            // 
            this.MiCuentaBoto.Text = "⚙️  Mi Cuenta";
            this.MiCuentaBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.MiCuentaBoto.ForeColor = Color.FromArgb(200, 200, 200);
            this.MiCuentaBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.MiCuentaBoto.FlatStyle = FlatStyle.Flat;
            this.MiCuentaBoto.FlatAppearance.BorderSize = 0;
            this.MiCuentaBoto.Size = new Size(240, 45);
            this.MiCuentaBoto.Location = new Point(10, 580);
            this.MiCuentaBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.MiCuentaBoto.Padding = new Padding(20, 0, 0, 0);
            this.MiCuentaBoto.Cursor = Cursors.Hand;
            this.MiCuentaBoto.Click += new System.EventHandler(this.MiCuentaBoto_Click);

            // 
            // TancarSessioBoto
            // 
            this.TancarSessioBoto.Text = "🚪  Cerrar Sesión";
            this.TancarSessioBoto.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.TancarSessioBoto.ForeColor = Color.FromArgb(150, 150, 150);
            this.TancarSessioBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.TancarSessioBoto.FlatStyle = FlatStyle.Flat;
            this.TancarSessioBoto.FlatAppearance.BorderSize = 0;
            this.TancarSessioBoto.Size = new Size(240, 45);
            this.TancarSessioBoto.Location = new Point(10, 720);
            this.TancarSessioBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.TancarSessioBoto.Padding = new Padding(20, 0, 0, 0);
            this.TancarSessioBoto.Cursor = Cursors.Hand;
            this.TancarSessioBoto.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.TancarSessioBoto.Click += new System.EventHandler(this.TancarSessioBoto_Click);

            // CapcaleraPanel
            this.CapcaleraPanel.Dock = DockStyle.Top;
            this.CapcaleraPanel.Height = 80;
            this.CapcaleraPanel.BackColor = Color.White;

            // TitolAppLbl
            this.TitolAppLbl.Text = "Panel de Administración";
            this.TitolAppLbl.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            this.TitolAppLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TitolAppLbl.AutoSize = true;
            this.TitolAppLbl.Location = new Point(30, 25);

            // BienvenidaLbl
            this.BienvenidaLbl.Text = "Bienvenido/a";
            this.BienvenidaLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.BienvenidaLbl.ForeColor = Color.FromArgb(139, 90, 60);
            this.BienvenidaLbl.AutoSize = true;
            this.BienvenidaLbl.Location = new Point(1050, 30);
            this.BienvenidaLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // ContenidoPanel
            this.ContenidoPanel.Location = new Point(260, 80);
            this.ContenidoPanel.Size = new Size(1140, 720);
            this.ContenidoPanel.BackColor = Color.FromArgb(250, 245, 240);
            this.ContenidoPanel.AutoScroll = true;

            // TitolPaginaLbl
            this.TitolPaginaLbl.Text = "Bienvenido al Sistema de Gestión";
            this.TitolPaginaLbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            this.TitolPaginaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TitolPaginaLbl.AutoSize = true;
            this.TitolPaginaLbl.Location = new Point(40, 30);

            // SubtitolPaginaLbl
            this.SubtitolPaginaLbl.Text = "Selecciona una opción para comenzar";
            this.SubtitolPaginaLbl.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            this.SubtitolPaginaLbl.ForeColor = Color.FromArgb(139, 90, 60);
            this.SubtitolPaginaLbl.AutoSize = true;
            this.SubtitolPaginaLbl.Location = new Point(40, 70);

            // TarjetaServiciosPanel
            this.TarjetaServiciosPanel.BackColor = Color.White;
            this.TarjetaServiciosPanel.Size = new Size(320, 200);
            this.TarjetaServiciosPanel.Location = new Point(40, 140);
            this.TarjetaServiciosPanel.Cursor = Cursors.Hand;
            this.TarjetaServiciosPanel.Click += new System.EventHandler(this.ServiciosBoto_Click);

            Label iconoServicios = new Label();
            iconoServicios.Text = "✂️";
            iconoServicios.Font = new Font("Segoe UI Emoji", 40F);
            iconoServicios.ForeColor = Color.FromArgb(255, 140, 0);
            iconoServicios.AutoSize = false;
            iconoServicios.Size = new Size(320, 80);
            iconoServicios.TextAlign = ContentAlignment.MiddleCenter;
            iconoServicios.Location = new Point(0, 20);

            Label tituloServicios = new Label();
            tituloServicios.Text = "Servicios";
            tituloServicios.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            tituloServicios.ForeColor = Color.FromArgb(45, 35, 30);
            tituloServicios.AutoSize = false;
            tituloServicios.Size = new Size(320, 40);
            tituloServicios.TextAlign = ContentAlignment.MiddleCenter;
            tituloServicios.Location = new Point(0, 100);

            Label descripcionServicios = new Label();
            descripcionServicios.Text = "Gestionar servicios de peluquería";
            descripcionServicios.Font = new Font("Segoe UI", 10F);
            descripcionServicios.ForeColor = Color.FromArgb(139, 90, 60);
            descripcionServicios.AutoSize = false;
            descripcionServicios.Size = new Size(320, 30);
            descripcionServicios.TextAlign = ContentAlignment.MiddleCenter;
            descripcionServicios.Location = new Point(0, 140);

            this.TarjetaServiciosPanel.Controls.Add(iconoServicios);
            this.TarjetaServiciosPanel.Controls.Add(tituloServicios);
            this.TarjetaServiciosPanel.Controls.Add(descripcionServicios);

            // TarjetaUsuariosPanel
            this.TarjetaUsuariosPanel.BackColor = Color.White;
            this.TarjetaUsuariosPanel.Size = new Size(320, 200);
            this.TarjetaUsuariosPanel.Location = new Point(390, 140);
            this.TarjetaUsuariosPanel.Cursor = Cursors.Hand;
            this.TarjetaUsuariosPanel.Click += new System.EventHandler(this.UsuariosBoto_Click);

            Label iconoUsuarios = new Label();
            iconoUsuarios.Text = "👥";
            iconoUsuarios.Font = new Font("Segoe UI Emoji", 40F);
            iconoUsuarios.ForeColor = Color.FromArgb(255, 140, 0);
            iconoUsuarios.AutoSize = false;
            iconoUsuarios.Size = new Size(320, 80);
            iconoUsuarios.TextAlign = ContentAlignment.MiddleCenter;
            iconoUsuarios.Location = new Point(0, 20);

            Label tituloUsuarios = new Label();
            tituloUsuarios.Text = "Usuarios";
            tituloUsuarios.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            tituloUsuarios.ForeColor = Color.FromArgb(45, 35, 30);
            tituloUsuarios.AutoSize = false;
            tituloUsuarios.Size = new Size(320, 40);
            tituloUsuarios.TextAlign = ContentAlignment.MiddleCenter;
            tituloUsuarios.Location = new Point(0, 100);

            Label descripcionUsuarios = new Label();
            descripcionUsuarios.Text = "Administrar usuarios del sistema";
            descripcionUsuarios.Font = new Font("Segoe UI", 10F);
            descripcionUsuarios.ForeColor = Color.FromArgb(139, 90, 60);
            descripcionUsuarios.AutoSize = false;
            descripcionUsuarios.Size = new Size(320, 30);
            descripcionUsuarios.TextAlign = ContentAlignment.MiddleCenter;
            descripcionUsuarios.Location = new Point(0, 140);

            this.TarjetaUsuariosPanel.Controls.Add(iconoUsuarios);
            this.TarjetaUsuariosPanel.Controls.Add(tituloUsuarios);
            this.TarjetaUsuariosPanel.Controls.Add(descripcionUsuarios);

            // TarjetaCuentaPanel
            this.TarjetaCuentaPanel.BackColor = Color.White;
            this.TarjetaCuentaPanel.Size = new Size(320, 200);
            this.TarjetaCuentaPanel.Location = new Point(740, 140);
            this.TarjetaCuentaPanel.Cursor = Cursors.Hand;
            this.TarjetaCuentaPanel.Click += new System.EventHandler(this.MiCuentaBoto_Click);

            Label iconoCuenta = new Label();
            iconoCuenta.Text = "⚙️";
            iconoCuenta.Font = new Font("Segoe UI Emoji", 40F);
            iconoCuenta.ForeColor = Color.FromArgb(255, 140, 0);
            iconoCuenta.AutoSize = false;
            iconoCuenta.Size = new Size(320, 80);
            iconoCuenta.TextAlign = ContentAlignment.MiddleCenter;
            iconoCuenta.Location = new Point(0, 20);

            Label tituloCuenta = new Label();
            tituloCuenta.Text = "Mi Cuenta";
            tituloCuenta.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            tituloCuenta.ForeColor = Color.FromArgb(45, 35, 30);
            tituloCuenta.AutoSize = false;
            tituloCuenta.Size = new Size(320, 40);
            tituloCuenta.TextAlign = ContentAlignment.MiddleCenter;
            tituloCuenta.Location = new Point(0, 100);

            Label descripcionCuenta = new Label();
            descripcionCuenta.Text = "Ver y editar información personal";
            descripcionCuenta.Font = new Font("Segoe UI", 10F);
            descripcionCuenta.ForeColor = Color.FromArgb(139, 90, 60);
            descripcionCuenta.AutoSize = false;
            descripcionCuenta.Size = new Size(320, 30);
            descripcionCuenta.TextAlign = ContentAlignment.MiddleCenter;
            descripcionCuenta.Location = new Point(0, 140);

            this.TarjetaCuentaPanel.Controls.Add(iconoCuenta);
            this.TarjetaCuentaPanel.Controls.Add(tituloCuenta);
            this.TarjetaCuentaPanel.Controls.Add(descripcionCuenta);

            // Component Adds
            this.ContenidoPanel.Controls.Add(this.TitolPaginaLbl);
            this.ContenidoPanel.Controls.Add(this.SubtitolPaginaLbl);
            this.ContenidoPanel.Controls.Add(this.TarjetaServiciosPanel);
            this.ContenidoPanel.Controls.Add(this.TarjetaUsuariosPanel);
            this.ContenidoPanel.Controls.Add(this.TarjetaCitasPanel);
            this.ContenidoPanel.Controls.Add(this.TarjetaCuentaPanel);

            this.LateralPanel.Controls.Add(this.LogoLbl);
            this.LateralPanel.Controls.Add(this.IniciBoto);
            this.LateralPanel.Controls.Add(this.ServiciosBoto);
            this.LateralPanel.Controls.Add(this.UsuariosBoto);
            this.LateralPanel.Controls.Add(this.ClientesBoto);
            this.LateralPanel.Controls.Add(this.CitasBoto);
            this.LateralPanel.Controls.Add(this.GruposBoto);
            this.LateralPanel.Controls.Add(this.HorarioBoto);
            this.LateralPanel.Controls.Add(this.HorarioSemanalBoto);
            this.LateralPanel.Controls.Add(this.ValoracionesBoto);
            this.LateralPanel.Controls.Add(this.MiCuentaBoto);
            this.LateralPanel.Controls.Add(this.TancarSessioBoto);

            this.CapcaleraPanel.Controls.Add(this.TitolAppLbl);
            this.CapcaleraPanel.Controls.Add(this.BienvenidaLbl);

            this.Controls.Add(this.ContenidoPanel);
            this.Controls.Add(this.CapcaleraPanel);
            this.Controls.Add(this.LateralPanel);
        }
    }
}