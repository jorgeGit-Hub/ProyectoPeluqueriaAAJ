using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    partial class ServiciosForm
    {
        private Panel LateralPanel;
        private Panel CapcaleraPanel;
        private Label LogoLbl;
        private Label TitolAppLbl;
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
        private TextBox BuscarServiciosTxt;
        private Button BuscarBtn;
        private Button CrearServicioBtn;
        private DataGridView ServiciosDataGrid;
        private Button EditarBtn;
        private Button EliminarBtn;
        private Button VerHorariosBtn;
        private Label BienvenidaLbl;

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
            this.ValoracionesBoto = new Button();
            this.MiCuentaBoto = new Button();
            this.TancarSessioBoto = new Button();
            this.TitolPaginaLbl = new Label();
            this.BuscarServiciosTxt = new TextBox();
            this.BuscarBtn = new Button();
            this.CrearServicioBtn = new Button();
            this.ServiciosDataGrid = new DataGridView();
            this.EditarBtn = new Button();
            this.EliminarBtn = new Button();
            this.VerHorariosBtn = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.ServiciosDataGrid)).BeginInit();

            this.ClientSize = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Peluquería Escola - Servicios";
            this.BackColor = Color.FromArgb(250, 245, 240);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.LateralPanel.Dock = DockStyle.Left;
            this.LateralPanel.Width = 260;
            this.LateralPanel.BackColor = Color.FromArgb(45, 35, 30);

            this.LogoLbl.Text = "✂️\nPeluquería\nEscola";
            this.LogoLbl.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            this.LogoLbl.ForeColor = Color.FromArgb(255, 140, 0);
            this.LogoLbl.AutoSize = false;
            this.LogoLbl.TextAlign = ContentAlignment.MiddleCenter;
            this.LogoLbl.Size = new Size(260, 100);
            this.LogoLbl.Location = new Point(0, 20);

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
            this.ServiciosBoto.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.ServiciosBoto.ForeColor = Color.White;
            this.ServiciosBoto.BackColor = Color.FromArgb(255, 140, 0);
            this.ServiciosBoto.FlatStyle = FlatStyle.Flat;
            this.ServiciosBoto.FlatAppearance.BorderSize = 0;
            this.ServiciosBoto.Size = new Size(240, 45);
            this.ServiciosBoto.Location = new Point(10, 180);
            this.ServiciosBoto.TextAlign = ContentAlignment.MiddleLeft;
            this.ServiciosBoto.Padding = new Padding(20, 0, 0, 0);
            this.ServiciosBoto.Cursor = Cursors.Hand;

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

            this.CapcaleraPanel.Dock = DockStyle.Top;
            this.CapcaleraPanel.Height = 80;
            this.CapcaleraPanel.BackColor = Color.White;

            this.TitolAppLbl.Text = "Gestión de Servicios";
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

            this.TitolPaginaLbl.Text = "Administrar Servicios";
            this.TitolPaginaLbl.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            this.TitolPaginaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TitolPaginaLbl.AutoSize = true;
            this.TitolPaginaLbl.Location = new Point(290, 110);

            this.BuscarServiciosTxt.Name = "BuscarServiciosTxt";
            this.BuscarServiciosTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.BuscarServiciosTxt.Size = new Size(400, 32);
            this.BuscarServiciosTxt.Location = new Point(290, 160);
            this.BuscarServiciosTxt.BorderStyle = BorderStyle.FixedSingle;
            this.BuscarServiciosTxt.BackColor = Color.White;
            this.BuscarServiciosTxt.ForeColor = Color.FromArgb(45, 35, 30);

            this.BuscarBtn.Text = "🔍 Buscar";
            this.BuscarBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.BuscarBtn.Size = new Size(120, 32);
            this.BuscarBtn.Location = new Point(700, 160);
            this.BuscarBtn.BackColor = Color.FromArgb(255, 140, 0);
            this.BuscarBtn.ForeColor = Color.White;
            this.BuscarBtn.FlatStyle = FlatStyle.Flat;
            this.BuscarBtn.FlatAppearance.BorderSize = 0;
            this.BuscarBtn.Cursor = Cursors.Hand;
            this.BuscarBtn.Click += new System.EventHandler(this.BuscarBtn_Click);

            this.CrearServicioBtn.Text = "➕ Crear Servicio";
            this.CrearServicioBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.CrearServicioBtn.Size = new Size(180, 40);
            this.CrearServicioBtn.Location = new Point(1150, 157);
            this.CrearServicioBtn.BackColor = Color.FromArgb(139, 90, 60);
            this.CrearServicioBtn.ForeColor = Color.White;
            this.CrearServicioBtn.FlatStyle = FlatStyle.Flat;
            this.CrearServicioBtn.FlatAppearance.BorderSize = 0;
            this.CrearServicioBtn.Cursor = Cursors.Hand;
            this.CrearServicioBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.CrearServicioBtn.Click += new System.EventHandler(this.CrearServicioBtn_Click);

            this.ServiciosDataGrid.Name = "ServiciosDataGrid";
            this.ServiciosDataGrid.Location = new Point(290, 220);
            this.ServiciosDataGrid.Size = new Size(1070, 440);
            this.ServiciosDataGrid.ScrollBars = ScrollBars.Both;
            this.ServiciosDataGrid.BackgroundColor = Color.White;
            this.ServiciosDataGrid.BorderStyle = BorderStyle.None;
            this.ServiciosDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ServiciosDataGrid.GridColor = Color.FromArgb(240, 240, 240);
            this.ServiciosDataGrid.DefaultCellStyle.BackColor = Color.White;
            this.ServiciosDataGrid.DefaultCellStyle.ForeColor = Color.FromArgb(45, 35, 30);
            this.ServiciosDataGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 160, 50);
            this.ServiciosDataGrid.DefaultCellStyle.SelectionForeColor = Color.White;
            this.ServiciosDataGrid.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            this.ServiciosDataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 140, 0);
            this.ServiciosDataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.ServiciosDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.ServiciosDataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ServiciosDataGrid.EnableHeadersVisualStyles = false;
            this.ServiciosDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.ServiciosDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.ServiciosDataGrid.MultiSelect = false;
            this.ServiciosDataGrid.ReadOnly = true;
            this.ServiciosDataGrid.AllowUserToAddRows = false;
            this.ServiciosDataGrid.AllowUserToDeleteRows = false;

            this.EditarBtn.Text = "✏️ Editar";
            this.EditarBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.EditarBtn.Size = new Size(150, 45);
            this.EditarBtn.Location = new Point(290, 680);
            this.EditarBtn.BackColor = Color.FromArgb(255, 140, 0);
            this.EditarBtn.ForeColor = Color.White;
            this.EditarBtn.FlatStyle = FlatStyle.Flat;
            this.EditarBtn.FlatAppearance.BorderSize = 0;
            this.EditarBtn.Cursor = Cursors.Hand;
            this.EditarBtn.Click += new System.EventHandler(this.EditarBtn_Click);

            this.EliminarBtn.Text = "🗑️ Eliminar";
            this.EliminarBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.EliminarBtn.Size = new Size(150, 45);
            this.EliminarBtn.Location = new Point(460, 680);
            this.EliminarBtn.BackColor = Color.FromArgb(200, 50, 50);
            this.EliminarBtn.ForeColor = Color.White;
            this.EliminarBtn.FlatStyle = FlatStyle.Flat;
            this.EliminarBtn.FlatAppearance.BorderSize = 0;
            this.EliminarBtn.Cursor = Cursors.Hand;
            this.EliminarBtn.Click += new System.EventHandler(this.EliminarBtn_Click);

            this.VerHorariosBtn.Text = "🕐 Ver Horarios";
            this.VerHorariosBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.VerHorariosBtn.Size = new Size(170, 45);
            this.VerHorariosBtn.Location = new Point(630, 680);
            this.VerHorariosBtn.BackColor = Color.FromArgb(70, 130, 180);
            this.VerHorariosBtn.ForeColor = Color.White;
            this.VerHorariosBtn.FlatStyle = FlatStyle.Flat;
            this.VerHorariosBtn.FlatAppearance.BorderSize = 0;
            this.VerHorariosBtn.Cursor = Cursors.Hand;
            this.VerHorariosBtn.Click += new System.EventHandler(this.VerHorariosBtn_Click);

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

            this.Controls.Add(this.VerHorariosBtn);
            this.Controls.Add(this.EliminarBtn);
            this.Controls.Add(this.EditarBtn);
            this.Controls.Add(this.ServiciosDataGrid);
            this.Controls.Add(this.CrearServicioBtn);
            this.Controls.Add(this.BuscarBtn);
            this.Controls.Add(this.BuscarServiciosTxt);
            this.Controls.Add(this.TitolPaginaLbl);
            this.Controls.Add(this.CapcaleraPanel);
            this.Controls.Add(this.LateralPanel);

            ((System.ComponentModel.ISupportInitialize)(this.ServiciosDataGrid)).EndInit();
        }
    }
}