using Domain.LUX.DTOs;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.LUX.Controllers.RequestExamples
{
    public class ProdutoRequestExample : IExamplesProvider<ProdutoDTO>

    {
        public ProdutoDTO GetExamples()
        {
            return new ProdutoDTO
            {
                DataAtualizacao = DateTimeOffset.UtcNow,
                DataCadastro = DateTimeOffset.UtcNow,
                Descricao = "Jack Daniels Maça Verde",
                Preco = 30
            };
        }
    }
}
