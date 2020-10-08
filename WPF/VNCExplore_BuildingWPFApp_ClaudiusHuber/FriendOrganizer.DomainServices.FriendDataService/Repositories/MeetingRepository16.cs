using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using FriendOrganizer.DataAccess05;
using FriendOrganizer.Domain;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.DomainServices.Repositories
{
    public class MeetingRepository16 : GenericRepository<Meeting15, FriendOrganizerDbContext>, 
        IMeetingRepository16
    {
        public MeetingRepository16(FriendOrganizerDbContext context) : base(context)
        {
        }

        public override async Task<Meeting15> FindByIdAsync(int meetingId)
        {
            return await Context.Meetings15
                .Include(m => m.Friends)
                .SingleAsync(f => f.Id == meetingId);
        }

        public async Task<List<Friend15>> GetAllFriendsAsync()
        {
            return await Context.Set<Friend15>()
                .ToListAsync();
        }

        public async Task ReloadFriendAsync(int friendId)
        {
            var dbEntityEntry = Context.ChangeTracker.Entries<Friend15>()
                .SingleOrDefault(db => db.Entity.Id == friendId);

            if (dbEntityEntry != null)
            {
                await dbEntityEntry.ReloadAsync();
            }
        }
    }
}
