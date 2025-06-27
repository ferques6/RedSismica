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
            CargarEventos();
        }

        private void InicializarControles()
        {
            this.Text = "Registro de revisi�n manual de eventos s�smicos";
            this.Width = 930;
            this.Height = 520;
            this.BackColor = Color.WhiteSmoke;

            Font fuenteTitulo = new Font("Segoe UI", 16, FontStyle.Bold);
            Font fuenteNormal = new Font("Segoe UI", 11);

            // T�tulo principal
            lblTitulo = new Label()
            {
                Text = "Registrar resultado de revisi�n manual",
                Font = fuenteTitulo,
                Left = 20,
                Top = 10,
                Width = 700,
                ForeColor = Color.DarkSlateBlue
            };

            // Subt�tulo eventos
            lblSubEventos = new Label()
            {
                Text = "Eventos s�smicos auto detectados",
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

            // Acci�n
            lblAccion = new Label()
            {
                Text = "Acci�n a realizar sobre el evento:",
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
            comboAccion.Items.AddRange(new string[] { "Confirmar", "Rechazar", "Solicitar revisi�n a experto" });
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

        private void CargarEventos()
        {
            eventos = gestor.obtenerEventosSismicosAutoDetectados();
            listBoxEventos.Items.Clear();
            foreach (var ev in eventos)
            {
                listBoxEventos.Items.Add(ev.getResumenEvento());
            }
            txtDetalles.Text = "";
            listBoxSeries.Items.Clear();
            lblMensaje.Text = "";
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            int indice = listBoxEventos.SelectedIndex;
            if (indice < 0)
            {
                lblMensaje.Text = "Seleccion� un evento s�smico para ver detalles.";
                lblMensaje.ForeColor = Color.IndianRed;
                return;
            }
            eventoSeleccionado = eventos[indice];
            gestor.tomarSeleccionEvento(eventoSeleccionado);

            txtDetalles.Text = eventoSeleccionado.getDatosEventoSismicoCompleto();

            // Mostrar series temporales asociadas
            listBoxSeries.Items.Clear();
            foreach (var st in eventoSeleccionado.getSerieTemporal())
            {
                listBoxSeries.Items.Add($"Estaci�n: {st.obtenerCodigoEstacion()} | Sism�grafo: {st.obtenerNombreSismografo()}");
            }
            lblMensaje.Text = "";
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (eventoSeleccionado == null)
            {
                lblMensaje.Text = "Seleccion� un evento y mostr� detalles primero.";
                lblMensaje.ForeColor = Color.IndianRed;
                return;
            }
            string accion = comboAccion.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(accion))
            {
                lblMensaje.Text = "Seleccion� una acci�n.";
                lblMensaje.ForeColor = Color.IndianRed;
                return;
            }
            gestor.tomarAccionConEvento(accion);
            lblMensaje.Text = "Evento actualizado correctamente.";
            lblMensaje.ForeColor = Color.DarkGreen;
            CargarEventos();
            comboAccion.SelectedIndex = 0;
            eventoSeleccionado = null;
        }
    }
}
