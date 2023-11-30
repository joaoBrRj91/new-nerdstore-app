using MediatR;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Abstracts;
using NewNerdStore.Core.Messages.Commons.Notifications.Errors;
using NewNerdStore.Core.Messages.Commons.Notifications.Events;
using NewNerdStore.Vendas.Application.ComunicationBridge.Events.Domain;
using NewNerdStore.Vendas.Domain.Interfaces.Repositories;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Commands.Handlers
{
    public class PedidoRemoverItemCommandHandler : BaseCommandHandler<RemoverItemPedidoCommand>,
        IRequestHandler<RemoverItemPedidoCommand, bool>, IDisposable
    {

        private readonly IPedidoRepository _pedidoRepository;
        private readonly INotificationEventManager _notificationEventManager;


        public PedidoRemoverItemCommandHandler(
            IPedidoRepository pedidoRepository,
            INotificationEventManager notificationEventManager,
            INotificationMediatorStrategy notificationMediatorStrategy)
            : base(notificationMediatorStrategy)
        {
            _pedidoRepository = pedidoRepository;
            _notificationEventManager = notificationEventManager;   
        }

        public async Task<bool> Handle(RemoverItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(message)) return false;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(message.ClienteId);

            if (pedido == null)
            {
                PublishDomainErrorNotification
                   (new DomainErrorNotifications(key: "Pedido", value: "Pedido não encontrado!"));

                return false;
            }

            var pedidoItem = await _pedidoRepository.ObterItemPorPedido(pedido.Id, message.ProdutoId);

            if (pedidoItem == null)
            {
                PublishDomainErrorNotification
                    (new DomainErrorNotifications(key: "Pedido", value: "Item do pedido não encontrado!"));
            }

            pedido.RemoverItem(pedidoItem);
            _notificationEventManager
                .AddNotificationEvent(new PedidoProdutoRemovidoDomainEvent(message.ClienteId, pedido.Id, message.ProdutoId));

            _pedidoRepository.RemoverItem(pedidoItem);
            _pedidoRepository.Atualizar(pedido);

            var isCommandChangeStatusEntity = await _pedidoRepository.UnitOfWork.Commit();

            if (isCommandChangeStatusEntity)
                await _notificationEventManager.SendAllNotificationEvents();

            return isCommandChangeStatusEntity;

        }


        public void Dispose()
        {
            _pedidoRepository.Dispose();
        }
    }
}
