
using FriendOrganizer.Domain;
using System.Threading.Tasks;

namespace VNCExplore_DDDinPractice.Core.DomainServices
{
    public interface IFriendRepository : IGenericRepository<Friend>
    {
        Task<bool> HasMeetingsAsync(int friendId);

        void RemovePhoneNumber(FriendPhoneNumber model);
    }
}
