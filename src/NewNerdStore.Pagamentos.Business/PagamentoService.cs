using System.Threading.Tasks;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.DomainObjects.Dtos;
using NewNerdStore.Core.Messages.Commons.Notifications.Errors;
using NewNerdStore.Core.Messages.Commons.Notifications.Events;

namespace NerdStore.Pagamentos.Business
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoCartaoCreditoFacade _pagamentoCartaoCreditoFacade;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IEventMediatorStrategy _eventMediatorStrategy;
        private readonly INotificationMediatorStrategy _notificationMediatorStrategy;

        public PagamentoService(IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade,
                                IPagamentoRepository pagamentoRepository,
                                IEventMediatorStrategy eventMediatorStrategy,
                                INotificationMediatorStrategy notificationMediatorStrategy)
        {
            _pagamentoCartaoCreditoFacade = pagamentoCartaoCreditoFacade;
            _pagamentoRepository = pagamentoRepository;
            _eventMediatorStrategy = eventMediatorStrategy;
            _notificationMediatorStrategy = notificationMediatorStrategy;
        }

        public async Task<Transacao> RealizarPagamentoPedido(PagamentoPedidoDto pagamentoPedido)
        {
            var pedido = new Pedido
            {
                Id = pagamentoPedido.PedidoId,
                Valor = pagamentoPedido.Total
            };

            var pagamento = new Pagamento
            {
                Valor = pagamentoPedido.Total,
                NomeCartao = pagamentoPedido.NomeCartao,
                NumeroCartao = pagamentoPedido.NumeroCartao,
                ExpiracaoCartao = pagamentoPedido.ExpiracaoCartao,
                CvvCartao = pagamentoPedido.CvvCartao,
                PedidoId = pagamentoPedido.PedidoId
            };

            var transacao = _pagamentoCartaoCreditoFacade.RealizarPagamento(pedido, pagamento);

            if (transacao.StatusTransacao == StatusTransacao.Pago)
            {

                _pagamentoRepository.Adicionar(pagamento);
                _pagamentoRepository.AdicionarTransacao(transacao);

                var isCommandChangeStatusEntity = await _pagamentoRepository.UnitOfWork.Commit();

                if (isCommandChangeStatusEntity)
                {
                   await _eventMediatorStrategy
                            .PublishEvent(new PagamentoRealizadoEvent(pedido.Id, pagamentoPedido.ClienteId, transacao.PagamentoId, transacao.Id, pedido.Valor));
                }

                return transacao;
            }

            await _notificationMediatorStrategy
                    .PublishNotification(new DomainErrorNotifications("pagamento", "A operadora recusou o pagamento"));

            await _eventMediatorStrategy
                    .PublishEvent(new PagamentoRecusadoEvent(pedido.Id, pagamentoPedido.ClienteId, transacao.PagamentoId, transacao.Id, pedido.Valor));

            return transacao;

        }
    }
}