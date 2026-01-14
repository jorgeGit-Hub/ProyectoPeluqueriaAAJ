using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    partial class CrearEditarCitaForm
    {
        private Label TituloLbl;
        private MonthCalendar FechaCalendar;
        private Label ClienteLbl;
        private ComboBox ClienteCombo;
        private Label ServicioLbl;
        private ComboBox ServicioCombo;
        private Label HoraInicioLbl;
        private TextBox HoraInicioTxt;
        private Label HoraFinLbl;
        private TextBox HoraFinTxt;
        private Label EstadoLbl;
        private ComboBox EstadoCombo;
        private Button GuardarBtn;
        private Button CancelarBtn;
        private Panel FormPanel;

        private void InitializeComponent()
        {
            this.FormPanel = new Panel();
            this.TituloLbl = new Label();
            this.FechaCalendar = new MonthCalendar();
            this.ClienteLbl = new Label();
            this.ClienteCombo = new ComboBox();
            this.ServicioLbl = new Label();
            this.ServicioCombo = new ComboBox();
            this.HoraInicioLbl = new Label();
            this.HoraInicioTxt = new TextBox();
            this.HoraFinLbl = new Label();
            this.HoraFinTxt = new TextBox();
            this.EstadoLbl = new Label();
            this.EstadoCombo = new ComboBox();
            this.GuardarBtn = new Button();
            this.CancelarBtn = new Button();

            // 
            // CrearEditarCitaForm
            // 
            this.ClientSize = new Size(750, 700);
            this.Text = "Gestión de Cita";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(250, 245, 240);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // 
            // FormPanel
            // 
            this.FormPanel.BackColor = Color.White;
            this.FormPanel.Location = new Point(25, 20);
            this.FormPanel.Size = new Size(700, 660);

            // 
            // TituloLbl
            // 
            this.TituloLbl.Text = "Crear Nueva Cita";
            this.TituloLbl.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            this.TituloLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TituloLbl.AutoSize = false;
            this.TituloLbl.Size = new Size(700, 50);
            this.TituloLbl.TextAlign = ContentAlignment.MiddleCenter;
            this.TituloLbl.Location = new Point(0, 20);

            // 
            // FechaCalendar
            // 
            this.FechaCalendar.Location = new Point(50, 90);
            this.FechaCalendar.MaxSelectionCount = 1;
            this.FechaCalendar.Font = new Font("Segoe UI", 10F);

            // 
            // ClienteLbl
            // 
            this.ClienteLbl.Text = "Cliente *";
            this.ClienteLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.ClienteLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.ClienteLbl.AutoSize = true;
            this.ClienteLbl.Location = new Point(360, 90);

            // 
            // ClienteCombo
            // 
            this.ClienteCombo.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.ClienteCombo.Size = new Size(290, 32);
            this.ClienteCombo.Location = new Point(360, 115);
            this.ClienteCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ClienteCombo.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // ServicioLbl
            // 
            this.ServicioLbl.Text = "Servicio *";
            this.ServicioLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.ServicioLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.ServicioLbl.AutoSize = true;
            this.ServicioLbl.Location = new Point(360, 165);

            // 
            // ServicioCombo
            // 
            this.ServicioCombo.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.ServicioCombo.Size = new Size(290, 32);
            this.ServicioCombo.Location = new Point(360, 190);
            this.ServicioCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ServicioCombo.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // HoraInicioLbl
            // 
            this.HoraInicioLbl.Text = "Hora Inicio (HH:MM:SS) *";
            this.HoraInicioLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.HoraInicioLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.HoraInicioLbl.AutoSize = true;
            this.HoraInicioLbl.Location = new Point(50, 315);

            // 
            // HoraInicioTxt
            // 
            this.HoraInicioTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.HoraInicioTxt.Size = new Size(280, 32);
            this.HoraInicioTxt.Location = new Point(50, 340);
            this.HoraInicioTxt.BorderStyle = BorderStyle.FixedSingle;
            this.HoraInicioTxt.BackColor = Color.FromArgb(250, 245, 240);
            this.HoraInicioTxt.PlaceholderText = "09:00:00";

            // 
            // HoraFinLbl
            // 
            this.HoraFinLbl.Text = "Hora Fin (HH:MM:SS) *";
            this.HoraFinLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.HoraFinLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.HoraFinLbl.AutoSize = true;
            this.HoraFinLbl.Location = new Point(370, 315);

            // 
            // HoraFinTxt
            // 
            this.HoraFinTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.HoraFinTxt.Size = new Size(280, 32);
            this.HoraFinTxt.Location = new Point(370, 340);
            this.HoraFinTxt.BorderStyle = BorderStyle.FixedSingle;
            this.HoraFinTxt.BackColor = Color.FromArgb(250, 245, 240);
            this.HoraFinTxt.PlaceholderText = "10:00:00";

            // 
            // EstadoLbl
            // 
            this.EstadoLbl.Text = "Estado *";
            this.EstadoLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.EstadoLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.EstadoLbl.AutoSize = true;
            this.EstadoLbl.Location = new Point(50, 395);

            // 
            // EstadoCombo
            // 
            this.EstadoCombo.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.EstadoCombo.Size = new Size(600, 32);
            this.EstadoCombo.Location = new Point(50, 420);
            this.EstadoCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.EstadoCombo.BackColor = Color.FromArgb(250, 245, 240);
            this.EstadoCombo.Items.AddRange(new object[] { "Pendiente", "Realizada", "Cancelada" });
            this.EstadoCombo.SelectedIndex = 0;

            // 
            // GuardarBtn
            // 
            this.GuardarBtn.Text = "💾 Guardar";
            this.GuardarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.GuardarBtn.Size = new Size(280, 50);
            this.GuardarBtn.Location = new Point(80, 500);
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
            this.CancelarBtn.Location = new Point(380, 500);
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
            this.FormPanel.Controls.Add(this.FechaCalendar);
            this.FormPanel.Controls.Add(this.ClienteLbl);
            this.FormPanel.Controls.Add(this.ClienteCombo);
            this.FormPanel.Controls.Add(this.ServicioLbl);
            this.FormPanel.Controls.Add(this.ServicioCombo);
            this.FormPanel.Controls.Add(this.HoraInicioLbl);
            this.FormPanel.Controls.Add(this.HoraInicioTxt);
            this.FormPanel.Controls.Add(this.HoraFinLbl);
            this.FormPanel.Controls.Add(this.HoraFinTxt);
            this.FormPanel.Controls.Add(this.EstadoLbl);
            this.FormPanel.Controls.Add(this.EstadoCombo);
            this.FormPanel.Controls.Add(this.GuardarBtn);
            this.FormPanel.Controls.Add(this.CancelarBtn);

            // 
            // Add controls to Form
            // 
            this.Controls.Add(this.FormPanel);
        }
    }
}