
using FriendOrganizer.Domain;
using System.Threading.Tasks;

namespace VNCExplore_FriendOrganizer.Core.DomainServices
{
    public interface IFriendRepository : IGenericRepository<Friend>
    {
        Task<bool> HasMeetingsAsync(int friendId);

        void RemovePhoneNumber(FriendPhoneNumber model);
    }
}
