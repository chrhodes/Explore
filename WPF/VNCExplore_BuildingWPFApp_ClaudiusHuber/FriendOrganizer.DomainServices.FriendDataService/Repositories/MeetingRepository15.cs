using System.Data.Entity;
using System.Threading.Tasks;

using FriendOrganizer.DataAccess05;
using FriendOrganizer.Domain;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.DomainServices.Repositories
{
    public class MeetingRepository15 : GenericRepository<Meeting15, FriendOrganizerDbContext>, 
        IMeetingRepository15
    {
        public MeetingRepository15(FriendOrganizerDbContext context) : base(context)
        {
        }

        public override async Task<Meeting15> FindByIdAsync(int meetingId)
        {
            return await Context.Meetings15
                .Include(m => m.Friends)
                .SingleAsync(f => f.Id == meetingId);
        }
    }
}
