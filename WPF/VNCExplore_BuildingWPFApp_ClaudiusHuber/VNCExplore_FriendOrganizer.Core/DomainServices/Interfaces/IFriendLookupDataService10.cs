using System.Collections.Generic;
using System.Threading.Tasks;

using FriendOrganizer.Domain;

namespace VNCExplore_FriendOrganizer.Core.DomainServices
{
    public interface IFriendLookupDataService10
    {
        Task<IEnumerable<LookupItem>> GetFriendLookupAsync();
    }
}