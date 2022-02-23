using Domain.LUX.Entities;
using Microsoft.Azure.Documents;

namespace Infra.Repositories
{
    public interface IDocumentCollectionContext<in T> where T : Entidade
    {
        string CollectionName { get; }

        PartitionKey ResolvePartitionKey(string entityId);
    }
}
