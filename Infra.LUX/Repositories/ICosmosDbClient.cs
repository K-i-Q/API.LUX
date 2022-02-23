using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public interface ICosmosDbClient
    {
        Task<IList<Document>> ReadDocumentBySqlAsync(string query);

        Task<IList<T>> ReadDocumentByAsync<T>(Expression<Func<T, bool>> predicate);

        Task<DocumentCollection> ReadDocumentCollectionAsync(
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Document> ReadDocumentByIdAsync(string documentId, RequestOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Document> CreateDocumentAsync(object document, RequestOptions options = null,
            bool disableAutomaticIdGeneration = false,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Document> ReplaceDocumentAsync(string documentId, object document, RequestOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Document> SaveDocumentAsync(object document, RequestOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Document> DeleteDocumentAsync(string documentId, RequestOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
