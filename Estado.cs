namespace RedSismicaWinForms
{
    public class Estado
    {
        private string descripcion;

        public Estado(string desc) { descripcion = desc; }
        public string getDescripcion() => descripcion;
        public bool esAutoDetectado() => descripcion == "Auto Detectado";
    }
}
