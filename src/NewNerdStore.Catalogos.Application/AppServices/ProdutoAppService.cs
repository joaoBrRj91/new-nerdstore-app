using AutoMapper;
using NewNerdStore.Catalogos.Application.AppServices.Interfaces;
using NewNerdStore.Catalogos.Application.Dtos;
using NewNerdStore.Catalogos.Domain.DomainServices;
using NewNerdStore.Catalogos.Domain.DomainServices.Interfaces;
using NewNerdStore.Catalogos.Domain.Entities;
using NewNerdStore.Catalogos.Domain.Interfaces.Repositories;
using NewNerdStore.Core.DomainObjects;

namespace NewNerdStore.Catalogos.Application.AppServices
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueService _estoqueService;
        private readonly IMapper _mapper;

        public ProdutoAppService(
            IProdutoRepository produtoRepository,
            IEstoqueService estoqueService,
            IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _estoqueService = estoqueService;
            _mapper = mapper;
        }

        #region Queries

        public async Task<IEnumerable<ProdutoDto>> ObterTodos() 
            => _mapper.Map<IEnumerable<ProdutoDto>>(await _produtoRepository.ObterTodos());

        public async Task<ProdutoDto> ObterPorId(Guid id) 
            => _mapper.Map<ProdutoDto>(await _produtoRepository.ObterPorId(id));

        public async Task<IEnumerable<CategoriaDto>> ObterCategorias() 
            => _mapper.Map<IEnumerable<CategoriaDto>>(await _produtoRepository.ObterCategorias());

        public async Task<IEnumerable<ProdutoDto>> ObterPorCategoria(int codigo) 
            => _mapper.Map<IEnumerable<ProdutoDto>>(await _produtoRepository.ObterPorCategoria(codigo));

        #endregion

        #region Commands

        public async Task AdicionarProduto(ProdutoDto produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            _produtoRepository.Adicionar(produto);

            await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task AtualizarProduto(ProdutoDto produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            _produtoRepository.Atualizar(produto);

            await _produtoRepository.UnitOfWork.Commit();
        }

        #endregion

        #region Domain Services

        public async Task<ProdutoDto> DebitarEstoque(Guid id, int quantidade)
        {
            if (!_estoqueService.DebitarEstoque(id, quantidade).Result)
            {
                throw new DomainException("Falha ao debitar estoque");
            }

            return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterPorId(id));
        }

        public async Task<ProdutoDto> ReporEstoque(Guid id, int quantidade)
        {
            if (!_estoqueService.ReporEstoque(id, quantidade).Result)
            {
                throw new DomainException("Falha ao repor estoque");
            }

            return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterPorId(id));
        }

        #endregion

        public void Dispose()
        {
            _produtoRepository?.Dispose();
            _estoqueService?.Dispose();
        }
    }
}
