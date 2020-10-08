using System.Collections.Generic;
using System.Threading.Tasks;
using FriendOrganizer.Domain;

namespace VNCExplore_FriendOrganizer.Core.DomainServices
{
    public interface IProgrammingLanguageLookupDataService12
    {
        Task<IEnumerable<LookupItem>> GetProgrammingLanguageLookupAsync();
    }
}