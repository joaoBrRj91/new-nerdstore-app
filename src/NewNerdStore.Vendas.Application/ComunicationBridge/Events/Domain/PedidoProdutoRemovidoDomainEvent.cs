using NewNerdStore.Core.Messages.Commons.DomainEvents;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Events.Domain
{
    public class PedidoProdutoRemovidoDomainEvent : DomainEvent
    {
        public PedidoProdutoRemovidoDomainEvent(Guid clienteId, Guid pedidoId, Guid produtoId)
            :base(aggregateId: pedidoId)
        {
            ClienteId = clienteId;
            PedidoId = pedidoId;
            ProdutoId = produtoId;
        }

        public Guid ClienteId { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }

    }
}
