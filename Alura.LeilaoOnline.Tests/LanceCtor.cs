using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            // Arranje
            var valor = -100;
            var leilao = new Leilao("Van Gogh", new MaiorValor());
            var cliente = new Interessado("Fulado", leilao);

            // Assert
            var execaoObtida = Assert.Throws<ArgumentException>(
                () => new Lance(cliente, valor)
            );

            string msgEsperada = "Valor do lance deve ser igual ou maior que zero.";
            Assert.Equal(msgEsperada, execaoObtida.Message);
        }
    }
}
