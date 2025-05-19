using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.ElasticSearch;

namespace FIAP.TechChallenge.ContactsInsertConsumer.Infrastructure.ElasticSearch
{
    public class ElasticSettings : IElasticSettings
    {
        public string ApiKey { get; set; }

        public string CloudId { get; set; }
    }
}
