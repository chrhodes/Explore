using System.Data.Entity;
using System.Threading.Tasks;

using FriendOrganizer.DataAccess05;
using FriendOrganizer.Domain;
using System;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.DomainServices.Repositories
{
    public class FriendRepository10 : IFriendRepository10
    {
        private FriendOrganizerDbContext _context;

        public FriendRepository10(FriendOrganizerDbContext context)
        {
            _context = context;
        }

        public async Task<Friend05> FindByIdAsync(int friendId)
        {
            return await _context.Friends05.SingleAsync(f => f.Id == friendId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Add(Friend05 friend)
        {
            _context.Friends05.Add(friend);
        }
        public void Remove(Friend05 friend)
        {
            _context.Friends05.Remove(friend);
        }
    }
}
