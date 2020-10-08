using System.Collections.Generic;
using System.Threading.Tasks;

namespace VNCExplore_DDDinPractice.Core.DomainServices
{
    public interface IGenericRepository<TEntity>
    {
        Task<TEntity> FindByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task UpdateAsync();
        bool HasChanges();
        void Add(TEntity model);
        void Remove(TEntity model);
    }
}
