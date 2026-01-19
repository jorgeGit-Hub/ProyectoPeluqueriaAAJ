using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    partial class HorariosServicioForm
    {
        private Panel FormPanel;
        private Label TituloLbl;
        private DataGridView HorariosDataGrid;
        private Button CrearHorarioBtn;
        private Button EditarBtn;
        private Button EliminarBtn;
        private Button CerrarBtn;

        private void InitializeComponent()
        {
            this.FormPanel = new Panel();
            this.TituloLbl = new Label();
            this.HorariosDataGrid = new DataGridView();
            this.CrearHorarioBtn = new Button();
            this.EditarBtn = new Button();
            this.EliminarBtn = new Button();
            this.CerrarBtn = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.HorariosDataGrid)).BeginInit();

            // 
            // HorariosServicioForm
            // 
            this.ClientSize = new Size(900, 650);
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
            this.FormPanel.Size = new Size(850, 600);

            // 
            // TituloLbl
            // 
            this.TituloLbl.Text = "Horarios del Servicio";
            this.TituloLbl.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            this.TituloLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TituloLbl.AutoSize = false;
            this.TituloLbl.Size = new Size(850, 50);
            this.TituloLbl.TextAlign = ContentAlignment.MiddleCenter;
            this.TituloLbl.Location = new Point(0, 20);

            // 
            // CrearHorarioBtn
            // 
            this.CrearHorarioBtn.Text = "➕ Crear Horario";
            this.CrearHorarioBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.CrearHorarioBtn.Size = new Size(180, 40);
            this.CrearHorarioBtn.Location = new Point(630, 80);
            this.CrearHorarioBtn.BackColor = Color.FromArgb(139, 90, 60);
            this.CrearHorarioBtn.ForeColor = Color.White;
            this.CrearHorarioBtn.FlatStyle = FlatStyle.Flat;
            this.CrearHorarioBtn.FlatAppearance.BorderSize = 0;
            this.CrearHorarioBtn.Cursor = Cursors.Hand;
            this.CrearHorarioBtn.Click += new System.EventHandler(this.CrearHorarioBtn_Click);

            // 
            // HorariosDataGrid
            // 
            this.HorariosDataGrid.Name = "HorariosDataGrid";
            this.HorariosDataGrid.Location = new Point(40, 140);
            this.HorariosDataGrid.Size = new Size(770, 350);
            this.HorariosDataGrid.BackgroundColor = Color.White;
            this.HorariosDataGrid.BorderStyle = BorderStyle.None;
            this.HorariosDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HorariosDataGrid.GridColor = Color.FromArgb(240, 240, 240);
            this.HorariosDataGrid.DefaultCellStyle.BackColor = Color.White;
            this.HorariosDataGrid.DefaultCellStyle.ForeColor = Color.FromArgb(45, 35, 30);
            this.HorariosDataGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 160, 50);
            this.HorariosDataGrid.DefaultCellStyle.SelectionForeColor = Color.White;
            this.HorariosDataGrid.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            this.HorariosDataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 140, 0);
            this.HorariosDataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.HorariosDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.HorariosDataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.HorariosDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.HorariosDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.HorariosDataGrid.MultiSelect = false;
            this.HorariosDataGrid.ReadOnly = true;
            this.HorariosDataGrid.AllowUserToAddRows = false;
            this.HorariosDataGrid.AllowUserToDeleteRows = false;

            // 
            // EditarBtn
            // 
            this.EditarBtn.Text = "✏️ Editar";
            this.EditarBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.EditarBtn.Size = new Size(150, 45);
            this.EditarBtn.Location = new Point(40, 510);
            this.EditarBtn.BackColor = Color.FromArgb(255, 140, 0);
            this.EditarBtn.ForeColor = Color.White;
            this.EditarBtn.FlatStyle = FlatStyle.Flat;
            this.EditarBtn.FlatAppearance.BorderSize = 0;
            this.EditarBtn.Cursor = Cursors.Hand;
            this.EditarBtn.Click += new System.EventHandler(this.EditarBtn_Click);

            // 
            // EliminarBtn
            // 
            this.EliminarBtn.Text = "🗑️ Eliminar";
            this.EliminarBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.EliminarBtn.Size = new Size(150, 45);
            this.EliminarBtn.Location = new Point(210, 510);
            this.EliminarBtn.BackColor = Color.FromArgb(200, 50, 50);
            this.EliminarBtn.ForeColor = Color.White;
            this.EliminarBtn.FlatStyle = FlatStyle.Flat;
            this.EliminarBtn.FlatAppearance.BorderSize = 0;
            this.EliminarBtn.Cursor = Cursors.Hand;
            this.EliminarBtn.Click += new System.EventHandler(this.EliminarBtn_Click);

            // 
            // CerrarBtn
            // 
            this.CerrarBtn.Text = "❌ Cerrar";
            this.CerrarBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.CerrarBtn.Size = new Size(150, 45);
            this.CerrarBtn.Location = new Point(660, 510);
            this.CerrarBtn.BackColor = Color.FromArgb(139, 90, 60);
            this.CerrarBtn.ForeColor = Color.White;
            this.CerrarBtn.FlatStyle = FlatStyle.Flat;
            this.CerrarBtn.FlatAppearance.BorderSize = 0;
            this.CerrarBtn.Cursor = Cursors.Hand;
            this.CerrarBtn.Click += new System.EventHandler(this.CerrarBtn_Click);

            // 
            // Add controls to FormPanel
            // 
            this.FormPanel.Controls.Add(this.TituloLbl);
            this.FormPanel.Controls.Add(this.CrearHorarioBtn);
            this.FormPanel.Controls.Add(this.HorariosDataGrid);
            this.FormPanel.Controls.Add(this.EditarBtn);
            this.FormPanel.Controls.Add(this.EliminarBtn);
            this.FormPanel.Controls.Add(this.CerrarBtn);

            // 
            // Add controls to Form
            // 
            this.Controls.Add(this.FormPanel);

            ((System.ComponentModel.ISupportInitialize)(this.HorariosDataGrid)).EndInit();
        }
    }
}