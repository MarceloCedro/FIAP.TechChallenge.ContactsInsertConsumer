using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Applications;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.DTOs.EntityDTOs;
using MassTransit;

namespace ContactsInsertConsumer.Api.Events
{
    public class ContactInsertConsumer : IConsumer<ContactDto>
    {
        private readonly IContactApplication _contactApplication;
        private Timer _timer;

        public ContactInsertConsumer(IContactApplication contactApplication)
        {
            _contactApplication = contactApplication;
        }

        public Task Consume(ConsumeContext<ContactDto> context)
        {
            try
            {
                if (context.Message != null)
                {
                    return _contactApplication.AddContactAsync(context.Message);
                }
                else
                    throw new Exception("Message cannot be null or empty.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
