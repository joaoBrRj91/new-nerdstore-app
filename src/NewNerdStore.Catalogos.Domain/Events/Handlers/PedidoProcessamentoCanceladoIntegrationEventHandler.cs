using MediatR;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NewNerdStore.Catalogos.Domain.DomainServices.Interfaces;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Commons.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewNerdStore.Catalogos.Domain.Events.Handlers
{
    public class PedidoProcessamentoCanceladoIntegrationEventHandler : INotificationHandler<PedidoProcessamentoCanceladoEvent>
    {
        private readonly IEstoqueService _estoqueService;


        public PedidoProcessamentoCanceladoIntegrationEventHandler(IEstoqueService estoqueService) 
        {
            _estoqueService = estoqueService;
        }

        public async Task Handle(PedidoProcessamentoCanceladoEvent message, CancellationToken cancellationToken)
        {
            await _estoqueService.ReporListaProdutosPedido(message.ProdutosPedido);
        }
    }
}
