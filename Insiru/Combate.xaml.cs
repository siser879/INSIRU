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
        public readonly Pokemon pokemon_aliado;
        public readonly Pokemon pokemon_enemigo;

        //Declaración de la seleccion de Shinys del Pokemon aliado y enemigo
        private readonly int shiny_aliado = 0;
        private readonly int shiny_enemigo = 0;

        //Declaración de la vida maxima del Pokemon aliado y enemigo
        private int pokemon_aliado_maxVida;
        private int pokemon_enemigo_maxVida;

        //Declaración de la variable booleana para comprobar si el boton de "Abandonar partida" se muestra o no
        private bool boton_start = false;

        //Declaración de variables públicas para acceder a los controles desde los test
        public Button AtaquePlacaje { get; private set; }
        public Button AtaqueEsquivar { get; private set; }
        public Button AtaqueCurar { get; private set; }

        /// <summary>
        /// Crea una nueva instancia de la ventana de Combate con los pokemon aliado y enemigo especificados.
        /// </summary>
        /// <param name="pokemon_aliado">El pokemon aliado en la batalla.</param>
        /// <param name="pokemon_enemigo">El pokemon enemigo en la batalla.</param>
        /// <param name="shiny1">El valor que indica si el pokemon aliado es shiny o no.</param>
        /// <param name="shiny2">El valor que indica si el pokemon enemigo es shiny o no.</param>
        public Combate(Pokemon pokemon_aliado, Pokemon pokemon_enemigo, int shiny1, int shiny2)
        {
            // Inicializar la ventana de Combate.
            InitializeComponent();

            // Asignar los pokemon aliado y enemigo a las variables de la clase.
            this.pokemon_aliado = pokemon_aliado;
            this.pokemon_enemigo = pokemon_enemigo;

            // Asignar los valores de shiny a las variables de la clase.
            shiny_aliado = shiny1;
            shiny_enemigo = shiny2;

            //Seleccion de un fondo de Combate aleatorio
            Random rnd = new Random();
            int eleccion = rnd.Next(1, 4);

            string rutaNuevaImagen = "/Images/Fondos de Combate/Fondo Combate (" + eleccion + ").png";
            BitmapImage nuevaImagen = new BitmapImage(new Uri(rutaNuevaImagen, UriKind.RelativeOrAbsolute));
            Fondo_Combate.Source = nuevaImagen;

        }

        /// <summary>
        /// Crea una nueva instancia de la ventana de Combate con el ataque de placaje seleccionado por defecto.
        /// </summary>
        public Combate()
        {
            // Inicializar la ventana de Combate.
            InitializeComponent();

            // Seleccionar el ataque de placaje por defecto.
            AtaquePlacaje = Ataque1;

            AtaqueEsquivar = Ataque2;
            AtaqueCurar = Ataque3;

            //Llamada a la clase CombateTests y ejecución de los mismos
            CombateTests combateTests = new CombateTests();
            if (combateTests.Comprobacion_Tests())
            {
                MessageBox.Show("Tests pasados con éxito");
            }
            else
            {
                MessageBox.Show("Tests fallidos");
            }
        }

        /// <summary>
        /// Maneja el evento Loaded del Grid que contiene los elementos visuales de la ventana de Combate. 
        /// Este método carga la información y las imágenes de los pokemon aliado y enemigo, inicializa 
        /// las variables de vida máxima de los pokemon y llama al método ObtenerAtaques para cargar los 
        /// ataques disponibles del pokemon aliado.
        /// </summary>
        /// <param name="sender">El objeto que disparó el evento.</param>
        /// <param name="e">La información del evento.</param>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            // Mostrar el nombre del pokemon aliado y enemigo en la interfaz gráfica.
            Nombre_Aliado.Content = pokemon_aliado.Nombre;
            Nombre_Enemigo.Content = pokemon_enemigo.Nombre;

            // Cargar las imágenes de los pokemon aliado y enemigo en la interfaz gráfica.
            if (shiny_aliado == 0) Pokemon_Aliado.Source = new BitmapImage(new Uri("/Images/Pokemons/Aliado/" + Conector.imagenes_Pokemon(pokemon_aliado.Nombre) + ".png", UriKind.Relative));
            else Pokemon_Aliado.Source = new BitmapImage(new Uri("/Images/Pokemons/Aliado Shiny/" + Conector.imagenes_Pokemon(pokemon_aliado.Nombre) + ".png", UriKind.Relative));

            if (shiny_enemigo == 0) Pokemon_Enemigo.Source = new BitmapImage(new Uri("/Images/Pokemons/Enemigo/" + Conector.imagenes_Pokemon(pokemon_enemigo.Nombre) + ".png", UriKind.Relative));
            else Pokemon_Enemigo.Source = new BitmapImage(new Uri("/Images/Pokemons/Enemigo Shiny/" + Conector.imagenes_Pokemon(pokemon_enemigo.Nombre) + ".png", UriKind.Relative));

            // Inicializar las variables de vida máxima de los pokemon aliado y enemigo.
            pokemon_aliado_maxVida = pokemon_aliado.Vida;
            pokemon_enemigo_maxVida = pokemon_enemigo.Vida;

            // Cargar los ataques disponibles del pokemon aliado.
            ObtenerAtaques();
        }

        /// <summary>
        /// Obtiene los nombres de los ataques y los asigna a los botones correspondientes.
        /// También selecciona el cuarto botón de ataque en función del tipo del pokemon aliado y le asigna un color.
        /// </summary>
        private void ObtenerAtaques()
        {
            // Obtener los nombres de los ataques desde la base de datos.
            ArrayList nombres = Conector.obtener_Ataque();

            // Asignar los nombres de los ataques a los botones correspondientes.
            Ataque1.Content = nombres[0];
            Ataque2.Content = "Esquivar";
            Ataque3.Content = nombres[2];

            // Seleccionar el cuarto ataque en función del tipo del pokemon aliado.
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

            // Asignar el nombre del cuarto ataque y su color correspondiente al botón Ataque4.
            Ataque4.Content = nombre_ataque;
            ObtenerColor(pokemon_aliado.Tipo, Ataque4);
        }


        /// <summary>
        /// Asigna un color de fondo al botón en función del tipo de pokemon.
        /// </summary>
        /// <param name="tipo">El tipo de pokemon.</param>
        /// <param name="boton">El botón al que se le asignará el color.</param>
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


        /// <summary>
        /// Elige aleatoriamente un ataque para el pokemon enemigo.
        /// </summary>
        /// <returns>El número del ataque elegido (entre 1 y 4).</returns>
        private int Elegir_ataque_enemigo()
        {
            Random rnd = new Random();

            int eleccion = rnd.Next(1, 5);

            return eleccion;
        }


        /// <summary>
        /// Muestra un mensaje con el ataque realizado por el pokemon enemigo.
        /// </summary>
        /// <param name="eleccion">El número del ataque elegido por el pokemon enemigo.</param>
        private void Mensaje_ataque_enemigo(int eleccion)
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


        /// <summary>
        /// Realiza una tirada de defensa para determinar si el pokemon aliado esquivará el próximo ataque del enemigo.
        /// </summary>
        /// <returns>Verdadero si la defensa es exitosa, falso si falla.</returns>
        private Boolean Defensa()
        {
            Random random = new Random();

            if (random.Next(0, 10) >= 5) { return true; }
            else { return false; }
        }


        //Placaje
        private void Ataque1_Click(object sender, RoutedEventArgs e)
        {
            // Se muestra un mensaje que indica el nombre del pokemon aliado y el ataque que está utilizando
            MessageBox.Show(pokemon_aliado.Nombre + " uso Placaje");

            // Se elige al azar un ataque para el pokemon enemigo usando la función "elegir_ataque_enemigo"
            int ataque_enemigo = Elegir_ataque_enemigo();

            // Se utiliza una estructura de control switch para ejecutar diferentes acciones basadas en el ataque elegido
            switch (ataque_enemigo)
            {
                case 1:
                    // Si el ataque enemigo es de tipo 1, se ejecuta el siguiente código:
                    // El pokemon aliado ataca al pokemon enemigo usando el método "Ataque1_Metodo"
                    Ataque1_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_enemigo_maxVida, true);
                    // El pokemon enemigo ataca al pokemon aliado usando el método "Ataque1_Metodo"
                    Mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque1_Metodo(Vida_Aliado, pokemon_aliado, pokemon_aliado_maxVida, false);
                    break;

                case 2:
                    // Si el ataque enemigo es de tipo 2, se ejecuta el siguiente código:
                    // El pokemon enemigo utiliza una acción defensiva usando la función "defensa"
                    Mensaje_ataque_enemigo(ataque_enemigo);
                    if (!Defensa())
                    {
                        // Si la función "defensa" devuelve false, el pokemon aliado ataca al pokemon enemigo usando el método "Ataque1_Metodo"
                        MessageBox.Show(pokemon_enemigo.Nombre + " fallo al esquivar!");
                        Ataque1_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_enemigo_maxVida, true);
                    }
                    else
                    {
                        // Si la función "defensa" devuelve true, el pokemon enemigo esquiva el ataque
                        MessageBox.Show("Ataque esquivado por " + pokemon_enemigo.Nombre + "!");
                    }
                    break;

                case 3:
                    // Si el ataque enemigo es de tipo 3, se ejecuta el siguiente código:
                    // El pokemon aliado ataca al pokemon enemigo usando el método "Ataque1_Metodo"
                    Ataque1_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_enemigo_maxVida, true);
                    // El pokemon enemigo se cura usando el método "Ataque3_Metodo"
                    Mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque3_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_aliado, pokemon_enemigo_maxVida, false);
                    break;

                case 4:
                    // Si el ataque enemigo es de tipo 4, se ejecuta el siguiente código:
                    // El pokemon aliado ataca al pokemon enemigo usando el método "Ataque1_Metodo"
                    Ataque1_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_enemigo_maxVida, true);
                    // El pokemon enemigo ataca al pokemon aliado usando el método "Ataque4_Metodo"
                    Mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque4_Metodo(Vida_Aliado, pokemon_aliado, pokemon_enemigo, pokemon_aliado_maxVida, false);
                    break;
            }
        }

        //Esquivar
        private void Ataque2_Click(object sender, RoutedEventArgs e)
        {
            // Muestra un mensaje indicando que el pokemon aliado ha usado Esquivar
            MessageBox.Show(pokemon_aliado.Nombre + " uso Esquivar");

            // Elige un ataque aleatorio para el enemigo y lo almacena en la variable ataque_enemigo
            int ataque_enemigo = Elegir_ataque_enemigo();

            // Realiza diferentes acciones según el ataque del enemigo
            switch (ataque_enemigo)
            {
                case 1:
                    // Si el ataque del enemigo es 1, el enemigo defiende y el pokemon aliado tiene que esquivar
                    // Muestra un mensaje indicando que el enemigo ha elegido defensa
                    Mensaje_ataque_enemigo(ataque_enemigo);

                    // Verifica si el pokemon aliado logró esquivar el ataque del enemigo
                    if (!Defensa())
                    {
                        // Si no logra esquivar, el pokemon aliado pierde vida y muestra un mensaje indicando que falló al esquivar
                        MessageBox.Show(pokemon_aliado.Nombre + " fallo al esquivar!");
                        Ataque1_Metodo(Vida_Aliado, pokemon_aliado, pokemon_aliado_maxVida, false);
                    }
                    else
                    {
                        // Si logra esquivar, muestra un mensaje indicando que el ataque del enemigo fue esquivado
                        MessageBox.Show("Ataque esquivado por " + pokemon_enemigo.Nombre + "!");
                    }
                    break;

                case 2:
                    // Si el ataque del enemigo es 2, ambos el pokemon aliado y el enemigo fallan al esquivar
                    // Muestra un mensaje indicando que el enemigo ha elegido el ataque 2 y que ambos pokemons fallaron al esquivar
                    Mensaje_ataque_enemigo(ataque_enemigo);
                    MessageBox.Show(pokemon_aliado.Nombre + " fallo al esquivar!");
                    MessageBox.Show(pokemon_enemigo.Nombre + " fallo al esquivar!");
                    break;

                case 3:
                    // Si el ataque del enemigo es 3, el enemigo se cura y realiza un ataque curativo en sí mismo
                    // Muestra un mensaje indicando que el enemigo ha elegido el ataque 3 y se cura
                    Mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque3_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_aliado, pokemon_enemigo_maxVida, false);
                    break;

                case 4:
                    // Si el ataque del enemigo es 4, el enemigo ataca y el pokemon aliado tiene que esquivar
                    // Muestra un mensaje indicando que el enemigo ha elegido ataque 4
                    Mensaje_ataque_enemigo(ataque_enemigo);
                    if (!Defensa())
                    {
                        // Si no logra esquivar, el pokemon aliado pierde vida y muestra un mensaje indicando que falló al esquivar
                        MessageBox.Show(pokemon_aliado.Nombre + " fallo al esquivar!");
                        Ataque4_Metodo(Vida_Aliado, pokemon_aliado, pokemon_enemigo, pokemon_aliado_maxVida, false);
                    }
                    else
                    {
                        // Si logra esquivar, muestra un mensaje indicando que el ataque del enemigo fue esquivado
                        MessageBox.Show("Ataque esquivado por " + pokemon_aliado.Nombre + "!");
                    }
                    break;
            }
        }

        //Curar
        private void Ataque3_Click(object sender, RoutedEventArgs e)
        {
            // Mensaje que muestra el nombre del Pokémon aliado y el ataque que usará
            MessageBox.Show(pokemon_aliado.Nombre + " uso Curar");

            // Se elige un ataque aleatorio del Pokémon enemigo
            int ataque_enemigo = Elegir_ataque_enemigo();

            // Se utiliza un switch para realizar acciones diferentes según el ataque del enemigo
            switch (ataque_enemigo)
            {
                case 1:
                    // Se cura el Pokémon aliado y se muestra un mensaje del ataque del enemigo
                    Ataque3_Metodo(Vida_Aliado, pokemon_aliado, pokemon_enemigo, pokemon_aliado_maxVida, true);
                    Mensaje_ataque_enemigo(ataque_enemigo);
                    // Se reduce la vida del aliado con el ataque 1 del enemigo
                    Ataque1_Metodo(Vida_Aliado, pokemon_aliado, pokemon_aliado_maxVida, false);
                    break;

                case 2:
                    // Se cura el Pokémon aliado y se muestra un mensaje de que el enemigo falló al esquivar
                    Ataque3_Metodo(Vida_Aliado, pokemon_aliado, pokemon_enemigo, pokemon_aliado_maxVida, true);
                    Mensaje_ataque_enemigo(ataque_enemigo);
                    MessageBox.Show(pokemon_enemigo.Nombre + " fallo al esquivar!");
                    break;

                case 3:
                    // Se cura el Pokémon aliado, se muestra un mensaje del ataque del enemigo y se cura el enemigo
                    Ataque3_Metodo(Vida_Aliado, pokemon_aliado, pokemon_enemigo, pokemon_aliado_maxVida, true);
                    Mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque3_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_aliado, pokemon_enemigo_maxVida, false);
                    break;

                case 4:
                    // Se cura el Pokémon aliado, se muestra un mensaje del ataque del enemigo y se realiza el ataque 4 del enemigo al aliado
                    Ataque3_Metodo(Vida_Aliado, pokemon_aliado, pokemon_enemigo, pokemon_aliado_maxVida, true);
                    Mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque4_Metodo(Vida_Aliado, pokemon_aliado, pokemon_enemigo, pokemon_aliado_maxVida, false);
                    break;

            }
        }

        //Elemental
        private void Ataque4_Click(object sender, RoutedEventArgs e)
        {
            // Se muestra un mensaje que indica el nombre del pokemon aliado y el ataque que está utilizando
            MessageBox.Show(pokemon_aliado.Nombre + " uso " + Ataque4.Content);

            // Se elige al azar un ataque para el pokemon enemigo usando la función "elegir_ataque_enemigo"
            int ataque_enemigo = Elegir_ataque_enemigo();

            // Se utiliza una estructura de control switch para ejecutar diferentes acciones basadas en el ataque elegido
            switch (ataque_enemigo)
            {
                case 1:
                    // Si el ataque enemigo es de tipo 1, se ejecuta el siguiente código:
                    // El pokemon aliado ataca al pokemon enemigo usando el método "Ataque4_Metodo"
                    Ataque4_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_aliado, pokemon_enemigo_maxVida, true);
                    // El pokemon enemigo ataca al pokemon aliado usando el método "Ataque1_Metodo"
                    Mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque1_Metodo(Vida_Aliado, pokemon_aliado, pokemon_aliado_maxVida, false);
                    break;

                case 2:
                    // Si el ataque enemigo es de tipo 2, se ejecuta el siguiente código:
                    // El pokemon enemigo utiliza una acción defensiva usando la función "defensa"
                    Mensaje_ataque_enemigo(ataque_enemigo);
                    if (!Defensa())
                    {
                        // Si la función "defensa" devuelve false, el pokemon aliado ataca al pokemon enemigo usando el método "Ataque4_Metodo"
                        MessageBox.Show(pokemon_enemigo.Nombre + " fallo al esquivar!");
                        Ataque4_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_aliado, pokemon_enemigo_maxVida, true);
                    }
                    else
                    {
                        // Si la función "defensa" devuelve true, el pokemon enemigo esquiva el ataque
                        MessageBox.Show("Ataque esquivado por " + pokemon_enemigo.Nombre + "!");
                    }
                    break;

                case 3:
                    // Si el ataque enemigo es de tipo 3, se ejecuta el siguiente código:
                    // El pokemon aliado ataca al pokemon enemigo usando el método "Ataque4_Metodo"
                    Ataque4_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_aliado, pokemon_enemigo_maxVida, true);
                    // El pokemon enemigo se cura usando el método "Ataque3_Metodo"
                    Mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque3_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_aliado, pokemon_enemigo_maxVida, false);
                    break;

                case 4:
                    // Si el ataque enemigo es de tipo 4, se ejecuta el siguiente código:
                    // El pokemon aliado ataca al pokemon enemigo usando el método "Ataque4_Metodo"
                    Ataque4_Metodo(Vida_Enemigo, pokemon_enemigo, pokemon_aliado, pokemon_enemigo_maxVida, true);
                    // El pokemon enemigo ataca al pokemon aliado usando el método "Ataque4_Metodo"
                    Mensaje_ataque_enemigo(ataque_enemigo);
                    Ataque4_Metodo(Vida_Aliado, pokemon_aliado, pokemon_enemigo, pokemon_aliado_maxVida, false);
                    break;
            }
        }

        /// <summary>
        /// Este método calcula el daño elemental que un pokemon aliado infligiría a un pokemon enemigo basado en los tipos de ambos.
        /// El parámetro tipo_aliado representa el tipo del pokemon aliado (Fuego, Agua o Planta).
        /// El parámetro tipo_enemigo representa el tipo del pokemon enemigo (Fuego, Agua o Planta).
        /// Devuelve el daño resultante como un número de punto flotante.
        /// </summary>
        private double Danio_Elemental(string tipo_aliado, string tipo_enemigo)
        {
            // Se establece un valor base para el daño elemental.
            double danio_por_tipo = 5;

            // Se comprueba el tipo del pokemon aliado y se ajusta el daño según el tipo del enemigo.
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

        /// <summary>
        /// Este método realiza un ataque de un Pokémon en el campo de batalla.
        /// Toma como argumentos el rectángulo que representa la barra de vida del Pokémon en el campo,
        /// el objeto Pokémon que está atacando, la cantidad máxima de vida que puede tener el Pokémon y 
        /// un indicador booleano de si el atacante ha ganado o perdido.
        /// </summary>
        private void Ataque1_Metodo(Rectangle campo, Pokemon pokemon, int vidaMax, Boolean ganador)
        {
            // Calcula el ancho de la barra de vida en función de la cantidad de vida actual del Pokémon.
            double widthBarra = ((pokemon.Vida - 5) * 127) / vidaMax;

            // Si la barra de vida se reduce a 0 o menos, el Pokémon ha perdido la batalla.
            if (widthBarra <= 0)
            {
                campo.Width = 0;
                pokemon.Vida = 0;

                // Muestra una pantalla de victoria, derrota o rendición dependiendo del valor del indicador "ganador".
                VictoriaDerrotaRendicion pantallafinal = new VictoriaDerrotaRendicion(ganador);
                pantallafinal.Show();
                Close();

            }
            else
            {
                // Si la barra de vida no se ha reducido a 0 o menos, actualiza su ancho y reduce la vida del Pokémon en 5.
                campo.Width = widthBarra;
                pokemon.Vida -= 5;
            }
        }

        /// <summary>
        /// Este método realiza un ataque de curación para un Pokémon en el campo de batalla.
        /// Toma como argumentos el rectángulo que representa la barra de vida del Pokémon en el campo,
        /// el objeto Pokémon que está curando, el objeto Pokémon enemigo, la cantidad máxima de vida que 
        /// puede tener el Pokémon y un indicador booleano de si el curandero es el Pokémon aliado o enemigo.
        /// </summary>
        /// <param name="campo"> el rectangulo de vida del que va a recibir la curacion</param>
        /// <param name="pokemon">el pokemon que va a recibir la curacion</param>
        /// <param name="enemigo">el pokemon que va a recibir la curacion</param>
        /// <param name="vidaMax">Vida maxima del pokemon que se va a curar</param>
        /// <param name="aliado">Saber si quien se va a curar es el aliado o el enemigo para mostrar el mensaje</param>
        private void Ataque3_Metodo(Rectangle campo, Pokemon pokemon, Pokemon enemigo, int vidaMax, Boolean aliado)
        {
            // Calcula el ancho de la barra de vida en función de la cantidad de vida actual del Pokémon más 3 puntos de vida curados.
            double widthBarra = ((pokemon.Vida + 3) * 127) / vidaMax;

            // Si la cantidad de vida del Pokémon ya está al máximo, muestra un mensaje de error.
            if (pokemon.Vida == vidaMax)
            {
                if (aliado)
                {
                    // Muestra un mensaje indicando que el Pokémon aliado no pudo curarse porque su vida ya está al máximo.
                    MessageBox.Show(pokemon.Nombre + " se intentado curar pero no tuvo efecto.");
                }
                else
                {
                    // Muestra un mensaje indicando que el Pokémon enemigo no pudo curarse porque su vida ya está al máximo.
                    MessageBox.Show(enemigo.Nombre + " se intento curar pero no tuvo efecto.");
                }

            }
            else
            {
                // Si la cantidad de vida del Pokémon no está al máximo, cura 3 puntos de vida.
                if (pokemon.Vida + 3 > vidaMax)
                {
                    // Si la cantidad de vida curada lleva la cantidad de vida del Pokémon por encima del máximo, establece la vida en el máximo.
                    campo.Width = 127;
                    pokemon.Vida = vidaMax;
                }
                else
                {
                    // Actualiza la barra de vida con el nuevo ancho y la cantidad de vida del Pokémon con la curación aplicada.
                    campo.Width = widthBarra;
                    pokemon.Vida += 3;
                }
            }
        }


        /// <summary>
        /// Este método realiza el ataque 4 del Pokemon 1 contra el Pokemon 2.
        /// Actualiza la barra de vida del Pokemon 1 y verifica si ha ganado o perdido.
        /// </summary>
        /// <param name="campo">El rectángulo que representa la barra de vida del Pokemon 1.</param>
        /// <param name="pokemon1">El Pokemon 1 que ataca.</param>
        /// <param name="pokemon2">El Pokemon 2 que recibe el ataque.</param>
        /// <param name="vidaMax">El valor máximo de vida posible para un Pokemon.</param>
        /// <param name="ganador">Un valor booleano que indica si se ha determinado un ganador en la batalla.</param>
        private void Ataque4_Metodo(Rectangle campo, Pokemon pokemon1, Pokemon pokemon2, int vidaMax, Boolean ganador)
        {
            // Calcula el ancho de la barra de vida del Pokemon 1.
            double widthBarra = ((pokemon1.Vida - Danio_Elemental(pokemon2.Tipo, pokemon1.Tipo)) * 127) / vidaMax;

            // Verifica si el Pokemon 1 ha perdido toda su vida.
            if (widthBarra <= 0)
            {
                // Si es así, actualiza la barra de vida del Pokemon 1 y su vida a cero.
                campo.Width = 0;
                pokemon1.Vida = 0;

                // Muestra la pantalla final con el resultado de la batalla.
                VictoriaDerrotaRendicion pantallafinal = new VictoriaDerrotaRendicion(ganador);
                pantallafinal.Show();
                Close();
            }
            else
            {
                // Si el Pokemon 1 todavía tiene vida, actualiza la barra de vida y reduce su vida por el daño recibido.
                campo.Width = widthBarra;
                pokemon1.Vida -= Convert.ToInt32(Danio_Elemental(pokemon2.Tipo, pokemon1.Tipo));
            }
        }


        /// <summary>
        /// Este método se llama cuando el jugador hace clic en el botón "Abandonar Partida".
        /// Muestra un mensaje de que el jugador ha abandonado la partida y cierra la ventana actual.
        /// </summary>
        /// <param name="sender">El objeto que llama al método.</param>
        /// <param name="e">Los argumentos del evento.</param>
        private void AbandonarPartida_Click(object sender, RoutedEventArgs e)
        {
            // Muestra un mensaje para informar al jugador de que ha abandonado la partida.
            MessageBox.Show("Has abandonado la partida");

            // Crea una nueva ventana para mostrar el resultado de la partida y la muestra.
            VictoriaDerrotaRendicion pantallafinal = new VictoriaDerrotaRendicion(false);
            pantallafinal.Show();

            // Cierra la ventana actual.
            Close();
        }


        /// <summary>
        /// Este método se llama cuando el usuario hace clic en el botón "Start/Stop".
        /// Cambia la visibilidad del botón "Abandonar Partida" y establece el valor de la variable "boton_start".
        /// </summary>
        /// <param name="sender">El objeto que llama al método.</param>
        /// <param name="e">Los argumentos del evento.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Verifica si el botón "Start/Stop" está en el estado "Start".
            if (!boton_start)
            {
                // Si es así, establece la variable "boton_start" en true y muestra el botón "Abandonar Partida".
                boton_start = true;
                AbandonarPartida.Visibility = Visibility.Visible;
            }
            else
            {
                // Si no, establece la variable "boton_start" en false y oculta el botón "Abandonar Partida".
                boton_start = false;
                AbandonarPartida.Visibility = Visibility.Hidden;
            }
        }

        //Eventos para mostrar mensajes de ayuda al usuario con información de la funcionalidad del elemento dónde esté el ratón

        private void Ataque1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Utilizar ataque Placaje de tú Pokemon";
        }

        private void Ataque1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Combate";
        }

        private void Ataque2_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Úsalo para intenta esquivar el ataque del rival";
        }

        private void Ataque2_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Combate";
        }

        private void Ataque3_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Úsalo para curarte";
        }

        private void Ataque3_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Combate";
        }

        private void Ataque4_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Utilizar ataque Elemental de tú Pokemon";
        }

        private void Ataque4_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Combate";
        }

        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Púlsa para poder abandonar la partida";
        }

        private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Combate";
        }

        private void AbandonarPartida_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Púlsa para huir";
        }

        private void AbandonarPartida_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Combate";
        }
    }
}