using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    partial class HorarioSemanalForm
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
        private Button MiCuentaBoto;
        private Button TancarSessioBoto;
        private Label TitolPaginaLbl;
        private Button CrearBloqueoBtn;
        private DataGridView BloqueosDataGrid;
        private Button EditarBtn;
        private Button EliminarBtn;

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
            this.HorarioSemanalBoto = new Button();
            this.HorarioBoto = new Button();
            this.MiCuentaBoto = new Button();
            this.TancarSessioBoto = new Button();
            this.TitolPaginaLbl = new Label();
            this.CrearBloqueoBtn = new Button();
            this.BloqueosDataGrid = new DataGridView();
            this.EditarBtn = new Button();
            this.EliminarBtn = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.BloqueosDataGrid)).BeginInit();

            // 
            // HorarioSemanalForm
            // 
            this.ClientSize = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Peluquería Escola - Horario Semanal";
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
            this.LogoLbl.Size = new Size(260, 120);
            this.LogoLbl.Location = new Point(0, 20);

            // 
            // IniciBoto
            // 
            this.IniciBoto.Text = "🏠  Inicio";
            this.IniciBoto.Name = "IniciBoto";
            this.IniciBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.IniciBoto.ForeColor = Color.FromArgb(200, 200, 200);
            this.IniciBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.IniciBoto.FlatStyle = FlatStyle.Flat;
            this.IniciBoto.FlatAppearance.BorderSize = 0;
            this.IniciBoto.Size = new Size(240, 50);
            this.IniciBoto.Location = new Point(10, 160);
            this.IniciBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.IniciBoto.Padding = new Padding(20, 0, 0, 0);
            this.IniciBoto.Cursor = Cursors.Hand;
            this.IniciBoto.Click += new System.EventHandler(this.IniciBoto_Click);

            // 
            // ServiciosBoto
            // 
            this.ServiciosBoto.Text = "✂️  Servicios";
            this.ServiciosBoto.Name = "ServiciosBoto";
            this.ServiciosBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.ServiciosBoto.ForeColor = Color.FromArgb(200, 200, 200);
            this.ServiciosBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.ServiciosBoto.FlatStyle = FlatStyle.Flat;
            this.ServiciosBoto.FlatAppearance.BorderSize = 0;
            this.ServiciosBoto.Size = new Size(240, 50);
            this.ServiciosBoto.Location = new Point(10, 220);
            this.ServiciosBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.ServiciosBoto.Padding = new Padding(20, 0, 0, 0);
            this.ServiciosBoto.Cursor = Cursors.Hand;
            this.ServiciosBoto.Click += new System.EventHandler(this.ServiciosBoto_Click);

            // 
            // UsuariosBoto
            // 
            this.UsuariosBoto.Text = "👥  Usuarios";
            this.UsuariosBoto.Name = "UsuariosBoto";
            this.UsuariosBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.UsuariosBoto.ForeColor = Color.FromArgb(200, 200, 200);
            this.UsuariosBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.UsuariosBoto.FlatStyle = FlatStyle.Flat;
            this.UsuariosBoto.FlatAppearance.BorderSize = 0;
            this.UsuariosBoto.Size = new Size(240, 50);
            this.UsuariosBoto.Location = new Point(10, 280);
            this.UsuariosBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.UsuariosBoto.Padding = new Padding(20, 0, 0, 0);
            this.UsuariosBoto.Cursor = Cursors.Hand;
            this.UsuariosBoto.Click += new System.EventHandler(this.UsuariosBoto_Click);

            // 
            // ClientesBoto
            // 
            this.ClientesBoto.Text = "👤  Clientes";
            this.ClientesBoto.Name = "ClientesBoto";
            this.ClientesBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.ClientesBoto.ForeColor = Color.FromArgb(200, 200, 200);
            this.ClientesBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.ClientesBoto.FlatStyle = FlatStyle.Flat;
            this.ClientesBoto.FlatAppearance.BorderSize = 0;
            this.ClientesBoto.Size = new Size(240, 50);
            this.ClientesBoto.Location = new Point(10, 340);
            this.ClientesBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.ClientesBoto.Padding = new Padding(20, 0, 0, 0);
            this.ClientesBoto.Cursor = Cursors.Hand;
            this.ClientesBoto.Click += new System.EventHandler(this.ClientesBoto_Click);

            // 
            // CitasBoto
            // 
            this.CitasBoto.Text = "📅  Citas";
            this.CitasBoto.Name = "CitasBoto";
            this.CitasBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.CitasBoto.ForeColor = Color.FromArgb(200, 200, 200);
            this.CitasBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.CitasBoto.FlatStyle = FlatStyle.Flat;
            this.CitasBoto.FlatAppearance.BorderSize = 0;
            this.CitasBoto.Size = new Size(240, 50);
            this.CitasBoto.Location = new Point(10, 400);
            this.CitasBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.CitasBoto.Padding = new Padding(20, 0, 0, 0);
            this.CitasBoto.Cursor = Cursors.Hand;
            this.CitasBoto.Click += new System.EventHandler(this.CitasBoto_Click);

            // 
            // GruposBoto
            // 
            this.GruposBoto.Text = "👨‍👩‍👧‍👦  Grupos";
            this.GruposBoto.Name = "GruposBoto";
            this.GruposBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.GruposBoto.ForeColor = Color.FromArgb(200, 200, 200);
            this.GruposBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.GruposBoto.FlatStyle = FlatStyle.Flat;
            this.GruposBoto.FlatAppearance.BorderSize = 0;
            this.GruposBoto.Size = new Size(240, 50);
            this.GruposBoto.Location = new Point(10, 460);
            this.GruposBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.GruposBoto.Padding = new Padding(20, 0, 0, 0);
            this.GruposBoto.Cursor = Cursors.Hand;
            this.GruposBoto.Click += new System.EventHandler(this.GruposBoto_Click);

            // HorarioBoto
            this.HorarioBoto.Text = "🗓️  Horario Semanal";
            this.HorarioBoto.Name = "HorarioBoto";
            this.HorarioBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.HorarioBoto.ForeColor = Color.FromArgb(200, 200, 200);
            this.HorarioBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.HorarioBoto.FlatStyle = FlatStyle.Flat;
            this.HorarioBoto.FlatAppearance.BorderSize = 0;
            this.HorarioBoto.Size = new Size(240, 50);
            this.HorarioBoto.Location = new Point(10, 520);
            this.HorarioBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.HorarioBoto.Padding = new Padding(20, 0, 0, 0);
            this.HorarioBoto.Cursor = Cursors.Hand;
            this.HorarioBoto.Click += new System.EventHandler(this.HorarioForm_Click);

            // 
            // HorarioSemanalBoto
            // 
            this.HorarioSemanalBoto.Text = "🕐  Bloqueo Horario";
            this.HorarioSemanalBoto.Name = "HorarioSemanalBoto";
            this.HorarioSemanalBoto.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.HorarioSemanalBoto.ForeColor = Color.White;
            this.HorarioSemanalBoto.BackColor = Color.FromArgb(255, 140, 0);
            this.HorarioSemanalBoto.FlatStyle = FlatStyle.Flat;
            this.HorarioSemanalBoto.FlatAppearance.BorderSize = 0;
            this.HorarioSemanalBoto.Size = new Size(240, 50);
            this.HorarioSemanalBoto.Location = new Point(10, 580);
            this.HorarioSemanalBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.HorarioSemanalBoto.Padding = new Padding(20, 0, 0, 0);
            this.HorarioSemanalBoto.Cursor = Cursors.Hand;

            // 
            // MiCuentaBoto
            // 
            this.MiCuentaBoto.Text = "⚙️  Mi Cuenta";
            this.MiCuentaBoto.Name = "MiCuentaBoto";
            this.MiCuentaBoto.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.MiCuentaBoto.ForeColor = Color.FromArgb(200, 200, 200);
            this.MiCuentaBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.MiCuentaBoto.FlatStyle = FlatStyle.Flat;
            this.MiCuentaBoto.FlatAppearance.BorderSize = 0;
            this.MiCuentaBoto.Size = new Size(240, 50);
            this.MiCuentaBoto.Location = new Point(10, 640);
            this.MiCuentaBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.MiCuentaBoto.Padding = new Padding(20, 0, 0, 0);
            this.MiCuentaBoto.Cursor = Cursors.Hand;
            this.MiCuentaBoto.Click += new System.EventHandler(this.MiCuentaBoto_Click);

            // 
            // TancarSessioBoto
            // 
            this.TancarSessioBoto.Text = "🚪  Cerrar Sesión";
            this.TancarSessioBoto.Name = "TancarSessioBoto";
            this.TancarSessioBoto.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.TancarSessioBoto.ForeColor = Color.FromArgb(150, 150, 150);
            this.TancarSessioBoto.BackColor = Color.FromArgb(45, 35, 30);
            this.TancarSessioBoto.FlatStyle = FlatStyle.Flat;
            this.TancarSessioBoto.FlatAppearance.BorderSize = 0;
            this.TancarSessioBoto.Size = new Size(240, 50);
            this.TancarSessioBoto.Location = new Point(10, 720);
            this.TancarSessioBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.TancarSessioBoto.Padding = new Padding(20, 0, 0, 0);
            this.TancarSessioBoto.Cursor = Cursors.Hand;
            this.TancarSessioBoto.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.TancarSessioBoto.Click += new System.EventHandler(this.TancarSessioBoto_Click);

            // 
            // CapcaleraPanel
            // 
            this.CapcaleraPanel.Dock = DockStyle.Top;
            this.CapcaleraPanel.Height = 80;
            this.CapcaleraPanel.BackColor = Color.White;

            // 
            // TitolAppLbl
            // 
            this.TitolAppLbl.Text = "Horario y Bloqueos";
            this.TitolAppLbl.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            this.TitolAppLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TitolAppLbl.AutoSize = true;
            this.TitolAppLbl.Location = new Point(30, 25);

            // 
            // BienvenidaLbl
            // 
            this.BienvenidaLbl.Text = "Bienvenido/a";
            this.BienvenidaLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.BienvenidaLbl.ForeColor = Color.FromArgb(139, 90, 60);
            this.BienvenidaLbl.AutoSize = true;
            this.BienvenidaLbl.Location = new Point(1050, 30);
            this.BienvenidaLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // 
            // TitolPaginaLbl
            // 
            this.TitolPaginaLbl.Text = "Gestión de Bloqueos Horarios";
            this.TitolPaginaLbl.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            this.TitolPaginaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TitolPaginaLbl.AutoSize = true;
            this.TitolPaginaLbl.Location = new Point(290, 110);

            /*
            // CrearBloqueoBtn
            // 
            this.CrearBloqueoBtn.Text = "➕ Crear Bloqueo";
            this.CrearBloqueoBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.CrearBloqueoBtn.Size = new Size(200, 40);
            this.CrearBloqueoBtn.Location = new Point(1130, 157);
            this.CrearBloqueoBtn.BackColor = Color.FromArgb(139, 90, 60);
            this.CrearBloqueoBtn.ForeColor = Color.White;
            this.CrearBloqueoBtn.FlatStyle = FlatStyle.Flat;
            this.CrearBloqueoBtn.FlatAppearance.BorderSize = 0;
            this.CrearBloqueoBtn.Cursor = Cursors.Hand;
            this.CrearBloqueoBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.CrearBloqueoBtn.Click += new System.EventHandler(this.CrearBloqueoBtn_Click);*/

            // 
            // BloqueosDataGrid
            // 
            this.BloqueosDataGrid.Name = "BloqueosDataGrid";
            this.BloqueosDataGrid.Location = new Point(290, 220);
            this.BloqueosDataGrid.Size = new Size(1070, 480);
            this.BloqueosDataGrid.BackgroundColor = Color.White;
            this.BloqueosDataGrid.BorderStyle = BorderStyle.None;
            this.BloqueosDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BloqueosDataGrid.GridColor = Color.FromArgb(240, 240, 240);
            this.BloqueosDataGrid.DefaultCellStyle.BackColor = Color.White;
            this.BloqueosDataGrid.DefaultCellStyle.ForeColor = Color.FromArgb(45, 35, 30);
            this.BloqueosDataGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 160, 50);
            this.BloqueosDataGrid.DefaultCellStyle.SelectionForeColor = Color.White;
            this.BloqueosDataGrid.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            this.BloqueosDataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 140, 0);
            this.BloqueosDataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.BloqueosDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.BloqueosDataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.BloqueosDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.BloqueosDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.BloqueosDataGrid.MultiSelect = false;
            this.BloqueosDataGrid.ReadOnly = true;
            this.BloqueosDataGrid.AllowUserToAddRows = false;
            this.BloqueosDataGrid.AllowUserToDeleteRows = false;

            /* 
            // EditarBtn
            // 
            this.EditarBtn.Text = "✏️ Editar";
            this.EditarBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.EditarBtn.Size = new Size(150, 45);
            this.EditarBtn.Location = new Point(290, 720);
            this.EditarBtn.BackColor = Color.FromArgb(255, 140, 0);
            this.EditarBtn.ForeColor = Color.White;
            this.EditarBtn.FlatStyle = FlatStyle.Flat;
            this.EditarBtn.FlatAppearance.BorderSize = 0;
            this.EditarBtn.Cursor = Cursors.Hand;
            this.EditarBtn.Click += new System.EventHandler(this.EditarBtn_Click);*/

            // 
            // EliminarBtn
            // 
            this.EliminarBtn.Text = "🗑️ Eliminar";
            this.EliminarBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.EliminarBtn.Size = new Size(150, 45);
            this.EliminarBtn.Location = new Point(460, 720);
            this.EliminarBtn.BackColor = Color.FromArgb(200, 50, 50);
            this.EliminarBtn.ForeColor = Color.White;
            this.EliminarBtn.FlatStyle = FlatStyle.Flat;
            this.EliminarBtn.FlatAppearance.BorderSize = 0;
            this.EliminarBtn.Cursor = Cursors.Hand;
            this.EliminarBtn.Click += new System.EventHandler(this.EliminarBtn_Click);

            // 
            // Add controls to LateralPanel
            // 
            this.LateralPanel.Controls.Add(this.LogoLbl);
            this.LateralPanel.Controls.Add(this.IniciBoto);
            this.LateralPanel.Controls.Add(this.ServiciosBoto);
            this.LateralPanel.Controls.Add(this.UsuariosBoto);
            this.LateralPanel.Controls.Add(this.ClientesBoto);
            this.LateralPanel.Controls.Add(this.CitasBoto);
            this.LateralPanel.Controls.Add(this.GruposBoto);
            this.LateralPanel.Controls.Add(this.HorarioSemanalBoto);
            this.LateralPanel.Controls.Add(this.HorarioBoto);
            this.LateralPanel.Controls.Add(this.MiCuentaBoto);
            this.LateralPanel.Controls.Add(this.TancarSessioBoto);

            // 
            // Add controls to CapcaleraPanel
            // 
            this.CapcaleraPanel.Controls.Add(this.TitolAppLbl);
            this.CapcaleraPanel.Controls.Add(this.BienvenidaLbl);

            // 
            // Add controls to Form
            // 
            this.Controls.Add(this.EliminarBtn);
            this.Controls.Add(this.EditarBtn);
            this.Controls.Add(this.BloqueosDataGrid);
            this.Controls.Add(this.CrearBloqueoBtn);
            this.Controls.Add(this.TitolPaginaLbl);
            this.Controls.Add(this.CapcaleraPanel);
            this.Controls.Add(this.LateralPanel);

            ((System.ComponentModel.ISupportInitialize)(this.BloqueosDataGrid)).EndInit();
        }
    }
}