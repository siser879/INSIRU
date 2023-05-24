using System.Windows;
using System.Windows.Controls;

namespace Insiru
{
    public class CombateTests
    {

        public bool Comprobacion_Tests()
        {

            if (TestUnitarios_Placaje_BajaVidaRivalEnTresDeVida())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool TestUnitarios_Placaje_BajaVidaRivalEnTresDeVida()
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

        private bool TestUnitarios_Esquivar_EsquivaAtaqueRivalConProbabilidadDeCincuentaPorCiento()
        {

            // Arrange
            Combate combate = new Combate();
            Button ataqueEsquivar = combate.AtaqueEsquivar;
            var click = new RoutedEventArgs(Button.ClickEvent);
            //double vidaEnemigoInicial = combate.pokemon_enemigo.Vida;
            //double vidaEnemigoEsperada = vidaEnemigoInicial - 5;

            // Act
            ataqueEsquivar.RaiseEvent(click);

            // Assert
            //if (vidaEnemigoEsperada == combate.pokemon_enemigo.Vida)
            //{
            //    return true;
            //}

            return false;

        }


    }
}
