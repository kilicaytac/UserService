
namespace UserService.Domain.Kernel
{
    public interface IDomainEvent
    {
        public string AggregateId { get; }
    }
}
