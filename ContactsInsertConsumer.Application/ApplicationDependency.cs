using FIAP.TechChallenge.ContactsInsertConsumer.Application.Applications;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Applications;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.TechChallenge.ContactsInsertConsumer.Application
{
    public static class ApplicationDependency
    {
        public static IServiceCollection AddApplicationDependency(this IServiceCollection service)
        {
            service.AddScoped<IContactApplication, ContactApplication>();

            return service;
        }
    }
}
