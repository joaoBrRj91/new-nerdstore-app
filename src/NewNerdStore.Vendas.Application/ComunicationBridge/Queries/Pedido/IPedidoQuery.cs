using NerdStore.Vendas.Application.Queries.Dtos;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Queries.Pedido
{
    public interface IPedidoQuery
    {
        Task<CarrinhoDto> ObterCarrinhoCliente(Guid clienteId);
        Task<IEnumerable<PedidoDto>> ObterPedidosCliente(Guid clienteId);
    }
}
