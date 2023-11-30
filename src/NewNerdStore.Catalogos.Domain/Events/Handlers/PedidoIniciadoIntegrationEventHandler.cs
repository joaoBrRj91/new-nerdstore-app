using MediatR;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NewNerdStore.Catalogos.Domain.DomainServices.Interfaces;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Commons.IntegrationEvents;

namespace NewNerdStore.Catalogos.Domain.Events.Handlers
{
    public class PedidoIniciadoIntegrationEventHandler : INotificationHandler<PedidoIniciadoEvent>
    {
        private readonly IEstoqueService _estoqueService;
        private readonly IEventMediatorStrategy _eventMediatorStrategy;


        public PedidoIniciadoIntegrationEventHandler
            (IEstoqueService estoqueService,
            IEventMediatorStrategy eventMediatorStrategy)
        {
            _estoqueService = estoqueService;
            _eventMediatorStrategy = eventMediatorStrategy;
        }

        public async Task Handle(PedidoIniciadoEvent message, CancellationToken cancellationToken)
        {
            var estoqueDebitadoComSucesso = await _estoqueService.DebitarListaProdutosPedido(message.PedidoProdutosItems);

            if (estoqueDebitadoComSucesso)
            {
                await _eventMediatorStrategy.PublishEvent(new PedidoEstoqueConfirmadoEvent(message.PedidoId, message.ClienteId, message.Total,
                     message.PedidoProdutosItems, message.NomeCartao, message.NumeroCartao, message.ExpiracaoCartao, message.CvvCartao));
            }

            else
                await _eventMediatorStrategy.PublishEvent(new PedidoEstoqueRejeitadoEvent(message.PedidoId, message.ClienteId));
            
        }
    }
}
