using Domain.LUX.Entities;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public interface IBaseCosmos<T> where T : Entidade
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetItemAsync(string itemId, string partitionKeyValue);

        Task<IEnumerable<T>> QueryAsync(QueryDefinition query, string? partitionKeyValue = null, QueryRequestOptions? queryRequestOptions = null);

        Task<T> UpsertItemAsync(T entity, string partitionKeyValue);
    }
}
