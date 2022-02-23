using Domain.LUX.DTOs;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.LUX.Controllers.ResponseExamples
{
    public class ProdutoSuccess : IExamplesProvider<ProdutoDTOResponse>

    {
        public ProdutoDTOResponse GetExamples()
        {
            return new ProdutoDTOResponse
            {
                Mensagem = "Sucesso",
                Sucesso = true
            };
        }
    }
}
