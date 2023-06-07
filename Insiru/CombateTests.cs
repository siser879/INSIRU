using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Insiru
{
    public class CombateTests
    {

        public bool Comprobacion_Tests()
        {

            if (TestUnitarios_Placaje_BajaVidaRivalEnCincoDeVida() && TestUnitarios_Curar_SubeVidaRivalEnTresDeVida())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool TestUnitarios_Placaje_BajaVidaRivalEnCincoDeVida()
        {

            // Arrange
            Combate combate = new Combate();
            Button ataquePlacaje = combate.AtaquePlacaje;
            var click = new RoutedEventArgs(Button.ClickEvent);
            double vidaEnemigoInicial = combate.pokemon_enemigo.Vida;
            double vidaEnemigoEsperada = vidaEnemigoInicial - 5;

            // Act
            ataquePlacaje.RaiseEvent(click);

            // Assert
            if (vidaEnemigoEsperada == combate.pokemon_enemigo.Vida)
            {
                return true;
            }

            return false;

        }

        private bool TestUnitarios_Curar_SubeVidaRivalEnTresDeVida()
        {

            // Arrange
            Combate combate = new Combate();
            Button ataqueCurar = combate.AtaqueCurar;
            var click = new RoutedEventArgs(Button.ClickEvent);
            double vidaAliadoInicial = combate.pokemon_aliado.Vida;
            double vidaAliadoEsperada = vidaAliadoInicial + 3;

            // Act
            ataqueCurar.RaiseEvent(click);

            // Assert
            if (vidaAliadoEsperada == combate.pokemon_aliado.Vida)
            {
                return true;
            }

            return false;

        }

    }
}
