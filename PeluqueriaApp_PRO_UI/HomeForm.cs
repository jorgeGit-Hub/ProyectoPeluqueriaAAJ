using PeluqueriaApp.Services;
using System;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
        }

        private void ServiciosBoto_Click(object sender, EventArgs e)
        {
            ServiciosForm serviciosForm = new ServiciosForm();
            serviciosForm.Show();
            this.Hide();
        }

        private void UsuariosBoto_Click(object sender, EventArgs e)
        {
            UsuariosForm usuariosForm = new UsuariosForm();
            usuariosForm.Show();
            this.Hide();
        }

        private void ClientesBoto_Click(object sender, EventArgs e)
        {
            ClientesForm clientesForm = new ClientesForm();
            clientesForm.Show();
            this.Hide();
        }

        private void CitasBoto_Click(object sender, EventArgs e)
        {
            CitasForm citasForm = new CitasForm();
            citasForm.Show();
            this.Hide();
        }

        private void GruposBoto_Click(object sender, EventArgs e)
        {
            GruposForm gruposForm = new GruposForm();
            gruposForm.Show();
            this.Hide();
        }

        private void HorarioSemanalBoto_Click(object sender, EventArgs e)
        {
            HorarioSemanalForm horarioForm = new HorarioSemanalForm();
            horarioForm.Show();
            this.Hide();
        }

        private void HorarioForm_Click(object sender, EventArgs e)
        {
            HorarioForm horarioForm = new HorarioForm();
            horarioForm.Show();
            this.Hide();
        }

        private void MiCuentaBoto_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pantalla de Mi Cuenta en desarrollo", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        

        private void TancarSessioBoto_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que quieres cerrar sesión?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // CRÍTICO: Limpiar token y sesión antes de volver al login
                ApiService.ClearAuthToken();
                UserSession.CerrarSesion();

                System.Diagnostics.Debug.WriteLine("Sesión cerrada - Token y datos de usuario eliminados");

                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
        }
    }
}