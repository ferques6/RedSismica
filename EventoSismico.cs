using System;
using System.Collections.Generic;
using System.Linq;

namespace RedSismicaWinForms
{
    public class EventoSismico
    {
        private DateTime fechaHoraOcurrencia;
        private Estado estadoActual;
        private List<CambioEstado> cambiosDeEstado = new List<CambioEstado>();
        private List<SerieTemporal> seriesTemporales = new List<SerieTemporal>();
        // Datos para ejemplo
        private double latEpicentro, longEpicentro, profEpicentro;
        private double latHipocentro, longHipocentro, profHipocentro;
        private double magnitud;

        public EventoSismico(DateTime fechaHora, double latE, double longE, double profE, double latH, double longH, double profH, double magnitud)
        {
            this.fechaHoraOcurrencia = fechaHora;
            this.latEpicentro = latE;
            this.longEpicentro = longE;
            this.profEpicentro = profE;
            this.latHipocentro = latH;
            this.longHipocentro = longH;
            this.profHipocentro = profH;
            this.magnitud = magnitud;
        }

        public DateTime getFechaHoraOcurrencia() => fechaHoraOcurrencia;
        public Estado obtenerEstadoActual() => estadoActual;
        public void agregarSerieTemporal(SerieTemporal st) => seriesTemporales.Add(st);
        public List<SerieTemporal> getSerieTemporal() => seriesTemporales;

        public void bloquearEventoSismico(Usuario usuario)
        {
            setEstado("Bloqueado en revisión", usuario);
        }

        public void setEstado(string descripcionNuevoEstado, Usuario usuario)
        {
            if (estadoActual != null)
            {
                var cambioActual = cambiosDeEstado.LastOrDefault();
                cambioActual?.setFechaHoraFin(DateTime.Now);
            }
            Estado nuevoEstado = new Estado(descripcionNuevoEstado);
            estadoActual = nuevoEstado;
            CambioEstado cambio = new CambioEstado(DateTime.Now, null, nuevoEstado, usuario);
            cambiosDeEstado.Add(cambio);
        }

        public string getResumenEvento()
        {
            return $"Fecha/Hora: {fechaHoraOcurrencia:dd/MM HH:mm} - Magnitud: {magnitud} - Estado: {estadoActual?.getDescripcion()}";
        }

        public string getDatosEventoSismicoCompleto()
        {
            return $"Fecha/Hora: {fechaHoraOcurrencia}\r\n" +
                   $"Epicentro: Lat {latEpicentro}, Long {longEpicentro}, Prof {profEpicentro} km\r\n" +
                   $"Hipocentro: Lat {latHipocentro}, Long {longHipocentro}, Prof {profHipocentro} km\r\n" +
                   $"Magnitud: {magnitud}\r\n" +
                   $"Estado actual: {estadoActual?.getDescripcion()}";
        }
    }
}
