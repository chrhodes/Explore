using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using FriendOrganizer.DataAccess05;
using FriendOrganizer.Domain;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.DomainServices.Repositories
{
    public class MeetingRepository19 
        : GenericRepository<Meeting19, FriendOrganizerDbContext>, 
        IMeetingRepository19
    {
        public MeetingRepository19(FriendOrganizerDbContext context) : base(context)
        {
        }

        public override async Task<Meeting19> FindByIdAsync(int meetingId)
        {
            return await Context.Meetings19
                .Include(m => m.Friends)
                .SingleAsync(f => f.Id == meetingId);
        }

        public async Task<List<Friend19>> GetAllFriendsAsync()
        {
            return await Context.Set<Friend19>()
                .ToListAsync();
        }

        public async Task ReloadFriendAsync(int friendId)
        {
            var dbEntityEntry = Context.ChangeTracker.Entries<Friend19>()
                .SingleOrDefault(db => db.Entity.Id == friendId);

            if (dbEntityEntry != null)
            {
                await dbEntityEntry.ReloadAsync();
            }
        }
    }
}
