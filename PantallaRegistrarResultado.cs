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
            // ... (todo el código de setup visual, igual que tu versión anterior) ...
            // OMITIDO para brevedad, copiá el bloque tal como lo tenías arriba
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
