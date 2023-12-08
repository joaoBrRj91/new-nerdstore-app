using NewNerdStore.Core.Messages.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewNerdStore.Core.Messages.Commons.IntegrationEvents
{
    public class PedidoFinalizadoEvent : Event
    {
        public Guid PedidoId { get; private set; }

        public PedidoFinalizadoEvent(Guid pedidoId)
        {
            PedidoId = pedidoId;
            AggregateId = pedidoId;
        }
    }
}
