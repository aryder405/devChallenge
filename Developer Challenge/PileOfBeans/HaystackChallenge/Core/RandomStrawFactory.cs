using System;
using System.Collections.Generic;

namespace eBags.PileOfBeans.HaystackChallenge.Core
{
    public class RandomStrawFactory : IStrawFactory
    {
        private const int COLOR_MIN = 0;
        private const int COLOR_MAX = 256;
        private const int LENGTH_MIN = 50;
        private const int LENGTH_MAX = 200;
        private readonly int _pileSizeMin;
        private readonly int _pileSizeMax;

        public RandomStrawFactory(
            int pileSizeMin,
            int pileSizeMax)
        {
            if (pileSizeMin <= 0 || pileSizeMin >= pileSizeMax)
                throw new ApplicationException($"Invalid {nameof(pileSizeMin)} and {nameof(pileSizeMax)} parameters specified.");

            _pileSizeMin = pileSizeMin;
            _pileSizeMax = pileSizeMax;
        }

        public IList<Straw> GetHaystack()
        {
            var generator = new Random(DateTime.Now.Millisecond);
            var size = generator.Next(_pileSizeMin, _pileSizeMax);

            var haystack = new List<Straw>();

            // Build the haystack...
            for (int i = 0; i < size; i++)
            {
                haystack.Add(
                    new Straw(
                        generator.Next(LENGTH_MIN, LENGTH_MAX) / (decimal)10,
                        generator.Next(COLOR_MIN, COLOR_MAX),
                        generator.Next(COLOR_MIN, COLOR_MAX),
                        generator.Next(COLOR_MIN, COLOR_MAX)
                    ));
            }

            return haystack;
        }
    }
}
