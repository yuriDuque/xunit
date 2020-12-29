﻿namespace Alura.LeilaoOnline.Core
{
    public class Lance
    {
        public Interessado Cliente { get; }
        public double Valor { get; }

        public Lance(Interessado cliente, double valor)
        {
            Cliente = cliente;
            Valor = valor;
        }
    }
}
