using System.Data.Entity;
using System.Threading.Tasks;

using FriendOrganizer.DataAccess05;
using FriendOrganizer.Domain;

using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.DomainServices.Repositories
{
    public class FriendRepository15 : GenericRepository<Friend15, FriendOrganizerDbContext>, 
        IFriendRepository15
    {
        public FriendRepository15(FriendOrganizerDbContext context) : base(context)
        {
        }

        public override async Task<Friend15> FindByIdAsync(int friendId)
        {
            return await Context.Friends15
                .Include(f => f.PhoneNumbers)
                .SingleAsync(f => f.Id == friendId);
        }

        public void RemovePhoneNumber(FriendPhoneNumber13 model)
        {
            Context.FriendPhoneNumbers13.Remove(model);
        }
    }
}
