using Insiru;
using System.Windows;
using System.Windows.Controls;

namespace CombateTests
{
    public class TestsUnitarios
    {
        [Fact]
        public void TestUnitarios_Placaje_BajaVidaRivalEnTresDeVida()
        {
            // Arrange
            Combate combate = new Combate();
            Button ataquePlacaje = combate.AtaquePlacaje;
            //var click = new RoutedEventArgs(Button.ClickEvent);
            double vidaEnemigoInicial = combate.pokemon_enemigo.Vida;
            double vidaEnemigoEsperada = vidaEnemigoInicial - 5;

            // Act
            //ataquePlacaje.RaiseEvent(click);

            // Assert
            Assert.Equal(vidaEnemigoEsperada, combate.pokemon_enemigo.Vida);

        }
    }
}