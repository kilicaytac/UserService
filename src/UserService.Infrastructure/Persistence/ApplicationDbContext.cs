using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Persistence.UserAggregate.EntityConfigurations;
using UserService.Infrastructure.Persistence.OutboxAggregate.EntityConfigurations;
using UserService.Domain.UserAggregate;
using UserService.Domain.OutboxAggregate;

namespace UserService.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<IntegrationEvent> IntegrationEvents { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new IntegrationEventEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}
