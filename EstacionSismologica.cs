namespace RedSismicaWinForms
{
    public class EstacionSismologica
    {
        private string codigoEstacion;
        private string nombreEstacion;

        public EstacionSismologica(string codigo, string nombre)
        {
            this.codigoEstacion = codigo;
            this.nombreEstacion = nombre;
        }

        public string obtenerCodigoEstacion() => codigoEstacion;
        public string obtenerNombreEstacion() => nombreEstacion;
    }
}
