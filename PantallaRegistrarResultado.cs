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
            // ... (todo el c�digo de setup visual, igual que tu versi�n anterior) ...
            // OMITIDO para brevedad, copi� el bloque tal como lo ten�as arriba
        }

        // === M�todos requeridos por el diagrama de clases/secuencia ===

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

        // 3. Pedir selecci�n de evento al usuario (simulado en GUI por selecci�n en ListBox)
        public int pedirSeleccionEvento()
        {
            return listBoxEventos.SelectedIndex;
        }

        // 4. Mostrar datos s�smicos del evento seleccionado
        public void mostrarDatosSismicos(EventoSismico evento)
        {
            txtDetalles.Text = evento.getDatosEventoSismicoCompleto();

            listBoxSeries.Items.Clear();
            foreach (var st in evento.getSerieTemporal())
            {
                listBoxSeries.Items.Add(
                    $"Estaci�n: {st.obtenerCodigoEstacion()} | Sism�grafo: {st.obtenerNombreSismografo()}"
                );
            }
        }

        // 5. Mostrar opci�n para modificar datos del evento (simulaci�n: mensaje)
        public void mostrarOpcionModificarDatosEvSismico()
        {
            MessageBox.Show("�Desea modificar los datos del evento s�smico?", "Modificar datos", MessageBoxButtons.YesNo);
        }

        // 6. Pedir acci�n con el evento (se lee del comboAccion)
        public string pedirAccionConEvento()
        {
            return comboAccion.SelectedItem?.ToString();
        }

        // 7. Tomar acci�n con el evento (delegado al gestor)
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

        // 8. Tomar opci�n de modificar datos del evento s�smico (simulaci�n)
        public void tomarOpcionModificarDatosEvSismico()
        {
            // Aqu� podr�as abrir una nueva ventana de edici�n, o habilitar campos para edici�n, etc.
            MessageBox.Show("Funcionalidad de modificar datos pendiente de implementaci�n.", "Info");
        }

        // 9. Tomar opci�n de visualizar sism�grafo (simulaci�n)
        public void tomarOpcionVisualizarSismografo()
        {
            if (eventoSeleccionado == null)
            {
                MessageBox.Show("Seleccion� un evento primero.", "Advertencia");
                return;
            }
            var series = eventoSeleccionado.getSerieTemporal();
            string msg = "";
            foreach (var st in series)
            {
                msg += $"Estaci�n: {st.obtenerCodigoEstacion()} - Sism�grafo: {st.obtenerNombreSismografo()}\n";
            }
            MessageBox.Show(msg, "Sism�grafos del evento seleccionado");
        }

        // 10. Tomar selecci�n de evento
        public void tomarSeleccionEvento()
        {
            int indice = pedirSeleccionEvento();
            if (indice < 0)
            {
                lblMensaje.Text = "Seleccion� un evento s�smico para ver detalles.";
                lblMensaje.ForeColor = Color.IndianRed;
                return;
            }
            eventoSeleccionado = eventos[indice];
            gestor.tomarSeleccionEvento(eventoSeleccionado);
            mostrarDatosSismicos(eventoSeleccionado);
            lblMensaje.Text = "";
        }

        // 11. Flujo principal: selecci�n de registro de revisi�n manual
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
                lblMensaje.Text = "Seleccion� un evento y mostr� detalles primero.";
                lblMensaje.ForeColor = Color.IndianRed;
                return;
            }
            if (string.IsNullOrEmpty(pedirAccionConEvento()))
            {
                lblMensaje.Text = "Seleccion� una acci�n.";
                lblMensaje.ForeColor = Color.IndianRed;
                return;
            }
            tomarAccionConEvento();
        }

        // ==== M�todo auxiliar ====
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
