using Domain.LUX.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> Salvar(Produto example);
        Task<bool> Atualizar(Produto example);
        Task<Produto> Buscar(Guid id);
        Task<IList<Produto>> Buscar();
    }
}
