using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Insiru
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string pokemon_aliado_seleccionado = "";
        public string pokemon_enemigo_seleccionado = "";

        private int shiny_aliado = 0;
        private int shiny_enemigo = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Insiru.Insiru_Aliado insiru_Aliado = ((Insiru.Insiru_Aliado)(this.FindResource("insiru_Aliado")));
            // Cargar datos en la tabla Pokemon. Puede modificar este código según sea necesario.
            Insiru.Insiru_AliadoTableAdapters.PokemonTableAdapter insiru_AliadoPokemonTableAdapter = new Insiru.Insiru_AliadoTableAdapters.PokemonTableAdapter();
            insiru_AliadoPokemonTableAdapter.Fill(insiru_Aliado.Pokemon);
            System.Windows.Data.CollectionViewSource pokemonViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pokemonViewSource")));
            pokemonViewSource.View.MoveCurrentToFirst();
            Insiru.Insiru_Enemigo insiru_Enemigo = ((Insiru.Insiru_Enemigo)(this.FindResource("insiru_Enemigo")));
            // Cargar datos en la tabla Pokemon. Puede modificar este código según sea necesario.
            Insiru.Insiru_EnemigoTableAdapters.PokemonTableAdapter insiru_EnemigoPokemonTableAdapter = new Insiru.Insiru_EnemigoTableAdapters.PokemonTableAdapter();
            insiru_EnemigoPokemonTableAdapter.Fill(insiru_Enemigo.Pokemon);
            System.Windows.Data.CollectionViewSource pokemonViewSource1 = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pokemonViewSource1")));
            pokemonViewSource1.View.MoveCurrentToFirst();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            DataRowView vrow = (DataRowView)Pokemon_Aliado.SelectedItem;
            DataRow row = vrow.Row;

            pokemon_aliado_seleccionado = row[1].ToString();
            ArrayList stats = Conector.obtenerStats(pokemon_aliado_seleccionado);
            Pokemon pokemon_aliado = new Pokemon(pokemon_aliado_seleccionado, (string)stats[0], int.Parse((string)stats[1]));

            vrow = (DataRowView)Pokemon_Enemigo.SelectedItem;
            row = vrow.Row;

            pokemon_enemigo_seleccionado = row[1].ToString();
            stats = Conector.obtenerStats(pokemon_enemigo_seleccionado);
            Pokemon pokemon_enemigo = new Pokemon(pokemon_enemigo_seleccionado, (string)stats[0], int.Parse((string)stats[1]));

            if (Shiny_Aliado.IsChecked == true) shiny_aliado = 1;
            if (Shiny_Enemigo.IsChecked == true) shiny_enemigo = 1;

            Combate combate = new Combate(pokemon_aliado, pokemon_enemigo, shiny_aliado, shiny_enemigo);
            combate.Show();

            Close();

        }
    }
}
