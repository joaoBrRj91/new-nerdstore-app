using System.Threading.Tasks;
using NewNerdStore.Core.DomainObjects.Dtos;

namespace NerdStore.Pagamentos.Business
{
    public interface IPagamentoService
    {
        Task<Transacao> RealizarPagamentoPedido(PagamentoPedidoDto pagamentoPedido);
    }
}