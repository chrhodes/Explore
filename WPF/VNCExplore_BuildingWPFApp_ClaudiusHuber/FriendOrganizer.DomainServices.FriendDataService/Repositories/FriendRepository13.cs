using System.Data.Entity;
using System.Threading.Tasks;

using FriendOrganizer.DataAccess05;
using FriendOrganizer.Domain;

using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.DomainServices.Repositories
{
    public class FriendRepository13 : IFriendRepository13
    {
        private FriendOrganizerDbContext _context;

        public FriendRepository13(FriendOrganizerDbContext context)
        {
            _context = context;
        }

        public async Task<Friend13> FindByIdAsync(int friendId)
        {
            return await _context.Friends13.SingleAsync(f => f.Id == friendId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Add(Friend13 friend)
        {
            _context.Friends13.Add(friend);
        }

        public void Remove(Friend13 friend)
        {
            _context.Friends13.Remove(friend);
        }

        public void RemovePhoneNumber(FriendPhoneNumber13 model)
        {
            _context.FriendPhoneNumbers13.Remove(model);
        }
    }
}
