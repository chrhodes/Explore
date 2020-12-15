using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using FriendOrganizer.DataAccess;
using FriendOrganizer.Domain;
using VNC;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.DomainServices.Repositories
{
    public class MeetingRepository
        : GenericRepository<Meeting, FriendOrganizerDbContext>, 
        IMeetingRepository
    {
        public MeetingRepository(FriendOrganizerDbContext context) : base(context)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        public override async Task<Meeting> FindByIdAsync(int meetingId)
        {
            Int64 startTicks = Log.DOMAINSERVICES("(MeetingRepository) Enter", Common.LOG_APPNAME);

            var result =  await Context.Meetings
                .Include(m => m.Friends)
                .SingleAsync(f => f.Id == meetingId);

            Log.DOMAINSERVICES("(MeetingRepository) Exit", Common.LOG_APPNAME, startTicks);

            return result;
        }

        public async Task<List<Friend>> GetAllFriendsAsync()
        {
            Int64 startTicks = Log.DOMAINSERVICES("(MeetingRepository) Enter", Common.LOG_APPNAME);

            var result = await Context.Set<Friend>()
                .ToListAsync();

            Log.DOMAINSERVICES("(MeetingRepository) Exit", Common.LOG_APPNAME, startTicks);

            return result;
        }

        public async Task ReloadFriendAsync(int friendId)
        {
            Int64 startTicks = Log.DOMAINSERVICES("(MeetingRepository) Enter", Common.LOG_APPNAME);

            var dbEntityEntry = Context.ChangeTracker.Entries<Friend>()
                .SingleOrDefault(db => db.Entity.Id == friendId);

            if (dbEntityEntry != null)
            {
                await dbEntityEntry.ReloadAsync();
            }

            Log.DOMAINSERVICES("(MeetingRepository) Exit", Common.LOG_APPNAME, startTicks);
        }
    }
}
