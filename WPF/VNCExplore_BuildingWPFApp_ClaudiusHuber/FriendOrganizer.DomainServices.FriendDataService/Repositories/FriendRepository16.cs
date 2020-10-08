using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using FriendOrganizer.DataAccess05;
using FriendOrganizer.Domain;

using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.DomainServices.Repositories
{
    public class FriendRepository16 : GenericRepository<Friend15, FriendOrganizerDbContext>, 
        IFriendRepository16
    {
        public FriendRepository16(FriendOrganizerDbContext context) : base(context)
        {
        }

        public override async Task<Friend15> FindByIdAsync(int friendId)
        {
            return await Context.Friends15
                .Include(f => f.PhoneNumbers)
                .SingleAsync(f => f.Id == friendId);
        }

        public async Task<bool> HasMeetingsAsync(int friendId)
        {
            return await Context.Meetings15.AsNoTracking()
                .Include(m => m.Friends)
                .AnyAsync(m => m.Friends.Any(f => f.Id == friendId));
        }

        public void RemovePhoneNumber(FriendPhoneNumber13 model)
        {
            Context.FriendPhoneNumbers13.Remove(model);
        }
    }
}
