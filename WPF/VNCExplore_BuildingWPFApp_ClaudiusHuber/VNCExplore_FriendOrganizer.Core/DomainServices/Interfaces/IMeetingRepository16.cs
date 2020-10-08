using FriendOrganizer.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VNCExplore_FriendOrganizer.Core.DomainServices
{
    public interface IMeetingRepository16 : IGenericRepository<Meeting15>
    {
        Task<List<Friend15>> GetAllFriendsAsync();
        Task ReloadFriendAsync(int friendId);
    }
}