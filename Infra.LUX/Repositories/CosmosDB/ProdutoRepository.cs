using Domain.LUX.Entities;
using Infra.LUX.Repositories.CosmosDB;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repositories.CosmosDB
{
    public class ProdutoRepository : CosmosDbRepository<Produto>, IProdutoRepository
    {
        public override string CollectionName { get; } = "";
        private string Uri;
        private string PrimaryKey;
        public ProdutoRepository(ICosmosDbClientFactory factory, IConfiguration configuration) : base(factory)
        {
            var Collections = configuration.GetSection("CosmosDb").Get<CosmosDBOptions>().Collections;

            CollectionName = configuration.GetSection("CosmosDb").Get<CosmosDBOptions>().Collections.Where(x =>
                x.Name.IndexOf("TaxaJuros") >= 0
            ).FirstOrDefault().Name;

            Uri = configuration.GetSection("CosmosDb").Get<CosmosDBOptions>().Uri.ToString();
            PrimaryKey = configuration.GetSection("CosmosDb").Get<CosmosDBOptions>().PrimaryKey.ToString();
        }

        public async Task<Produto> Salvar(Produto example)
        {
            var entity = await AddAsync(example);
            return entity;
        }
        public async Task<bool> Atualizar(Produto example)
        {
            return await UpsertAsync(example);
        }
        public async Task<Produto> Buscar(Guid id)
        {
            try
            {
                return await GetByIdAsync(id.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<IList<Produto>> Buscar()
        {
            try
            {
                return await GetAll(_ => true);
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
