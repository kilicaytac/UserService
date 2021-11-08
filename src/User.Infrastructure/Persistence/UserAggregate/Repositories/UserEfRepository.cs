using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.UserAggregate;

namespace User.Infrastructure.Persistence.UserAggregate.Repositories
{
    public class UserEfRepository: EfRepository<ApplicationDbContext, User.Domain.UserAggregate.User, int>, IUserRepository
    {
        public UserEfRepository(ApplicationDbContext dbContext) : base(dbContext, false)
        {

        }
    }
}
