using System;
using System.Data.Entity;
using System.Threading.Tasks;

using FriendOrganizer.Domain;

using FriendOrganizer.DataAccess05;

using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.DomainServices
{
    public class FriendDataService06 : IFriendDataService06
    {
        private Func<FriendOrganizerDbContext> _contextCreator;

        public FriendDataService06(Func<FriendOrganizerDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<Friend05> GetByIdAsync(int friendId)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Friends05.AsNoTracking().SingleAsync(f => f.Id == friendId);
            }
        }
    }
}
