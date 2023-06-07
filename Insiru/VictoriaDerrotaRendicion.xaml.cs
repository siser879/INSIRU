using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Insiru
{
    /// <summary>
    /// Lógica de interacción para VictoriaDerrotaRendicion.xaml
    /// </summary>
    
    // Clase para mostrar si el usuario ganó o perdió y permitir la opción de jugar de nuevo
    public partial class VictoriaDerrotaRendicion : Window
    {
        // Variable para indicar si el usuario ganó o perdió
        readonly Boolean victoria_derrota;

        // Constructor de la clase
        public VictoriaDerrotaRendicion(Boolean esVictoria)
        {
            InitializeComponent();

            // Almacenar el valor de si el usuario ganó o perdió
            victoria_derrota = esVictoria;

            // Llamar al método para cambiar el texto según si el usuario ganó o perdió
            CambioTexto();
        }

        // Método para cambiar el texto en la ventana según si el usuario ganó o perdió
        public void CambioTexto()
        {
            // Si el usuario ganó, cambiar el texto a "¡VICTORIA!"
            if (victoria_derrota == true)
            {
                victoriaDerrotaRendicion.Content = "¡VICTORIA!";
            }
            // Si el usuario perdió, cambiar el texto a "¡DERROTA!"
            else
            {
                victoriaDerrotaRendicion.Content = "¡DERROTA!";
            }
        }

        // Método para manejar el evento de hacer clic en el botón "revancha"
        private void revancha_Click(object sender, RoutedEventArgs e)
        {
            // Crear una nueva instancia de la ventana principal del juego
            MainWindow volverAEmpezar = new MainWindow();
            // Mostrar la nueva ventana
            volverAEmpezar.Show();
            // Cerrar la ventana actual
            Close();
        }
    }

}
