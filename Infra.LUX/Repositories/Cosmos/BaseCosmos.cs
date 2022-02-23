using Domain.LUX.Entities;
using Infra.LUX.Repositories;
using Infra.LUX.Repositories.Cosmos;
using Infra.Repositories;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Infra.LUX.Repositories.Cosmos
{
    public class BaseCosmos<T> : IBaseCosmos<T> where T : Entidade
    {
        public virtual string? COLLECTION_NAME { get; }

        protected readonly Container _container;

        protected BaseCosmos(CosmosSettings settings, CosmosClient client)
        {
            if (settings == null)
                throw new ArgumentException(nameof(settings));
            if (string.IsNullOrEmpty(settings.DatabaseName) || string.IsNullOrWhiteSpace(settings.DatabaseName))
                throw new ArgumentException(nameof(settings.DatabaseName));
            if (settings.Collections.Where(c => c.Name == COLLECTION_NAME).FirstOrDefault() == null)
                throw new ArgumentException(nameof(COLLECTION_NAME));

            _container = client.GetContainer(settings.DatabaseName, COLLECTION_NAME);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var results = new List<T>();

            var query = new QueryDefinition("SELECT * FROM c");

            var feedIterator = _container.GetItemQueryIterator<T>(query);

            while (feedIterator.HasMoreResults)
            {
                var response = await feedIterator.ReadNextAsync();
                results.AddRange(response.Resource);
            }

            return results;
        }

        /// <summary>
        /// Retorna o primeiro objeto da consulta, ou default caso não encontre.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade para consulta</typeparam>
        /// <param name="itemId">Valor do id do item</param>
        /// <param name="partitionKeyValue">Valor da chave de partição do item</param>
        /// <returns>Primeiro item encontrado, ou default se não encontrar.</returns>
        public async Task<T> GetItemAsync(string itemId, string partitionKeyValue)
        {
            try
            {
                var response = await _container.ReadItemAsync<T>(itemId, new PartitionKey(partitionKeyValue));

                return response.Resource!;
            }
            catch (CosmosException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    return null;

                throw;
            }
        }

        /// <summary>
        /// Retorna os resultados da consulta.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade para consulta</typeparam>
        /// <param name="query">Definição da consulta</param>
        /// <param name="partitionKeyValue">Valor da chave de partição para a consulta</param>
        /// <param name="queryRequestOptions">Opções para a query</param>
        /// <returns>Se a consulta foi com sucesso, retorna uma lista com os dados recuperados.
        /// Se houve erro na consulta, retorna uma lista vazia.</returns>
        public async Task<IEnumerable<T>> QueryAsync(QueryDefinition query, string? partitionKeyValue = null, QueryRequestOptions? queryRequestOptions = null)
        {
            var results = new List<T>();

            var feedIterator = _container.GetItemQueryIterator<T>(
                query, null, queryRequestOptions ?? GetQueryRequestOptions(partitionKeyValue)
            );

            while (feedIterator.HasMoreResults)
            {
                var response = await feedIterator.ReadNextAsync();
                results.AddRange(response.Resource);
            }

            return results;
        }

        /// <summary>
        /// Altera totalmente um novo objeto na base de dados
        /// </summary>
        /// <typeparam name="T">Tipo do item</typeparam>
        /// <param name="item">Item a ser alterado (atualização sempre total, nunca parcial)</param>
        /// <param name="partitionKeyValue">Valor da chave de partição do item a ser alterado</param>
        /// <param name="itemId">Id do item a ser alterado</param>
        /// <param name="itemRequestOptions">Opções para o update (exemplo: triggers a serem ativados, consistência customizada)</param>
        /// <returns>True se a atualização foi com sucesso, false, se houve erro.</returns>
        public async Task<T> UpsertItemAsync(T entity, string partitionKeyValue)
        {
            try
            {
                await _container.UpsertItemAsync(entity, new PartitionKey(partitionKeyValue));

                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region Metodos Auxiliares

        public QueryRequestOptions GetQueryRequestOptions(string? partitionKeyValue, int? maxItemCount = null) =>
           new QueryRequestOptions()
           {
               PartitionKey = partitionKeyValue != null ? new PartitionKey(partitionKeyValue) : PartitionKey.None,
               MaxItemCount = maxItemCount ?? -1,
           };

        #endregion
    }
}
