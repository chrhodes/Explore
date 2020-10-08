using FriendOrganizer.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VNCExplore_FriendOrganizer.Core.DomainServices
{
    public interface IMeetingRepository19 : IGenericRepository<Meeting19>
    {
        Task<List<Friend19>> GetAllFriendsAsync();
        Task ReloadFriendAsync(int friendId);
    }
}