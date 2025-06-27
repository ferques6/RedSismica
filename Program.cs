using System;
using System.Windows.Forms;

namespace RedSismicaWinForms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {

            // Configuración de la aplicación
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Cambiá aquí para crear un usuario y pasar el gestor:
            Usuario analistaEnSismos = new Usuario("Analista Principal en sismos");
            GestorRegistrarResultado gestor = new GestorRegistrarResultado(analista);

            Application.Run(new PantRegistrarResultado(gestor));
        }
    }
}
