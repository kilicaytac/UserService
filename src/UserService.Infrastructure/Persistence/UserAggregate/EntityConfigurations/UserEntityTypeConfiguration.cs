using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.UserAggregate;

namespace UserService.Infrastructure.Persistence.UserAggregate.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.FirstName).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(o => o.LastName).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(o => o.Email).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(o => o.Password).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
