using System.Collections.Generic;
using System.Threading.Tasks;
using FriendOrganizer.Domain;

namespace VNCExplore_FriendOrganizer.Core.DomainServices
{
    public interface IFriendDataService06
    {
        Task<Friend05> GetByIdAsync(int friendId);
    }
}