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

        private bool boton_start = false;

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
        private int elegir_ataque_enemigo()
        {
            Random rnd = new Random();

            int eleccion = rnd.Next(1, 5);

            return eleccion;
        }

        private void mensaje_ataque_enemigo(int eleccion)
        {
            string nombre_ataque = "";

            if (pokemon_enemigo.Tipo == "Fuego")
            {
                nombre_ataque = "Lanzallamas";
            }
            else if (pokemon_enemigo.Tipo == "Agua")
            {
                nombre_ataque = "Pistola Agua";
            }
            else if (pokemon_enemigo.Tipo == "Planta")
            {
                nombre_ataque = "Hoja Afilada";
            };

            if (eleccion == 1) { MessageBox.Show(pokemon_enemigo.Nombre + " uso Placaje"); }
            else if (eleccion == 2) { MessageBox.Show(pokemon_enemigo.Nombre + " uso Esquivar"); }
            else if (eleccion == 3) { MessageBox.Show(pokemon_enemigo.Nombre + " uso Curar"); }
            else { MessageBox.Show(pokemon_enemigo.Nombre + " uso " + nombre_ataque); }
        }

        private Boolean defensa()
        {
            Random random = new Random();

            if (random.Next(0, 10) >= 5) { return true; }
            else { return false; }
        }

        //Placaje
        private void Ataque1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(pokemon_aliado.Nombre + " uso Placaje");

            int ataque_enemigo = elegir_ataque_enemigo();

            switch (ataque_enemigo)
            {
                case 1:
                    //ALIADO BAJA VIDA ENEMIGO
                    Ataque1_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_enemigo_maxVida, true);
                    //ENEMIGO BAJA VIDA ALIADO
                    mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque1_Metodo(Vida_Aliado, pokemon_aliado, pokemon_aliado_maxVida, false);
                    break;

                case 2:
                    //DEFENSA DEL ENEMIGO
                    mensaje_ataque_enemigo(ataque_enemigo);
                    if (!defensa())
                    {
                        //ALIADO BAJA VIDA ENEMIGO
                        MessageBox.Show(pokemon_enemigo.Nombre + " fallo al esquivar!");
                        Ataque1_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_enemigo_maxVida, true);
                    }
                    else
                    {
                        //ENEMIGO ESQUIVA CORRECTAMENTE
                        MessageBox.Show("Ataque esquivado por " + pokemon_enemigo.Nombre + "!");
                    }
                    break;

                case 3:
                    //ALIADO BAJA VIDA ENEMIGO
                    Ataque1_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_enemigo_maxVida, true);
                    //ENEMIGO SE CURA
                    mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque3_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_aliado, pokemon_enemigo_maxVida, false);
                    break;

                case 4:
                    //ALIADO BAJA VIDA ENEMIGO
                    Ataque1_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_enemigo_maxVida, true);
                    //ENEMIGO ELEMENTAL A ALIADO
                    mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque4_Metodo(Vida_Aliado, pokemon_aliado, pokemon_enemigo, pokemon_aliado_maxVida, false);
                    break;
            }
        }

        //Esquivar
        private void Ataque2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(pokemon_aliado.Nombre + " uso Esquivar");

            int ataque_enemigo = elegir_ataque_enemigo();

            switch (ataque_enemigo)
            {
                case 1:
                    //DEFENSA ALIADO
                    mensaje_ataque_enemigo(ataque_enemigo);
                    if (!defensa())
                    {
                        //ENEMIGO BAJA VIDA ALIADO
                        MessageBox.Show(pokemon_aliado.Nombre + " fallo al esquivar!");
                        Ataque1_Metodo(Vida_Aliado, pokemon_aliado, pokemon_aliado_maxVida, false);
                    }
                    else
                    {
                        //ALIADO ESQUIVA CORRECTAMENTE
                        MessageBox.Show("Ataque esquivado por " + pokemon_enemigo.Nombre + "!");
                    }
                    break;

                case 2:
                    mensaje_ataque_enemigo(ataque_enemigo);
                    MessageBox.Show(pokemon_aliado.Nombre + " fallo al esquivar!");
                    MessageBox.Show(pokemon_enemigo.Nombre + " fallo al esquivar!");
                    break;

                case 3:
                    //ENEMIGO SE CURA
                    mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque3_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_aliado, pokemon_enemigo_maxVida, false);
                    break;

                case 4:
                    //DEFENSA ALIADO
                    mensaje_ataque_enemigo(ataque_enemigo);
                    if (!defensa())
                    {
                        //ENEMIGO BAJA VIDA ALIADO
                        MessageBox.Show(pokemon_aliado.Nombre + " fallo al esquivar!");
                        Ataque4_Metodo(Vida_Aliado, pokemon_aliado, pokemon_enemigo, pokemon_aliado_maxVida, false);
                    }
                    else
                    {
                        //ALIADO ESQUIVA CORRECTAMENTE
                        MessageBox.Show("Ataque esquivado por " + pokemon_aliado.Nombre + "!");
                    }
                    break;
            }
        }

        //Curar
        private void Ataque3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(pokemon_aliado.Nombre + " uso Curar");

            int ataque_enemigo = elegir_ataque_enemigo();

            switch (ataque_enemigo)
            {
                case 1:
                    //ALIADO SE CURA
                    Ataque3_Metodo(Vida_Aliado, pokemon_aliado, pokemon_enemigo, pokemon_aliado_maxVida, true);
                    //ENEMIGO BAJA VIDA ALIADO
                    mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque1_Metodo(Vida_Aliado, pokemon_aliado, pokemon_aliado_maxVida, false);
                    break;

                case 2:
                    //ALIADO SE CURA
                    Ataque3_Metodo(Vida_Aliado, pokemon_aliado, pokemon_enemigo, pokemon_aliado_maxVida, true);
                    mensaje_ataque_enemigo(ataque_enemigo);
                    MessageBox.Show(pokemon_enemigo.Nombre + " fallo al esquivar!");
                    break;

                case 3:
                    //ALIADO SE CURA
                    Ataque3_Metodo(Vida_Aliado, pokemon_aliado, pokemon_enemigo, pokemon_aliado_maxVida, true);
                    //ENEMIGO SE CURA
                    mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque3_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_aliado, pokemon_enemigo_maxVida, false);
                    break;

                case 4:
                    //ALIADO SE CURA
                    Ataque3_Metodo(Vida_Aliado, pokemon_aliado, pokemon_enemigo, pokemon_aliado_maxVida, true);
                    //ENEMIGO ELEMENTAL A ALIADO
                    mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque4_Metodo(Vida_Aliado, pokemon_aliado, pokemon_enemigo, pokemon_aliado_maxVida, false);
                    break;

            }
        }

        //Elemental
        private void Ataque4_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(pokemon_aliado.Nombre + " uso " + Ataque4.Content);

            int ataque_enemigo = elegir_ataque_enemigo();

            switch (ataque_enemigo)
            {
                case 1:
                    //ALIADO ELEMENTAL A ENEMIGO
                    Ataque4_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_aliado, pokemon_enemigo_maxVida, true);
                    //ENEMIGO BAJA VIDA ALIADO
                    mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque1_Metodo(Vida_Aliado, pokemon_aliado, pokemon_aliado_maxVida, false);
                    break;

                case 2:
                    //DEFENSA DEL ENEMIGO
                    mensaje_ataque_enemigo(ataque_enemigo);
                    if (!defensa())
                    {
                        //ALIADO BAJA VIDA ENEMIGO
                        MessageBox.Show(pokemon_enemigo.Nombre + " fallo al esquivar!");
                        Ataque4_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_aliado, pokemon_enemigo_maxVida, true);
                    }
                    else
                    {
                        //ENEMIGO ESQUIVA CORRECTAMENTE
                        MessageBox.Show("Ataque esquivado por " + pokemon_enemigo.Nombre + "!");
                    }
                    break;

                case 3:
                    //ALIADO ELEMENTAL A ENEMIGO
                    Ataque4_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_aliado, pokemon_enemigo_maxVida, true);
                    //ENEMIGO SE CURA
                    mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque3_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_aliado, pokemon_enemigo_maxVida, false);
                    break;

                case 4:
                    //ALIADO ELEMENTAL A ENEMIGO
                    Ataque4_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_aliado, pokemon_enemigo_maxVida, true);
                    //ENEMIGO ELEMENTAL A ALIADO
                    mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque4_Metodo(Vida_Aliado, pokemon_aliado, pokemon_enemigo, pokemon_aliado_maxVida, false);
                    break;
            }
        }

        private double Danio_Elemental(string tipo_aliado, string tipo_enemigo)
        {
            double danio_por_tipo = 5;

            if (tipo_aliado == "Fuego")
            {
                if (tipo_enemigo == "Fuego")
                {
                    return danio_por_tipo;
                }
                else if (tipo_enemigo == "Agua")
                {
                    return danio_por_tipo *= 0.5;
                }
                else
                {
                    return danio_por_tipo *= 2;
                }
            }
            else if (tipo_aliado == "Agua")
            {
                if (tipo_enemigo == "Fuego")
                {
                    return danio_por_tipo *= 2;
                }
                else if (tipo_enemigo == "Agua")
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
                if (tipo_enemigo == "Fuego")
                {
                    return danio_por_tipo *= 0.5;
                }
                else if (tipo_enemigo == "Agua")
                {
                    return danio_por_tipo *= 2;
                }
                else
                {
                    return danio_por_tipo;
                }
            }
        }

        private void Ataque1_Metodo(Rectangle campo, Pokemon pokemon, int vidaMax, Boolean ganador)
        {

            double widthBarra = ((pokemon.Vida - 5) * 127) / vidaMax;

            if (widthBarra <= 0)
            {
                campo.Width = 0;
                pokemon.Vida = 0;

                VictoriaDerrotaRendicion pantallafinal = new VictoriaDerrotaRendicion(ganador);
                pantallafinal.Show();
                Close();

            }
            else
            {
                campo.Width = widthBarra;
                pokemon.Vida -= 5;
            }
        }

        private void Ataque3_Metodo(Rectangle campo, Pokemon pokemon, Pokemon enemigo, int vidaMax, Boolean aliado)
        {
            double widthBarra = ((pokemon.Vida + 3) * 127) / vidaMax;

            if (pokemon.Vida == vidaMax)
            {
                if (aliado)
                {
                    //Mostrar mensaje - Ya tienes la vida al máximo
                    MessageBox.Show(pokemon.Nombre + " se intentado curar pero no tuvo efecto.");
                }
                else
                {
                    //Mostrar mensaje - Ya tienes la vida al máximo
                    MessageBox.Show(enemigo.Nombre + " se intento curar pero no tuvo efecto.");
                }

            }
            else
            {
                if (pokemon.Vida + 3 > vidaMax)
                {
                    campo.Width = 127;
                    pokemon.Vida = vidaMax;
                }
                else
                {
                    campo.Width = widthBarra;
                    pokemon.Vida += 3;
                }
            }
        }

        private void Ataque4_Metodo(Rectangle campo, Pokemon pokemon1, Pokemon pokemon2, int vidaMax, Boolean ganador)
        {

            double widthBarra = ((pokemon1.Vida - Danio_Elemental(pokemon2.Tipo, pokemon1.Tipo)) * 127) / vidaMax;

            if (widthBarra <= 0)
            {
                campo.Width = 0;
                pokemon1.Vida = 0;

                VictoriaDerrotaRendicion pantallafinal = new VictoriaDerrotaRendicion(ganador);
                pantallafinal.Show();
                Close();

            }
            else
            {
                campo.Width = widthBarra;
                pokemon1.Vida -= Convert.ToInt32(Danio_Elemental(pokemon2.Tipo, pokemon1.Tipo));
            }
        }

        private void AbandonarPartida_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Has abandonado la partida");
            VictoriaDerrotaRendicion pantallafinal = new VictoriaDerrotaRendicion(false);
            pantallafinal.Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!boton_start)
            {
                boton_start = true;
                AbandonarPartida.Visibility = Visibility.Visible;
            }
            else
            {
                boton_start = false;
                AbandonarPartida.Visibility = Visibility.Hidden;
            }
        }
    }
}