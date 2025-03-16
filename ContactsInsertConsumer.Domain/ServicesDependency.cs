using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Services;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.TechChallenge.ContactsInsertConsumer.Domain
{
    public static class ServicesDependency
    {
        public static IServiceCollection AddServicesDependency(this IServiceCollection service)
        {
            service.AddScoped<IContactService, ContactService>();

            return service;
        }
    }
}
