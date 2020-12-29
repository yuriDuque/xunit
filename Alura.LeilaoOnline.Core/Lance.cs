using System;

namespace Alura.LeilaoOnline.Core
{
    public class Lance
    {
        public Interessado Cliente { get; }
        public double Valor { get; }

        public Lance(Interessado cliente, double valor)
        {
            if (valor < 0)
                throw new ArgumentException("Valor do lance deve ser igual ou maior que zero.");

            Cliente = cliente;
            Valor = valor;
        }
    }
}
