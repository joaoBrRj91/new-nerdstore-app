using MediatR;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NewNerdStore.Vendas.Application.ComunicationBridge.Events.Domain;

namespace NerdStore.Vendas.Application.Events.Handler
{

    //TODO : REFACTORING : Criar handlers especificos para cada DomainEvent
    public class PedidoEventHandler :
        INotificationHandler<PedidoRascunhoIniciadoDomainEvent>,
        INotificationHandler<PedidoRascunhoAtualizadoDomainEvent>,
        INotificationHandler<PedidoItemAdicionadoDomainEvent>,
        INotificationHandler<PedidoProdutoRemovidoDomainEvent>,
        INotificationHandler<VoucherAplicadoPedidoDomainEvent>,
        INotificationHandler<PedidoEstoqueRejeitadoEvent>
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

        public Task Handle(PedidoProdutoRemovidoDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(VoucherAplicadoPedidoDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(PedidoEstoqueRejeitadoEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}