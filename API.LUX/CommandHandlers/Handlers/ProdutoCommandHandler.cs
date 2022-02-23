using AutoMapper;
using Domain.LUX.DTOs;
using Domain.LUX.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.LUX.CommandHandlers.Handlers
{
    public class ProdutoCommandHandler : IProdutoCommandHandler
    {
        private readonly IMapper _mapper;

        public async Task<ProdutoDTOResponse> HandlerAsync(ProdutoDTO command)
        {
            var response = new ProdutoDTOResponse
            {
                Sucesso = false,
                Mensagem = string.Empty
            };
            try
            {
                var entidade = _mapper.Map<Produto>(command);



                response.Sucesso = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;

                return response;
            }


        }
    }
}
