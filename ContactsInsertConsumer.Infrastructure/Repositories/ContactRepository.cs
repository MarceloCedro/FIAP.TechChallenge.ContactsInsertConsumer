using FIAP.TechChallenge.ContactsInsertConsumer.Infrastructure.Data;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Entities;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TechChallenge.ContactsInsertConsumer.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository

    {
        private readonly ContactsDbContext _context;

        public ContactRepository(ContactsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }
    }
}