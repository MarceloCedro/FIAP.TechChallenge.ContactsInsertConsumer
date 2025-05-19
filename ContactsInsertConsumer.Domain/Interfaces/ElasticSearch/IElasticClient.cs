using Elastic.Clients.Elasticsearch;

namespace FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.ElasticSearch
{
    public interface IElasticClient<T>
    {
        Task<bool> Create(T log, IndexName index);
    }
}
