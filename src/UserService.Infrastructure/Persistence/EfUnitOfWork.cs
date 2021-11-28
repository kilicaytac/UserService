using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserService.Domain.Kernel;
using UserService.Domain.OutboxAggregate;

namespace UserService.Infrastructure.Persistence
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private IDbContextTransaction _currentTransaction;
        public EfUnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task BeginAsync(IsolationLevel isolationLevel = IsolationLevel.Unspecified, CancellationToken cancellationToken = default)
        {
            _currentTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public virtual async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);

            var aggregateRoots = _dbContext.ChangeTracker
                .Entries<IAggregateRoot>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());


            foreach (var aggregateRoot in aggregateRoots)
            {
                var domainEvents = aggregateRoot.Entity.DomainEvents;

                foreach (var domainEvent in domainEvents)
                {

                    string aggregateType = aggregateRoot.Entity.GetType().Name;
                    string aggregateId = domainEvent.AggregateId;
                    string eventType = domainEvent.GetType().Name;
                    string payload = JsonConvert.SerializeObject(domainEvent);

                    IntegrationEvent integrationEvent = new IntegrationEvent(aggregateType, aggregateId, eventType, payload);

                    await _dbContext.IntegrationEvents.AddAsync(integrationEvent);
                }

                aggregateRoot.Entity.ClearDomainEvents();
            }

            await _dbContext.SaveChangesAsync();
                
            await _currentTransaction.CommitAsync(cancellationToken);
        }

        public virtual async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            await _currentTransaction.RollbackAsync(cancellationToken);
        }

        public virtual void Dispose()
        {
            _currentTransaction?.Dispose();
        }
    }
}
