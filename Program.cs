using System;
using System.Windows.Forms;

namespace RedSismicaWinForms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {

            // Configuraci�n de la aplicaci�n
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Cambi� aqu� para crear un usuario y pasar el gestor:
            Usuario analista = new Usuario("Analista Principal");
            GestorRegistrarResultado gestor = new GestorRegistrarResultado(analista);

            Application.Run(new PantRegistrarResultado(gestor));
        }
    }
}
