using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using User.Domain.OutboxAggregate;

namespace User.Infrastructure.Persistence.OutboxAggregate.EntityConfigurations
{
    public class IntegrationEventEntityTypeConfiguration : IEntityTypeConfiguration<IntegrationEvent>
    {
        public void Configure(EntityTypeBuilder<IntegrationEvent> builder)
        {
            builder.ToTable("IntegrationEvents");

            builder.HasKey(o => o.Id);
            builder.Property(p => p.Id).HasDefaultValueSql("NEWID()");

            builder.Property(o => o.AggregateType).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(o => o.AggregateId).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(o => o.EventType).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(o => o.Payload).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
