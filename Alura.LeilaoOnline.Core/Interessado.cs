namespace Alura.LeilaoOnline.Core
{
    public class Interessado
    {
        public string Nome { get; }
        public Leilao Leilao { get; set; }

        public Interessado(string nome, Leilao leilao)
        {
            Nome = nome;
            Leilao = leilao;
        }

        public Interessado(string nome)
        {
            Nome = nome;
            Leilao = null;
        }
    }
}
