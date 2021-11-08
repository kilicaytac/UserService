using Microsoft.EntityFrameworkCore;
using User.Infrastructure.Persistence.UserAggregate.EntityConfigurations;
using User.Infrastructure.Persistence.OutboxAggregate.EntityConfigurations;

namespace User.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User.Domain.UserAggregate.User> Users { get; set; }
        public DbSet<User.Domain.OutboxAggregate.IntegrationEvent> IntegrationEvents { get; set; }

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
