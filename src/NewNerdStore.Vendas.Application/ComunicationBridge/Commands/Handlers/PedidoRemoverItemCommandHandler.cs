using MediatR;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Commands.Handlers
{
    public class PedidoRemoverItemCommandHandler : BaseCommandHandler<RemoverItemPedidoCommand>,
        IRequestHandler<RemoverItemPedidoCommand, bool>, IDisposable
    {

        public PedidoRemoverItemCommandHandler(INotificationMediatorStrategy notificationMediatorStrategy)
            : base(notificationMediatorStrategy)
        {
        }

        public Task<bool> Handle(RemoverItemPedidoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
