using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eBags.PileOfBeans.HaystackChallenge.Core;
using eBags.PileOfBeans.HaystackChallenge.Organizers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eBags.PileOfBeans.Test.Unit
{
    [TestClass]
    public class StrawTester
    {
        //2 million max size
        public int MaxRequestSize = 2000000;
        public HaystackOrganizer Organizer { get; set; }
        public OrganizerRequest Request { get; set; }

        //A single response we can test on
        public OrganizerResponse Response { get; set; }

        [TestInitialize]
        public void Init()
        {
            Organizer = new HaystackOrganizer();
            Request = GetRequest(MaxRequestSize-100, MaxRequestSize);
            Response = Organizer.Organize(Request);
        }

        [TestMethod]
        public void StrawCountsAreCorrect()
        {
            var haystack = Request.Haystack;

            //The number of duplicates in original haystack
            var dupeCount = haystack.Count - haystack.Distinct().Count();

            //Response end count
            var endCount = Response.Reds.Count + Response.Greens.Count + Response.Blues.Count + Response.Grays.Count;

            Assert.IsTrue(endCount + dupeCount == haystack.Count);
        }

        [TestMethod]
        public void StrawsAreSorted()
        {
            //test red sorted
            Assert.IsTrue(IsSorted(Response.Reds));
            Assert.IsTrue(IsSorted(Response.Blues));
            Assert.IsTrue(IsSorted(Response.Greens));
            Assert.IsTrue(IsSorted(Response.Grays));
        }

        [TestMethod]
        public void BluesAreActuallyBlue()
        {
            Assert.IsTrue(TestBlues(Response.Blues));
        }

        [TestMethod]
        public void RedsAreActuallyRed()
        {
            Assert.IsTrue(TestReds(Response.Reds));
        }

        [TestMethod]
        public void GreensAreActuallyGreen()
        {
            Assert.IsTrue(TestGreens(Response.Greens));
        }

        [TestMethod]
        public void GraysAreActuallyGray()
        {
            Assert.IsTrue(TestGrays(Response.Grays));
        }

        [TestMethod]
        public void TimeIsUnder30()
        {
            var starTime = DateTime.Now;
            var response = (OrganizerResponseWithTime) Organizer.Organize(Request);
            var endTime = DateTime.Now;

            var secondsTaken = (endTime - starTime).TotalSeconds;

            Assert.IsTrue(secondsTaken < 30, $"Time limit exceeded 30 seconds: {secondsTaken}");

            //make sure response.timeTaken and perceived time are equal
            Assert.AreEqual(Math.Round(response.TimeTaken.TotalSeconds, 2), Math.Round(secondsTaken, 2));
        }

        [TestMethod]
        public async Task TimeIsUnder30Async()
        {
            var starTime = DateTime.Now;
            var response = (OrganizerResponseWithTime)await Organizer.OrganizeAsync(Request);
            var endTime = DateTime.Now;

            var secondsTaken = (endTime - starTime).TotalSeconds;

            Assert.IsTrue(secondsTaken < 30, $"Time limit exceeded 30 seconds: {secondsTaken}" );

            //make sure response.timeTaken and perceived time are equal
            Assert.AreEqual( Math.Round(response.TimeTaken.TotalSeconds, 2), Math.Round(secondsTaken, 2));
        }

        /// <summary>
        /// Curious about times for larger data sets.
        /// n = 2mil,  seconds = 3.508  (normal), 1.571  (async)
        /// n = 5mil,  seconds = 9.848  (normal), 4.140  (async)
        /// n = 8mil,  seconds = 17.174 (normal), 6.763  (async)
        /// n = 10mil, seconds = 23.929 (normal), 9.96   (async)
        /// n = 10mil, seconds = -----  (normal), 10.952 (async)
        /// n = 15mil, OOM exception
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestTimes()
        {
            var count = 5000000;

            var request = GetRequest(count - 100, count);

            var responseAsync = (OrganizerResponseWithTime)await Organizer.OrganizeAsync(request);
            var response = (OrganizerResponseWithTime) Organizer.Organize(request);

            var time = Math.Round(response.TimeTaken.TotalSeconds, 3);
            var timeAsync = Math.Round(responseAsync.TimeTaken.TotalSeconds, 3);

            //Async times should be shorter
            Assert.IsTrue(timeAsync < time);
        }


        [TestMethod]
        public async Task TestAsync()
        {
            
            var response = (OrganizerResponseWithTime) await Organizer.OrganizeAsync(Request);

            //check validity
            Assert.IsTrue(TestBlues(response.Blues));
            Assert.IsTrue(TestReds(response.Reds));
            Assert.IsTrue(TestGreens(response.Greens));
            Assert.IsTrue(TestGrays(response.Grays));

            //check count
            var count = Request.Haystack.Distinct().Count();
            Assert.IsTrue((response.Reds.Count + response.Greens.Count + response.Blues.Count + response.Grays.Count) == count);

            //test sorted
            //test red sorted
            Assert.IsTrue(IsSorted(response.Reds));
            Assert.IsTrue(IsSorted(response.Blues));
            Assert.IsTrue(IsSorted(response.Greens));
            Assert.IsTrue(IsSorted(response.Grays));

        }

        public OrganizerRequest GetRequest(int min, int max)
        {
            var request = new OrganizerRequest();
            var factory = new RandomStrawFactory(min, max);
            var haystack = factory.GetHaystack();

            request.Haystack = haystack;

            return request;
        }


        public bool TestGreens(IEnumerable<Straw> straws)
        {
            foreach (var straw in straws)
            {
                var blue = straw.ColorBlue;
                var red = straw.ColorRed;
                var green = straw.ColorGreen;
                if (green > red && green > blue)
                    continue;
                return false;
            }

            return true;
        }

        public bool TestReds(IEnumerable<Straw> straws)
        {
            foreach (var straw in straws)
            {
                var blue = straw.ColorBlue;
                var red = straw.ColorRed;
                var green = straw.ColorGreen;
                if (red > green && red > blue || red == green && red > blue)
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        public bool TestBlues(IEnumerable<Straw> straws)
        {
            foreach (var straw in straws)
            {
                var blue = straw.ColorBlue;
                var red = straw.ColorRed;
                var green = straw.ColorGreen;
                if (blue > green && blue > red
                    || blue == green && blue > red
                    || blue == red && blue > green)
                    continue;
                return false;
            }

            return true;
        }

        public bool TestGrays(IEnumerable<Straw> straws)
        {
            foreach (var straw in straws)
            {
                var blue = straw.ColorBlue;
                var red = straw.ColorRed;
                var green = straw.ColorGreen;
                if (green == red && green == blue)
                    continue;
                return false;
            }

            return true;
        }

        public bool IsSorted(IList<Straw> straws)
        {
            //test blue sorted
            for (int i = 1; i < straws.Count(); i++)
            {
                if (straws[i].LengthInCm >= straws[i - 1].LengthInCm)
                    continue;
                return false;
            }

            return true;
        }
    }
}
