using User.Domain.Kernel;

namespace User.Domain.UserAggregate
{
    public class User : AggregateRoot<int>
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;
        public string FirstName { get => _firstName; }
        public string LastName { get => _lastName; }
        public string Email { get => _email; }
        public string Password { get => _password; }
        public User(string firstName, string lastName, string email, string password)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _password = password;

            base.AddDomainEvent(new UserCreatedDomainEvent(this));
        }

        public void UpdateInfo(string firstName, string lastName)
        {
            //business rules
            _firstName = firstName;
            _lastName = lastName;

            base.AddDomainEvent(new UserInfoUpdatedDomainEvent(Id, _firstName, _lastName));
        }

        public void ChangeEmail(string email)
        {
            //business rules
            _email = email;
            base.AddDomainEvent(new UserEmailChangedDomainEvent(Id, _email));
        }
        public void ChangePassword(string password)
        {
            _password = password;
            //no need for domain event
        }
    }
}
