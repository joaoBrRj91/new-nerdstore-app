using AutoMapper;
using NewNerdStore.Catalogos.Application.Dtos;
using NewNerdStore.Catalogos.Domain.Entities;
using NewNerdStore.Catalogos.Domain.ValueObjects;

namespace NewNerdStore.Catalogos.Application.Mappers.AutoMapper
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<ProdutoDto, Produto>()
                .ConstructUsing(p =>
                    new Produto(p.Nome, p.Descricao, p.Ativo,
                        p.Valor, p.DataCadastro, p.Imagem, p.CategoriaId, new Dimensoes(p.Altura, p.Largura, p.Profundidade), 0));

            CreateMap<CategoriaDto, Categoria>()
                .ConstructUsing(c => new Categoria(c.Nome, c.Codigo));
        }
    }
}
