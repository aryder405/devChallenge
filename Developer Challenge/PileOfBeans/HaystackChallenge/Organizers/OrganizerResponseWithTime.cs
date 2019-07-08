using System;
using eBags.PileOfBeans.HaystackChallenge.Core;
using System.Collections.Generic;

namespace eBags.PileOfBeans.HaystackChallenge.Organizers
{
    public class OrganizerResponseWithTime : OrganizerResponse
    {
        public TimeSpan TimeTaken { get; set; }

    }
}
