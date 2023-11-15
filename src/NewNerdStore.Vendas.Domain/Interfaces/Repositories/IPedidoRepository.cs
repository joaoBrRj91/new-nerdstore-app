using NewNerdStore.Core.Data;
using NewNerdStore.Vendas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewNerdStore.Vendas.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        #region Agregation Root - Pedido
        Task<Pedido> ObterPorId(Guid id);
        Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId);
        Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId);
        void Adicionar(Pedido pedido);
        void Atualizar(Pedido pedido);
        #endregion

        #region Agregation - PedidoItem
        Task<PedidoItem> ObterItemPorId(Guid id);
        Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId);
        void AdicionarItem(PedidoItem pedidoItem);
        void AtualizarItem(PedidoItem pedidoItem);
        void RemoverItem(PedidoItem pedidoItem);
        #endregion

        #region Agregation - Voucher
        Task<Voucher> ObterVoucherPorCodigo(string codigo);
        #endregion
    }
}
