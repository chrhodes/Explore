using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using FriendOrganizer.DataAccess;
using FriendOrganizer.Domain;
using VNC;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.DomainServices.Lookups
{
    public class LookupDataService
        : IFriendLookupDataService, 
        IProgrammingLanguageLookupDataService, 
        IMeetingLookupDataService
    {
        private Func<FriendOrganizerDbContext> _contextCreator;

        public LookupDataService(Func<FriendOrganizerDbContext> contextCreator)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            _contextCreator = contextCreator;

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        public async Task<IEnumerable<LookupItem>> GetFriendLookupAsync()
        {
            Int64 startTicks = Log.DOMAINSERVICES("(LookupDataService) Enter", Common.LOG_APPNAME);

            IEnumerable<LookupItem> result;

            using (var ctx = _contextCreator())
            {
                result = await ctx.Friends.AsNoTracking()
                  .Select(f =>
                  new LookupItem
                  {
                      Id = f.Id,
                      DisplayMember = f.FirstName + " " + f.LastName
                  })
                  .ToListAsync();
            }

            Log.DOMAINSERVICES("(LookupDataService) Exit", Common.LOG_APPNAME, startTicks);

            return result;
        }

        public async Task<IEnumerable<LookupItem>> GetProgrammingLanguageLookupAsync()
        {
            Int64 startTicks = Log.DOMAINSERVICES("(LookupDataService) Enter", Common.LOG_APPNAME);

            IEnumerable<LookupItem> result;

            using (var ctx = _contextCreator())
            {
                result =  await ctx.ProgrammingLanguages.AsNoTracking()
                  .Select(f =>
                  new LookupItem
                  {
                      Id = f.Id,
                      DisplayMember = f.Name
                  })
                  .ToListAsync();
            }

            Log.DOMAINSERVICES("(LookupDataService) Exit", Common.LOG_APPNAME, startTicks);

            return result;
        }

        public async Task<IEnumerable<LookupItem>> GetMeetingLookupAsync()
        {
            Int64 startTicks = Log.DOMAINSERVICES("(LookupDataService) Enter", Common.LOG_APPNAME);

            IEnumerable<LookupItem> result;

            using (var ctx = _contextCreator())
            {
                 result = await ctx.Meetings.AsNoTracking()
                  .Select(m =>
                  new LookupItem
                  {
                      Id = m.Id,
                      DisplayMember = m.Title
                  })
                  .ToListAsync();
            }

            Log.DOMAINSERVICES("(LookupDataService) Exit", Common.LOG_APPNAME, startTicks);

            return result;
        }
    }
}
