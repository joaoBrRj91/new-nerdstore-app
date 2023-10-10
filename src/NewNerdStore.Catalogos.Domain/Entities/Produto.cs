using NewNerdStore.Catalogos.Domain.ValueObjects;
using NewNerdStore.Core.DomainObjects;

namespace NewNerdStore.Catalogos.Domain.Entities
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
            Guid categoriaId,
            Dimensoes dimensoes,
            int quantidadeEstoque = 0)
        {
            CategoriaId = categoriaId;
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
            QuantidadeEstoque = quantidadeEstoque;
            DataCadastro = dataCadastro;
            Imagem = imagem;
            Dimensoes = dimensoes;

            Validar();
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

        public Guid CategoriaId { get; private set; }

        public Dimensoes Dimensoes { get; private set; }

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
            => QuantidadeEstoque >= quantidade;
        #endregion

        #region Validate
        public void Validar()
        {
            Validations.ValidarSeVazio(Nome, "O campo Nome do produto não pode estar vazio");
            Validations.ValidarSeVazio(Descricao, "O campo Descricao do produto não pode estar vazio");
            Validations.ValidarSeIgual(CategoriaId, Guid.Empty, "O campo CategoriaId do produto não pode estar vazio");
            Validations.ValidarSeMenorQue(Valor, 1, "O campo Valor do produto não pode se menor igual a 0");
            Validations.ValidarSeVazio(Imagem, "O campo Imagem do produto não pode estar vazio");
        }
        #endregion

    }
}
