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
        public VictoriaDerrotaRendicion()
        {
            InitializeComponent();
        }

        public void cambioTexto(Boolean esVictoria)
        {
            if (esVictoria == true)
            {
                victoriaDerrotaRendicion.Content = "VICTORIA";
            }
            else {
                victoriaDerrotaRendicion.Content = "DERROTA";
            }
        }
    }
}
