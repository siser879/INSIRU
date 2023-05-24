using System.Collections;
using System.Data;
using System.Windows;

namespace Insiru
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Este método se llama cuando se carga la ventana principal. 
        /// Carga los datos en las tablas Pokemon de los objetos Insiru_Aliado e Insiru_Enemigo, y mueve las vistas actuales a la primera posición.
        /// </summary>
        /// <param name="sender">El objeto que llama al método.</param>
        /// <param name="e">Los argumentos del evento.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Obtiene el objeto Insiru_Aliado y carga los datos en la tabla Pokemon utilizando el adaptador de tabla Pokemon.
            Insiru.Insiru_Aliado insiru_Aliado = ((Insiru.Insiru_Aliado)(this.FindResource("insiru_Aliado")));
            Insiru.Insiru_AliadoTableAdapters.PokemonTableAdapter insiru_AliadoPokemonTableAdapter = new Insiru.Insiru_AliadoTableAdapters.PokemonTableAdapter();
            insiru_AliadoPokemonTableAdapter.Fill(insiru_Aliado.Pokemon);

            // Obtiene la vista de origen del CollectionViewSource de la tabla Pokemon y mueve la vista actual a la primera posición.
            System.Windows.Data.CollectionViewSource pokemonViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pokemonViewSource")));
            pokemonViewSource.View.MoveCurrentToFirst();

            // Obtiene el objeto Insiru_Enemigo y carga los datos en la tabla Pokemon utilizando el adaptador de tabla Pokemon.
            Insiru.Insiru_Enemigo insiru_Enemigo = ((Insiru.Insiru_Enemigo)(this.FindResource("insiru_Enemigo")));
            Insiru.Insiru_EnemigoTableAdapters.PokemonTableAdapter insiru_EnemigoPokemonTableAdapter = new Insiru.Insiru_EnemigoTableAdapters.PokemonTableAdapter();
            insiru_EnemigoPokemonTableAdapter.Fill(insiru_Enemigo.Pokemon);

            // Obtiene la vista de origen del CollectionViewSource de la tabla Pokemon y mueve la vista actual a la primera posición.
            System.Windows.Data.CollectionViewSource pokemonViewSource1 = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pokemonViewSource1")));
            pokemonViewSource1.View.MoveCurrentToFirst();
        }


        /// <summary>
        /// Maneja el evento clic del botón "Empezar partida". Selecciona un Pokemon para el jugador y otro para el enemigo y comienza el combate.
        /// </summary>
        /// <param name="sender">Objeto que desencadenó el evento</param>
        /// <param name="e">Argumentos del evento</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int shiny_aliado = 0;
            int shiny_enemigo = 0;

            // Obtener el Pokemon seleccionado por el jugador y crear un objeto Pokemon con sus características.
            DataRowView vrow = (DataRowView)Pokemon_Aliado.SelectedItem;
            DataRow row = vrow.Row;

            string pokemon_aliado_seleccionado = row[1].ToString();
            ArrayList stats = Conector.obtenerStats(pokemon_aliado_seleccionado);
            Pokemon pokemon_aliado = new Pokemon(pokemon_aliado_seleccionado, (string)stats[0], int.Parse((string)stats[1]));

            // Obtener el Pokemon seleccionado por el enemigo y crear un objeto Pokemon con sus características.
            vrow = (DataRowView)Pokemon_Enemigo.SelectedItem;
            row = vrow.Row;

            string pokemon_enemigo_seleccionado = row[1].ToString();
            stats = Conector.obtenerStats(pokemon_enemigo_seleccionado);
            Pokemon pokemon_enemigo = new Pokemon(pokemon_enemigo_seleccionado, (string)stats[0], int.Parse((string)stats[1]));

            // Establecer las variables shiny_aliado y shiny_enemigo según si se han seleccionado Pokemon brillantes o no.
            if (Shiny_Aliado.IsChecked == true) shiny_aliado = 1;
            if (Shiny_Enemigo.IsChecked == true) shiny_enemigo = 1;

            // Iniciar el combate con los dos Pokemon seleccionados y las variables shiny_aliado y shiny_enemigo.
            Combate combate = new Combate(pokemon_aliado, pokemon_enemigo, shiny_aliado, shiny_enemigo);
            combate.Show();

            Close();

        }

        //Eventos para mostrar mensajes de ayuda al usuario con información de la funcionalidad del elemento dónde esté el ratón

        private void Shiny_Aliado_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Activar shiny del Pokemon aliado";
        }

        private void Shiny_Aliado_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Selector de Pokemon";
        }

        private void Shiny_Enemigo_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Activar shiny del Pokemon rival";
        }

        private void Shiny_Enemigo_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Selector de Pokemon";
        }

        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Ir al combate";
        }

        private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Selector de Pokemon";
        }

        private void Pokemon_Aliado_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Elige qué Pokemon quieres";
        }

        private void Pokemon_Aliado_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Selector de Pokemon";
        }

        private void Pokemon_Enemigo_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Elige el Pokemon del enemigos";
        }

        private void Pokemon_Enemigo_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = "Selector de Pokemon";
        }
    }
}