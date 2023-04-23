using System;
using System.Collections;
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
    /// Lógica de interacción para Combate.xaml
    /// </summary>
    public partial class Combate : Window
    {

        private Pokemon pokemon_aliado;
        private Pokemon pokemon_enemigo;

        private int shiny_aliado = 0;
        private int shiny_enemigo = 0;
        private int pokemon_aliado_maxVida;
        private int pokemon_enemigo_maxVida;

        public Combate(Pokemon pokemon_aliado, Pokemon pokemon_enemigo, int shiny1, int shiny2)
        {
            InitializeComponent();

            this.pokemon_aliado = pokemon_aliado;
            this.pokemon_enemigo = pokemon_enemigo;

            shiny_aliado = shiny1;
            shiny_enemigo = shiny2;

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Nombre_Aliado.Content = pokemon_aliado.Nombre;
            Nombre_Enemigo.Content = pokemon_enemigo.Nombre;

            if (shiny_aliado == 0) Pokemon_Aliado.Source = new BitmapImage(new Uri("/Images/Pokemons/Aliado/" + Conector.imagenes_Pokemon(pokemon_aliado.Nombre) + ".png", UriKind.Relative));
            else Pokemon_Aliado.Source = new BitmapImage(new Uri("/Images/Pokemons/Aliado Shiny/" + Conector.imagenes_Pokemon(pokemon_aliado.Nombre) + ".png", UriKind.Relative));

            if (shiny_enemigo == 0) Pokemon_Enemigo.Source = new BitmapImage(new Uri("/Images/Pokemons/Enemigo/" + Conector.imagenes_Pokemon(pokemon_enemigo.Nombre) + ".png", UriKind.Relative));
            else Pokemon_Enemigo.Source = new BitmapImage(new Uri("/Images/Pokemons/Enemigo Shiny/" + Conector.imagenes_Pokemon(pokemon_enemigo.Nombre) + ".png", UriKind.Relative));

            pokemon_aliado_maxVida = pokemon_aliado.Vida;
            pokemon_enemigo_maxVida = pokemon_enemigo.Vida;

            obtenerAtaques();

        }

        private void obtenerAtaques()
        {
            ArrayList nombres = Conector.obtener_Ataque();

            Ataque1.Content = nombres[0];
            Ataque2.Content = nombres[1];
            Ataque3.Content = nombres[2];
            Ataque4.Content = nombres[3];

            obtenerColor(pokemon_aliado.Tipo, Ataque4);

        }

        private void obtenerColor(string tipo, Button boton)
        {
            switch (tipo)
            {
                case "Agua":
                    boton.Background = new SolidColorBrush(Color.FromRgb(26, 53, 255));
                    break;

                case "Fuego":
                    boton.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    break;

                case "Planta":
                    boton.Background = new SolidColorBrush(Color.FromRgb(69, 255, 0));
                    break;
            }
        }

        private void Ataque1_Click(object sender, RoutedEventArgs e)
        {

            double WidthBarraEnemiga = ((pokemon_enemigo.Vida - 5) * 164) / pokemon_enemigo_maxVida;

            if (WidthBarraEnemiga <= 0)
            {
                Vida_Enemigo.Width = 0;
                pokemon_enemigo.Vida = 0;

                //Enviar a la pantalla de victoria

            }
            else
            {
                Vida_Enemigo.Width = WidthBarraEnemiga;
                pokemon_enemigo.Vida -= 5;
            }
            
        }
    }
}
