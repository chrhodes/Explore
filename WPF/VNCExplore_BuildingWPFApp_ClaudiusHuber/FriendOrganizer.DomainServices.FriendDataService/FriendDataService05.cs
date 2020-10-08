using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using FriendOrganizer.Domain;

using FriendOrganizer.DataAccess05;

using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.DomainServices
{
    public class FriendDataService05 : IFriendDataService05
    {
        // Somehow this is magically enough to make Unity happy.
        // Need to learn if this is new every time or a singleton.

        private Func<FriendOrganizerDbContext> _contextCreator;

        // Somehow this is magically enough to make Unity happy.
        public FriendDataService05(Func<FriendOrganizerDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public IEnumerable<Friend05> GetAll()
        {
            using (var ctx = _contextCreator())
            {
                return ctx.Friends05.AsNoTracking().ToList();
            }                ;
        }

        public async Task<List<Friend05>> GetAllAsync()
        {
            using (var ctx = _contextCreator())
            {
                // Can test Async loading

                //var friends = await ctx.Friends05.AsNoTracking().ToListAsync();
                //await Task.Delay(10000);

                //return friends;

                return await ctx.Friends05.AsNoTracking().ToListAsync();
            }
        }

        public IEnumerable<IFriend> GetAll2()
        {
            using (var ctx = _contextCreator())
            {
                return ctx.Friends05.AsNoTracking().ToList();
            }
        }

        public Task<List<IFriend>> GetAllAsync2()
        {
            throw new NotImplementedException();
        }

        //public Task<List<IFriend>> GetAllAsync2()
        //{
        //    using (var ctx = _contextCreator())
        //    {
        //        // TODO(crhodes)
        //        // Learn how to make this behave.  Says can't convert IFriend to Friend05

        //        //return await ctx.Friends05.AsNoTracking().ToListAsync();
        //    }
        //}
    }
}
