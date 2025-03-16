using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Repositories;
using FIAP.TechChallenge.ContactsInsertConsumer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FIAP.TechChallenge.ContactsInsertConsumer.Infrastructure.Data;

namespace FIAP.TechChallenge.ContactsInsertConsumer.Infrastructure
{
    public static class DatabaseDependency
    {
        public static IServiceCollection AddRepositoriesDependency(this IServiceCollection service)
        {
            service.AddScoped<IContactRepository, ContactRepository>();

            return service;
        }

        public static IServiceCollection AddDbContextDependency(this IServiceCollection service, string connectionString)
        {
            service.AddDbContext<ContactsDbContext>(options => options.UseMySql(connectionString,
                                                               new MySqlServerVersion(new Version(8, 0, 21)),
                                                               mySqlOptions => mySqlOptions.MigrationsAssembly("FIAP.TechChallenge.ContactsInsertConsumer.Infrastructure")));

            return service;
        }
    }
}
