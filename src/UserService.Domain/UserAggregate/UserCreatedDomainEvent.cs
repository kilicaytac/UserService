using MediatR;
using System;
using UserService.Domain.Kernel;

namespace UserService.Domain.UserAggregate
{
    public class UserCreatedDomainEvent : IDomainEvent, INotification
    {
        [NonSerialized]
        private User _user;
        public string FirstName { get => _user.FirstName; }
        public string LastName { get => _user.LastName; }
        public string Email { get => _user.Email; }
        public string AggregateId { get => _user.Id.ToString(); }

        public UserCreatedDomainEvent(User user)
        {
            _user = user;
        }
    }
}
