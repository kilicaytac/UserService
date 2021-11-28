using System;
using UserService.Domain.Kernel;

namespace UserService.Domain.OutboxAggregate
{
    public interface IIntegrationEventRepository : IRepository<IntegrationEvent, Guid>
    {
    }
}
