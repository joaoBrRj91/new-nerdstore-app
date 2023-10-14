using NewNerdStore.Core.DomainObjects;

namespace NewNerdStore.Core.Data
{

    /// <summary>
    /// Repositorio por agregação. Não é persistido diretamente um elemento associado ao agregado.
    /// Por exemplo : Produto é a agregação e categoria faz parte dela; caso
    ///nosso negócio permita o CRUD de categorias iremos fazer através do
    ///do repositorio da raiz da agregação que é o Produto
    /// </summary>
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
