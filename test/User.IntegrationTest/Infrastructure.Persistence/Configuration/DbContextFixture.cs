using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Modules;
using DotNet.Testcontainers.Containers.WaitStrategies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using User.Infrastructure.Persistence;
using Xunit;

namespace User.IntegrationTest.Infrastructure.Persistence.Configuration
{
    public class DbContextFixture : IAsyncLifetime
    {
        private DbContextOptions<ApplicationDbContext> _appDbContextOptions;
        private TestcontainersContainer _testContainer;
        private string _appDbConnectionString;

        public DbContextOptions<ApplicationDbContext> AppDbContextDbOptions { get { return _appDbContextOptions; } }
        public string AppDbConnectionString { get { return _appDbConnectionString; } }
        public DbContextFixture()
        {
        }

        public async Task InitializeAsync()
        {
            var testcontainersBuilder = new TestcontainersBuilder<TestcontainersContainer>()
                                              .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
                                              .WithPortBinding(1433, true)
                                              .WithEnvironment("ACCEPT_EULA", "Y")
                                              .WithEnvironment("SA_PASSWORD", ")dvr@8BMu!^t?4-U")
                                              .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433));

            _testContainer = testcontainersBuilder.Build();


            await _testContainer.StartAsync();

            DbContextOptionsBuilder<ApplicationDbContext> appDbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            string connectionString = string.Format("Server=tcp:localhost,{0};Database=UserStore;User Id=sa;Password=)dvr@8BMu!^t?4-U", _testContainer.GetMappedPublicPort(1433));

            _appDbConnectionString = connectionString;

            appDbContextOptionsBuilder.UseSqlServer(connectionString,
                       sqlServerOptionsAction: sqlOptions =>
                       {
                           sqlOptions.MigrationsAssembly(typeof(User.Infrastructure.Persistence.ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name);
                       });

            _appDbContextOptions = appDbContextOptionsBuilder.Options;
        }

        public async Task DisposeAsync()
        {
            if (_testContainer != null)
            {
                await _testContainer.DisposeAsync();
            }
        }
    }

    [CollectionDefinition("DbContext collection", DisableParallelization = true)]
    public class DatabaseCollection : ICollectionFixture<DbContextFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
