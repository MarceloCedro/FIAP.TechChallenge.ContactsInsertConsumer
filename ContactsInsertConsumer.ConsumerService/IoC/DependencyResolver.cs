﻿using FIAP.TechChallenge.ContactsInsertConsumer.Application;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain;
using FIAP.TechChallenge.ContactsInsertConsumer.Infrastructure;

namespace FIAP.TechChallenge.ContactsInsertConsumer.ConsumerService.IoC
{
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services, string connectionString)
        {
            services.AddRepositoriesDependency();
            services.AddDbContextDependency(connectionString);
            services.AddServicesDependency();
            services.AddApplicationDependency();
        }
    }
}
