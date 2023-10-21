using AutoMapper;
using NewNerdStore.Catalogos.Application.Dtos;
using NewNerdStore.Catalogos.Domain.Entities;

namespace NewNerdStore.Catalogos.Application.Mappers.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Produto, ProdutoDto>()
                .ForMember(destinationMember: d => d.Largura, memberOptions: o => o.MapFrom(s => s.Dimensoes.Largura))
                .ForMember(destinationMember: d => d.Altura, memberOptions: o => o.MapFrom(s => s.Dimensoes.Altura))
                .ForMember(destinationMember: d => d.Profundidade, memberOptions: o => o.MapFrom(s => s.Dimensoes.Profundidade));

            CreateMap<Categoria, CategoriaDto>();

        }
    }
}
