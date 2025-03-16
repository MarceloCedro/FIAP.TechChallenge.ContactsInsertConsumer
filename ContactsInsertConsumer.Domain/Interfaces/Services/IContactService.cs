using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Entities;

namespace FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Services
{
    public interface IContactService
    {
        Task InsertAsync(Contact contact);
    }
}