using NewNerdStore.Core.DomainObjects.Dtos;

namespace NewNerdStore.Catalogos.Domain.DomainServices.Interfaces
{
    public interface IEstoqueService: IDisposable
    {
        Task<bool> DebitarEstoque(Guid produtoId, int quantidade);
        Task<bool> DebitarListaProdutosPedido(PedidoProdutosItemsDto pedidoProdutosItems);
        Task<bool> ReporEstoque(Guid produtoId, int quantidade);
        Task<bool> ReporListaProdutosPedido(PedidoProdutosItemsDto pedidoProdutosItems);

    }
}
