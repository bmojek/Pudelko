using P = PudelkoLibrary.Pudelko;
using PudelkoLibrary;
using System;
using System.Collections.Generic;

namespace PudelkoApp
{
    public class Program
    {
        public static int ComparePudelka(P p1, P p2)
        {
            if (p1.Objetosc < p2.Objetosc) return -1;
            else if (p1.Objetosc == p2.Objetosc && p1.Pole < p2.Pole) return -1;
            else if (p1.Pole == p2.Pole && p1.A + p1.B + p1.C < p2.A + p2.B + p2.C) return -1;
            else return 1;
        }

        private static void Main(string[] args)
        {
            List<P> pudelka = new List<P>();
            P p1 = new P(1000, 2000, 3000, UnitOfMeasure.milimeter);
            P p2 = new P(1.4, 2, 3.45);
            P p6 = new P(1.4, 2, 6);
            P p3 = new P(2.5, 9.321, 0.1, UnitOfMeasure.meter);
            P p5 = new P(500, 345, 100, UnitOfMeasure.centimeter);
            P p4 = new P(50, 40.2, 7, UnitOfMeasure.centimeter);

            pudelka.Add(p1);
            pudelka.Add(p2);
            pudelka.Add(p3);
            pudelka.Add(p4);
            pudelka.Add(p5);
            pudelka.Add(p6);

            foreach (var x in pudelka)
            {
                Console.WriteLine(x.ToString());
            }
            pudelka.Sort(ComparePudelka);
            Console.WriteLine("------------------------");
            foreach (var x in pudelka)
            {
                Console.WriteLine($"{x.ToString()} objetosc: {x.Objetosc} m^3");
            }
        }
    }
}