using System.Drawing;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    partial class CrearEditarServicioForm
    {
        private Label TituloLbl;
        private Label NombreLbl;
        private TextBox NombreTxt;
        private Label DescripcionLbl;
        private TextBox DescripcionTxt;
        private Label DuracionLbl;
        private TextBox DuracionTxt;
        private Label PrecioLbl;
        private TextBox PrecioTxt;
        private Button GuardarBtn;
        private Button CancelarBtn;
        private Panel FormPanel;

        private void InitializeComponent()
        {
            this.FormPanel = new Panel();
            this.TituloLbl = new Label();
            this.NombreLbl = new Label();
            this.NombreTxt = new TextBox();
            this.DescripcionLbl = new Label();
            this.DescripcionTxt = new TextBox();
            this.DuracionLbl = new Label();
            this.DuracionTxt = new TextBox();
            this.PrecioLbl = new Label();
            this.PrecioTxt = new TextBox();
            this.GuardarBtn = new Button();
            this.CancelarBtn = new Button();

            // 
            // CrearEditarServicioForm
            // 
            this.ClientSize = new Size(600, 550);
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
            this.FormPanel.Location = new Point(50, 30);
            this.FormPanel.Size = new Size(500, 480);

            // 
            // TituloLbl
            // 
            this.TituloLbl.Text = "Crear Nuevo Servicio";
            this.TituloLbl.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            this.TituloLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.TituloLbl.AutoSize = false;
            this.TituloLbl.Size = new Size(500, 50);
            this.TituloLbl.TextAlign = ContentAlignment.MiddleCenter;
            this.TituloLbl.Location = new Point(0, 20);

            // 
            // NombreLbl
            // 
            this.NombreLbl.Text = "Nombre *";
            this.NombreLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.NombreLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.NombreLbl.AutoSize = true;
            this.NombreLbl.Location = new Point(50, 90);

            // 
            // NombreTxt
            // 
            this.NombreTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.NombreTxt.Size = new Size(400, 32);
            this.NombreTxt.Location = new Point(50, 115);
            this.NombreTxt.BorderStyle = BorderStyle.FixedSingle;
            this.NombreTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // DescripcionLbl
            // 
            this.DescripcionLbl.Text = "Descripción";
            this.DescripcionLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.DescripcionLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.DescripcionLbl.AutoSize = true;
            this.DescripcionLbl.Location = new Point(50, 160);

            // 
            // DescripcionTxt
            // 
            this.DescripcionTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.DescripcionTxt.Size = new Size(400, 80);
            this.DescripcionTxt.Location = new Point(50, 185);
            this.DescripcionTxt.BorderStyle = BorderStyle.FixedSingle;
            this.DescripcionTxt.BackColor = Color.FromArgb(250, 245, 240);
            this.DescripcionTxt.Multiline = true;

            // 
            // DuracionLbl
            // 
            this.DuracionLbl.Text = "Duración (minutos) *";
            this.DuracionLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.DuracionLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.DuracionLbl.AutoSize = true;
            this.DuracionLbl.Location = new Point(50, 285);

            // 
            // DuracionTxt
            // 
            this.DuracionTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.DuracionTxt.Size = new Size(180, 32);
            this.DuracionTxt.Location = new Point(50, 310);
            this.DuracionTxt.BorderStyle = BorderStyle.FixedSingle;
            this.DuracionTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // PrecioLbl
            // 
            this.PrecioLbl.Text = "Precio (€) *";
            this.PrecioLbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.PrecioLbl.ForeColor = Color.FromArgb(45, 35, 30);
            this.PrecioLbl.AutoSize = true;
            this.PrecioLbl.Location = new Point(270, 285);

            // 
            // PrecioTxt
            // 
            this.PrecioTxt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.PrecioTxt.Size = new Size(180, 32);
            this.PrecioTxt.Location = new Point(270, 310);
            this.PrecioTxt.BorderStyle = BorderStyle.FixedSingle;
            this.PrecioTxt.BackColor = Color.FromArgb(250, 245, 240);

            // 
            // GuardarBtn
            // 
            this.GuardarBtn.Text = "💾 Guardar";
            this.GuardarBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.GuardarBtn.Size = new Size(180, 50);
            this.GuardarBtn.Location = new Point(80, 380);
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
            this.CancelarBtn.Size = new Size(180, 50);
            this.CancelarBtn.Location = new Point(280, 380);
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
            this.FormPanel.Controls.Add(this.DescripcionLbl);
            this.FormPanel.Controls.Add(this.DescripcionTxt);
            this.FormPanel.Controls.Add(this.DuracionLbl);
            this.FormPanel.Controls.Add(this.DuracionTxt);
            this.FormPanel.Controls.Add(this.PrecioLbl);
            this.FormPanel.Controls.Add(this.PrecioTxt);
            this.FormPanel.Controls.Add(this.GuardarBtn);
            this.FormPanel.Controls.Add(this.CancelarBtn);

            // 
            // Add controls to Form
            // 
            this.Controls.Add(this.FormPanel);
        }
    }
}