using System;

namespace RedSismicaWinForms
{
    public class Usuario
    {
        private string contraseņa;
        private string nombre;
        private DateTime fechaAlta;

        public Usuario(string nombre) { this.nombre = nombre; }
        public string getUsuario() => nombre;
    }
}
