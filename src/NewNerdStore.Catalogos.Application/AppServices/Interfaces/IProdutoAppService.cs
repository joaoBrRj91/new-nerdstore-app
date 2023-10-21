using NewNerdStore.Catalogos.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewNerdStore.Catalogos.Application.AppServices.Interfaces
{
    public interface IProdutoAppService : IDisposable
    {
        Task<IEnumerable<ProdutoDto>> ObterPorCategoria(int codigo);
        Task<ProdutoDto> ObterPorId(Guid id);
        Task<IEnumerable<ProdutoDto>> ObterTodos();
        Task<IEnumerable<CategoriaDto>> ObterCategorias();

        Task AdicionarProduto(ProdutoDto produtoDto);
        Task AtualizarProduto(ProdutoDto produtoDtp);

        Task<ProdutoDto> DebitarEstoque(Guid id, int quantidade);
        Task<ProdutoDto> ReporEstoque(Guid id, int quantidade);
    }
}
