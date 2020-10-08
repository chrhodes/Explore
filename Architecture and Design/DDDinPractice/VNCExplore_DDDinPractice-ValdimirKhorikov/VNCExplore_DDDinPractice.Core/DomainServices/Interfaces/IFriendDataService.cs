using System.Collections.Generic;
using System.Threading.Tasks;
using FriendOrganizer.Domain;

namespace VNCExplore_DDDinPractice.Core.DomainServices
{
    public interface IFriendDataService
    {
        Task<Friend> FindByIdAsync(int friendId);
        Task UpdateAsync(Friend friend);
    }
}