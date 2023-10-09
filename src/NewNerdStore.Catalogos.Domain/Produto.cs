using NewNerdStore.Core.DomainObjects;

namespace NewNerdStore.Catalogos.Domain
{
    public class Produto : Entity, IAggregateRoot
    {

        #region Constructors
        public Produto(string nome,
            string descricao,
            bool ativo,
            decimal valor,
            DateTime dataCadastro,
            string imagem,
            int quantidadeEstoque,
            Guid categoriaId)
        {
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
            DataCadastro = dataCadastro;
            Imagem = imagem;
        }

        protected Produto() : base() { }
        #endregion

        #region Properties
        public string Nome { get; private set; }

        public string Descricao { get; private set; }

        public bool Ativo { get; private set; }

        public decimal Valor { get; private set; }

        public DateTime DataCadastro { get; private set; }

        public string Imagem { get; private set; }

        public int QuantidadeEstoque { get; private set; }

        public Guid  CategoriaId { get; private set; }

        public Categoria Categoria { get; private set; }
        #endregion

        #region Ad-hoc setters
        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        public void AlterarCategoria(Categoria categoria)
        {
            Categoria = categoria;
            CategoriaId = categoria.Id; 
        }

        public void DebitarEstoque(int quantidade)
        {
            if (quantidade < 0) quantidade *= -1;
            QuantidadeEstoque -= quantidade;
        }

        public void ReporEstoque(int quantidade)
        {
            if (quantidade < 0) quantidade *= -1;
            QuantidadeEstoque += quantidade;
        }

        public bool PossuiEstoque(int quantidade)
            => QuantidadeEstoque>= quantidade;
        #endregion

        #region Validate
        public void Validar()
        {

        }
        #endregion

    }

    public class Categoria : Entity
    {
        public Categoria(string nome, string codigo)
        {
            Nome = nome;
            Codigo = codigo;
        }

        protected Categoria(): base() { }

        public string Nome { get; private set; }

        public string Codigo { get; private set; }

        public override string ToString() 
            => $"{Nome} - {Codigo}";
    }
}
