using NewNerdStore.Catalogos.Domain.DomainServices.Interfaces;
using NewNerdStore.Catalogos.Domain.Events;
using NewNerdStore.Catalogos.Domain.Interfaces.Repositories;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;

namespace NewNerdStore.Catalogos.Domain.DomainServices
{
    //TODO : Domain services fazem parte da linguagem ubiqua; a definição do workflow de um domain service é definido pelo domain expert
    public class EstoqueService : IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IDomainMediatorHandler _bus;

        public EstoqueService(IProdutoRepository produtoRepository, IDomainMediatorHandler bus)
        {
            this._produtoRepository = produtoRepository;
            this._bus = bus;
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;

            if (!produto.PossuiEstoque(quantidade)) return false;

            produto.DebitarEstoque(quantidade);

            //TODO: Parametrizar a quantidade de estoque baixo
            if (produto.QuantidadeEstoque < 10)
               await _bus.PublishEvent(new ProdutoAbaixoEstoqueEvent
                   (aggregateId: produto.Id, quantidadeRestante: produto.QuantidadeEstoque));

            _produtoRepository.Atualizar(produto);

            return await _produtoRepository.UnitOfWork.Commit();

        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;
            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public void Dispose() => _produtoRepository?.Dispose();
    }
}
