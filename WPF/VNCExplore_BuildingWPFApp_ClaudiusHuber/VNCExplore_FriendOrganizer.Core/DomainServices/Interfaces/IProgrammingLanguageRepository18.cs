using System.Threading.Tasks;
using FriendOrganizer.Domain;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.DomainServices.Repositories
{
    public interface IProgrammingLanguageRepository18
        : IGenericRepository<ProgrammingLanguage12>
    {
        Task<bool> IsReferencedByFriendAsync(int programmingLanguageId);
    }
}
