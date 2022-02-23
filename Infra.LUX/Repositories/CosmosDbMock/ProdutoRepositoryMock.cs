using Domain.LUX.Entities;
using Infra.Repositories.CosmosDB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Repositories.CosmosDbMock
{
    public class ProdutoRepositoryMock : CosmosDbRepository<Produto>, IProdutoRepository
    {
        public override string CollectionName { get; } = "";

        public ProdutoRepositoryMock(ICosmosDbClientFactory factory) : base(factory) { }

        public async Task<Produto> Salvar(Produto efetuarCartaoFisico)
        {
            var entity = new Produto();
            return await Task.Run(() => entity);
        }

        public async Task<bool> Atualizar(Produto efetuarSaqueCartaoFisico)
        {
            return await Task.FromResult(true);
        }

        public async Task<Produto> Buscar(Guid id)
        {
            var entity = new Produto();
            return await Task.Run(() => entity);
        }
        public async Task<IList<Produto>> Buscar()
        {
            var entity = new List<Produto> { new Produto { Id = Guid.NewGuid().ToString() } };

            return await Task.Run(() => entity);
        }
    }
}
