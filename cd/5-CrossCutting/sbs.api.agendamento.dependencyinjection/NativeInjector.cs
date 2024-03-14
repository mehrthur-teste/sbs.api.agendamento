using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sbs.api.agendamento.aplicacao.Services;
using sbs.api.agendamento.dominio.Interface;
using sbs.api.agendamento.repository.Configuration;
using sbs.api.agendamento.repository.Repository;

namespace sbs.api.agendamento.dependencyinjection
{
    public static class NativeInjector
    {

        public static void RegistrarDependencias(this IServiceCollection services, DbConfiguration _dbConfiguration)
        {
            Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos
                                                                 .CosmosClient(
                                                                       _dbConfiguration.Account, _dbConfiguration.Key);

            #region Aplicacao         
            services.AddSingleton<IBilheteIdentidadeServices,BilheteIdentidadeServices>();
            services.AddSingleton<IAgendamentoServices, AgendamentoServices>();
            #endregion


            #region Infra 
            services.AddSingleton<IAgendamentoRepository>(
                                                          InitializeCosmosClientAgendamentoAsync(
                                                                                                 _dbConfiguration,
                                                                                                 client
                                                                                                 ).GetAwaiter().GetResult());
            #endregion

        }

        private static async Task<AgendamentoRepository> InitializeCosmosClientAgendamentoAsync(
                                                                                               DbConfiguration _dbConfiguration,
                                                                                               Microsoft.Azure.Cosmos.CosmosClient client
                                                                                              )
        {
            AgendamentoRepository cosmosDbService = new AgendamentoRepository(client, _dbConfiguration.DatabaseName, _dbConfiguration.ContainerName);
            Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(_dbConfiguration.DatabaseName);
            await database.Database.CreateContainerIfNotExistsAsync(_dbConfiguration.ContainerName, "/id");
            return cosmosDbService;
        }

    }
}
