using MediatR;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Abstracts;
using NewNerdStore.Core.Messages.Commons.Notifications.Errors;
using NewNerdStore.Core.Messages.Commons.Notifications.Events;
using NewNerdStore.Vendas.Application.ComunicationBridge.Events.Domain;
using NewNerdStore.Vendas.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Commands.Handlers
{
    public class PedidoAplicarVoucherCommandHandler : BaseCommandHandler<AplicarVoucherPedidoCommand>,
       IRequestHandler<AplicarVoucherPedidoCommand, bool>, IDisposable
    {

        private readonly IPedidoRepository _pedidoRepository;
        private readonly INotificationEventManager _notificationEventManager;

        public PedidoAplicarVoucherCommandHandler(
            IPedidoRepository pedidoRepository,
            INotificationEventManager notificationEventManager,
            INotificationMediatorStrategy notificationMediatorStrategy)
            : base(notificationMediatorStrategy)
        {
            _pedidoRepository = pedidoRepository;
            _notificationEventManager = notificationEventManager;
        }

        public async Task<bool> Handle(AplicarVoucherPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(message)) return false;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(message.ClienteId);

            if (pedido == null)
            {
                PublishDomainErrorNotification
                   (new DomainErrorNotifications(key: "Pedido", value: "Pedido não encontrado!"));

                return false;
            }

            var voucher = await _pedidoRepository.ObterVoucherPorCodigo(message.CodigoVoucher);

            if (voucher == null)
            {
                PublishDomainErrorNotification
                    (new DomainErrorNotifications(key: "Pedido", value: "Item do pedido não encontrado!"));
            }

            var voucherAplicacaoValidation = pedido.AplicarVoucher(voucher);
            if (!voucherAplicacaoValidation.IsValid)
            {
                foreach (var error in voucherAplicacaoValidation.Errors)
                {
                     PublishDomainErrorNotification(new DomainErrorNotifications(error.ErrorCode, error.ErrorMessage));
                }

                return false;
            }

            _notificationEventManager
                .AddNotificationEvent(new PedidoRascunhoAtualizadoDomainEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));

            _notificationEventManager
                 .AddNotificationEvent(new VoucherAplicadoPedidoDomainEvent(message.ClienteId, pedido.Id, voucher.Id));

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
