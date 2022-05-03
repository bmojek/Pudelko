using PudelkoLibrary;
using System;

namespace PudelkoApp
{
    public static class PKompresuj
    {
        public static Pudelko Kompresuj(this Pudelko p)
        {
            var objetosc = p.Objetosc;

            var side = Math.Cbrt(objetosc);

            return new Pudelko(side, side, side);
        }
    }
}