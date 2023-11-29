using NewNerdStore.Core.Messages.Abstracts;
using NewNerdStore.Vendas.Application.ComunicationBridge.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Commands
{
    public class AtualizarItemPedidoCommand : Command
    {
        public Guid ClienteId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }

        public AtualizarItemPedidoCommand(Guid clienteId, Guid produtoId, int quantidade)
        {
            ClienteId = clienteId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
        }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarItemPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
