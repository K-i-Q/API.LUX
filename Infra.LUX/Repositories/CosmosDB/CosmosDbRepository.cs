using Domain.LUX.Entities;
using Infra.LUX.Exceptions;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace Infra.Repositories.CosmosDB
{
    public abstract class CosmosDbRepository<T> : IRepository<T>, IDocumentCollectionContext<T> where T : Entidade
    {
        private readonly ICosmosDbClientFactory _cosmosDbClientFactory;
        RequestOptions requestoptions = new RequestOptions();

        protected CosmosDbRepository(ICosmosDbClientFactory cosmosDbClientFactory)
        {
            _cosmosDbClientFactory = cosmosDbClientFactory;

            StringEnumConverter stringEnumConverter = new StringEnumConverter();
            List<Newtonsoft.Json.JsonConverter> converters = new List<Newtonsoft.Json.JsonConverter>();
            converters.Add(stringEnumConverter);
            requestoptions.JsonSerializerSettings = new JsonSerializerSettings
            {
                Converters = converters, 
                ContractResolver = new CamelCasePropertyNamesContractResolver(), 
                NullValueHandling = NullValueHandling.Ignore
            };
            requestoptions.DisableRUPerMinuteUsage = true;
           
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                return await cosmosDbClient.ReadDocumentByAsync<T>(predicate);

            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException();
                }

                throw;
            }
        }

        public async Task<IList<T>> GetAll(string sql)
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                var document = await cosmosDbClient.ReadDocumentBySqlAsync(sql);
                var arrayDocument = string.Join<Document>(",", document.ToList());
                return JsonConvert.DeserializeObject<IList<T>>($"[ {arrayDocument} ]");
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException();
                }

                throw;
            }
        }

        public async Task<IList<TResult>> GetAll<TResult>(string sql)
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                var document = await cosmosDbClient.ReadDocumentBySqlAsync(sql);
                var arrayDocument = string.Join<Document>(",", document.ToList());
                var retorno = JsonConvert.DeserializeObject<IList<TResult>>($"[ {arrayDocument} ]");
                return retorno;

            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException();
                }

                throw;
            }
        }

        public async Task<T> GetByIdAsync(string id)
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                var document = await cosmosDbClient.ReadDocumentByIdAsync(id, new RequestOptions
                {
                    PartitionKey = ResolvePartitionKey(id)
                });

                return JsonConvert.DeserializeObject<T>(document.ToString());
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException();
                }

                throw ;
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {

                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                var document = await cosmosDbClient.CreateDocumentAsync(entity, requestoptions);
                return JsonConvert.DeserializeAnonymousType(document.ToString(), entity);
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new EntityAlreadyExistsException();
                }

                throw;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> UpsertAsync(T entity)
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                var document = await cosmosDbClient.SaveDocumentAsync(entity, requestoptions);
                if (document.Id == entity.Id)
                    return true;

                return false;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);

                var documento = await cosmosDbClient.ReadDocumentByIdAsync(entity.Id, new RequestOptions
                {
                    PartitionKey = ResolvePartitionKey(entity.Id)
                });


                if (string.IsNullOrEmpty(documento.Id))
                    return false;


                await cosmosDbClient.DeleteDocumentAsync(entity.Id, new RequestOptions
                {
                    PartitionKey = ResolvePartitionKey(entity.Id)
                });

                return true;

            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException();
                }

                throw;
            }
        }

        public abstract string CollectionName { get; }

        public virtual PartitionKey ResolvePartitionKey(string entityId)  => new PartitionKey(entityId);

    }
}
