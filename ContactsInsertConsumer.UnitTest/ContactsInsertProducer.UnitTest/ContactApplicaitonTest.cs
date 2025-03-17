using FIAP.TechChallenge.ContactsInsertConsumer.Application.Applications;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Entities;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Services;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.DTOs.EntityDTOs;
using Microsoft.Extensions.Logging;
using Moq;

namespace ContactsInsertProducer.UnitTest
{
    public class ContactApplicaitonTest
    {
        private readonly Mock<IContactService> _contactServiceMock;
        private readonly Mock<ILogger<ContactApplication>> _loggerMock;
        private readonly ContactApplication contactApplication;

        public ContactApplicaitonTest()
        {
            _contactServiceMock = new Mock<IContactService>();
            _loggerMock = new Mock<ILogger<ContactApplication>>();

            contactApplication = new ContactApplication(_contactServiceMock.Object,
                                                        _loggerMock.Object);
        }
        
        private ContactDto GetMockedContactDto()
            => new ()
            {
                Name = "Marcelo Cedro",
                Email = "marcel1234ocedro@gmail.com",
                AreaCode = "11",
                Phone = "982840611"
            };

        private async Task SetupDomainServiceAsync(bool exception)
        {
            if (exception)
                _contactServiceMock.Setup(u => u.InsertAsync(It.IsAny<Contact>())).ThrowsAsync(new Exception("Simulated Error"));
            else
                _contactServiceMock.Setup(u => u.InsertAsync(It.IsAny<Contact>()));
        }

        [Fact]
        public async Task AddContactAsyncException()
        {
            await SetupDomainServiceAsync(true);
            var expectedPostContact = GetMockedContactDto();

            Action testCode = () => { };

            var exception = Assert.ThrowsAsync<Exception>(() => contactApplication.AddContactAsync(expectedPostContact));

            Assert.Equal("Simulated Error", exception.Result.Message);
        }

        [Fact]
        public async Task AddContactAsyncSuccess()
        {
            await SetupDomainServiceAsync(false);
            var expectedPostContact = GetMockedContactDto();

            await contactApplication.AddContactAsync(expectedPostContact);

            await VerifyInsertContactAsync(Times.Once());
        }
       
        private async Task VerifyInsertContactAsync(Times times)
        {
            _contactServiceMock.Verify(u => u.InsertAsync(It.IsAny<Contact>()), times);
        }
    }
}