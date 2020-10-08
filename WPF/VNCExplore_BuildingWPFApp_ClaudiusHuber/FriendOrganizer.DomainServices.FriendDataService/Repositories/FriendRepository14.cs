using System.Data.Entity;
using System.Threading.Tasks;

using FriendOrganizer.DataAccess05;
using FriendOrganizer.Domain;

using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.DomainServices.Repositories
{
    public class FriendRepository14 : GenericRepository<Friend13, FriendOrganizerDbContext>, 
        IFriendRepository13
    {
        public FriendRepository14(FriendOrganizerDbContext context) : base(context)
        {
        }

        public override async Task<Friend13> FindByIdAsync(int friendId)
        {
            return await Context.Friends13
                .Include(f => f.PhoneNumbers)
                .SingleAsync(f => f.Id == friendId);
        }

        public void RemovePhoneNumber(FriendPhoneNumber13 model)
        {
            Context.FriendPhoneNumbers13.Remove(model);
        }
    }
}
