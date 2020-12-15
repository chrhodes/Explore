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
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        public async Task<bool> IsReferencedByFriendAsync(int programmingLanguageId)
        {
            Int64 startTicks = Log.DOMAINSERVICES("(ProgrammingLanguageRepository) Enter", Common.LOG_APPNAME);

            var result = await Context.Friends.AsNoTracking()
                .AnyAsync(f => f.FavoriteLanguageId == programmingLanguageId);

            Log.DOMAINSERVICES("(ProgrammingLanguageRepository) Exit", Common.LOG_APPNAME, startTicks);

            return result;
        }
    }
}
