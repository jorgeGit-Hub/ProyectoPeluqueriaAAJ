using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.IO;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class ClientesForm : Form
    {
        public ClientesForm()
        {
            InitializeComponent();
            ConfigurarDataGrid();
            CargarClientes();
        }

        private void ConfigurarDataGrid()
        {
            ClientesDataGrid.Columns.Clear();
            ClientesDataGrid.Columns.Add("idUsuario", "ID");
            ClientesDataGrid.Columns.Add("nombre", "Nombre");
            ClientesDataGrid.Columns.Add("apellidos", "Apellidos");
            ClientesDataGrid.Columns.Add("correo", "Email");
            ClientesDataGrid.Columns.Add("telefono", "Teléfono");
            ClientesDataGrid.Columns.Add("direccion", "Dirección");

            ClientesDataGrid.Columns["idUsuario"].Width = 50;
        }

        private void CargarClientes()
        {
            // Primero obtener todos los usuarios
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

                            ClientesDataGrid.Rows.Clear();

                            if (usuarios != null && usuarios.Count > 0)
                            {
                                foreach (var usuario in usuarios)
                                {
                                    // Solo procesar clientes
                                    if (usuario.rol?.ToLower() == "cliente")
                                    {
                                        string telefono = "N/A";
                                        string direccion = "N/A";

                                        // Intentar obtener información adicional del cliente
                                        try
                                        {
                                            var urlCliente = $"http://localhost:8090/api/clientes/{usuario.idUsuario}";
                                            var requestCliente = (HttpWebRequest)WebRequest.Create(urlCliente);
                                            requestCliente.Method = "GET";
                                            requestCliente.ContentType = "application/json";
                                            requestCliente.Accept = "application/json";

                                            if (!string.IsNullOrEmpty(ApiService.Token))
                                            {
                                                requestCliente.Headers.Add("Authorization", $"Bearer {ApiService.Token}");
                                            }

                                            using (WebResponse responseCliente = requestCliente.GetResponse())
                                            {
                                                using (Stream strReaderCliente = responseCliente.GetResponseStream())
                                                {
                                                    if (strReaderCliente != null)
                                                    {
                                                        using (StreamReader objReaderCliente = new StreamReader(strReaderCliente))
                                                        {
                                                            string responseBodyCliente = objReaderCliente.ReadToEnd();
                                                            var clienteInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<Cliente>(responseBodyCliente);

                                                            if (clienteInfo != null)
                                                            {
                                                                telefono = clienteInfo.telefono ?? "N/A";
                                                                direccion = clienteInfo.direccion ?? "N/A";
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            // Si falla, usar N/A
                                        }

                                        ClientesDataGrid.Rows.Add(
                                            usuario.idUsuario,
                                            usuario.nombre ?? "",
                                            usuario.apellidos ?? "",
                                            usuario.correo ?? "",
                                            telefono,
                                            direccion
                                        );
                                    }
                                }
                            }

                            if (ClientesDataGrid.Rows.Count == 0)
                            {
                                MessageBox.Show("No hay clientes registrados", "Información",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarBtn_Click(object sender, EventArgs e)
        {
            string textoBusqueda = BuscarClientesTxt.Text.Trim();
            string filtroSeleccionado = FiltroBusquedaCombo.SelectedItem?.ToString() ?? "Nombre";

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                CargarClientes();
                return;
            }

            try
            {
                ClientesDataGrid.Rows.Clear();

                if (filtroSeleccionado == "Teléfono")
                {
                    // Buscar por teléfono
                    var url = $"http://localhost:8090/api/clientes/telefono/{textoBusqueda}";
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.ContentType = "application/json";
                    request.Accept = "application/json";

                    if (!string.IsNullOrEmpty(ApiService.Token))
                    {
                        request.Headers.Add("Authorization", $"Bearer {ApiService.Token}");
                    }

                    using (WebResponse response = request.GetResponse())
                    {
                        using (Stream strReader = response.GetResponseStream())
                        {
                            if (strReader != null)
                            {
                                using (StreamReader objReader = new StreamReader(strReader))
                                {
                                    string responseBody = objReader.ReadToEnd();
                                    var clientes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cliente>>(responseBody);

                                    if (clientes != null)
                                    {
                                        foreach (var cliente in clientes)
                                        {
                                            CargarUsuarioDeCliente(cliente);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (filtroSeleccionado == "Dirección")
                {
                    // Buscar por dirección
                    var url = $"http://localhost:8090/api/clientes/direccion/{textoBusqueda}";
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.ContentType = "application/json";
                    request.Accept = "application/json";

                    if (!string.IsNullOrEmpty(ApiService.Token))
                    {
                        request.Headers.Add("Authorization", $"Bearer {ApiService.Token}");
                    }

                    using (WebResponse response = request.GetResponse())
                    {
                        using (Stream strReader = response.GetResponseStream())
                        {
                            if (strReader != null)
                            {
                                using (StreamReader objReader = new StreamReader(strReader))
                                {
                                    string responseBody = objReader.ReadToEnd();
                                    var clientes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cliente>>(responseBody);

                                    if (clientes != null)
                                    {
                                        foreach (var cliente in clientes)
                                        {
                                            CargarUsuarioDeCliente(cliente);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    // Buscar por nombre o email
                    var url = "http://localhost:8090/api/usuarios";
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.ContentType = "application/json";
                    request.Accept = "application/json";

                    if (!string.IsNullOrEmpty(ApiService.Token))
                    {
                        request.Headers.Add("Authorization", $"Bearer {ApiService.Token}");
                    }

                    using (WebResponse response = request.GetResponse())
                    {
                        using (Stream strReader = response.GetResponseStream())
                        {
                            if (strReader != null)
                            {
                                using (StreamReader objReader = new StreamReader(strReader))
                                {
                                    string responseBody = objReader.ReadToEnd();
                                    var usuarios = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Usuario>>(responseBody);

                                    if (usuarios != null)
                                    {
                                        foreach (var usuario in usuarios)
                                        {
                                            if (usuario.rol?.ToLower() == "cliente")
                                            {
                                                bool coincide = false;

                                                if (filtroSeleccionado == "Email")
                                                {
                                                    coincide = usuario.correo?.ToLower().Contains(textoBusqueda.ToLower()) == true;
                                                }
                                                else // Nombre
                                                {
                                                    coincide = (usuario.nombre?.ToLower().Contains(textoBusqueda.ToLower()) == true) ||
                                                               (usuario.apellidos?.ToLower().Contains(textoBusqueda.ToLower()) == true);
                                                }

                                                if (coincide)
                                                {
                                                    string telefono = "N/A";
                                                    string direccion = "N/A";

                                                    try
                                                    {
                                                        var urlCliente = $"http://localhost:8090/api/clientes/{usuario.idUsuario}";
                                                        var requestCliente = (HttpWebRequest)WebRequest.Create(urlCliente);
                                                        requestCliente.Method = "GET";
                                                        requestCliente.ContentType = "application/json";
                                                        requestCliente.Accept = "application/json";

                                                        if (!string.IsNullOrEmpty(ApiService.Token))
                                                        {
                                                            requestCliente.Headers.Add("Authorization", $"Bearer {ApiService.Token}");
                                                        }

                                                        using (WebResponse responseCliente = requestCliente.GetResponse())
                                                        {
                                                            using (Stream strReaderCliente = responseCliente.GetResponseStream())
                                                            {
                                                                if (strReaderCliente != null)
                                                                {
                                                                    using (StreamReader objReaderCliente = new StreamReader(strReaderCliente))
                                                                    {
                                                                        string responseBodyCliente = objReaderCliente.ReadToEnd();
                                                                        var clienteInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<Cliente>(responseBodyCliente);

                                                                        if (clienteInfo != null)
                                                                        {
                                                                            telefono = clienteInfo.telefono ?? "N/A";
                                                                            direccion = clienteInfo.direccion ?? "N/A";
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    catch { }

                                                    ClientesDataGrid.Rows.Add(
                                                        usuario.idUsuario,
                                                        usuario.nombre ?? "",
                                                        usuario.apellidos ?? "",
                                                        usuario.correo ?? "",
                                                        telefono,
                                                        direccion
                                                    );
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (ClientesDataGrid.Rows.Count == 0)
                {
                    MessageBox.Show($"No se encontraron clientes con '{textoBusqueda}' en {filtroSeleccionado}",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show($"Error al buscar clientes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarUsuarioDeCliente(Cliente cliente)
        {
            try
            {
                var url = $"http://localhost:8090/api/usuarios/{cliente.idUsuario}";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Accept = "application/json";

                if (!string.IsNullOrEmpty(ApiService.Token))
                {
                    request.Headers.Add("Authorization", $"Bearer {ApiService.Token}");
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader != null)
                        {
                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                string responseBody = objReader.ReadToEnd();
                                var usuario = Newtonsoft.Json.JsonConvert.DeserializeObject<Usuario>(responseBody);

                                if (usuario != null)
                                {
                                    ClientesDataGrid.Rows.Add(
                                        usuario.idUsuario,
                                        usuario.nombre ?? "",
                                        usuario.apellidos ?? "",
                                        usuario.correo ?? "",
                                        cliente.telefono ?? "N/A",
                                        cliente.direccion ?? "N/A"
                                    );
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void CrearClienteBtn_Click(object sender, EventArgs e)
        {
            CrearEditarClienteForm crearForm = new CrearEditarClienteForm();
            DialogResult result = crearForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                CargarClientes();
            }
        }

        private void EditarBtn_Click(object sender, EventArgs e)
        {
            if (ClientesDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un cliente para editar",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idCliente = Convert.ToInt32(ClientesDataGrid.SelectedRows[0].Cells["idUsuario"].Value);

            CrearEditarClienteForm editarForm = new CrearEditarClienteForm(idCliente);
            DialogResult result = editarForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                CargarClientes();
            }
        }

        private void EliminarBtn_Click(object sender, EventArgs e)
        {
            if (ClientesDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un cliente para eliminar",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que quieres eliminar este cliente?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    int idCliente = Convert.ToInt32(ClientesDataGrid.SelectedRows[0].Cells["idUsuario"].Value);

                    var url = $"http://localhost:8090/api/clientes/{idCliente}";
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "DELETE";
                    request.ContentType = "application/json";
                    request.Accept = "application/json";

                    if (!string.IsNullOrEmpty(ApiService.Token))
                    {
                        request.Headers.Add("Authorization", $"Bearer {ApiService.Token}");
                    }

                    using (WebResponse response = request.GetResponse())
                    {
                        MessageBox.Show("Cliente eliminado correctamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarClientes();
                    }
                }
                catch (WebException ex)
                {
                    MessageBox.Show($"Error al eliminar cliente: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void UsuariosBoto_Click(object sender, EventArgs e)
        {
            UsuariosForm usuariosForm = new UsuariosForm();
            usuariosForm.Show();
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