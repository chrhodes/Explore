using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using FriendOrganizer.DataAccess;
using FriendOrganizer.Domain;
using VNC;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.DomainServices.Repositories
{
    public class FriendRepository
        : GenericRepository<Friend, FriendOrganizerDbContext>, 
        IFriendRepository
    {
        public FriendRepository(FriendOrganizerDbContext context) : base(context)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        public override async Task<Friend> FindByIdAsync(int friendId)
        {
            Int64 startTicks = Log.DOMAINSERVICES("(FriendRepository) Enter", Common.LOG_APPNAME);

            var result =  await Context.Friends
                .Include(f => f.PhoneNumbers)
                .SingleAsync(f => f.Id == friendId);

            Log.DOMAINSERVICES("(FriendRepository) Exit", Common.LOG_APPNAME, startTicks);

            return result;
        }

        public async Task<bool> HasMeetingsAsync(int friendId)
        {
            Int64 startTicks = Log.DOMAINSERVICES("(FriendRepository) Enter", Common.LOG_APPNAME);

            var result = await Context.Meetings.AsNoTracking()
                .Include(m => m.Friends)
                .AnyAsync(m => m.Friends.Any(f => f.Id == friendId));

            Log.DOMAINSERVICES("(FriendRepository) Exit", Common.LOG_APPNAME, startTicks);

            return result;
        }

        public void RemovePhoneNumber(FriendPhoneNumber model)
        {
            Int64 startTicks = Log.DOMAINSERVICES("Enter", Common.LOG_APPNAME);

            Context.FriendPhoneNumbers.Remove(model);

            Log.DOMAINSERVICES("Exit", Common.LOG_APPNAME, startTicks);
        }
    }
}
