using FIAP.TechChallenge.ContactsInsertConsumer.Application.Applications;
using FIAP.TechChallenge.ContactsInsertConsumer.ConsumerService.Events;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Applications;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Repositories;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Services;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Services;
using FIAP.TechChallenge.ContactsInsertConsumer.Infrastructure.Repositories;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.DTOs.EntityDTOs;
using FIAP.TechChallenge.ContactsInsertProducer.IntegrationTests.Config;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;

namespace FIAP.TechChallenge.ContactsConsult.IntegrationTest.Validations
{
    public class ContactConsumerTest : BaseServiceTests
    {
        private readonly IContactRepository _contactRepository;
        private readonly IContactService _contactService;
        private readonly IContactApplication _contactApplication;
        private readonly ContactInsertConsumer _contactsConsumer;
        
        private Mock<ILogger<ContactService>> _loggerServiceMock;
        private Mock<ILogger<ContactApplication>> _loggerApplicationMock;
        
        public ContactConsumerTest()
        {            
            _loggerServiceMock = new Mock<ILogger<ContactService>>();
            _loggerApplicationMock = new Mock<ILogger<ContactApplication>>();

            _contactRepository = new ContactRepository(_context);

            _contactService = new ContactService(_contactRepository, _loggerServiceMock.Object);
           
            _contactApplication = new ContactApplication(_contactService,
                                                         _loggerApplicationMock.Object);
            
            _contactsConsumer = new ContactInsertConsumer(_contactApplication);
        }

        [Fact]
        public async Task InsertContactEmptyMessageAsync()
        {
            var context = Mock.Of<ConsumeContext<ContactDto>>(_ =>
            _.Message == null);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _contactsConsumer.Consume(context));

            Assert.Equal("Message cannot be null or empty.", exception.Message);
        }

        [Fact]
        public async Task InsertContactSuccessAsync()
        {
            var contactDtoTestObject = ContactFixtures.CreateFakeContactDto(111111);

            var context = Mock.Of<ConsumeContext<ContactDto>>(_ =>
            _.Message == contactDtoTestObject);

            var exception = Record.ExceptionAsync(() => _contactsConsumer.Consume(context));
            Assert.Null(exception.Result);
        }
    }
}
