using System.Collections.Generic;

namespace eBags.PileOfBeans.HaystackChallenge.Core
{
    public interface IStrawFactory
    {
        IList<Straw> GetHaystack();
    }
}
