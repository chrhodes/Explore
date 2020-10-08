using System.Collections.Generic;
using System.Threading.Tasks;
using FriendOrganizer.Domain;

namespace VNCExplore_FriendOrganizer.Core.DomainServices
{
    public interface IFriendDataService05
    {
        IEnumerable<IFriend> GetAll2();

        Task<List<IFriend>> GetAllAsync2();

        IEnumerable<Friend05> GetAll();

        Task<List<Friend05>> GetAllAsync();
    }
}