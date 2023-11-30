namespace NewNerdStore.Core.DomainObjects.Dtos
{
    public class PedidoProdutosItemsDto
    {
        public Guid PedidoId { get; set; }
        public ICollection<ProdutoItemDto> Itens { get; set; }
    }

    public class ProdutoItemDto
    {
        public Guid Id { get; set; }
        public int Quantidade { get; set; }
    }
}
