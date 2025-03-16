using FIAP.TechChallenge.ContactsInsertProducer.Domain.DTOs.EntityDTOs;

namespace FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Applications
{
    public interface IContactApplication
    {
        Task AddContactAsync(ContactDto contactCreate);
    }
}