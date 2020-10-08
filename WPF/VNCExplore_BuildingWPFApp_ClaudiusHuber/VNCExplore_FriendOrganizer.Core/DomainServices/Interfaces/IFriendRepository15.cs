
using FriendOrganizer.Domain;

namespace VNCExplore_FriendOrganizer.Core.DomainServices
{
    public interface IFriendRepository15 : IGenericRepository<Friend15>
    {
        void RemovePhoneNumber(FriendPhoneNumber13 model);
    }
}
