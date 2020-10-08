using System.Threading.Tasks;

using FriendOrganizer.Domain;

namespace VNCExplore_FriendOrganizer.Core.DomainServices
{
    public interface IFriendRepository10
    {
        Task<Friend05> FindByIdAsync(int friendId);
        Task UpdateAsync();
        bool HasChanges();
        void Add(Friend05 friend);
        void Remove(Friend05 friend);
    }
}
