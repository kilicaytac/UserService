using System;
using User.Domain.Kernel;

namespace User.Domain.OutboxAggregate
{
    public interface IIntegrationEventRepository : IRepository<IntegrationEvent, Guid>
    {
    }
}
