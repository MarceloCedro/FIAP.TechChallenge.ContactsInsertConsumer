using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Entities;

namespace FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Applications
{
    public interface ITokenApplication
    {
        public string GetToken(User usuario);
    }
}
