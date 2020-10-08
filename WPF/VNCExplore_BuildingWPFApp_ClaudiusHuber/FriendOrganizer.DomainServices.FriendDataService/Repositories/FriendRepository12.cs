using System.Data.Entity;
using System.Threading.Tasks;

using FriendOrganizer.DataAccess05;
using FriendOrganizer.Domain;
using System;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.DomainServices.Repositories
{
    public class FriendRepository12 : IFriendRepository12
    {
        private FriendOrganizerDbContext _context;

        public FriendRepository12(FriendOrganizerDbContext context)
        {
            _context = context;
        }

        public async Task<Friend12> FindByIdAsync(int friendId)
        {
            return await _context.Friends12.SingleAsync(f => f.Id == friendId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Add(Friend12 friend)
        {
            _context.Friends12.Add(friend);
        }
        public void Remove(Friend12 friend)
        {
            _context.Friends12.Remove(friend);
        }
    }
}
