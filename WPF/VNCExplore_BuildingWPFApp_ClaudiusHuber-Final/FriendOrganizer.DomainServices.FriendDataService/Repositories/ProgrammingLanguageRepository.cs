using System;
using System.Data.Entity;
using System.Threading.Tasks;

using FriendOrganizer.DataAccess;
using FriendOrganizer.Domain;

using VNC;

namespace FriendOrganizer.DomainServices.Repositories
{
    public class ProgrammingLanguageRepository 
        : GenericRepository<ProgrammingLanguage, FriendOrganizerDbContext>,
        IProgrammingLanguageRepository
    {
        public ProgrammingLanguageRepository(FriendOrganizerDbContext context) 
            : base(context)
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);
        }

        public async Task<bool> IsReferencedByFriendAsync(int programmingLanguageId)
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);

            return await Context.Friends.AsNoTracking()
                .AnyAsync(f => f.FavoriteLanguageId == programmingLanguageId);
        }
    }
}
