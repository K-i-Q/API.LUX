using Infra.LUX.Repositories.CosmosDB;
using Infra.Repositories;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Infra.LUX.Configurations
{
    public static class CosmosDbConfiguration
    {
        public static IServiceCollection AddCosmosDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var cosmosDBOptions = configuration.GetSection("CosmosDB").Get<CosmosDBOptions>();

            var documentClient = new DocumentClient(cosmosDBOptions.Uri, cosmosDBOptions.PrimaryKey, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Populate,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            documentClient.OpenAsync().Wait();
            var cosmosDbClientFactory = new CosmosDbClientFactory(cosmosDBOptions, documentClient);
            cosmosDbClientFactory.EnsureDbSetupAsync().Wait();
            services.AddSingleton<ICosmosDbClientFactory>(cosmosDbClientFactory);

            return services;
        }
    }
}
