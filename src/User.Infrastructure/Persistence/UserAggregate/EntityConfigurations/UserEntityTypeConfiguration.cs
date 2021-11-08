using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User.Infrastructure.Persistence.UserAggregate.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User.Domain.UserAggregate.User>
    {
        public void Configure(EntityTypeBuilder<User.Domain.UserAggregate.User> builder)
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
