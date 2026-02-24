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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
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
            TitolPaginaLbl = new Label();
            BuscarServiciosTxt = new TextBox();
            BuscarBtn = new Button();
            CrearServicioBtn = new Button();
            ServiciosDataGrid = new DataGridView();
            EditarBtn = new Button();
            EliminarBtn = new Button();
            VerHorariosBtn = new Button();
            pbLogo = new PictureBox();
            LateralPanel.SuspendLayout();
            CapcaleraPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ServiciosDataGrid).BeginInit();
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
            LateralPanel.TabIndex = 9;
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
            ServiciosBoto.BackColor = Color.FromArgb(255, 140, 0);
            ServiciosBoto.Cursor = Cursors.Hand;
            ServiciosBoto.FlatAppearance.BorderSize = 0;
            ServiciosBoto.FlatStyle = FlatStyle.Flat;
            ServiciosBoto.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            ServiciosBoto.ForeColor = Color.White;
            ServiciosBoto.Location = new Point(10, 180);
            ServiciosBoto.Name = "ServiciosBoto";
            ServiciosBoto.Padding = new Padding(20, 0, 0, 0);
            ServiciosBoto.Size = new Size(240, 45);
            ServiciosBoto.TabIndex = 2;
            ServiciosBoto.Text = "✂️  Servicios";
            ServiciosBoto.TextAlign = ContentAlignment.MiddleLeft;
            ServiciosBoto.UseVisualStyleBackColor = false;
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
            CapcaleraPanel.TabIndex = 8;
            // 
            // TitolAppLbl
            // 
            TitolAppLbl.AutoSize = true;
            TitolAppLbl.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            TitolAppLbl.ForeColor = Color.FromArgb(45, 35, 30);
            TitolAppLbl.Location = new Point(30, 25);
            TitolAppLbl.Name = "TitolAppLbl";
            TitolAppLbl.Size = new Size(222, 30);
            TitolAppLbl.TabIndex = 0;
            TitolAppLbl.Text = "Gestión de Servicios";
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
            TitolPaginaLbl.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            TitolPaginaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            TitolPaginaLbl.Location = new Point(290, 110);
            TitolPaginaLbl.Name = "TitolPaginaLbl";
            TitolPaginaLbl.Size = new Size(259, 32);
            TitolPaginaLbl.TabIndex = 7;
            TitolPaginaLbl.Text = "Administrar Servicios";
            // 
            // BuscarServiciosTxt
            // 
            BuscarServiciosTxt.BackColor = Color.White;
            BuscarServiciosTxt.BorderStyle = BorderStyle.FixedSingle;
            BuscarServiciosTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            BuscarServiciosTxt.ForeColor = Color.FromArgb(45, 35, 30);
            BuscarServiciosTxt.Location = new Point(290, 160);
            BuscarServiciosTxt.Name = "BuscarServiciosTxt";
            BuscarServiciosTxt.Size = new Size(400, 27);
            BuscarServiciosTxt.TabIndex = 6;
            // 
            // BuscarBtn
            // 
            BuscarBtn.BackColor = Color.FromArgb(255, 140, 0);
            BuscarBtn.Cursor = Cursors.Hand;
            BuscarBtn.FlatAppearance.BorderSize = 0;
            BuscarBtn.FlatStyle = FlatStyle.Flat;
            BuscarBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            BuscarBtn.ForeColor = Color.White;
            BuscarBtn.Location = new Point(700, 160);
            BuscarBtn.Name = "BuscarBtn";
            BuscarBtn.Size = new Size(120, 32);
            BuscarBtn.TabIndex = 5;
            BuscarBtn.Text = "🔍 Buscar";
            BuscarBtn.UseVisualStyleBackColor = false;
            BuscarBtn.Click += BuscarBtn_Click;
            // 
            // CrearServicioBtn
            // 
            CrearServicioBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CrearServicioBtn.BackColor = Color.FromArgb(139, 90, 60);
            CrearServicioBtn.Cursor = Cursors.Hand;
            CrearServicioBtn.FlatAppearance.BorderSize = 0;
            CrearServicioBtn.FlatStyle = FlatStyle.Flat;
            CrearServicioBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            CrearServicioBtn.ForeColor = Color.White;
            CrearServicioBtn.Location = new Point(1120, 157);
            CrearServicioBtn.Name = "CrearServicioBtn";
            CrearServicioBtn.Size = new Size(180, 40);
            CrearServicioBtn.TabIndex = 4;
            CrearServicioBtn.Text = "➕ Crear Servicio";
            CrearServicioBtn.UseVisualStyleBackColor = false;
            CrearServicioBtn.Click += CrearServicioBtn_Click;
            // 
            // ServiciosDataGrid
            // 
            ServiciosDataGrid.AllowUserToAddRows = false;
            ServiciosDataGrid.AllowUserToDeleteRows = false;
            ServiciosDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ServiciosDataGrid.BackgroundColor = Color.White;
            ServiciosDataGrid.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(255, 140, 0);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            ServiciosDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            ServiciosDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(45, 35, 30);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(255, 160, 50);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            ServiciosDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            ServiciosDataGrid.EnableHeadersVisualStyles = false;
            ServiciosDataGrid.GridColor = Color.FromArgb(240, 240, 240);
            ServiciosDataGrid.Location = new Point(290, 220);
            ServiciosDataGrid.MultiSelect = false;
            ServiciosDataGrid.Name = "ServiciosDataGrid";
            ServiciosDataGrid.ReadOnly = true;
            ServiciosDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ServiciosDataGrid.Size = new Size(1070, 440);
            ServiciosDataGrid.TabIndex = 3;
            // 
            // EditarBtn
            // 
            EditarBtn.BackColor = Color.FromArgb(255, 140, 0);
            EditarBtn.Cursor = Cursors.Hand;
            EditarBtn.FlatAppearance.BorderSize = 0;
            EditarBtn.FlatStyle = FlatStyle.Flat;
            EditarBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            EditarBtn.ForeColor = Color.White;
            EditarBtn.Location = new Point(290, 680);
            EditarBtn.Name = "EditarBtn";
            EditarBtn.Size = new Size(150, 45);
            EditarBtn.TabIndex = 2;
            EditarBtn.Text = "✏️ Editar";
            EditarBtn.UseVisualStyleBackColor = false;
            EditarBtn.Click += EditarBtn_Click;
            // 
            // EliminarBtn
            // 
            EliminarBtn.BackColor = Color.FromArgb(200, 50, 50);
            EliminarBtn.Cursor = Cursors.Hand;
            EliminarBtn.FlatAppearance.BorderSize = 0;
            EliminarBtn.FlatStyle = FlatStyle.Flat;
            EliminarBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            EliminarBtn.ForeColor = Color.White;
            EliminarBtn.Location = new Point(460, 680);
            EliminarBtn.Name = "EliminarBtn";
            EliminarBtn.Size = new Size(150, 45);
            EliminarBtn.TabIndex = 1;
            EliminarBtn.Text = "🗑️ Eliminar";
            EliminarBtn.UseVisualStyleBackColor = false;
            EliminarBtn.Click += EliminarBtn_Click;
            // 
            // VerHorariosBtn
            // 
            VerHorariosBtn.BackColor = Color.FromArgb(70, 130, 180);
            VerHorariosBtn.Cursor = Cursors.Hand;
            VerHorariosBtn.FlatAppearance.BorderSize = 0;
            VerHorariosBtn.FlatStyle = FlatStyle.Flat;
            VerHorariosBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            VerHorariosBtn.ForeColor = Color.White;
            VerHorariosBtn.Location = new Point(630, 680);
            VerHorariosBtn.Name = "VerHorariosBtn";
            VerHorariosBtn.Size = new Size(170, 45);
            VerHorariosBtn.TabIndex = 0;
            VerHorariosBtn.Text = "🕐 Ver Horarios";
            VerHorariosBtn.UseVisualStyleBackColor = false;
            VerHorariosBtn.Click += VerHorariosBtn_Click;
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
            // ServiciosForm
            // 
            BackColor = Color.FromArgb(250, 245, 240);
            ClientSize = new Size(1370, 749);
            Controls.Add(VerHorariosBtn);
            Controls.Add(EliminarBtn);
            Controls.Add(EditarBtn);
            Controls.Add(ServiciosDataGrid);
            Controls.Add(CrearServicioBtn);
            Controls.Add(BuscarBtn);
            Controls.Add(BuscarServiciosTxt);
            Controls.Add(TitolPaginaLbl);
            Controls.Add(CapcaleraPanel);
            Controls.Add(LateralPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ServiciosForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Peluquería Bernat Sarriá - Servicios";
            LateralPanel.ResumeLayout(false);
            CapcaleraPanel.ResumeLayout(false);
            CapcaleraPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ServiciosDataGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private PictureBox pbLogo;
    }
}