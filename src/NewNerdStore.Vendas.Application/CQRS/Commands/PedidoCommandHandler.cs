using MediatR;
using NewNerdStore.Core.Events;
using NewNerdStore.Vendas.Domain.Entities;
using NewNerdStore.Vendas.Domain.Factories;
using NewNerdStore.Vendas.Domain.Interfaces.Repositories;

namespace NewNerdStore.Vendas.Application.CQRS.Commands
{
    public class PedidoCommandHandler : BaseCommandHandler<AdicionarItemPedidoCommand>, IRequestHandler<AdicionarItemPedidoCommand, bool>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<bool> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(message.ClienteId);
            var pedidoItem = new PedidoItem(message.ProdutoId, message.ProdutoNome, message.Quantidade, message.ValorUnitario);

            if (pedido is null)
                CriarNovoPedidoRascunho(pedidoItem, message.ClienteId);
            else
                GerenciarItemPedido(pedido, pedidoItem);

            return await _pedidoRepository.UnitOfWork.Commit(); 

        }

        private void CriarNovoPedidoRascunho(PedidoItem pedidoItem, Guid clienteId)
        {
            var pedido = PedidoFactory.NovoPedidoRascunho(clienteId);
            pedido.AdicionarItem(pedidoItem);

            _pedidoRepository.Adicionar(pedido);
        }

        private void GerenciarItemPedido(Pedido pedido, PedidoItem pedidoItem)
        {
            pedido.AdicionarItem(pedidoItem);
            var pedidoItemExistente = pedido.PedidoItemExistente(pedidoItem);

            if (pedidoItemExistente)
                _pedidoRepository.AtualizarItem(pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId));
            else
                _pedidoRepository.AdicionarItem(pedidoItem);

        }

    }
}
