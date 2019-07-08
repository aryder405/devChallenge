
using System;

namespace eBags.PileOfBeans.HaystackChallenge.Core
{
    public class Straw
    {
        private const int COLOR_MIN = 0;
        private const int COLOR_MAX = 256;
        private const decimal LENGTH_MIN = 5;
        private const decimal LENGTH_MAX = 20;

        public decimal LengthInCm { get; private set; }
        public int ColorRed { get; private set; }
        public int ColorGreen { get; private set; }
        public int ColorBlue { get; private set; }

        public Straw(
            decimal lengthInCm,
            int colorRed,
            int colorGreen,
            int colorBlue)
        {
            if (lengthInCm < LENGTH_MIN || lengthInCm >= LENGTH_MAX)
                throw new ApplicationException($"Allowable range of {nameof(lengthInCm)} parameter is: {LENGTH_MIN} >= and < {LENGTH_MAX}");

            if (colorRed < COLOR_MIN || colorRed >= COLOR_MAX)
                throw new ApplicationException($"Allowable range of {nameof(colorRed)} parameter is: COLOR_MIN >= and < COLOR_MAX");

            if (colorGreen < COLOR_MIN || colorGreen >= COLOR_MAX)
                throw new ApplicationException($"Allowable range of {nameof(colorGreen)} parameter is: COLOR_MIN >= and < COLOR_MAX");

            if (colorBlue < COLOR_MIN || colorBlue >= COLOR_MAX)
                throw new ApplicationException($"Allowable range of {nameof(colorBlue)} parameter is: COLOR_MIN >= and < COLOR_MAX");

            LengthInCm = lengthInCm;
            ColorRed = colorRed;
            ColorGreen = colorGreen;
            ColorBlue = colorBlue;
        }
    }
}
