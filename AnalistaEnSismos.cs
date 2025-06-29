using System;

namespace RedSismicaWinForms
{
    public class AnalistaEnSismos
    {
        private string nombre;
        private string mail;

        public AnalistaEnSismos(string nombre, string mail)
        {
            this.nombre = nombre;
        }
        
        public string getNombre() => nombre;
    }
}
