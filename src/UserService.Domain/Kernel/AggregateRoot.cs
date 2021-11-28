using System.Collections.Generic;

namespace UserService.Domain.Kernel
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    {
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        private List<IDomainEvent> _domainEvents;

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(domainEvent);
        }
        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
