using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Applications;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.DTOs.EntityDTOs;
using MassTransit;

namespace FIAP.TechChallenge.ContactsInsertConsumer.ConsumerService.Events
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
                   // _timer = new Timer(async state => await _contactApplication.AddContactAsync(context.Message), null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
                    //_contactApplication.AddContactAsync(context.Message).ConfigureAwait(true);
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
