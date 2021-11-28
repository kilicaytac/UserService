using UserService.Domain.UserAggregate;

namespace UserService.Infrastructure.Persistence.UserAggregate.Repositories
{
    public class UserEfRepository: EfRepository<ApplicationDbContext, User, int>, IUserRepository
    {
        public UserEfRepository(ApplicationDbContext dbContext) : base(dbContext, false)
        {

        }
    }
}
