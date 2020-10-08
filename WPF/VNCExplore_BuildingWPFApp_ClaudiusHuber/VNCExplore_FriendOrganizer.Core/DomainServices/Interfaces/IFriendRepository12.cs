using System.Threading.Tasks;

using FriendOrganizer.Domain;

namespace VNCExplore_FriendOrganizer.Core.DomainServices
{
    public interface IFriendRepository12
    {
        Task<Friend12> FindByIdAsync(int friendId);
        Task UpdateAsync();
        bool HasChanges();
        void Add(Friend12 friend);
        void Remove(Friend12 friend);
    }
}
