namespace RedSismicaWinForms
{
    public class SerieTemporal
    {
        private EstacionSismologica estacion;
        private Sismografo sismografo;
        // En este ejemplo, no usamos muestras detalladas

        public SerieTemporal(EstacionSismologica estacion, Sismografo sismografo)
        {
            this.estacion = estacion;
            this.sismografo = sismografo;
        }

        public string obtenerCodigoEstacion()
        {
            return estacion.obtenerCodigoEstacion();
        }

        public string obtenerNombreSismografo()
        {
            return sismografo.getNombreSismografo();
        }
    }
}
