using System;
using eBags.PileOfBeans.HaystackChallenge.Core;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Threading.Tasks;

namespace eBags.PileOfBeans.HaystackChallenge.Organizers
{
    public class HaystackOrganizer : IHaystackOrganizer
    {
        public OrganizerResponse Organize(OrganizerRequest request)
        {
            var response = OrganizeStraws(request.Haystack);

            return response;
        }

        /// <summary>
        /// Asynchronous pattern is well suited for this task, as the
        /// work can be divided between the colors.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OrganizerResponse> OrganizeAsync(OrganizerRequest request)
        {
            var response = await OrganizeStrawsAsync(request.Haystack);

            return response;
        }

        private async Task<OrganizerResponse> OrganizeStrawsAsync(IEnumerable<Straw> strawList)
        {
            var start = DateTime.Now;

            var blues = GetBluesAsync(strawList);

            var reds = GetRedsAsync(strawList);

            var greens = GetGreensAsync(strawList);

            var grays = GetGraysAsync(strawList);

            var response = new OrganizerResponseWithTime
            {
                Blues = blues.Result,
                Greens = greens.Result,
                Reds = reds.Result,
                Grays = grays.Result
            };

            response.TimeTaken = DateTime.Now - start;

            return response;
        }

        private OrganizerResponse OrganizeStraws(IEnumerable<Straw> strawList)
        {
            var start = DateTime.Now;
            
            var blues = GetBlues(strawList);

            var reds = GetReds(strawList);

            var greens = GetGreens(strawList);

            var grays = GetGrays(strawList);

            var timeTaken = DateTime.Now - start;

            return new OrganizerResponseWithTime
            {
                Blues = blues,
                Greens = greens,
                Reds = reds,
                Grays = grays,
                TimeTaken = timeTaken
            };
        }

        /// <summary>
        /// Return sorted blue straws and remove duplicates.
        /// Blue wins the tie over blue/green and blue/red.
        /// </summary>
        /// <param name="strawList"></param>
        /// <returns></returns>
        private IList<Straw> GetBlues(IEnumerable<Straw> strawList)
        {
            //blue wins over blue/green tie
            //blue wins over blue/red tie
            return strawList.Where(s => s.ColorBlue > s.ColorGreen && s.ColorBlue > s.ColorRed
                                          || s.ColorBlue == s.ColorGreen && s.ColorBlue > s.ColorRed
                                          || s.ColorBlue == s.ColorRed && s.ColorBlue > s.ColorGreen)
                .Distinct().OrderBy(s => s.LengthInCm).ToList();
        }

        private async Task<IList<Straw>> GetBluesAsync(IEnumerable<Straw> strawList)
        {
            return await Task.Run(() => GetBlues(strawList));
        }

        /// <summary>
        /// Return sorted red straws and remove duplicates.
        /// Red wins the tie over red/green.
        /// </summary>
        /// <param name="strawList"></param>
        /// <returns></returns>
        private IList<Straw> GetReds(IEnumerable<Straw> strawList)
        {
            //red wins over red/green tie
            return strawList.Where(s => s.ColorRed > s.ColorGreen && s.ColorRed > s.ColorBlue
                                         || s.ColorRed == s.ColorGreen && s.ColorRed > s.ColorBlue)
                .Distinct().OrderBy(s => s.LengthInCm).ToList();
        }

        private async Task<IList<Straw>> GetRedsAsync(IEnumerable<Straw> strawList)
        {
            return await Task.Run(() => GetReds(strawList));
        }

        /// <summary>
        /// Return sorted green straws and remove duplicates.
        /// Green doesn't win any ties.
        /// </summary>
        /// <param name="strawList"></param>
        /// <returns></returns>
        private IList<Straw> GetGreens(IEnumerable<Straw> strawList)
        {
            //green doesn't win ties
            return strawList.Where(s => s.ColorGreen > s.ColorBlue && s.ColorGreen > s.ColorRed)
                .Distinct().OrderBy(s => s.LengthInCm).ToList();
        }

        private async Task<IList<Straw>> GetGreensAsync(IEnumerable<Straw> strawList)
        {
            return await Task.Run(() => GetGreens(strawList));
        }

        /// <summary>
        /// Return sorted gray straws and remove duplicates.
        /// </summary>
        /// <param name="strawList"></param>
        /// <returns></returns>
        private IList<Straw> GetGrays(IEnumerable<Straw> strawList)
        {
            return strawList.Where(s => s.ColorBlue == s.ColorGreen && s.ColorBlue == s.ColorRed)
                .Distinct().OrderBy(s => s.LengthInCm).ToList();
        }

        private async Task<IList<Straw>> GetGraysAsync(IEnumerable<Straw> strawList)
        {
            return await Task.Run(() => GetGrays(strawList));
        }

    }
}
