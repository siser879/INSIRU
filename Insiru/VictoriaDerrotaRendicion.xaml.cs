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
    public partial class VictoriaDerrotaRendicion : Window
    {
        private Boolean victoria_derrota;

        public VictoriaDerrotaRendicion(Boolean esVictoria)
        {
            InitializeComponent();
            
            victoria_derrota= esVictoria;
            cambioTexto();
        }

        public void cambioTexto()
        {
            if (victoria_derrota == true)
            {
                victoriaDerrotaRendicion.Content = "¡VICTORIA!";
            }
            else {
                victoriaDerrotaRendicion.Content = "¡DERROTA!";
            }
        }

        private void revancha_Click(object sender, RoutedEventArgs e)
        {
            MainWindow volverAEmpezar = new MainWindow();
            volverAEmpezar.Show();
            Close();
        }
    }
}
