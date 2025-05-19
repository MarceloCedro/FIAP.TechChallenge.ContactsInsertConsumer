using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using FIAP.TechChallenge.ContactsInsertConsumer.Domain.Interfaces.ElasticSearch;

namespace FIAP.TechChallenge.ContactsInsertConsumer.Infrastructure.ElasticSearch
{
    public class ElasticClient<T>(IElasticSettings settings) : IElasticClient<T>
    {
        private readonly ElasticsearchClient _client = new ElasticsearchClient(settings.CloudId, new ApiKey(settings.ApiKey));

        public async Task<bool> Create(T log, IndexName index)
        {
            var response = await _client.IndexAsync(log, index);

            return response.IsValidResponse;
        }
    }
}
