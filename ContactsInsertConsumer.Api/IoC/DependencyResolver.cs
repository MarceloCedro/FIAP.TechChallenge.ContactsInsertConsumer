using FIAP.TechChallenge.ContactsInsertConsumer.Application;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain;
using FIAP.TechChallenge.ContactsInsertConsumer.Infrastructure;

namespace ContactsInsertConsumer.Api.IoC
{
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositoriesDependency(configuration);
            services.AddDbContextDependency(configuration.GetConnectionString("DefaultConnection"));
            services.AddServicesDependency();
            services.AddApplicationDependency();
        }
    }
}
