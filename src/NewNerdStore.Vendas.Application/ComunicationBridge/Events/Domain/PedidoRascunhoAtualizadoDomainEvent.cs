using NewNerdStore.Core.Messages.Commons.DomainEvents;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Events.Domain
{
    public class PedidoRascunhoAtualizadoDomainEvent : DomainEvent
    {
        public PedidoRascunhoAtualizadoDomainEvent(Guid clienteId, Guid pedidoId, decimal valorTotal)
            : base(aggregateId: pedidoId)
        {
            ClienteId = clienteId;
            PedidoId = pedidoId;
            ValorTotal = valorTotal;
        }

        public Guid ClienteId { get; private set; }
        public Guid PedidoId { get; private set; }
        public decimal ValorTotal { get; private set; }
    }
}
