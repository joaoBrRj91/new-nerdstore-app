using NewNerdStore.Core.Messages.Commons.DomainEvents;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Events.Domain
{
    public class PedidoRascunhoIniciadoDomainEvent : DomainEvent
    {
        public PedidoRascunhoIniciadoDomainEvent(Guid clienteId, Guid pedidoId)
            :base(aggregateId: pedidoId)
        {
            PedidoId = pedidoId;
            ClienteId = clienteId;
        }

        public Guid ClienteId { get; private set; }
        public Guid PedidoId { get; private set; }

    }
}
