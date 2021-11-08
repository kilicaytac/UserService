using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.Kernel;

namespace User.Infrastructure.Persistence
{
    public class EfRepository<TDbContext, TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
                                                                                   where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        private readonly DbSet<TEntity> _dbSet;
        protected bool AutoFlushEnabled { get; set; }
        protected TDbContext DbContext { get { return _dbContext; } }
        protected DbSet<TEntity> DbSet { get { return _dbSet; } }

        public EfRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public EfRepository(TDbContext dbContext, bool autoFlushEnabled) : this(dbContext)
        {
            AutoFlushEnabled = autoFlushEnabled;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Add(entity);

            if (AutoFlushEnabled)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return entity;
        }
        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            if (AutoFlushEnabled)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return entity;
        }
        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;

            if (AutoFlushEnabled)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        public virtual async Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }
    }
}
