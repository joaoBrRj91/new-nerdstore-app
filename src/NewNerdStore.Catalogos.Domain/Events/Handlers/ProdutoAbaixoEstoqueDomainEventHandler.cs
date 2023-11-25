using MediatR;
using NewNerdStore.Catalogos.Domain.Interfaces.Repositories;

namespace NewNerdStore.Catalogos.Domain.Events.Handlers
{
    public class ProdutoAbaixoEstoqueDomainEventHandler : INotificationHandler<ProdutoAbaixoEstoqueDomainEvent>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoAbaixoEstoqueDomainEventHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Handle(ProdutoAbaixoEstoqueDomainEvent notification, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorId(notification.AggregateId);

            //TODO: Realizar algum tratamento desse evento como enviar um email de notificação de baixo estoque

        }
    }
}
