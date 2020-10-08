using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendOrganizer.DataAccess05;
using FriendOrganizer.Domain;

namespace FriendOrganizer.DomainServices.Repositories
{
    public class ProgrammingLanguageRepository18 
        : GenericRepository<ProgrammingLanguage12, FriendOrganizerDbContext>,
        IProgrammingLanguageRepository18
    {
        public ProgrammingLanguageRepository18(FriendOrganizerDbContext context) 
            : base(context)
        {
        }

        public async Task<bool> IsReferencedByFriendAsync(int programmingLanguageId)
        {
            return await Context.Friends15.AsNoTracking()
                .AnyAsync(f => f.FavoriteLanguageId == programmingLanguageId);
        }
    }
}
