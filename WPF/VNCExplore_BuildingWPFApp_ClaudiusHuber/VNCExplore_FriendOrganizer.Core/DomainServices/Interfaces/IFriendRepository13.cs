using System.Threading.Tasks;

using FriendOrganizer.Domain;

namespace VNCExplore_FriendOrganizer.Core.DomainServices
{
    public interface IFriendRepository13
    {
        Task<Friend13> FindByIdAsync(int friendId);
        Task UpdateAsync();
        bool HasChanges();
        void Add(Friend13 friend);
        void Remove(Friend13 friend);
        void RemovePhoneNumber(FriendPhoneNumber13 model);
    }
}
