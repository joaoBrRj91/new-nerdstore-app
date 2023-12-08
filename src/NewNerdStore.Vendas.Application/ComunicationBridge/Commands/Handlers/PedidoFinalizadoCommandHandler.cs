﻿using MediatR;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.DomainObjects.Dtos;
using NewNerdStore.Core.Extensions;
using NewNerdStore.Core.Messages.Commons.IntegrationEvents;
using NewNerdStore.Core.Messages.Commons.Notifications.Errors;
using NewNerdStore.Core.Messages.Commons.Notifications.Events;
using NewNerdStore.Vendas.Domain.Interfaces.Repositories;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Commands.Handlers
{
    public class PedidoFinalizadoCommandHandler : BaseCommandHandler<FinalizarPedidoCommand>,
        IRequestHandler<FinalizarPedidoCommand, bool>, IDisposable
    {

        private readonly IPedidoRepository _pedidoRepository;
        private readonly IEventMediatorStrategy  _eventMediatorStrategy;


        public PedidoFinalizadoCommandHandler(
            IPedidoRepository pedidoRepository,
            IEventMediatorStrategy eventMediatorStrategy,
            INotificationMediatorStrategy notificationMediatorStrategy)
            : base(notificationMediatorStrategy)
        {
            _pedidoRepository = pedidoRepository;
            _eventMediatorStrategy = eventMediatorStrategy;
        }

        public async Task<bool> Handle(FinalizarPedidoCommand message, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPorId(message.PedidoId);

            if (pedido == null)
            {
                 PublishDomainErrorNotification(new DomainErrorNotifications("pedido", "Pedido não encontrado!"));
                return false;
            }

            pedido.FinalizarPedido();

            await _eventMediatorStrategy.PublishEvent(new PedidoFinalizadoEvent(message.PedidoId));
            return await _pedidoRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _pedidoRepository.Dispose();
        }
    }
}
