using MediatR;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Abstracts;
using NewNerdStore.Vendas.Application.ComunicationBridge.Commands;
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
        INotificationHandler<PedidoEstoqueRejeitadoEvent>,
        INotificationHandler<PagamentoRealizadoEvent>,
        INotificationHandler<PagamentoRecusadoEvent>
    {
        private readonly ICommandMediatorStrategy _commandMediatorStrategy;

        public PedidoEventHandler(ICommandMediatorStrategy commandMediatorStrategy)
        {
            _commandMediatorStrategy = commandMediatorStrategy;
        }

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

        public async Task Handle(PedidoEstoqueRejeitadoEvent notification, CancellationToken cancellationToken)
        {
            await _commandMediatorStrategy.Send(new CancelarProcessamentoPedidoCommand(notification.PedidoId, notification.ClienteId));
        }

        public async Task Handle(PagamentoRecusadoEvent notification, CancellationToken cancellationToken)
        {
            await _commandMediatorStrategy.Send(new FinalizarPedidoCommand(notification.PedidoId, notification.ClienteId));
        }
        public async Task Handle(PagamentoRealizadoEvent notification, CancellationToken cancellationToken)
        {
            await _commandMediatorStrategy.Send(new CancelarProcessamentoPedidoEstornarEstoqueCommand(notification.PedidoId, notification.ClienteId));
        }

    }
}