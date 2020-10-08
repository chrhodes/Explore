
using FriendOrganizer.Domain;

namespace VNCExplore_FriendOrganizer.Core.DomainServices
{
    public interface IFriendRepository14 : IGenericRepository<Friend13>
    {
        void RemovePhoneNumber(FriendPhoneNumber13 model);
    }
}
