using NewNerdStore.Core.Events.Types;
using NewNerdStore.Vendas.Application.CQRS.Validations;

namespace NewNerdStore.Vendas.Application.CQRS.Commands
{
    public class AdicionarItemPedidoCommand : Command
    {
        public AdicionarItemPedidoCommand(
            Guid clienteId,
            Guid produtoId,
            string produtoNome,
            int quantidade,
            decimal valorUnitario)
        {
            ClienteId = clienteId;
            ProdutoId = produtoId;
            ProdutoNome = produtoNome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public Guid ClienteId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string ProdutoNome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarItemPedidoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
