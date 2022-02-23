using AutoMapper;
using Domain.LUX.DTOs;
using Domain.LUX.Entities;

namespace Domain.Mapper
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            //Exemplo sem configuração de membro
            CreateMap<ProdutoDTOResponse, Produto>();

            //CreateMap<ExemploDtoResponse, ExampleCommandResponse>(MemberList.None)
            //    .ForMember(dest => dest.Message, opt => opt.Ignore())
            //    .ForMember(dest => dest.CurrentBalance, opt => opt.MapFrom(src => src.Saldo));


        }
    }
}
