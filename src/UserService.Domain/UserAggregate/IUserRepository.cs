using UserService.Domain.Kernel;

namespace UserService.Domain.UserAggregate
{
    public interface IUserRepository : IRepository<User, int>
    {
    }
}
