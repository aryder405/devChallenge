
using System.Threading.Tasks;

namespace eBags.PileOfBeans.HaystackChallenge.Organizers
{
    public interface IHaystackOrganizer
    {
        OrganizerResponse Organize(OrganizerRequest request);

        Task<OrganizerResponse> OrganizeAsync(OrganizerRequest request);

    }
}
