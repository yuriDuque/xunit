using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, 1250, new double[] {800, 1150, 1400, 1250 }, new string[] { "Fulano", "Maria", "Jose", "Carla" })]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(double valorDestino, double valorEsperado, double[] ofertas, string[] nomeInteressados)
        {
            //Arranje - cenário
            IModalidadeAvaliacao modalidade = new OfertaSuperiorMaisProxima(valorDestino);
            var leilao = new Leilao("Van Gogh", modalidade);
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

        [Theory]
        [InlineData(1200, new double[] { 800, 900, 1000, 1200 }, new string[] { "Fulano", "Maria", "Jose", "Carla" })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 }, new string[] { "Fulano", "Maria", "Jose", "Carla" })]
        [InlineData(800, new double[] { 800 }, new string[] { "Fulano" })]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas, string[] nomeInteressados)
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
            var leilao = new Leilao("Van Gogh", new MaiorValor());
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
            var leilao = new Leilao("Van Gogh", new MaiorValor());

            // Assert
            var execaoObtida = Assert.Throws<InvalidOperationException>(
                //Act - método sob teste
                () => leilao.TerminaPregao()
            );

            string msgEsperada = "Não é possível terminiar o pregão sem que ele tenha começado. Para isso, utilize o método 'IniciaPregao'";
            Assert.Equal(msgEsperada, execaoObtida.Message);
        }
    }
}
