using PudelkoLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace PudelkoApp
{
    public static class PKompresuj
    {
        public static Pudelko Kompresuj(this Pudelko p)
        {
            var volume = p.Volume;

            var side = Math.Cbrt(volume);

            return new Pudelko(side, side, side);
        }
    }
}
