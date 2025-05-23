﻿using FIAP.TechChallenge.ContactsInsertConsumer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FIAP.TechChallenge.ContactsInsertConsumer.Infrastructure.Data
{
    public class ContactsDbContextFactory : IDesignTimeDbContextFactory<ContactsDbContext>
    {
        public ContactsDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ContactsDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)),
                                    mySqlOptions => mySqlOptions.MigrationsAssembly("FIAP.TechChallenge.ContactsInsertConsumer.Infrastructure"));

            return new ContactsDbContext(optionsBuilder.Options);
        }
    }
}
