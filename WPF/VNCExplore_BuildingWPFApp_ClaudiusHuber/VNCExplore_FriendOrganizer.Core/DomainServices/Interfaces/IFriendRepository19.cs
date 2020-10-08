
using FriendOrganizer.Domain;
using System.Threading.Tasks;

namespace VNCExplore_FriendOrganizer.Core.DomainServices
{
    public interface IFriendRepository19 : IGenericRepository<Friend19>
    {
        Task<bool> HasMeetingsAsync(int friendId);

        void RemovePhoneNumber(FriendPhoneNumber13 model);
    }
}
