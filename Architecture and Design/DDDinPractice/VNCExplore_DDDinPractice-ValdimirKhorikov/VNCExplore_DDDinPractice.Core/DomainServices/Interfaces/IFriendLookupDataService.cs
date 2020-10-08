using System.Collections.Generic;
using System.Threading.Tasks;

using FriendOrganizer.Domain;

namespace VNCExplore_DDDinPractice.Core.DomainServices
{
    public interface IFriendLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetFriendLookupAsync();
    }
}