using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Kernel;
using User.Domain.UserAggregate;
using Xunit;

namespace User.UnitTest.Domain.UserAggregate
{
    public class UserTest
    {
        public UserTest()
        {

        }

        public void Constructor_SetFields_WhenParametersAreValid()
        {
            //Arrange


            //Act

            //Assert
        }

        [Fact]
        public void Constructor_AddsUserCreatedDomainEventToDomainEvents_WhenParametersAreValid()
        {
            //Arrange
            //Act
            var user = new User.Domain.UserAggregate.User("Aytaç", "Kılıç", "kilicaytac@hotmail.com", "mystrongpass");

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
