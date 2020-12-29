using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        public static IEnumerable<object[]> Data()
        {
            return new List<object[]>
            {
                new object[] { 1200, new double[] { 800, 900, 1000, 1200 }, new string[] { "Fulano", "Maria", "Jose", "Carla" } },
                new object[] { 1000, new double[] { 800, 900, 1000, 990 }, new string[] { "Fulano", "Maria", "Jose", "Carla" } },
                new object[] { 800, new double[] { 800 }, new string[] { "Fulano" } },
            };
        }

        [Theory, MemberData(nameof(Data))]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas, string[] nomeInteressados)
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");
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

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");
            leilao.IniciaPregao();

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");

            // Assert
            Assert.Throws<InvalidOperationException>(
                //Act - método sob teste
                () => leilao.TerminaPregao()
            );
        }
    }
}
