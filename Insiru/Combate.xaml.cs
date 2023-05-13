using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
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
        //Declaración del Pokemon aliado y enemigo
        private readonly Pokemon pokemon_aliado;
        public readonly Pokemon pokemon_enemigo;

        private readonly int shiny_aliado = 0;
        private readonly int shiny_enemigo = 0;
        private int pokemon_aliado_maxVida;
        private int pokemon_enemigo_maxVida;
        private Boolean turnoJugador;

        //Declaración de variables públicas para acceder a los controles desde los test
        public Button AtaquePlacaje { get; private set; }

        public Combate(Pokemon pokemon_aliado, Pokemon pokemon_enemigo, int shiny1, int shiny2)
        {
            InitializeComponent();

            this.pokemon_aliado = pokemon_aliado;
            this.pokemon_enemigo = pokemon_enemigo;

            shiny_aliado = shiny1;
            shiny_enemigo = shiny2;

        }

        public Combate()
        {
            InitializeComponent();
            AtaquePlacaje = Ataque1;
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

            ObtenerAtaques();

        }

        private void ObtenerAtaques()
        {
            ArrayList nombres = Conector.obtener_Ataque();

            //PRUEBA DE CAMBIO

            Ataque1.Content = nombres[0];
            Ataque2.Content = "Esquivar";
            Ataque3.Content = nombres[2];

            string nombre_ataque = "";
            if (pokemon_aliado.Tipo == "Fuego")
            {
                nombre_ataque = "Lanzallamas";
            }
            else if (pokemon_aliado.Tipo == "Agua")
            {
                nombre_ataque = "Pistola Agua";
            }
            else if (pokemon_aliado.Tipo == "Planta")
            {
                nombre_ataque = "Hoja Afilada";
            };

            Ataque4.Content = nombre_ataque;

            ObtenerColor(pokemon_aliado.Tipo, Ataque4);

        }

        private void ObtenerColor(string tipo, Button boton)
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

        //Turnos
        private void Turno()
        {
            //if (turnoJugador == true)
            //{
            //    MessageBox.Show("Es turno del jugador");
            //    turnoJugador = false;
            //}
            //else {
            //    MessageBox.Show("Es turno de la maquina");


            //    turnoJugador = true;
            //}

            MessageBox.Show("Es turno de la maquina");

            Random rnd = new Random();

            int eleccion = rnd.Next(1, 5);

            if (eleccion == 1) { Ataque1_Metodo(Vida_Aliado, pokemon_aliado, pokemon_aliado_maxVida, false); }
            else if (eleccion == 2) { Ataque2_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_enemigo_maxVida, false); }
            else if (eleccion == 3) { Ataque3_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_enemigo_maxVida, false); }
            else if (eleccion == 4) { Ataque4_Metodo(Vida_Aliado, pokemon_aliado, pokemon_enemigo, pokemon_aliado_maxVida, false); }

            MessageBox.Show("Es turno del jugador");

        }

        //Placaje
        private void Ataque1_Click(object sender, RoutedEventArgs e)
        {
            Ataque1_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_enemigo_maxVida, true);
        }

        //Esquivar
        private void Ataque2_Click(object sender, RoutedEventArgs e)
        {
            Ataque2_Metodo(Vida_Aliado, pokemon_aliado, pokemon_aliado_maxVida, true);
        }

        //Curar
        private void Ataque3_Click(object sender, RoutedEventArgs e)
        {
            Ataque3_Metodo(Vida_Aliado, pokemon_aliado, pokemon_aliado_maxVida, true);
        }

        //Elemental
        private void Ataque4_Click(object sender, RoutedEventArgs e)
        {
            Ataque4_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_aliado, pokemon_enemigo_maxVida, true);
        }

        private double Danio_Elemental(string tipo)
        {
            double danio_por_tipo = 5;

            if (pokemon_aliado.Tipo == "Fuego")
            {
                if (pokemon_enemigo.Tipo == "Fuego")
                {
                    return danio_por_tipo;
                }
                else if (pokemon_enemigo.Tipo == "Agua")
                {
                    return danio_por_tipo *= 0.5;
                }
                else
                {
                    return danio_por_tipo *= 2;
                }
            }
            else if (pokemon_aliado.Tipo == "Agua")
            {
                if (pokemon_enemigo.Tipo == "Fuego")
                {
                    return danio_por_tipo *= 2;
                }
                else if (pokemon_enemigo.Tipo == "Agua")
                {
                    return danio_por_tipo;
                }
                else
                {
                    return danio_por_tipo *= 0.5;
                }
            }
            else
            {
                if (pokemon_enemigo.Tipo == "Fuego")
                {
                    return danio_por_tipo *= 0.5;
                }
                else if (pokemon_enemigo.Tipo == "Agua")
                {
                    return danio_por_tipo *= 2;
                }
                else
                {
                    return danio_por_tipo;
                }
            }
        }

        private void Ataque1_Metodo(Rectangle campo, Pokemon pokemon, int vidaMax, Boolean turnoMaquina)
        {
            double widthBarra = ((pokemon.Vida - 5) * 164) / vidaMax;

            if (widthBarra <= 0)
            {
                campo.Width = 0;
                pokemon.Vida = 0;

                //Enviar a la pantalla de victoria

            }
            else
            {
                campo.Width = widthBarra;
                pokemon.Vida -= 5;
                if (turnoMaquina == true)
                {
                    Turno();
                }
            }
        }

        private void Ataque2_Metodo(Rectangle campo, Pokemon pokemon, int vidaMax, Boolean turnoMaquina)
        {
            double widthBarra = ((pokemon.Vida - 5) * 164) / vidaMax;

            Random random = new Random();
            int numeroAleatorio = random.Next(0, 10);

            if (numeroAleatorio >= 0)
            {
                MessageBox.Show("Ataque esquivado");
            }
            else
            {

                if (turnoMaquina == true)
                {
                    Turno();
                }
            }
        }

        private void Ataque3_Metodo(Rectangle campo, Pokemon pokemon, int vidaMax, Boolean turnoMaquina)
        {
            double widthBarra = ((pokemon.Vida + 3) * 164) / vidaMax;

            if (widthBarra <= 0)
            {
                campo.Width = 0;
                pokemon.Vida = 0;

                //Enviar a la pantalla de derrota

            }
            else if (widthBarra >= 164)
            {

                //Mostrar mensaje - Ya tienes la vida al máximo
                MessageBox.Show("Ya tienes la vida al máximo");

            }
            else
            {
                campo.Width = widthBarra;
                pokemon.Vida += 3;
                if (turnoMaquina == true)
                {
                    Turno();
                }
            }
        }

        private void Ataque4_Metodo(Rectangle campo, Pokemon pokemon1, Pokemon pokemon2, int vidaMax, Boolean turnoMaquina)
        {
            double widthBarra = ((pokemon1.Vida - Danio_Elemental(pokemon2.Tipo)) * 164) / vidaMax;

            if (widthBarra <= 0)
            {
                campo.Width = 0;
                pokemon1.Vida = 0;

                //Enviar a la pantalla de victoria

            }
            else
            {
                campo.Width = widthBarra;
                pokemon1.Vida -= Convert.ToInt32(Danio_Elemental(pokemon2.Tipo));
                if (turnoMaquina == true)
                {
                    Turno();
                }
            }
        }

        private void AbandonarPartida_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Has abandonado la partida");
            Close();
        }
    }
}