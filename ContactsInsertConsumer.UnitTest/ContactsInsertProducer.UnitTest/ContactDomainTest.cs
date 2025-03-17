using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Entities;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Repositories;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace FIAP.TechChallenge.ContactsInsertProducer.UnitTest
{
    public class ContactDomainTest
    {
        private readonly Mock<IContactRepository> _contactRepository;

        private readonly Mock<ILogger<ContactService>> _loggerMock;

        private readonly ContactService contactService;

        public ContactDomainTest()
        {
            _contactRepository = new Mock<IContactRepository>();
            _loggerMock = new Mock<ILogger<ContactService>>();

            contactService = new ContactService(_contactRepository.Object, _loggerMock.Object);
        }

        private async Task SetupRepositoryServiceAsync(bool exception)
        {
            if (exception)
                _contactRepository.Setup(u => u.AddAsync(It.IsAny<Contact>())).ThrowsAsync(new Exception("Simulated Error"));
            else
                _contactRepository.Setup(u => u.AddAsync(It.IsAny<Contact>()));
        }

        private async Task VerifyInsertContactRepositoryAsync(Times times)
        {
            _contactRepository.Verify(u => u.AddAsync(It.IsAny<Contact>()), times);
        }

        private Contact GetMockedContactDto()
            => new()
            {
                Name = "Marcelo Cedro",
                Email = "marcel1234ocedro@gmail.com",
                AreaCode = "11",
                Phone = "982840611"
            };

        [Fact]
        public async Task AddContactAsyncRepositoryException()
        {
            await SetupRepositoryServiceAsync(true);
            var expectedPostContact = GetMockedContactDto();
            Action testCode = () => { };

            var exception = Assert.ThrowsAsync<Exception>(() => contactService.InsertAsync(expectedPostContact));

            Assert.Equal("Some error occour when trying to insert new Contact. Error: Simulated Error", exception.Result.Message);
        }

        [Fact]
        public async Task AddContactAsyncRepositorySuccess()
        {
            await SetupRepositoryServiceAsync(false);
            var expectedPostContact = GetMockedContactDto();

            await contactService.InsertAsync(expectedPostContact);

            await VerifyInsertContactRepositoryAsync(Times.Once());
        }
    }
}
