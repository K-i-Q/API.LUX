using Domain.LUX.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.LUX.CommandHandlers
{
    public interface IProdutoCommandHandler
    {
        Task<ProdutoDTOResponse> HandlerAsync(ProdutoDTO command);
    }
}
