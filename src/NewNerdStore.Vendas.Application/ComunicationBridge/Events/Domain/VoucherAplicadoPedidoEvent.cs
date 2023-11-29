using NewNerdStore.Core.Messages.Commons.DomainEvents;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Events.Domain
{
    public class VoucherAplicadoPedidoEvent : DomainEvent
    {
        public VoucherAplicadoPedidoEvent(Guid clienteId, Guid pedidoId, Guid voucherId)
            :base(aggregateId: pedidoId)
        {
            ClienteId = clienteId;
            PedidoId = pedidoId;
            VoucherId = voucherId;
        }

        public Guid ClienteId { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid VoucherId { get; private set; }

    }
}
