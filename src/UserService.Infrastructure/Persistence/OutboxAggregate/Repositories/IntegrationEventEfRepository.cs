using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.OutboxAggregate;

namespace UserService.Infrastructure.Persistence.OutboxAggregate.Repositories
{
    public class IntegrationEventEfRepository : EfRepository<ApplicationDbContext, IntegrationEvent, Guid>, IIntegrationEventRepository
    {
        public IntegrationEventEfRepository(ApplicationDbContext dbContext) : base(dbContext, false)
        {

        }
    }
}
