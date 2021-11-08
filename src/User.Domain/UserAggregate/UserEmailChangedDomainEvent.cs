using MediatR;
using User.Domain.Kernel;

namespace User.Domain.UserAggregate
{
    public class UserEmailChangedDomainEvent : IDomainEvent, INotification
    {
        public string Email { get; set; }
        public string AggregateId { get; }
        public UserEmailChangedDomainEvent(int userId, string email)
        {
            AggregateId = userId.ToString();
            Email = email;
        }
    }
}
