using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class ClientesForm : Form
    {
        private List<ClienteCompleto> listaClientesOriginal = new List<ClienteCompleto>();

        public ClientesForm()
        {
            InitializeComponent();
            this.Load += ClientesForm_Load;
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

        private async void CargarClientes()
        {
            try
            {
                // ✅ Usa ApiService.GetAsync igual que GruposForm
                // El endpoint /api/clientes devuelve la lista de clientes con datos básicos
                var clientes = await ApiService.GetAsync<List<ClienteCompleto>>("api/clientes/completos");

                listaClientesOriginal = clientes ?? new List<ClienteCompleto>();

                MostrarClientesEnGrid(listaClientesOriginal);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarClientesEnGrid(List<ClienteCompleto> clientes)
        {
            ClientesDataGrid.Rows.Clear();

            if (clientes == null || clientes.Count == 0)
            {
                MessageBox.Show("No hay clientes registrados", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var cliente in clientes)
            {
                ClientesDataGrid.Rows.Add(
                    cliente.idUsuario,
                    cliente.nombre ?? "",
                    cliente.apellidos ?? "",
                    cliente.correo ?? "",
                    cliente.telefono ?? "N/A",
                    cliente.direccion ?? "N/A"
                );
            }
        }

        private void BuscarBtn_Click(object sender, EventArgs e)
        {
            string textoBusqueda = BuscarClientesTxt.Text.Trim().ToLower();
            string filtroSeleccionado = FiltroBusquedaCombo.SelectedItem?.ToString() ?? "Nombre";

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                MostrarClientesEnGrid(listaClientesOriginal);
                return;
            }

            var filtrados = new List<ClienteCompleto>();

            foreach (var c in listaClientesOriginal)
            {
                bool coincide = false;
                switch (filtroSeleccionado)
                {
                    case "Email":
                        coincide = c.correo?.ToLower().Contains(textoBusqueda) == true;
                        break;
                    case "Teléfono":
                        coincide = c.telefono?.ToLower().Contains(textoBusqueda) == true;
                        break;
                    case "Dirección":
                        coincide = c.direccion?.ToLower().Contains(textoBusqueda) == true;
                        break;
                    default: // Nombre
                        coincide = (c.nombre?.ToLower().Contains(textoBusqueda) == true) ||
                                   (c.apellidos?.ToLower().Contains(textoBusqueda) == true);
                        break;
                }
                if (coincide) filtrados.Add(c);
            }

            ClientesDataGrid.Rows.Clear();
            if (filtrados.Count == 0)
                MessageBox.Show($"No se encontraron clientes con '{textoBusqueda}' en {filtroSeleccionado}",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MostrarClientesEnGrid(filtrados);
        }

        private void CrearClienteBtn_Click(object sender, EventArgs e)
        {
            CrearEditarClienteForm crearForm = new CrearEditarClienteForm();
            if (crearForm.ShowDialog() == DialogResult.OK)
                CargarClientes();
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
            if (editarForm.ShowDialog() == DialogResult.OK)
                CargarClientes();
        }

        private async void EliminarBtn_Click(object sender, EventArgs e)
        {
            if (ClientesDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un cliente para eliminar",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Estás seguro de que quieres eliminar este cliente y su cuenta de usuario asociado?\n\nEsta acción no se puede deshacer.",
                "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    int idCliente = Convert.ToInt32(ClientesDataGrid.SelectedRows[0].Cells["idUsuario"].Value);

                    // 1. Eliminamos primero el cliente (rol)
                    bool eliminadoCliente = await ApiService.DeleteAsync($"api/clientes/{idCliente}");

                    if (eliminadoCliente)
                    {
                        // 2. Si se eliminó el cliente correctamente, eliminamos el usuario base
                        try
                        {
                            await ApiService.DeleteAsync($"api/usuarios/{idCliente}");
                        }
                        catch (Exception ex)
                        {
                            // Ignoramos o mostramos un pequeño aviso si el usuario ya no existe
                            Console.WriteLine($"Nota: No se pudo eliminar el usuario base o ya estaba borrado. {ex.Message}");
                        }

                        MessageBox.Show("Cliente y usuario eliminados correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarClientes();
                    }
                }
                catch (Exception ex)
                {
                    // Aquí capturamos el error del Cliente 7 (Claves foráneas)
                    MessageBox.Show($"No se puede eliminar el cliente porque tiene citas o valoraciones asociadas.\n\nDetalle técnico: {ex.Message}", "Error de Dependencias",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void IniciBoto_Click(object sender, EventArgs e) { new HomeForm().Show(); this.Hide(); }
        private void ServiciosBoto_Click(object sender, EventArgs e) { new ServiciosForm().Show(); this.Hide(); }
        private void UsuariosBoto_Click(object sender, EventArgs e) { new UsuariosForm().Show(); this.Hide(); }
        private void CitasBoto_Click(object sender, EventArgs e) { new CitasForm().Show(); this.Hide(); }
        private void GruposBoto_Click(object sender, EventArgs e) { new GruposForm().Show(); this.Hide(); }
        private void HorarioSemanalBoto_Click(object sender, EventArgs e) { new HorarioSemanalForm().Show(); this.Hide(); }

        private void HorarioForm_Click(object sender, EventArgs e)
        {
            HorarioForm horarioForm = new HorarioForm();
            horarioForm.Show();
            this.Hide();
        }

        private void ValoracionForm_Click(object sender, EventArgs e)
        {
            ValoracionesForm valoracionform = new ValoracionesForm();
            valoracionform.Show();
            this.Hide();
        }

        private void MiCuentaBoto_Click(object sender, EventArgs e)
        {
            MiCuentaForm form = new MiCuentaForm();
            form.Show();
            this.Hide();
        }

        private void TancarSessioBoto_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estás seguro de que quieres cerrar sesión?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ApiService.ClearAuthToken();
                UserSession.CerrarSesion();
                new LoginForm().Show();
                this.Close();
            }
        }

        private void ClientesForm_Load(object sender, EventArgs e)
        {
            // Cargar el logo desde la memoria global de la app
            if (UserSession.LogoApp != null)
            {
                pbLogo.Image = UserSession.LogoApp;
            }

            // (Aquí debajo dejas el resto de código que ya tuvieras en este Load, si había algo)
        }
    }
}