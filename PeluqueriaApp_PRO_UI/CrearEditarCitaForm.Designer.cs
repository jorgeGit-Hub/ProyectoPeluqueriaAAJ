using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    partial class CrearEditarCitaForm
    {
        private Label TituloLbl;
        private MonthCalendar FechaCalendar;

        // ✅ NUEVOS CAMPOS DE HORA
        private Label HoraInicioLbl;
        private TextBox HoraInicioTxt;
        private Label HoraFinLbl;
        private TextBox HoraFinTxt;

        private Label ClienteLbl;
        private ComboBox ClienteCombo;
        private Label ServicioLbl;
        private ComboBox ServicioCombo;
        private Label HorariosLbl;
        private ListBox HorariosListBox;
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

            // Inicializar nuevos campos
            this.HoraInicioLbl = new Label();
            this.HoraInicioTxt = new TextBox();
            this.HoraFinLbl = new Label();
            this.HoraFinTxt = new TextBox();

            this.ClienteLbl = new Label();
            this.ClienteCombo = new ComboBox();
            this.ServicioLbl = new Label();
            this.ServicioCombo = new ComboBox();
            this.HorariosLbl = new Label();
            this.HorariosListBox = new ListBox();
            this.EstadoLbl = new Label();
            this.EstadoCombo = new ComboBox();
            this.GuardarBtn = new Button();
            this.CancelarBtn = new Button();

            // Form
            this.ClientSize = new Size(750, 700);
            this.Text = "Gestión de Cita";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(250, 245, 240);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // FormPanel
            this.FormPanel.BackColor = Color.White;
            this.FormPanel.Location = new Point(20, 20);
            this.FormPanel.Size = new Size(700, 640);
            this.FormPanel.BorderStyle = BorderStyle.FixedSingle;

            // TituloLbl
            this.TituloLbl.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            this.TituloLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TituloLbl.Location = new Point(30, 20);
            this.TituloLbl.Size = new Size(640, 40);
            this.TituloLbl.TextAlign = ContentAlignment.MiddleCenter;

            // FechaCalendar (Izquierda)
            this.FechaCalendar.Location = new Point(40, 80);
            this.FechaCalendar.MaxSelectionCount = 1;

            // ✅ HORA INICIO (Izquierda, bajo el calendario)
            this.HoraInicioLbl.Text = "Hora Inicio (HH:mm):";
            this.HoraInicioLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.HoraInicioLbl.Location = new Point(40, 260);
            this.HoraInicioLbl.Size = new Size(200, 25);

            this.HoraInicioTxt.Font = new Font("Segoe UI", 11F);
            this.HoraInicioTxt.Location = new Point(40, 290);
            this.HoraInicioTxt.Size = new Size(227, 32);

            // ✅ HORA FIN (Izquierda, bajo hora de inicio)
            this.HoraFinLbl.Text = "Hora Fin (HH:mm):";
            this.HoraFinLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.HoraFinLbl.Location = new Point(40, 340);
            this.HoraFinLbl.Size = new Size(200, 25);

            this.HoraFinTxt.Font = new Font("Segoe UI", 11F);
            this.HoraFinTxt.Location = new Point(40, 370);
            this.HoraFinTxt.Size = new Size(227, 32);

            // Cliente (Derecha)
            this.ClienteLbl.Text = "Cliente:";
            this.ClienteLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.ClienteLbl.Location = new Point(340, 80);
            this.ClienteLbl.Size = new Size(300, 25);

            this.ClienteCombo.Font = new Font("Segoe UI", 11F);
            this.ClienteCombo.Location = new Point(340, 110);
            this.ClienteCombo.Size = new Size(320, 33);
            this.ClienteCombo.DropDownStyle = ComboBoxStyle.DropDownList;

            // Servicio (Derecha)
            this.ServicioLbl.Text = "Servicio:";
            this.ServicioLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.ServicioLbl.Location = new Point(340, 160);
            this.ServicioLbl.Size = new Size(300, 25);

            this.ServicioCombo.Font = new Font("Segoe UI", 11F);
            this.ServicioCombo.Location = new Point(340, 190);
            this.ServicioCombo.Size = new Size(320, 33);
            this.ServicioCombo.DropDownStyle = ComboBoxStyle.DropDownList;

            // HorariosDisponibles (Derecha)
            this.HorariosLbl.Text = "Turnos Semanales Base:";
            this.HorariosLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.HorariosLbl.Location = new Point(340, 240);
            this.HorariosLbl.Size = new Size(300, 25);

            this.HorariosListBox.Font = new Font("Segoe UI", 10F);
            this.HorariosListBox.Location = new Point(340, 270);
            this.HorariosListBox.Size = new Size(320, 104);
            // Evento para autocompletar horas al seleccionar un turno
            this.HorariosListBox.SelectedIndexChanged += new System.EventHandler(this.HorariosListBox_SelectedIndexChanged);

            // Estado (Derecha)
            this.EstadoLbl.Text = "Estado:";
            this.EstadoLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.EstadoLbl.Location = new Point(340, 390);
            this.EstadoLbl.Size = new Size(300, 25);

            this.EstadoCombo.Font = new Font("Segoe UI", 11F);
            this.EstadoCombo.Location = new Point(340, 420);
            this.EstadoCombo.Size = new Size(320, 33);
            this.EstadoCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.EstadoCombo.Items.AddRange(new object[] { "pendiente", "realizada", "cancelada" });

            // GuardarBtn
            this.GuardarBtn.Text = "💾 Guardar Cita";
            this.GuardarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.GuardarBtn.Size = new Size(280, 50);
            this.GuardarBtn.Location = new Point(40, 540);
            this.GuardarBtn.BackColor = Color.FromArgb(255, 140, 0);
            this.GuardarBtn.ForeColor = Color.White;
            this.GuardarBtn.FlatStyle = FlatStyle.Flat;
            this.GuardarBtn.FlatAppearance.BorderSize = 0;
            this.GuardarBtn.Cursor = Cursors.Hand;
            this.GuardarBtn.Click += new System.EventHandler(this.GuardarBtn_Click);

            // CancelarBtn
            this.CancelarBtn.Text = "❌ Cancelar";
            this.CancelarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.CancelarBtn.Size = new Size(280, 50);
            this.CancelarBtn.Location = new Point(380, 540);
            this.CancelarBtn.BackColor = Color.FromArgb(139, 90, 60);
            this.CancelarBtn.ForeColor = Color.White;
            this.CancelarBtn.FlatStyle = FlatStyle.Flat;
            this.CancelarBtn.FlatAppearance.BorderSize = 0;
            this.CancelarBtn.Cursor = Cursors.Hand;
            this.CancelarBtn.Click += new System.EventHandler(this.CancelarBtn_Click);

            // Add controls to FormPanel
            this.FormPanel.Controls.Add(this.TituloLbl);
            this.FormPanel.Controls.Add(this.FechaCalendar);

            // Campos Hora
            this.FormPanel.Controls.Add(this.HoraInicioLbl);
            this.FormPanel.Controls.Add(this.HoraInicioTxt);
            this.FormPanel.Controls.Add(this.HoraFinLbl);
            this.FormPanel.Controls.Add(this.HoraFinTxt);

            this.FormPanel.Controls.Add(this.ClienteLbl);
            this.FormPanel.Controls.Add(this.ClienteCombo);
            this.FormPanel.Controls.Add(this.ServicioLbl);
            this.FormPanel.Controls.Add(this.ServicioCombo);
            this.FormPanel.Controls.Add(this.HorariosLbl);
            this.FormPanel.Controls.Add(this.HorariosListBox);
            this.FormPanel.Controls.Add(this.EstadoLbl);
            this.FormPanel.Controls.Add(this.EstadoCombo);
            this.FormPanel.Controls.Add(this.GuardarBtn);
            this.FormPanel.Controls.Add(this.CancelarBtn);

            this.Controls.Add(this.FormPanel);
        }
    }
}