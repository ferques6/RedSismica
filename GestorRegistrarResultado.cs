using System;
using System.Collections.Generic;
using System.Linq;

namespace RedSismicaWinForms
{
    public class GestorRegistrarResultado
    {
        private List<EventoSismico> eventosSismicosAutoDetectados;
        private EventoSismico eventoSeleccionado;
        private Usuario usuarioLogueado;

        public GestorRegistrarResultado(Usuario usuario)
        {
            usuarioLogueado = usuario;
            eventosSismicosAutoDetectados = CrearEventosDePrueba();
        }

        public List<EventoSismico> obtenerEventosSismicosAutoDetectados()
        {
            List<EventoSismico> listaAutoDetectados = new List<EventoSismico>();
            foreach (var ev in eventosSismicosAutoDetectados)
            {
                // Verifico si el evento está en estado "Auto Detectado"
                if (ev.esAutoDetectado())
                {
                    listaAutoDetectados.Add(ev);
                }
            }
            // Ordeno la lista por fecha/hora de ocurrencia
            listaAutoDetectados.Sort((a, b) => a.getFechaHoraOcurrencia().CompareTo(b.getFechaHoraOcurrencia()));
            return listaAutoDetectados;
        }


        public void tomarSeleccionEvento(EventoSismico evento)
        {
            eventoSeleccionado = evento;
            eventoSeleccionado.bloquearEventoSismico(usuarioLogueado);
        }

        public void tomarAccionConEvento(string accion)
        {
            if (eventoSeleccionado == null) return;

            if (accion == "Confirmar")
            {
                eventoSeleccionado.setEstado("Confirmado", usuarioLogueado);
            }
            else if (accion == "Rechazar")
            {
                eventoSeleccionado.setEstado("Rechazado", usuarioLogueado);
            }
            else if (accion == "Solicitar revisión a experto")
            {
                eventoSeleccionado.setEstado("Derivado a Experto", usuarioLogueado);
            }
        }

        // Simulación de eventos auto detectados con datos de estación y sismógrafo
        private List<EventoSismico> CrearEventosDePrueba()
        {
            var lista = new List<EventoSismico>();
            var est1 = new EstacionSismologica("EST01", "Córdoba Capital");
            var est2 = new EstacionSismologica("EST02", "Mendoza Sur");
            var est3 = new EstacionSismologica("EST03", "Salta Norte");
            var sis1 = new Sismografo("Sismo-X1");
            var sis2 = new Sismografo("Sismo-Alpha");
            var sis3 = new Sismografo("Sismo-Beta");

            var serie1 = new SerieTemporal(est1, sis1);
            var serie2 = new SerieTemporal(est2, sis2);
            var serie3 = new SerieTemporal(est3, sis3);

            var evento1 = new EventoSismico(DateTime.Now.AddMinutes(-20), 31.4, -64.2, 10.1, 31.3, -64.1, 10.2, 4.5);
            evento1.setEstado("Auto Detectado", usuarioLogueado);
            evento1.agregarSerieTemporal(serie1);

            var evento2 = new EventoSismico(DateTime.Now.AddMinutes(-15), -32.9, -68.8, 12.0, -32.8, -68.7, 11.9, 4.2);
            evento2.setEstado("Auto Detectado", usuarioLogueado);
            evento2.agregarSerieTemporal(serie2);

            var evento3 = new EventoSismico(DateTime.Now.AddMinutes(-10), -24.7, -65.4, 8.7, -24.6, -65.5, 8.9, 4.0);
            evento3.setEstado("Auto Detectado", usuarioLogueado);
            evento3.agregarSerieTemporal(serie3);

            lista.Add(evento1);
            lista.Add(evento2);
            lista.Add(evento3);

            return lista;
        }
    }
}
