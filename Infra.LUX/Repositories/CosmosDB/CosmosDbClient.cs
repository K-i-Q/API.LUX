using Infra.Repositories;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infra.LUX.Repositories.CosmosDB
{
    public class CosmosDbClient : ICosmosDbClient
    {
        private readonly string _databaseName;
        private readonly string _collectionName;
        private readonly IDocumentClient _documentClient;

        public CosmosDbClient(string databaseName, string collectionName, IDocumentClient documentClient)
        {
            _databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
            _collectionName = collectionName ?? throw new ArgumentNullException(nameof(collectionName));
            _documentClient = documentClient ?? throw new ArgumentNullException(nameof(documentClient));
        }

        public async Task<DocumentCollection> ReadDocumentCollectionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _documentClient.ReadDocumentCollectionAsync(
                UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName)
            );
        }

        public async Task<IList<T>> ReadDocumentByAsync<T>(Expression<Func<T, bool>> predicate)
        {
            var option = new FeedOptions { EnableCrossPartitionQuery = true };

            var response = await _documentClient.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName), option
            ).Where(predicate)
            .AsDocumentQuery()
            .ExecuteNextAsync<T>();

            return response.ToList<T>();
        }

        public async Task<IList<Document>> ReadDocumentBySqlAsync(string query)
        {
            var option = new FeedOptions { EnableCrossPartitionQuery = true };

            var response = await _documentClient.CreateDocumentQuery<Document>(
                UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName), query, option
            ).AsDocumentQuery()
            .ExecuteNextAsync<Document>();

            return response.ToList<Document>();
        }

        public async Task<IList<Document>> ReadDocumentByAsync()
        {
            var option = new FeedOptions { EnableCrossPartitionQuery = true };

            var response = await _documentClient.CreateDocumentQuery<Document>(
                UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName), option
            )
            .AsDocumentQuery()
            .ExecuteNextAsync<Document>();

            return response.ToList<Document>();
        }

        public async Task<Document> ReadDocumentByIdAsync(string documentId, RequestOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {

            var response = await _documentClient.ReadDocumentAsync(
            UriFactory.CreateDocumentUri(_databaseName, _collectionName, documentId), options, cancellationToken);


            return response;
        }

        public async Task<Document> CreateDocumentAsync(object document, RequestOptions options = null,
            bool disableAutomaticIdGeneration = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _documentClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName), document, options,
                disableAutomaticIdGeneration, cancellationToken);

            if (response.StatusCode != HttpStatusCode.Created)
                throw new Exception($"Ocorreu Ocorreu o HTTP Erro {response.StatusCode} no método {MethodBase.GetCurrentMethod().Name}");

            return response;
        }

        public async Task<Document> ReplaceDocumentAsync(string documentId, object document,
            RequestOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var documentCollectionUri = UriFactory.CreateDocumentUri(_databaseName, _collectionName, documentId);
            var response = await _documentClient.UpsertDocumentAsync(documentCollectionUri, document);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Ocorreu Ocorreu o HTTP Erro {response.StatusCode} no método {MethodBase.GetCurrentMethod().Name}");

            return response;
        }

        public async Task<Document> SaveDocumentAsync(object document,
            RequestOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var documentCollectionUri = UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName);
            var response = await _documentClient.UpsertDocumentAsync(documentCollectionUri, document, options);

            if (response.StatusCode != HttpStatusCode.Created && response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Ocorreu Ocorreu o HTTP Erro {response.StatusCode} no método {MethodBase.GetCurrentMethod().Name}");

            return response;
        }

        public async Task<Document> DeleteDocumentAsync(string documentId, RequestOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _documentClient.DeleteDocumentAsync(
                UriFactory.CreateDocumentUri(_databaseName, _collectionName, documentId), options, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Ocorreu Ocorreu o HTTP Erro {response.StatusCode} no método {MethodBase.GetCurrentMethod().Name}");

            return response;
        }

    }
}
