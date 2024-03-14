using sbs.api.agendamento.dominio.Interface;
using sbs.api.agendamento.repository.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using sbs.api.agendamento.dominio.Models;

namespace sbs.api.agendamento.repository.Repository
{
    public class AgendamentoRepository: IAgendamentoRepository
    {
       // private readonly DbConfiguration? _dbConfiguration;
        private Container? _container;
        public AgendamentoRepository(//DbConfiguration? dbConfiguration,
                                    CosmosClient? dbClient,
                                    string databaseName,
                                    string containerName)
        {
           // _dbConfiguration = dbConfiguration;
            //_dbConfiguration.DatabaseName = "CONSULADO-ANGOLA-SP";
            //_dbConfiguration.ContainerName = "AGENDAMENTO";
            _container = dbClient?.GetContainer(databaseName,
                                                containerName);
        }

        public async Task AddItemAsync(Agendamento item)
        {
            await _container.CreateItemAsync<Agendamento>(item, new PartitionKey(item.id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await _container.DeleteItemAsync<Agendamento>(id, new PartitionKey(id));
        }

        public async Task<Agendamento> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Agendamento> response = await _container.ReadItemAsync<Agendamento>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Agendamento>> GetItemsAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Agendamento>(new QueryDefinition(queryString));
            List<Agendamento> results = new List<Agendamento>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, Agendamento item)
        {
            await _container.UpsertItemAsync<Agendamento>(item, new PartitionKey(id));
        }

    }
}
