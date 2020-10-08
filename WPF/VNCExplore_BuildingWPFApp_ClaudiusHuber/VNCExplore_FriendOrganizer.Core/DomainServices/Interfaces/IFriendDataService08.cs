using System.Collections.Generic;
using System.Threading.Tasks;
using FriendOrganizer.Domain;

namespace VNCExplore_FriendOrganizer.Core.DomainServices
{
    public interface IFriendDataService08
    {
        Task<Friend05> FindByIdAsync(int friendId);
        Task UpdateAsync(Friend05 friend);
    }
}