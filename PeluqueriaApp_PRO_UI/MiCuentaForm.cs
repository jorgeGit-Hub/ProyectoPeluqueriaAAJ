using PeluqueriaApp.Services;
using System;
using System.Windows.Forms;

namespace PeluqueriaApp
{
    public partial class MiCuentaForm : Form
    {
        public MiCuentaForm()
        {
            InitializeComponent();
            CargarDatosUsuario();
        }

        private void CargarDatosUsuario()
        {
            // Saludo superior
            BienvenidaLbl.Text = $"Bienvenido/a, {UserSession.Nombre}";

            // Rellenar las cajas de texto con los datos de sesión
            NombreTxt.Text = UserSession.Nombre;
            ApellidosTxt.Text = UserSession.Apellidos;
            CorreoTxt.Text = UserSession.Correo;

            // Ponemos la primera letra del rol en mayúscula para que quede más bonito
            if (!string.IsNullOrEmpty(UserSession.Rol))
            {
                RolTxt.Text = char.ToUpper(UserSession.Rol[0]) + UserSession.Rol.Substring(1).ToLower();
            }
        }

        private void AccionDesarrollo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Esta funcionalidad (guardar perfil o cambiar contraseña) estará disponible próximamente.", "En Desarrollo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // -------------------------------------------------------------------
        // NAVEGACIÓN DEL MENÚ LATERAL
        // -------------------------------------------------------------------

        private void IniciBoto_Click(object sender, EventArgs e)
        {
            HomeForm form = new HomeForm();
            form.Show();
            this.Hide();
        }

        private void ServiciosBoto_Click(object sender, EventArgs e)
        {
            ServiciosForm form = new ServiciosForm();
            form.Show();
            this.Hide();
        }

        private void UsuariosBoto_Click(object sender, EventArgs e)
        {
            UsuariosForm form = new UsuariosForm();
            form.Show();
            this.Hide();
        }

        private void ClientesBoto_Click(object sender, EventArgs e)
        {
            ClientesForm form = new ClientesForm();
            form.Show();
            this.Hide();
        }

        private void CitasBoto_Click(object sender, EventArgs e)
        {
            CitasForm form = new CitasForm();
            form.Show();
            this.Hide();
        }

        private void GruposBoto_Click(object sender, EventArgs e)
        {
            GruposForm form = new GruposForm();
            form.Show();
            this.Hide();
        }

        private void HorarioForm_Click(object sender, EventArgs e)
        {
            HorarioForm form = new HorarioForm();
            form.Show();
            this.Hide();
        }

        private void HorarioSemanalBoto_Click(object sender, EventArgs e)
        {
            HorarioSemanalForm form = new HorarioSemanalForm();
            form.Show();
            this.Hide();
        }

        private void ValoracionForm_Click(object sender, EventArgs e)
        {
            ValoracionesForm form = new ValoracionesForm();
            form.Show();
            this.Hide();
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
                ApiService.ClearAuthToken();
                UserSession.CerrarSesion();
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
        }
    }
}