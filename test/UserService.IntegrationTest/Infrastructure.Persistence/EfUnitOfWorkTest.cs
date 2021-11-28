using System.Threading.Tasks;
using UserService.Infrastructure.Persistence;
using UserService.Infrastructure.Persistence.UserAggregate.Repositories;
using UserService.IntegrationTest.Infrastructure.Persistence.Configuration;
using Xunit;

namespace UserService.IntegrationTest.Infrastructure.Persistence
{
    [Collection("DbContext collection")]
    public class EfUnitOfWorkTest : IAsyncLifetime
    {
        private readonly DbContextFixture _dbContextFixture;
        private ApplicationDbContext _applicationDbContext;
        private EfUnitOfWork _efUnitOfWork;
        private UserEfRepository _userEfRepository;
        public EfUnitOfWorkTest(DbContextFixture dbContextFixture)
        {
            _dbContextFixture = dbContextFixture;
        }

        public Task InitializeAsync()
        {
            _applicationDbContext = new ApplicationDbContext(_dbContextFixture.AppDbContextDbOptions);
            _applicationDbContext.Database.EnsureCreated();
            _efUnitOfWork = new EfUnitOfWork(_applicationDbContext);
            _userEfRepository = new UserEfRepository(_applicationDbContext);

            return Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            _efUnitOfWork.Dispose();
            _applicationDbContext.Database.EnsureDeleted();
            await _applicationDbContext.DisposeAsync();
        }
    }
}
