using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Entities;

namespace FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Repositories
{
    public interface IContactRepository
    {
        Task AddAsync(Contact contact);
    }
}
