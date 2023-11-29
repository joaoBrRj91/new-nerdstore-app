using MediatR;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
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

        public PedidoAplicarVoucherCommandHandler(INotificationMediatorStrategy notificationMediatorStrategy)
            : base(notificationMediatorStrategy)
        {
        }

        public Task<bool> Handle(AplicarVoucherPedidoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
