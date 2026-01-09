using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.IO;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class UsuariosForm : Form
    {
        public UsuariosForm()
        {
            InitializeComponent();
            ConfigurarDataGrid();
            CargarUsuarios();
        }
        // buenas
        private void ConfigurarDataGrid()
        {
            UsuariosDataGrid.Columns.Clear();
            UsuariosDataGrid.Columns.Add("idUsuario", "ID");
            UsuariosDataGrid.Columns.Add("nombre", "Nombre");
            UsuariosDataGrid.Columns.Add("apellidos", "Apellidos");
            UsuariosDataGrid.Columns.Add("correo", "Email");
            UsuariosDataGrid.Columns.Add("rol", "Rol");

            UsuariosDataGrid.Columns["idUsuario"].Width = 50;
        }

        private void CargarUsuarios()
        {
            var url = "http://localhost:8090/api/usuarios";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            // Añadir token de autorización
            if (!string.IsNullOrEmpty(ApiService.Token))
            {
                request.Headers.Add("Authorization", $"Bearer {ApiService.Token}");
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            // Deserializar la respuesta
                            var usuarios = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Usuario>>(responseBody);

                            // Limpiar DataGrid
                            UsuariosDataGrid.Rows.Clear();

                            // Cargar datos
                            if (usuarios != null && usuarios.Count > 0)
                            {
                                foreach (var usuario in usuarios)
                                {
                                    UsuariosDataGrid.Rows.Add(
                                        usuario.idUsuario,
                                        usuario.nombre ?? "",
                                        usuario.apellidos ?? "",
                                        usuario.correo ?? "",
                                        usuario.rol ?? ""
                                    );
                                }
                            }
                            else
                            {
                                MessageBox.Show("No hay usuarios registrados", "Información",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarBtn_Click(object sender, EventArgs e)
        {
            string textoBusqueda = BuscarUsuariosTxt.Text.Trim();

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                CargarUsuarios();
                return;
            }

            var url = "http://localhost:8090/api/usuarios";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            if (!string.IsNullOrEmpty(ApiService.Token))
            {
                request.Headers.Add("Authorization", $"Bearer {ApiService.Token}");
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            var usuarios = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Usuario>>(responseBody);

                            UsuariosDataGrid.Rows.Clear();

                            if (usuarios != null)
                            {
                                foreach (var usuario in usuarios)
                                {
                                    if ((usuario.nombre?.ToLower().Contains(textoBusqueda.ToLower()) == true) ||
                                        (usuario.apellidos?.ToLower().Contains(textoBusqueda.ToLower()) == true) ||
                                        (usuario.correo?.ToLower().Contains(textoBusqueda.ToLower()) == true))
                                    {
                                        UsuariosDataGrid.Rows.Add(
                                            usuario.idUsuario,
                                            usuario.nombre ?? "",
                                            usuario.apellidos ?? "",
                                            usuario.correo ?? "",
                                            usuario.rol ?? ""
                                        );
                                    }
                                }
                            }

                            if (UsuariosDataGrid.Rows.Count == 0)
                            {
                                MessageBox.Show($"No se encontraron usuarios con '{textoBusqueda}'",
                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show($"Error al buscar usuarios: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearUsuarioBtn_Click(object sender, EventArgs e)
        {
            CrearEditarUsuarioForm crearForm = new CrearEditarUsuarioForm();
            DialogResult result = crearForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                CargarUsuarios();
            }
        }

        private void IniciBoto_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm();
            homeForm.Show();
            this.Hide();
        }

        private void ServiciosBoto_Click(object sender, EventArgs e)
        {
            ServiciosForm serviciosForm = new ServiciosForm();
            serviciosForm.Show();
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

        private void MiCuentaBoto_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pantalla de Mi Cuenta en desarrollo", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
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