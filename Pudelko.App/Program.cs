using P = PudelkoLibrary.Pudelko;
using PudelkoLibrary;
using System;

namespace PudelkoApp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Pudełko toString: ");
            Console.WriteLine(new P(2.5, 9.321, 0.1, UnitOfMeasure.meter).ToString());
            Console.WriteLine(new P(2.5, 9.321, 0.1, UnitOfMeasure.meter).ToString("m"));
            Console.WriteLine(new P(2.5, 9.321, 0.1, UnitOfMeasure.meter).ToString("cm"));
            Console.WriteLine(new P(2.5, 9.321, 0.1, UnitOfMeasure.meter).ToString("mm"));
            P pudelko = new P(4, 8, 9);
            Console.WriteLine($"\nObjetosc pudelka o wymiarach: { pudelko.ToString()} wynosi {pudelko.Objetosc} m^3");
            Console.WriteLine($"Pole pudelka o wymiarach: { pudelko.ToString()} wynosi {pudelko.Pole} m^2");

            P p1 = new P(7, 4, 2);
            P p2 = new P(2, 4, 7);

            if (p1 == p2)
            {
                Console.WriteLine($"\nPudelko {p1.ToString("mm")} jest takie samo jak pudelko {p2.ToString()}");
            }
            Console.WriteLine((p1 + p2).ToString());
        }
    }
}