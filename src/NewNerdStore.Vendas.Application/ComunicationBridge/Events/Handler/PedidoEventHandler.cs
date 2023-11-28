using MediatR;
using NewNerdStore.Vendas.Application.ComunicationBridge.Events.Domain;

namespace NerdStore.Vendas.Application.Events.Handler
{
    public class PedidoEventHandler :
        INotificationHandler<PedidoRascunhoIniciadoDomainEvent>,
        INotificationHandler<PedidoRascunhoAtualizadoDomainEvent>,
        INotificationHandler<PedidoItemAdicionadoDomainEvent>
    {

        public Task Handle(PedidoRascunhoIniciadoDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoRascunhoAtualizadoDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoItemAdicionadoDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }
}