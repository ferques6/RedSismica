namespace RedSismicaWinForms
{
    public class Sismografo
    {
        private string nombreSismografo;

        public Sismografo(string nombre)
        {
            this.nombreSismografo = nombre;
        }

        public string getNombreSismografo() => nombreSismografo;
    }
}
