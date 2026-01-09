using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using PeluqueriaApp.Models;
using PeluqueriaApp.Services;

namespace PeluqueriaApp
{
    public partial class ServiciosForm : Form
    {
        private List<Servicio> servicios = new List<Servicio>();

        public ServiciosForm()
        {
            InitializeComponent();
            CargarServicios();
        }

        private async void CargarServicios()
        {
            try
            {
                // Mostrar indicador de carga
                TarjetesFlowPanel.Controls.Clear();
                Label cargandoLbl = new Label
                {
                    Text = "Cargando servicios...",
                    Font = new Font("Segoe UI", 14F, FontStyle.Italic),
                    ForeColor = Color.FromArgb(139, 90, 60),
                    AutoSize = true
                };
                TarjetesFlowPanel.Controls.Add(cargandoLbl);

                // Llamar a la API
                servicios = await ApiService.GetAsync<List<Servicio>>("api/servicios");

                // Limpiar y mostrar servicios
                TarjetesFlowPanel.Controls.Clear();

                if (servicios == null || servicios.Count == 0)
                {
                    Label sinDatosLbl = new Label
                    {
                        Text = "No hay servicios disponibles",
                        Font = new Font("Segoe UI", 12F),
                        ForeColor = Color.FromArgb(139, 90, 60),
                        AutoSize = true
                    };
                    TarjetesFlowPanel.Controls.Add(sinDatosLbl);
                    return;
                }

                // Crear tarjetas para cada servicio
                foreach (var servicio in servicios)
                {
                    Panel tarjeta = CrearTarjetaServicio(servicio);
                    TarjetesFlowPanel.Controls.Add(tarjeta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar servicios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CrearTarjetaServicio(Servicio servicio)
        {
            Panel tarjeta = new Panel
            {
                Size = new Size(320, 220),
                BackColor = Color.White,
                Margin = new Padding(15),
                Cursor = Cursors.Hand
            };

            // Icono
            Label iconoLbl = new Label
            {
                Text = "✂️",
                Font = new Font("Segoe UI Emoji", 32F),
                ForeColor = Color.FromArgb(255, 140, 0),
                AutoSize = false,
                Size = new Size(320, 60),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 15)
            };

            // Nombre
            Label nombreLbl = new Label
            {
                Text = servicio.nombre,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 35, 30),
                AutoSize = false,
                Size = new Size(300, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 80)
            };

            // Descripción
            Label descripcionLbl = new Label
            {
                Text = servicio.descripcion ?? "Sin descripción",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoSize = false,
                Size = new Size(300, 40),
                TextAlign = ContentAlignment.TopCenter,
                Location = new Point(10, 115)
            };

            // Duración
            Label duracionLbl = new Label
            {
                Text = $"⏱️ {servicio.duracion} min",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(139, 90, 60),
                AutoSize = false,
                Size = new Size(150, 25),
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(20, 165)
            };

            // Precio
            Label precioLbl = new Label
            {
                Text = $"{servicio.precio:F2} €",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 140, 0),
                AutoSize = false,
                Size = new Size(150, 25),
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(150, 165)
            };

            tarjeta.Controls.Add(iconoLbl);
            tarjeta.Controls.Add(nombreLbl);
            tarjeta.Controls.Add(descripcionLbl);
            tarjeta.Controls.Add(duracionLbl);
            tarjeta.Controls.Add(precioLbl);

            return tarjeta;
        }

        private async void BuscarBtn_Click(object sender, EventArgs e)
        {
            string textoBusqueda = BuscarServicisTxt.Text.Trim();

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                CargarServicios();
                return;
            }

            try
            {
                // Buscar servicios por nombre que empiecen con el texto
                servicios = await ApiService.GetAsync<List<Servicio>>($"api/servicios/buscar/empieza/{textoBusqueda}");

                // Actualizar vista
                TarjetesFlowPanel.Controls.Clear();

                if (servicios == null || servicios.Count == 0)
                {
                    Label sinResultadosLbl = new Label
                    {
                        Text = $"No se encontraron servicios que empiecen con '{textoBusqueda}'",
                        Font = new Font("Segoe UI", 12F),
                        ForeColor = Color.FromArgb(139, 90, 60),
                        AutoSize = true
                    };
                    TarjetesFlowPanel.Controls.Add(sinResultadosLbl);
                    return;
                }

                foreach (var servicio in servicios)
                {
                    Panel tarjeta = CrearTarjetaServicio(servicio);
                    TarjetesFlowPanel.Controls.Add(tarjeta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar servicios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IniciBoto_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm();
            homeForm.Show();
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

        private void MiCuentaBoto_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pantalla de Mi Cuenta en desarrollo", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // INSTRUCCIONES: Reemplazar el método TancarSessioBoto_Click en TODOS los formularios
        // (HomeForm.cs, UsuariosForm.cs, ClientesForm.cs, ServiciosForm.cs)

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