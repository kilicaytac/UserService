using System.Linq;
using UserService.Domain.Kernel;
using UserService.Domain.UserAggregate;
using Xunit;

namespace UserService.UnitTest.Domain.UserAggregate
{
    public class UserTest
    {
        public UserTest()
        {

        }

        [Fact]
        public void Constructor_SetFields_WhenParametersAreValid()
        {
            //Arrange
            string firstName = "Aytaç";
            string lastName = "Kılıç";
            string email = "kilicaytac@hotmail.com";
            string password = "mystrongpass";

            //Act
            var user = new User(firstName, lastName, email, password);

            //Assert
            Assert.Equal(firstName, user.FirstName);
            Assert.Equal(lastName, user.LastName);
            Assert.Equal(email, user.Email);
            Assert.Equal(password, user.Password);
        }

        [Fact]
        public void Constructor_AddsUserCreatedDomainEventToDomainEvents_WhenParametersAreValid()
        {
            //Arrange
            //Act
            var user = new User("Aytaç", "Kılıç", "kilicaytac@hotmail.com", "mystrongpass");

            //Assert
            Assert.NotNull(user.DomainEvents);
            Assert.Equal(1, user.DomainEvents.Count);
            IDomainEvent domainEvent = user.DomainEvents.FirstOrDefault();
            Assert.True(domainEvent is UserCreatedDomainEvent);
            UserCreatedDomainEvent userCreatedDomainEvent = (UserCreatedDomainEvent)domainEvent;
            Assert.Equal(user.FirstName, userCreatedDomainEvent.FirstName);
            Assert.Equal(user.LastName, userCreatedDomainEvent.LastName);
            Assert.Equal(user.Email, userCreatedDomainEvent.Email);
        }
    }
}
