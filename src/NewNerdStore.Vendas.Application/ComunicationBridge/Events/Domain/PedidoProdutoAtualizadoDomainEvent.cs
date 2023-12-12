using NewNerdStore.Core.Messages.Abstracts;
using NewNerdStore.Core.Messages.Commons.DomainEvents;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Events.Domain
{
    public class PedidoProdutoAtualizadoDomainEvent : DomainEvent
    {
        public Guid ClienteId { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }

        public PedidoProdutoAtualizadoDomainEvent(Guid clienteId, Guid pedidoId, Guid produtoId, int quantidade)
            :base(aggregateId: pedidoId)
        {
            ClienteId = clienteId;
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
        }
    }
}
