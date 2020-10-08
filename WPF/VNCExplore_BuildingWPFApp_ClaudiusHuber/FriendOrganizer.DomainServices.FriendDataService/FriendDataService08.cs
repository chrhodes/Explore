using System;
using System.Data.Entity;
using System.Threading.Tasks;

using FriendOrganizer.Domain;

using FriendOrganizer.DataAccess05;

using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.DomainServices
{
    public class FriendDataService08 : IFriendDataService08
    {
        private Func<FriendOrganizerDbContext> _contextCreator;

        public FriendDataService08(Func<FriendOrganizerDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<Friend05> FindByIdAsync(int friendId)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Friends05.AsNoTracking().SingleAsync(f => f.Id == friendId);
            }
        }
        public async Task UpdateAsync(Friend05 friend)
        {
            using (var ctx = _contextCreator())
            {
                ctx.Friends05.Attach(friend);
                ctx.Entry(friend).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
        }
    }
}
