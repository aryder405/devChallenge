using eBags.PileOfBeans.HaystackChallenge.Core;
using System.Collections.Generic;

namespace eBags.PileOfBeans.HaystackChallenge.Organizers
{
    public class OrganizerResponse
    {
        public IList<Straw> Reds { get; set; }
        public IList<Straw> Greens { get; set; }
        public IList<Straw> Blues { get; set; }
        public IList<Straw> Grays { get; set; }
    }
}
