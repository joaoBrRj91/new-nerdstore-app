using NewNerdStore.Core.DomainObjects;

namespace NewNerdStore.Catalogos.Domain
{
    public class Categoria : Entity
    {
        public Categoria(string nome, string codigo)
        {
            Nome = nome;
            Codigo = codigo;

            Validar();
        }

        protected Categoria(): base() { }

        public string Nome { get; private set; }

        public string Codigo { get; private set; }

        public override string ToString() 
            => $"{Nome} - {Codigo}";

        public void Validar()
        {
            Validations.ValidarSeVazio(Nome, "O campo Nome da categoria não pode estar vazio");
            Validations.ValidarSeIgual(Codigo, 0, "O campo Codigo não pode ser 0");
        }
    }
}
