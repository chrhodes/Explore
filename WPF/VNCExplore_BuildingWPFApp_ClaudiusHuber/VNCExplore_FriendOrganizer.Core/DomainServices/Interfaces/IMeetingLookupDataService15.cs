using System.Collections.Generic;
using System.Threading.Tasks;

using FriendOrganizer.Domain;

namespace VNCExplore_FriendOrganizer.Core.DomainServices
{
    public interface IMeetingLookupDataService15
    {
        Task<IEnumerable<LookupItem>> GetMeetingLookupAsync();
    }
}