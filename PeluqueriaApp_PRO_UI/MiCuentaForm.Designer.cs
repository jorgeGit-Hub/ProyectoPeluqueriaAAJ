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
            this.LateralPanel = new Panel();
            this.CapcaleraPanel = new Panel();
            this.LogoLbl = new Label();
            this.TitolAppLbl = new Label();
            this.BienvenidaLbl = new Label();
            this.IniciBoto = new Button();
            this.ServiciosBoto = new Button();
            this.UsuariosBoto = new Button();
            this.ClientesBoto = new Button();
            this.CitasBoto = new Button();
            this.GruposBoto = new Button();
            this.HorarioBoto = new Button();
            this.HorarioSemanalBoto = new Button();
            this.ValoracionesBoto = new Button();
            this.MiCuentaBoto = new Button();
            this.TancarSessioBoto = new Button();

            this.TitolPaginaLbl = new Label();
            this.TarjetaPerfilPanel = new Panel();
            this.TituloDatosLbl = new Label();
            this.NombreLbl = new Label();
            this.NombreTxt = new TextBox();
            this.ApellidosLbl = new Label();
            this.ApellidosTxt = new TextBox();
            this.CorreoLbl = new Label();
            this.CorreoTxt = new TextBox();
            this.RolLbl = new Label();
            this.RolTxt = new TextBox();
            this.GuardarCambiosBtn = new Button();
            this.CambiarPasswordBtn = new Button();

            this.NuevaContrasenaLbl = new Label();
            this.NuevaContrasenaTxt = new TextBox();
            this.ConfirmarContrasenaLbl = new Label();
            this.ConfirmarContrasenaTxt = new TextBox();
            this.GuardarContrasenaBtn = new Button();

            this.SuspendLayout();

            // 
            // MiCuentaForm
            // 
            this.ClientSize = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Peluquería Escola - Mi Cuenta";
            this.BackColor = Color.FromArgb(250, 245, 240);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // LateralPanel
            this.LateralPanel.Dock = DockStyle.Left;
            this.LateralPanel.Width = 260;
            this.LateralPanel.BackColor = Color.FromArgb(45, 35, 30);

            // LogoLbl
            this.LogoLbl.Text = "✂️\nPeluquería\nEscola";
            this.LogoLbl.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            this.LogoLbl.ForeColor = Color.FromArgb(255, 140, 0);
            this.LogoLbl.AutoSize = false;
            this.LogoLbl.TextAlign = ContentAlignment.MiddleCenter;
            this.LogoLbl.Size = new Size(260, 100);
            this.LogoLbl.Location = new Point(0, 20);

            // Botones Menú Lateral (MiCuenta es el Activo)
            this.IniciBoto.Text = "🏠  Inicio";
            this.IniciBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.IniciBoto.ForeColor = Color.FromArgb(200, 200, 200);
            this.IniciBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.IniciBoto.FlatStyle = FlatStyle.Flat;
            this.IniciBoto.FlatAppearance.BorderSize = 0;
            this.IniciBoto.Size = new Size(240, 45);
            this.IniciBoto.Location = new Point(10, 130);
            this.IniciBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.IniciBoto.Padding = new Padding(20, 0, 0, 0);
            this.IniciBoto.Cursor = Cursors.Hand;
            this.IniciBoto.Click += new System.EventHandler(this.IniciBoto_Click);

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

            // BOTÓN ACTIVO (Mi Cuenta)
            this.MiCuentaBoto.Text = "⚙️  Mi Cuenta";
            this.MiCuentaBoto.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.MiCuentaBoto.ForeColor = Color.White;
            this.MiCuentaBoto.BackColor = Color.FromArgb(255, 140, 0);
            this.MiCuentaBoto.FlatStyle = FlatStyle.Flat;
            this.MiCuentaBoto.FlatAppearance.BorderSize = 0;
            this.MiCuentaBoto.Size = new Size(240, 45);
            this.MiCuentaBoto.Location = new Point(10, 580);
            this.MiCuentaBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.MiCuentaBoto.Padding = new Padding(20, 0, 0, 0);
            this.MiCuentaBoto.Cursor = Cursors.Hand;

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

            this.TitolAppLbl.Text = "Mi Perfil";
            this.TitolAppLbl.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            this.TitolAppLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TitolAppLbl.AutoSize = true;
            this.TitolAppLbl.Location = new Point(30, 25);

            this.BienvenidaLbl.Text = "Bienvenido/a";
            this.BienvenidaLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.BienvenidaLbl.ForeColor = Color.FromArgb(139, 90, 60);
            this.BienvenidaLbl.AutoSize = true;
            this.BienvenidaLbl.Location = new Point(1050, 30);
            this.BienvenidaLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // TitolPaginaLbl
            this.TitolPaginaLbl.Text = "Datos de tu Cuenta";
            this.TitolPaginaLbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            this.TitolPaginaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TitolPaginaLbl.AutoSize = true;
            this.TitolPaginaLbl.Location = new Point(290, 110);

            // TarjetaPerfilPanel
            this.TarjetaPerfilPanel.BackColor = Color.White;
            this.TarjetaPerfilPanel.Location = new Point(290, 170);
            this.TarjetaPerfilPanel.Size = new Size(600, 580);
            this.TarjetaPerfilPanel.BorderStyle = BorderStyle.FixedSingle;

            // TituloDatosLbl
            this.TituloDatosLbl.Text = "Información Personal";
            this.TituloDatosLbl.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            this.TituloDatosLbl.ForeColor = Color.FromArgb(255, 140, 0);
            this.TituloDatosLbl.AutoSize = true;
            this.TituloDatosLbl.Location = new Point(30, 30);

            // Nombre
            this.NombreLbl.Text = "Nombre:";
            this.NombreLbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.NombreLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.NombreLbl.Location = new Point(30, 80);
            this.NombreLbl.AutoSize = true;

            this.NombreTxt.Font = new Font("Segoe UI", 11F);
            this.NombreTxt.Location = new Point(30, 110);
            this.NombreTxt.Size = new Size(530, 32);
            this.NombreTxt.ReadOnly = true;
            this.NombreTxt.BackColor = Color.FromArgb(245, 245, 245);

            // Apellidos
            this.ApellidosLbl.Text = "Apellidos:";
            this.ApellidosLbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.ApellidosLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.ApellidosLbl.Location = new Point(30, 160);
            this.ApellidosLbl.AutoSize = true;

            this.ApellidosTxt.Font = new Font("Segoe UI", 11F);
            this.ApellidosTxt.Location = new Point(30, 190);
            this.ApellidosTxt.Size = new Size(530, 32);
            this.ApellidosTxt.ReadOnly = true;
            this.ApellidosTxt.BackColor = Color.FromArgb(245, 245, 245);

            // Correo
            this.CorreoLbl.Text = "Correo Electrónico:";
            this.CorreoLbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.CorreoLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.CorreoLbl.Location = new Point(30, 240);
            this.CorreoLbl.AutoSize = true;

            this.CorreoTxt.Font = new Font("Segoe UI", 11F);
            this.CorreoTxt.Location = new Point(30, 270);
            this.CorreoTxt.Size = new Size(530, 32);
            this.CorreoTxt.ReadOnly = true;
            this.CorreoTxt.BackColor = Color.FromArgb(245, 245, 245);

            // Rol
            this.RolLbl.Text = "Rol en el Sistema:";
            this.RolLbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.RolLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.RolLbl.Location = new Point(30, 320);
            this.RolLbl.AutoSize = true;

            this.RolTxt.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.RolTxt.Location = new Point(30, 350);
            this.RolTxt.Size = new Size(530, 32);
            this.RolTxt.ReadOnly = true;
            this.RolTxt.BackColor = Color.FromArgb(245, 245, 245);
            this.RolTxt.ForeColor = Color.FromArgb(139, 90, 60);

            // --- CAMPOS NUEVA CONTRASEÑA ---

            this.NuevaContrasenaLbl.Text = "Nueva Contraseña:";
            this.NuevaContrasenaLbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.NuevaContrasenaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.NuevaContrasenaLbl.Location = new Point(30, 400);
            this.NuevaContrasenaLbl.AutoSize = true;
            this.NuevaContrasenaLbl.Visible = false;

            this.NuevaContrasenaTxt.Font = new Font("Segoe UI", 11F);
            this.NuevaContrasenaTxt.Location = new Point(30, 430);
            this.NuevaContrasenaTxt.Size = new Size(530, 32);
            this.NuevaContrasenaTxt.PasswordChar = '*';
            this.NuevaContrasenaTxt.Visible = false;

            this.ConfirmarContrasenaLbl.Text = "Confirmar Contraseña:";
            this.ConfirmarContrasenaLbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.ConfirmarContrasenaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.ConfirmarContrasenaLbl.Location = new Point(30, 480);
            this.ConfirmarContrasenaLbl.AutoSize = true;
            this.ConfirmarContrasenaLbl.Visible = false;

            this.ConfirmarContrasenaTxt.Font = new Font("Segoe UI", 11F);
            this.ConfirmarContrasenaTxt.Location = new Point(30, 510);
            this.ConfirmarContrasenaTxt.Size = new Size(530, 32);
            this.ConfirmarContrasenaTxt.PasswordChar = '*';
            this.ConfirmarContrasenaTxt.Visible = false;

            // Botones de acción principales
            this.GuardarCambiosBtn.Text = "✏️ Editar Perfil";
            this.GuardarCambiosBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.GuardarCambiosBtn.Size = new Size(200, 45);
            this.GuardarCambiosBtn.Location = new Point(920, 170);
            this.GuardarCambiosBtn.BackColor = Color.FromArgb(139, 90, 60);
            this.GuardarCambiosBtn.ForeColor = Color.White;
            this.GuardarCambiosBtn.FlatStyle = FlatStyle.Flat;
            this.GuardarCambiosBtn.FlatAppearance.BorderSize = 0;
            this.GuardarCambiosBtn.Cursor = Cursors.Hand;
            this.GuardarCambiosBtn.Click += new System.EventHandler(this.GuardarCambiosBtn_Click); // ENLAZADO

            this.CambiarPasswordBtn.Text = "🔑 Cambiar Contraseña";
            this.CambiarPasswordBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.CambiarPasswordBtn.Size = new Size(220, 45);
            this.CambiarPasswordBtn.Location = new Point(920, 230);
            this.CambiarPasswordBtn.BackColor = Color.FromArgb(70, 130, 180);
            this.CambiarPasswordBtn.ForeColor = Color.White;
            this.CambiarPasswordBtn.FlatStyle = FlatStyle.Flat;
            this.CambiarPasswordBtn.FlatAppearance.BorderSize = 0;
            this.CambiarPasswordBtn.Cursor = Cursors.Hand;
            this.CambiarPasswordBtn.Click += new System.EventHandler(this.AccionDesarrollo_Click); // ENLAZADO

            // Botón Guardar Nueva Contraseña
            this.GuardarContrasenaBtn.Text = "✓ Guardar Nueva Contraseña";
            this.GuardarContrasenaBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.GuardarContrasenaBtn.Size = new Size(250, 45);
            this.GuardarContrasenaBtn.Location = new Point(920, 290);
            this.GuardarContrasenaBtn.BackColor = Color.FromArgb(34, 139, 34);
            this.GuardarContrasenaBtn.ForeColor = Color.White;
            this.GuardarContrasenaBtn.FlatStyle = FlatStyle.Flat;
            this.GuardarContrasenaBtn.FlatAppearance.BorderSize = 0;
            this.GuardarContrasenaBtn.Cursor = Cursors.Hand;
            this.GuardarContrasenaBtn.Visible = false;
            this.GuardarContrasenaBtn.Click += new System.EventHandler(this.GuardarContrasenaBtn_Click); // ENLAZADO

            // Agrupamos en Tarjeta 
            this.TarjetaPerfilPanel.Controls.Add(this.TituloDatosLbl);
            this.TarjetaPerfilPanel.Controls.Add(this.NombreLbl);
            this.TarjetaPerfilPanel.Controls.Add(this.NombreTxt);
            this.TarjetaPerfilPanel.Controls.Add(this.ApellidosLbl);
            this.TarjetaPerfilPanel.Controls.Add(this.ApellidosTxt);
            this.TarjetaPerfilPanel.Controls.Add(this.CorreoLbl);
            this.TarjetaPerfilPanel.Controls.Add(this.CorreoTxt);
            this.TarjetaPerfilPanel.Controls.Add(this.RolLbl);
            this.TarjetaPerfilPanel.Controls.Add(this.RolTxt);
            this.TarjetaPerfilPanel.Controls.Add(this.NuevaContrasenaLbl);
            this.TarjetaPerfilPanel.Controls.Add(this.NuevaContrasenaTxt);
            this.TarjetaPerfilPanel.Controls.Add(this.ConfirmarContrasenaLbl);
            this.TarjetaPerfilPanel.Controls.Add(this.ConfirmarContrasenaTxt);

            // LateralPanel
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

            // CapcaleraPanel
            this.CapcaleraPanel.Controls.Add(this.TitolAppLbl);
            this.CapcaleraPanel.Controls.Add(this.BienvenidaLbl);

            // Form
            this.Controls.Add(this.GuardarCambiosBtn);
            this.Controls.Add(this.CambiarPasswordBtn);
            this.Controls.Add(this.GuardarContrasenaBtn);
            this.Controls.Add(this.TarjetaPerfilPanel);
            this.Controls.Add(this.TitolPaginaLbl);
            this.Controls.Add(this.CapcaleraPanel);
            this.Controls.Add(this.LateralPanel);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}