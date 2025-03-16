using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Entities;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Repositories;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace FIAP.TechChallenge.ContactsInsertConsumer.Domain.Services
{
    public class ContactService(IContactRepository contactRepository, ILogger<ContactService> logger) : IContactService
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly ILogger<ContactService> _logger = logger;

        public async Task InsertAsync(Contact contact)
        {
            try
            {
                await _contactRepository.AddAsync(contact);
            }
            catch (Exception e)
            {
                var message = $"Some error occour when trying to insert new Contact. Error: {e.Message}";
                _logger.LogError(message, e);
                throw new Exception(message);
            }
        }
    }
}