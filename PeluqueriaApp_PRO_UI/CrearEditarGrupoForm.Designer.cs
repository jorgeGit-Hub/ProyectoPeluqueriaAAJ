using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    partial class CrearEditarGrupoForm
    {
        private Label TituloLbl;
        private Label CursoLbl;
        private TextBox CursoTxt;
        private Label TurnoLbl;
        private ComboBox TurnoCombo;
        private Label CantAlumnosLbl;
        private TextBox CantAlumnosTxt;
        private Button GuardarBtn;
        private Button CancelarBtn;
        private Panel FormPanel;

        private void InitializeComponent()
        {
            this.FormPanel = new Panel();
            this.TituloLbl = new Label();
            this.CursoLbl = new Label();
            this.CursoTxt = new TextBox();
            this.TurnoLbl = new Label();
            this.TurnoCombo = new ComboBox();
            this.CantAlumnosLbl = new Label();
            this.CantAlumnosTxt = new TextBox();
            this.GuardarBtn = new Button();
            this.CancelarBtn = new Button();

            // CrearEditarGrupoForm
            this.ClientSize = new Size(600, 380);
            this.Text = "Gestión de Grupo";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(250, 245, 240);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // FormPanel
            this.FormPanel.BackColor = Color.White;
            this.FormPanel.Location = new Point(50, 30);
            this.FormPanel.Size = new Size(500, 320);

            // TituloLbl
            this.TituloLbl.Text = "Crear Nuevo Grupo";
            this.TituloLbl.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            this.TituloLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TituloLbl.AutoSize = false;
            this.TituloLbl.Size = new Size(500, 50);
            this.TituloLbl.TextAlign = ContentAlignment.MiddleCenter;
            this.TituloLbl.Location = new Point(0, 20);

            // CursoLbl
            this.CursoLbl.Text = "Curso *";
            this.CursoLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.CursoLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.CursoLbl.AutoSize = true;
            this.CursoLbl.Location = new Point(50, 90);

            // CursoTxt
            this.CursoTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.CursoTxt.Size = new Size(400, 32);
            this.CursoTxt.Location = new Point(50, 115);
            this.CursoTxt.BorderStyle = BorderStyle.FixedSingle;
            this.CursoTxt.BackColor = Color.FromArgb(250, 245, 240);

            // TurnoLbl
            this.TurnoLbl.Text = "Turno *";
            this.TurnoLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.TurnoLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TurnoLbl.AutoSize = true;
            this.TurnoLbl.Location = new Point(50, 165);

            // TurnoCombo
            this.TurnoCombo.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.TurnoCombo.Size = new Size(180, 32);
            this.TurnoCombo.Location = new Point(50, 190);
            this.TurnoCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.TurnoCombo.BackColor = Color.FromArgb(250, 245, 240);
            this.TurnoCombo.Items.AddRange(new object[] { "Manana", "Tarde" });
            this.TurnoCombo.SelectedIndex = 0;

            // CantAlumnosLbl
            this.CantAlumnosLbl.Text = "Capacidad (alumnos)";
            this.CantAlumnosLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.CantAlumnosLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.CantAlumnosLbl.AutoSize = true;
            this.CantAlumnosLbl.Location = new Point(270, 165);

            // CantAlumnosTxt
            this.CantAlumnosTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.CantAlumnosTxt.Size = new Size(180, 32);
            this.CantAlumnosTxt.Location = new Point(270, 190);
            this.CantAlumnosTxt.BorderStyle = BorderStyle.FixedSingle;
            this.CantAlumnosTxt.BackColor = Color.FromArgb(250, 245, 240);
            this.CantAlumnosTxt.PlaceholderText = "Ej: 10";

            // GuardarBtn
            this.GuardarBtn.Text = "💾 Guardar";
            this.GuardarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.GuardarBtn.Size = new Size(180, 50);
            this.GuardarBtn.Location = new Point(50, 250);
            this.GuardarBtn.BackColor = Color.FromArgb(255, 140, 0);
            this.GuardarBtn.ForeColor = Color.White;
            this.GuardarBtn.FlatStyle = FlatStyle.Flat;
            this.GuardarBtn.FlatAppearance.BorderSize = 0;
            this.GuardarBtn.Cursor = Cursors.Hand;
            this.GuardarBtn.Click += new System.EventHandler(this.GuardarBtn_Click);

            // CancelarBtn
            this.CancelarBtn.Text = "❌ Cancelar";
            this.CancelarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.CancelarBtn.Size = new Size(180, 50);
            this.CancelarBtn.Location = new Point(270, 250);
            this.CancelarBtn.BackColor = Color.FromArgb(139, 90, 60);
            this.CancelarBtn.ForeColor = Color.White;
            this.CancelarBtn.FlatStyle = FlatStyle.Flat;
            this.CancelarBtn.FlatAppearance.BorderSize = 0;
            this.CancelarBtn.Cursor = Cursors.Hand;
            this.CancelarBtn.Click += new System.EventHandler(this.CancelarBtn_Click);

            // Add controls to FormPanel
            this.FormPanel.Controls.Add(this.TituloLbl);
            this.FormPanel.Controls.Add(this.CursoLbl);
            this.FormPanel.Controls.Add(this.CursoTxt);
            this.FormPanel.Controls.Add(this.TurnoLbl);
            this.FormPanel.Controls.Add(this.TurnoCombo);
            this.FormPanel.Controls.Add(this.CantAlumnosLbl);
            this.FormPanel.Controls.Add(this.CantAlumnosTxt);
            this.FormPanel.Controls.Add(this.GuardarBtn);
            this.FormPanel.Controls.Add(this.CancelarBtn);

            // Add controls to Form
            this.Controls.Add(this.FormPanel);
        }
    }
}