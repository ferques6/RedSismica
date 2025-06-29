using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RedSismicaWinForms
{
    public partial class PantRegistrarResultado : Form
    {
        private GestorRegistrarResultado gestor;
        private List<EventoSismico> eventos;
        private EventoSismico eventoSeleccionado;

        // Controles visuales
        private ListBox listBoxEventos;
        private Button btnSeleccionar;
        private TextBox txtDetalles;
        private ListBox listBoxSeries;
        private ComboBox comboAccion;
        private Button btnRegistrar;
        private Label lblTitulo, lblSubEventos, lblDetalles, lblAccion, lblSeries, lblMensaje;

        public PantRegistrarResultado(GestorRegistrarResultado gestor)
        {
            this.gestor = gestor;
            InicializarControles();
            seleccionarRegistroResultadoRevisionManual(); // Llamo al flujo principal de la pantalla
        }

        private void InicializarControles()
        {
            this.Text = "Registro de revisión manual de eventos sísmicos";
            this.Width = 930;
            this.Height = 550;
            this.BackColor = Color.WhiteSmoke;

            Font fuenteTitulo = new Font("Segoe UI", 16, FontStyle.Bold);
            Font fuenteNormal = new Font("Segoe UI", 11);

            // Título principal
            lblTitulo = new Label()
            {
                Text = "Registrar resultado de revisión manual",
                Font = fuenteTitulo,
                Left = 20,
                Top = 10,
                Width = 700,
                ForeColor = Color.DarkSlateBlue
            };

            // Subtítulo eventos
            lblSubEventos = new Label()
            {
                Text = "Eventos sísmicos auto detectados",
                Font = fuenteNormal,
                Left = 20,
                Top = 60,
                Width = 350
            };

            listBoxEventos = new ListBox()
            {
                Left = 20,
                Top = 90,
                Width = 370,
                Height = 140,
                Font = fuenteNormal,
                BackColor = Color.White
            };

            btnSeleccionar = new Button()
            {
                Left = 410,
                Top = 90,
                Width = 140,
                Height = 38,
                Text = "Mostrar detalles",
                Font = fuenteNormal,
                BackColor = Color.FromArgb(100, 149, 237),
                ForeColor = Color.White
            };
            btnSeleccionar.FlatStyle = FlatStyle.Flat;
            btnSeleccionar.FlatAppearance.BorderSize = 0;
            btnSeleccionar.Cursor = Cursors.Hand;
            btnSeleccionar.Click += btnSeleccionar_Click;

            // Detalles del evento
            lblDetalles = new Label()
            {
                Text = "Detalle del evento seleccionado",
                Font = fuenteNormal,
                Left = 20,
                Top = 240,
                Width = 400
            };

            txtDetalles = new TextBox()
            {
                Left = 20,
                Top = 270,
                Width = 530,
                Height = 90,
                Multiline = true,
                ReadOnly = true,
                Font = fuenteNormal,
                BackColor = Color.Gainsboro,
                ScrollBars = ScrollBars.Vertical
            };

            // Series temporales
            lblSeries = new Label()
            {
                Text = "Series temporales asociadas",
                Font = fuenteNormal,
                Left = 570,
                Top = 60,
                Width = 260
            };

            listBoxSeries = new ListBox()
            {
                Left = 570,
                Top = 90,
                Width = 320,
                Height = 270,
                Font = fuenteNormal,
                BackColor = Color.White
            };

            // Acción
            lblAccion = new Label()
            {
                Text = "Acción a realizar sobre el evento:",
                Font = fuenteNormal,
                Left = 20,
                Top = 375,
                Width = 260
            };

            comboAccion = new ComboBox()
            {
                Left = 20,
                Top = 400,
                Width = 230,
                Font = fuenteNormal,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            comboAccion.Items.AddRange(new string[] { "Confirmar", "Rechazar", "Solicitar revisión a experto" });
            comboAccion.SelectedIndex = 0;

            btnRegistrar = new Button()
            {
                Left = 270,
                Top = 400,
                Width = 170,
                Height = 38,
                Text = "Registrar resultado",
                Font = fuenteNormal,
                BackColor = Color.MediumSeaGreen,
                ForeColor = Color.White
            };
            btnRegistrar.FlatStyle = FlatStyle.Flat;
            btnRegistrar.FlatAppearance.BorderSize = 0;
            btnRegistrar.Cursor = Cursors.Hand;
            btnRegistrar.Click += btnRegistrar_Click;

            // Mensaje de feedback
            lblMensaje = new Label()
            {
                Left = 20,
                Top = 450,
                Width = 880,
                Height = 28,
                Font = fuenteNormal,
                ForeColor = Color.DarkGreen
            };

            // Agrego los controles al formulario
            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblSubEventos);
            this.Controls.Add(listBoxEventos);
            this.Controls.Add(btnSeleccionar);
            this.Controls.Add(lblDetalles);
            this.Controls.Add(txtDetalles);
            this.Controls.Add(lblSeries);
            this.Controls.Add(listBoxSeries);
            this.Controls.Add(lblAccion);
            this.Controls.Add(comboAccion);
            this.Controls.Add(btnRegistrar);
            this.Controls.Add(lblMensaje);
        }

        // === Métodos requeridos por el diagrama de clases/secuencia ===

        // 1. Habilitar la ventana (p. ej., al arrancar)
        public void habilitarVentana()
        {
            this.Enabled = true;
        }

        // 2. Mostrar eventos ordenados
        public void mostrarEventosOrdenados(List<EventoSismico> eventos)
        {
            listBoxEventos.Items.Clear();
            foreach (var ev in eventos)
                listBoxEventos.Items.Add(ev.getResumenEvento());
        }

        // 3. Pedir selección de evento al usuario (simulado en GUI por selección en ListBox)
        public int pedirSeleccionEvento()
        {
            return listBoxEventos.SelectedIndex;
        }

        // 4. Mostrar datos sísmicos del evento seleccionado
        public void mostrarDatosSismicos(EventoSismico evento)
        {
            txtDetalles.Text = evento.getDatosEventoSismicoCompleto();

            listBoxSeries.Items.Clear();
            foreach (var st in evento.getSerieTemporal())
            {
                listBoxSeries.Items.Add(
                    $"Estación: {st.obtenerCodigoEstacion()} | Sismógrafo: {st.obtenerNombreSismografo()}"
                );
            }
        }

        // 5. Mostrar opción para modificar datos del evento (simulación: mensaje)
        public void mostrarOpcionModificarDatosEvSismico()
        {
            MessageBox.Show("¿Desea modificar los datos del evento sísmico?", "Modificar datos", MessageBoxButtons.YesNo);
        }

        // 6. Pedir acción con el evento (se lee del comboAccion)
        public string pedirAccionConEvento()
        {
            return comboAccion.SelectedItem?.ToString();
        }

        // 7. Tomar acción con el evento (delegado al gestor)
        public void tomarAccionConEvento()
        {
            string accion = pedirAccionConEvento();
            gestor.tomarAccionConEvento(accion);
            lblMensaje.Text = "Evento actualizado correctamente.";
            lblMensaje.ForeColor = Color.DarkGreen;
            CargarEventos();
            comboAccion.SelectedIndex = 0;
            eventoSeleccionado = null;
        }

        // 8. Tomar opción de modificar datos del evento sísmico (simulación)
        public void tomarOpcionModificarDatosEvSismico()
        {
            // Aquí podrías abrir una nueva ventana de edición, o habilitar campos para edición, etc.
            MessageBox.Show("Funcionalidad de modificar datos pendiente de implementación.", "Info");
        }

        // 9. Tomar opción de visualizar sismógrafo (simulación)
        public void tomarOpcionVisualizarSismografo()
        {
            if (eventoSeleccionado == null)
            {
                MessageBox.Show("Seleccioná un evento primero.", "Advertencia");
                return;
            }
            var series = eventoSeleccionado.getSerieTemporal();
            string msg = "";
            foreach (var st in series)
            {
                msg += $"Estación: {st.obtenerCodigoEstacion()} - Sismógrafo: {st.obtenerNombreSismografo()}\n";
            }
            MessageBox.Show(msg, "Sismógrafos del evento seleccionado");
        }

        // 10. Tomar selección de evento
        public void tomarSeleccionEvento()
        {
            int indice = pedirSeleccionEvento();
            if (indice < 0)
            {
                lblMensaje.Text = "Seleccioná un evento sísmico para ver detalles.";
                lblMensaje.ForeColor = Color.IndianRed;
                return;
            }
            eventoSeleccionado = eventos[indice];
            gestor.tomarSeleccionEvento(eventoSeleccionado);
            mostrarDatosSismicos(eventoSeleccionado);
            lblMensaje.Text = "";
        }

        // 11. Flujo principal: selección de registro de revisión manual
        public void seleccionarRegistroResultadoRevisionManual()
        {
            this.habilitarVentana();
            eventos = gestor.obtenerEventosSismicosAutoDetectados();
            mostrarEventosOrdenados(eventos);
            // El resto del flujo es guiado por los botones (UI event handlers)
        }

        // ==== Handlers de botones ====
        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            tomarSeleccionEvento();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (eventoSeleccionado == null)
            {
                lblMensaje.Text = "Seleccioná un evento y mostrá detalles primero.";
                lblMensaje.ForeColor = Color.IndianRed;
                return;
            }
            if (string.IsNullOrEmpty(pedirAccionConEvento()))
            {
                lblMensaje.Text = "Seleccioná una acción.";
                lblMensaje.ForeColor = Color.IndianRed;
                return;
            }
            tomarAccionConEvento();
        }

        // ==== Método auxiliar ====
        private void CargarEventos()
        {
            eventos = gestor.obtenerEventosSismicosAutoDetectados();
            mostrarEventosOrdenados(eventos);
            txtDetalles.Text = "";
            listBoxSeries.Items.Clear();
            lblMensaje.Text = "";
        }
    }
}
