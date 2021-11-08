using User.Domain.Kernel;

namespace User.Domain.UserAggregate
{
    public interface IUserRepository : IRepository<User, int>
    {
    }
}
