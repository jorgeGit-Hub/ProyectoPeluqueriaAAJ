using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    partial class CrearEditarServicioForm
    {
        private Label TituloLbl;
        private Label NombreLbl;
        private TextBox NombreTxt;
        private Label ModuloLbl;
        private TextBox ModuloTxt;
        private Label AulaLbl;
        private TextBox AulaTxt;
        private Label TiempoClienteLbl;
        private TextBox TiempoClienteTxt;
        private Label PrecioLbl;
        private TextBox PrecioTxt;
        private Label DiaSemanaLbl;
        private ComboBox DiaSemanaCombo;
        private Label HorarioLbl;
        private TextBox HorarioTxt;
        private Label GrupoLbl;
        private ComboBox GrupoCombo;
        private Button GuardarBtn;
        private Button CancelarBtn;
        private Panel FormPanel;

        private void InitializeComponent()
        {
            this.FormPanel = new Panel();
            this.TituloLbl = new Label();
            this.NombreLbl = new Label();
            this.NombreTxt = new TextBox();
            this.ModuloLbl = new Label();
            this.ModuloTxt = new TextBox();
            this.AulaLbl = new Label();
            this.AulaTxt = new TextBox();
            this.TiempoClienteLbl = new Label();
            this.TiempoClienteTxt = new TextBox();
            this.PrecioLbl = new Label();
            this.PrecioTxt = new TextBox();
            this.DiaSemanaLbl = new Label();
            this.DiaSemanaCombo = new ComboBox();
            this.HorarioLbl = new Label();
            this.HorarioTxt = new TextBox();
            this.GrupoLbl = new Label();
            this.GrupoCombo = new ComboBox();
            this.GuardarBtn = new Button();
            this.CancelarBtn = new Button();

            // 
            // CrearEditarServicioForm
            // 
            this.ClientSize = new Size(750, 700);
            this.Text = "Gestión de Servicio";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(250, 245, 240);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // 
            // FormPanel
            // 
            this.FormPanel.BackColor = Color.White;
            this.FormPanel.Location = new Point(25, 25);
            this.FormPanel.Size = new Size(700, 650);
            this.FormPanel.AutoScroll = true;

            // 
            // TituloLbl
            // 
            this.TituloLbl.Text = "Crear Nuevo Servicio";
            this.TituloLbl.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            this.TituloLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TituloLbl.AutoSize = false;
            this.TituloLbl.Size = new Size(700, 50);
            this.TituloLbl.TextAlign = ContentAlignment.MiddleCenter;
            this.TituloLbl.Location = new Point(0, 20);

            int col1X = 50;
            int col2X = 370;
            int startY = 90;
            int rowH = 70;

            // 
            // NombreLbl y NombreTxt
            // 
            this.NombreLbl.Text = "Nombre *";
            this.NombreLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.NombreLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.NombreLbl.AutoSize = true;
            this.NombreLbl.Location = new Point(col1X, startY);

            this.NombreTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.NombreTxt.Size = new Size(600, 32);
            this.NombreTxt.Location = new Point(col1X, startY + 25);
            this.NombreTxt.BorderStyle = BorderStyle.FixedSingle;
            this.NombreTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // ModuloLbl y ModuloTxt
            // 
            this.ModuloLbl.Text = "Módulo *";
            this.ModuloLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.ModuloLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.ModuloLbl.AutoSize = true;
            this.ModuloLbl.Location = new Point(col1X, startY + rowH);

            this.ModuloTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.ModuloTxt.Size = new Size(600, 32);
            this.ModuloTxt.Location = new Point(col1X, startY + rowH + 25);
            this.ModuloTxt.BorderStyle = BorderStyle.FixedSingle;
            this.ModuloTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // AulaLbl y AulaTxt
            // 
            this.AulaLbl.Text = "Aula";
            this.AulaLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.AulaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.AulaLbl.AutoSize = true;
            this.AulaLbl.Location = new Point(col1X, startY + rowH * 2);

            this.AulaTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.AulaTxt.Size = new Size(280, 32);
            this.AulaTxt.Location = new Point(col1X, startY + rowH * 2 + 25);
            this.AulaTxt.BorderStyle = BorderStyle.FixedSingle;
            this.AulaTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // TiempoClienteLbl y TiempoClienteTxt
            // 
            this.TiempoClienteLbl.Text = "Tiempo por Cliente";
            this.TiempoClienteLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.TiempoClienteLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TiempoClienteLbl.AutoSize = true;
            this.TiempoClienteLbl.Location = new Point(col2X, startY + rowH * 2);

            this.TiempoClienteTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.TiempoClienteTxt.Size = new Size(280, 32);
            this.TiempoClienteTxt.Location = new Point(col2X, startY + rowH * 2 + 25);
            this.TiempoClienteTxt.BorderStyle = BorderStyle.FixedSingle;
            this.TiempoClienteTxt.BackColor = Color.FromArgb(250, 245, 240);
            this.TiempoClienteTxt.PlaceholderText = "Ej: 45', 1h, 1.5h";

            // 
            // PrecioLbl y PrecioTxt
            // 
            this.PrecioLbl.Text = "Precio (€) *";
            this.PrecioLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.PrecioLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.PrecioLbl.AutoSize = true;
            this.PrecioLbl.Location = new Point(col1X, startY + rowH * 3);

            this.PrecioTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.PrecioTxt.Size = new Size(280, 32);
            this.PrecioTxt.Location = new Point(col1X, startY + rowH * 3 + 25);
            this.PrecioTxt.BorderStyle = BorderStyle.FixedSingle;
            this.PrecioTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // DiaSemanaLbl y DiaSemanaCombo
            // 
            this.DiaSemanaLbl.Text = "Día de la Semana";
            this.DiaSemanaLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.DiaSemanaLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.DiaSemanaLbl.AutoSize = true;
            this.DiaSemanaLbl.Location = new Point(col2X, startY + rowH * 3);

            this.DiaSemanaCombo.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.DiaSemanaCombo.Size = new Size(280, 32);
            this.DiaSemanaCombo.Location = new Point(col2X, startY + rowH * 3 + 25);
            this.DiaSemanaCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.DiaSemanaCombo.BackColor = Color.FromArgb(250, 245, 240);
            this.DiaSemanaCombo.Items.AddRange(new object[] {
                "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo"
            });
            this.DiaSemanaCombo.SelectedIndex = 0;

            // 
            // HorarioLbl y HorarioTxt
            // 
            this.HorarioLbl.Text = "Horario";
            this.HorarioLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.HorarioLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.HorarioLbl.AutoSize = true;
            this.HorarioLbl.Location = new Point(col1X, startY + rowH * 4);

            this.HorarioTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.HorarioTxt.Size = new Size(600, 32);
            this.HorarioTxt.Location = new Point(col1X, startY + rowH * 4 + 25);
            this.HorarioTxt.BorderStyle = BorderStyle.FixedSingle;
            this.HorarioTxt.BackColor = Color.FromArgb(250, 245, 240);
            this.HorarioTxt.PlaceholderText = "Ej: 8:50 a 10:30";

            // 
            // GrupoLbl y GrupoCombo
            // 
            this.GrupoLbl.Text = "Grupo *";
            this.GrupoLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.GrupoLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.GrupoLbl.AutoSize = true;
            this.GrupoLbl.Location = new Point(col1X, startY + rowH * 5);

            this.GrupoCombo.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.GrupoCombo.Size = new Size(600, 32);
            this.GrupoCombo.Location = new Point(col1X, startY + rowH * 5 + 25);
            this.GrupoCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.GrupoCombo.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // GuardarBtn
            // 
            this.GuardarBtn.Text = "💾 Guardar";
            this.GuardarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.GuardarBtn.Size = new Size(280, 50);
            this.GuardarBtn.Location = new Point(80, startY + rowH * 6 + 10);
            this.GuardarBtn.BackColor = Color.FromArgb(255, 140, 0);
            this.GuardarBtn.ForeColor = Color.White;
            this.GuardarBtn.FlatStyle = FlatStyle.Flat;
            this.GuardarBtn.FlatAppearance.BorderSize = 0;
            this.GuardarBtn.Cursor = Cursors.Hand;
            this.GuardarBtn.Click += new System.EventHandler(this.GuardarBtn_Click);

            // 
            // CancelarBtn
            // 
            this.CancelarBtn.Text = "❌ Cancelar";
            this.CancelarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.CancelarBtn.Size = new Size(280, 50);
            this.CancelarBtn.Location = new Point(380, startY + rowH * 6 + 10);
            this.CancelarBtn.BackColor = Color.FromArgb(139, 90, 60);
            this.CancelarBtn.ForeColor = Color.White;
            this.CancelarBtn.FlatStyle = FlatStyle.Flat;
            this.CancelarBtn.FlatAppearance.BorderSize = 0;
            this.CancelarBtn.Cursor = Cursors.Hand;
            this.CancelarBtn.Click += new System.EventHandler(this.CancelarBtn_Click);

            // 
            // Add controls to FormPanel
            // 
            this.FormPanel.Controls.Add(this.TituloLbl);
            this.FormPanel.Controls.Add(this.NombreLbl);
            this.FormPanel.Controls.Add(this.NombreTxt);
            this.FormPanel.Controls.Add(this.ModuloLbl);
            this.FormPanel.Controls.Add(this.ModuloTxt);
            this.FormPanel.Controls.Add(this.AulaLbl);
            this.FormPanel.Controls.Add(this.AulaTxt);
            this.FormPanel.Controls.Add(this.TiempoClienteLbl);
            this.FormPanel.Controls.Add(this.TiempoClienteTxt);
            this.FormPanel.Controls.Add(this.PrecioLbl);
            this.FormPanel.Controls.Add(this.PrecioTxt);
            this.FormPanel.Controls.Add(this.DiaSemanaLbl);
            this.FormPanel.Controls.Add(this.DiaSemanaCombo);
            this.FormPanel.Controls.Add(this.HorarioLbl);
            this.FormPanel.Controls.Add(this.HorarioTxt);
            this.FormPanel.Controls.Add(this.GrupoLbl);
            this.FormPanel.Controls.Add(this.GrupoCombo);
            this.FormPanel.Controls.Add(this.GuardarBtn);
            this.FormPanel.Controls.Add(this.CancelarBtn);

            // 
            // Add controls to Form
            // 
            this.Controls.Add(this.FormPanel);
        }
    }
}