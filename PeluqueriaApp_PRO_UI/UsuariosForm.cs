using System;
using System.Collections.Generic;
using System.Windows.Forms;
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

        private async void CargarUsuarios()
        {
            try
            {
                var usuarios = await ApiService.GetAsync<List<Usuario>>("api/usuarios");

                UsuariosDataGrid.Rows.Clear();

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
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BuscarBtn_Click(object sender, EventArgs e)
        {
            string textoBusqueda = BuscarUsuariosTxt.Text.Trim();

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                CargarUsuarios();
                return;
            }

            try
            {
                var usuarios = await ApiService.GetAsync<List<Usuario>>("api/usuarios");
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
                    MessageBox.Show($"No se encontraron usuarios con '{textoBusqueda}'",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar usuarios: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearUsuarioBtn_Click(object sender, EventArgs e)
        {
            CrearEditarUsuarioForm crearForm = new CrearEditarUsuarioForm();
            if (crearForm.ShowDialog() == DialogResult.OK)
                CargarUsuarios();
        }

        private void EditarBtn_Click(object sender, EventArgs e)
        {
            if (UsuariosDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un usuario para editar",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idUsuario = Convert.ToInt32(UsuariosDataGrid.SelectedRows[0].Cells["idUsuario"].Value);
            CrearEditarUsuarioForm editarForm = new CrearEditarUsuarioForm(idUsuario);
            if (editarForm.ShowDialog() == DialogResult.OK)
                CargarUsuarios();
        }

        private async void EliminarBtn_Click(object sender, EventArgs e)
        {
            if (UsuariosDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un usuario para eliminar",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Estás seguro de que quieres eliminar este usuario y sus perfiles (Cliente/Administrador)?\n\nEsta acción no se puede deshacer.",
                "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int idUsuario = Convert.ToInt32(UsuariosDataGrid.SelectedRows[0].Cells["idUsuario"].Value);
                string rol = UsuariosDataGrid.SelectedRows[0].Cells["rol"].Value?.ToString() ?? "";

                try
                {
                    // 1. Intentamos eliminar el perfil de rol específico primero
                    try
                    {
                        if (rol.Equals("CLIENTE", StringComparison.OrdinalIgnoreCase))
                        {
                            await ApiService.DeleteAsync($"api/clientes/{idUsuario}");
                        }
                        else if (rol.Equals("ADMINISTRADOR", StringComparison.OrdinalIgnoreCase))
                        {
                            // Asumiendo que tu endpoint es /api/administradores. Cámbialo si es distinto.
                            await ApiService.DeleteAsync($"api/administradores/{idUsuario}");
                        }
                    }
                    catch (Exception)
                    {
                        // Dejamos que continúe por si el rol ya fue borrado previamente o no existía.
                    }

                    // 2. Eliminamos el usuario base de la tabla `usuario`
                    bool eliminadoUsuario = await ApiService.DeleteAsync($"api/usuarios/{idUsuario}");
                    if (eliminadoUsuario)
                    {
                        MessageBox.Show("Usuario y sus roles asociados eliminados correctamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarUsuarios();
                    }
                }
                catch (Exception ex)
                {
                    // Error capturado si el usuario/cliente tiene entidades bloqueantes (ej: Cliente 7 y sus citas)
                    MessageBox.Show($"Error al eliminar el usuario. Es posible que tenga citas, valoraciones o bloqueos de horario asociados que debes eliminar primero.\n\nDetalle técnico: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void IniciBoto_Click(object sender, EventArgs e) { new HomeForm().Show(); this.Hide(); }
        private void ServiciosBoto_Click(object sender, EventArgs e) { new ServiciosForm().Show(); this.Hide(); }
        private void ClientesBoto_Click(object sender, EventArgs e) { new ClientesForm().Show(); this.Hide(); }
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
    }
}