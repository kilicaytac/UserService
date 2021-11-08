using System.Threading;
using System.Threading.Tasks;

namespace User.Domain.Kernel
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    }
}
