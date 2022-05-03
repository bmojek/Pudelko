using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PudelkoLibrary
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable<double>, IEnumerator<double>
    {
        private readonly double _a = 0.1;
        private readonly double _b = 0.1;
        private readonly double _c = 0.1;
        private readonly UnitOfMeasure unit;

        public double A { get => CutNumb(_a); }
        public double B { get => CutNumb(_b); }
        public double C { get => CutNumb(_c); }

        public double Objetosc { get => Math.Round((A * B * C), 9); }

        public double Pole { get => Math.Round((2 * A * B + 2 * A * C + 2 * B * C), 6); }

        private double[] ArrPudelko => new double[] { A, B, C };

        public double this[int i]
        {
            get => ArrPudelko[i];
        }

        private void CheckNumb(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
                throw new ArgumentOutOfRangeException();

            if (a > 10 || b > 10 || c > 10)
                throw new ArgumentOutOfRangeException();
        }

        private double CutNumb(double num)
        {
            return Math.Truncate(num * 1000) / 1000;
        }

        private double ToMeters(double number, UnitOfMeasure unit)
        {
            if (unit is UnitOfMeasure.milimeter)
            {
                return number / 1000;
            }
            else if (unit is UnitOfMeasure.centimeter)
            {
                return number / 100;
            }
            else
            {
                return number;
            }
        }

        public Pudelko(double a, double b, double c, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            _a = ToMeters(a, unit);
            _b = ToMeters(b, unit);
            _c = ToMeters(c, unit);
            this.unit = unit;

            CheckNumb(A, B, C);
        }

        public Pudelko(double a, double b, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            _a = ToMeters(a, unit);
            _b = ToMeters(b, unit);

            CheckNumb(A, B, C);
        }

        public Pudelko(double a = 0.1, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            _a = ToMeters(a, unit);

            CheckNumb(A, B, C);
        }

        public override string ToString()
        {
            return ToString("m", CultureInfo.GetCultureInfo("en-US"));
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.GetCultureInfo("en-US"));
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return $"{ A.ToString("F3", formatProvider) } m \u00D7 { B.ToString("F3", formatProvider) } m \u00D7 { C.ToString("F3", formatProvider) } m";
            }
            switch (format.ToUpperInvariant())
            {
                case "M":
                    return $"{ A.ToString("F3", formatProvider) } { format } \u00D7 { B.ToString("F3", formatProvider) } { format } \u00D7 { C.ToString("F3", formatProvider) } { format }";

                case "CM":
                    return $"{ (A * 100).ToString("F1", formatProvider) } { format } \u00D7 { (B * 100).ToString("F1", formatProvider) } { format } \u00D7 { (C * 100).ToString("F1", formatProvider) } { format }";

                case "MM":
                    return $"{ A * 1000 } { format } \u00D7 { B * 1000 } { format } \u00D7 { C * 1000 } { format }";

                default:
                    throw new FormatException();
            }
        }

        public bool Equals(Pudelko pudelko)
        {
            return this == pudelko;
        }

        public override bool Equals(object obj)
        {
            if (obj is Pudelko pudelko)
                return this == pudelko;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Pudelko p1, Pudelko p2)
        {
            var p1Values = new double[] { p1.A, p1.B, p1.C };
            var p2Values = new double[] { p2.A, p2.B, p2.C };

            var sortedP1Values = p1Values.OrderBy(x => x);
            var sortedP2Values = p2Values.OrderBy(x => x);

            return Enumerable.SequenceEqual(sortedP1Values, sortedP2Values);
        }

        public static bool operator !=(Pudelko p1, Pudelko p2)
        {
            return !(p1 == p2);
        }

        public static Pudelko operator +(Pudelko p1, Pudelko p2)
        {
            double[] arrA =
            {
                p1.A,
                p1.B,
                p1.C
            };
            double[] arrB =
            {
                p2.A,
                p2.B,
                p2.C
            };
            arrA = arrA.OrderByDescending(x => x).ToArray();
            arrB = arrB.OrderByDescending(x => x).ToArray();

            double side1 = arrA[1];
            double side2 = arrA[0];

            if (arrB[1] > arrA[1]) side1 = arrB[1];
            if (arrB[0] > arrA[0]) side2 = arrB[0];

            return new Pudelko(arrA[2] + arrB[2], side1, side2);
        }

        public IEnumerator<double> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int position = -1;

        public double Current => this[position];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            position = -1;
        }

        public bool MoveNext()
        {
            position++;

            return (position < 3);
        }

        public void Reset()
        {
            position = -1;
        }

        public static explicit operator double[](Pudelko p)
        {
            double[] pudelkoArr =
            {
                p.A,
                p.B,
                p.C
            };

            return pudelkoArr;
        }

        public static implicit operator Pudelko(ValueTuple<int, int, int> dimensions)
        {
            return new Pudelko(dimensions.Item1, dimensions.Item2, dimensions.Item3, UnitOfMeasure.milimeter);
        }

        public Pudelko Parse(string stringToParse)
        {
            return Parse(stringToParse, CultureInfo.GetCultureInfo("en-US"));
        }

        public Pudelko Parse(string stringToParse, IFormatProvider format)
        {
            var arr = stringToParse.Split(' ');

            UnitOfMeasure unit;

            switch (arr[1])
            {
                case "cm":
                    unit = UnitOfMeasure.centimeter;
                    break;

                case "mm":
                    unit = UnitOfMeasure.milimeter;
                    break;

                default:
                    unit = UnitOfMeasure.meter;
                    break;
            }

            var a = ToMeters(double.Parse(arr[0], format), unit);
            var b = ToMeters(double.Parse(arr[3], format), unit);
            var c = ToMeters(double.Parse(arr[6], format), unit);

            return new Pudelko(a, b, c);
        }
    }
}