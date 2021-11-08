using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Infrastructure.Persistence;
using User.Infrastructure.Persistence.UserAggregate.Repositories;
using User.IntegrationTest.Infrastructure.Persistence.Configuration;
using Xunit;

namespace User.IntegrationTest.Infrastructure.Persistence
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

        public async Task InitializeAsync()
        {
            _applicationDbContext = new ApplicationDbContext(_dbContextFixture.AppDbContextDbOptions);
            _applicationDbContext.Database.EnsureCreated();
            _efUnitOfWork = new EfUnitOfWork(_applicationDbContext);
            _userEfRepository = new UserEfRepository(_applicationDbContext);
        }

        public async Task DisposeAsync()
        {
            _efUnitOfWork.Dispose();
            _applicationDbContext.Database.EnsureDeleted();
            await _applicationDbContext.DisposeAsync();
        }
    }
}
