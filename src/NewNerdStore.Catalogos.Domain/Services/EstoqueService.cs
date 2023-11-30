using NewNerdStore.Catalogos.Domain.DomainServices.Interfaces;
using NewNerdStore.Catalogos.Domain.Events;
using NewNerdStore.Catalogos.Domain.Interfaces.Repositories;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.DomainObjects.Dtos;
using NewNerdStore.Core.Messages.Commons.Notifications.Errors;

namespace NewNerdStore.Catalogos.Domain.DomainServices
{
    //TODO : Domain services fazem parte da linguagem ubiqua; a definição do workflow de um domain service é definido pelo domain expert
    public class EstoqueService : IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEventMediatorStrategy _eventMediatorStrategy;
        private readonly INotificationMediatorStrategy _notificationMediatorStrategy;


        public EstoqueService(
            IProdutoRepository produtoRepository,
            IEventMediatorStrategy  eventMediatorStrategy,
            INotificationMediatorStrategy notificationMediatorStrategy)
        {
            _produtoRepository = produtoRepository;
            _eventMediatorStrategy = eventMediatorStrategy;
            _notificationMediatorStrategy = notificationMediatorStrategy;
        }

        #region Debitar Estoque
        private async Task<bool> DebitarItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;

            if (!produto.PossuiEstoque(quantidade))
            {
                await _notificationMediatorStrategy
                    .PublishNotification(new DomainErrorNotifications("Estoque", $"Produto - {produto.Nome} sem estoque"));

                return false;
            }

            produto.DebitarEstoque(quantidade);

            // TODO: 10 pode ser parametrizavel em arquivo de configuração
            if (produto.QuantidadeEstoque < 10)
            {
                await _eventMediatorStrategy
                    .PublishEvent(new ProdutoAbaixoEstoqueDomainEvent(produto.Id, produto.QuantidadeEstoque));
            }

            _produtoRepository.Atualizar(produto);

            return true;
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            if (!await DebitarItemEstoque(produtoId, quantidade)) return false;

            return await _produtoRepository.UnitOfWork.Commit();

        }

        public async Task<bool> DebitarListaProdutosPedido(PedidoProdutosItemsDto pedidoProdutosItems)
        {
            foreach (var item in pedidoProdutosItems.Itens)
            {
                if (!await DebitarItemEstoque(item.Id, item.Quantidade)) return false;
            }

            return await _produtoRepository.UnitOfWork.Commit();
        }

        #endregion


        #region Repor Estoque
        private async Task<bool> ReporItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;
            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);

            return true;
        }

        public async Task<bool> ReporListaProdutosPedido(PedidoProdutosItemsDto pedidoProdutosItems)
        {
            foreach (var item in pedidoProdutosItems.Itens)
            {
                await ReporItemEstoque(item.Id, item.Quantidade);
            }

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var sucesso = await ReporItemEstoque(produtoId, quantidade);

            if (!sucesso) return false;

            return await _produtoRepository.UnitOfWork.Commit();
        }
        #endregion

        public void Dispose() => _produtoRepository?.Dispose();
    }
}
