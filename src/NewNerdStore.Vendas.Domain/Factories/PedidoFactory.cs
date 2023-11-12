using NewNerdStore.Vendas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewNerdStore.Vendas.Domain.Factories
{
    public static class PedidoFactory
    {
        public static Pedido NovoPedidoRascunho(Guid clienteId)
        {
            Pedido pedido = new()
            {
                ClienteId = clienteId,
            };

            pedido.TornarRascunho();
            return pedido;
        }
    }
}
