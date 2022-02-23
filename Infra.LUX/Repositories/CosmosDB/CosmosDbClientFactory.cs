using Infra.Repositories;
using Microsoft.Azure.Documents;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.LUX.Repositories.CosmosDB
{
    public class CosmosDbClientFactory : ICosmosDbClientFactory
    {
        private readonly CosmosDBOptions _cosmosDBOptions;
        private readonly IDocumentClient _documentClient;

        public CosmosDbClientFactory(CosmosDBOptions cosmosDBOptions, IDocumentClient documentClient)
        {
            _cosmosDBOptions = cosmosDBOptions ?? throw new ArgumentNullException(nameof(cosmosDBOptions));
            _documentClient = documentClient ?? throw new ArgumentNullException(nameof(documentClient));
        }

        public ICosmosDbClient GetClient(string collectionName)
        {
            if (!_cosmosDBOptions.Collections.Any(_ => _.Name.Equals(collectionName)))
            {
                throw new ArgumentException($"Unable to find collection: {collectionName}");
            }
            return new CosmosDbClient(_cosmosDBOptions.DatabaseName, collectionName, _documentClient);
        }

        public async Task EnsureDbSetupAsync()
        {
            var created = await _documentClient.CreateDatabaseIfNotExistsAsync(new Database { Id = _cosmosDBOptions.DatabaseName });

            foreach (var collection in _cosmosDBOptions.Collections)
            {
                await _documentClient.CreateDocumentCollectionIfNotExistsAsync
                (
                    databaseLink: created.Resource.AltLink,
                    documentCollection: new DocumentCollection
                    {
                        Id = collection.Name,
                        PartitionKey = new PartitionKeyDefinition { Paths = { collection.PartitionKeyPath } }
                    }
                );
            }

        }
    }
}
