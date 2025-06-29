using System;

namespace RedSismicaWinForms
{
    public class Sesion
    {
        private DateTime fechaHoraInicio;
        private DateTime? fechaHoraFin;
        private Usuario usuario;

        public Sesion(DateTime inicio, Usuario usuario)
        {
            this.fechaHoraInicio = inicio;
            this.usuario = usuario;
        }
        public Usuario getUsuarioLogueado() => usuario;
    }
}
