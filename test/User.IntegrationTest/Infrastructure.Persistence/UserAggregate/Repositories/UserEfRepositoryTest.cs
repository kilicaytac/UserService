using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Infrastructure.Persistence;
using User.Infrastructure.Persistence.UserAggregate.Repositories;
using User.IntegrationTest.Infrastructure.Persistence.Configuration;
using Xunit;

namespace User.IntegrationTest.Infrastructure.Persistence.UserAggregate.Repositories
{
    [Collection("DbContext collection")]
    public class UserEfRepositoryTest : IAsyncLifetime
    {
        private readonly DbContextFixture _dbContextFixture;
        private ApplicationDbContext _applicationDbContext;
        private UserEfRepository _userEfRepository;
        public UserEfRepositoryTest(DbContextFixture dbContextFixture)
        {
            _dbContextFixture = dbContextFixture;
        }

        public async Task InitializeAsync()
        {
            _applicationDbContext = new ApplicationDbContext(_dbContextFixture.AppDbContextDbOptions);
            _applicationDbContext.Database.EnsureCreated();
            _userEfRepository = new UserEfRepository(_applicationDbContext);
        }

        public async Task DisposeAsync()
        {
            _applicationDbContext.Database.EnsureDeleted();
            await _applicationDbContext.DisposeAsync();
        }

        [Fact]
        public async Task InsertAsync_AddUserToUnderlyingDatabase_WhenSchemaIsValid()
        {
            //Arrange         
            var user = new User.Domain.UserAggregate.User("Aytaç", "Kılıç", "kilicaytac@hotmail.com", "!mystrongpassword");

            //Act
            await _userEfRepository.InsertAsync(user);
            await _applicationDbContext.SaveChangesAsync();

            //Assert
            using (ApplicationDbContext applicationDbContext = new ApplicationDbContext(_dbContextFixture.AppDbContextDbOptions))
            {
                var insertedUser = await applicationDbContext.Users.FindAsync(user.Id);

                Assert.NotNull(insertedUser);
                Assert.True(insertedUser.Id > 0);
                Assert.Equal(user.FirstName, insertedUser.FirstName);
                Assert.Equal(user.LastName, insertedUser.LastName);
                Assert.Equal(user.Email, insertedUser.Email);
                Assert.Equal(user.Password, insertedUser.Password);
            }
        }
    }
}
