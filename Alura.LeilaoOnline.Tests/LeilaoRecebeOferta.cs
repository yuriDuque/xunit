using Xunit;
using Alura.LeilaoOnline.Core;
using System.Linq;
using System.Collections.Generic;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {
        public static IEnumerable<object[]> Data()
        {
            return new List<object[]>
            {
                new object[] { 4, new double[] { 1000, 1200, 1400, 1300 }, new string[] { "Fulano", "Maria", "Jose", "Carla" } },
                new object[] { 2, new double[] { 800, 900 }, new string[] { "Fulano", "Maria" } },
            };
        }

        [Theory, MemberData(nameof(Data))]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(
            int qtdeEsperada, double[] ofertas, string[] nomeInteressados)
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh", new MaiorValor());
            var interessados = new List<Interessado>();

            foreach (var nome in nomeInteressados)
            {
                interessados.Add(new Interessado(nome, leilao));
            }

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var oferta = ofertas[i];
                var interessado = interessados[i];

                leilao.RecebeLance(interessado, oferta);
            }
            leilao.TerminaPregao();

            //Act - método sob teste
            leilao.RecebeLance(interessados.First(), 1000);

            //Assert
            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada, qtdeObtida);
        }

        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimo()
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh", new MaiorValor());
            var fulano = new Interessado("Fulano", leilao);

            leilao.IniciaPregao();

            leilao.RecebeLance(fulano, 800);

            //Act - método sob teste
            leilao.RecebeLance(fulano, 900);

            //Assert
            var qtdeEsperada = 1;
            var qtdeObtida = leilao.Lances.Count();

            Assert.Equal(qtdeEsperada, qtdeObtida);
        }
    }
}
