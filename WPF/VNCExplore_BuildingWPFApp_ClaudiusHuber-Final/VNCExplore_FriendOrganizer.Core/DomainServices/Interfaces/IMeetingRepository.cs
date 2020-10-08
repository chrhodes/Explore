using FriendOrganizer.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VNCExplore_FriendOrganizer.Core.DomainServices
{
    public interface IMeetingRepository : IGenericRepository<Meeting>
    {
        Task<List<Friend>> GetAllFriendsAsync();
        Task ReloadFriendAsync(int friendId);
    }
}