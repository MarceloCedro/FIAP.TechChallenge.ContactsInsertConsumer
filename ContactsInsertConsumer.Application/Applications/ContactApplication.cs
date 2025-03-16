using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Entities;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Applications;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Services;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.DTOs.EntityDTOs;
using Microsoft.Extensions.Logging;

namespace FIAP.TechChallenge.ContactsInsertConsumer.Application.Applications
{
    public class ContactApplication(IContactService contactService,
                                    ILogger<ContactApplication> logger) : IContactApplication
    {
        private readonly IContactService _contactService = contactService;
        private readonly ILogger<ContactApplication> _logger = logger;

        public async Task AddContactAsync(ContactDto contactDto)
        {
            try
            {
                var contact = new Contact
                {
                    Name = contactDto.Name,
                    Email = contactDto.Email,
                    AreaCode = contactDto.AreaCode,
                    Phone = contactDto.Phone
                };

                await _contactService.InsertAsync(contact);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception(e.Message);
            }
        }
    }
}