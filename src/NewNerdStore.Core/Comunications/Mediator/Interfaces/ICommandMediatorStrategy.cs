using NewNerdStore.Core.Messages.Abstracts;

namespace NewNerdStore.Core.Comunications.Mediator.Interfaces
{
    public interface ICommandMediatorStrategy
    {
        Task<bool> Send<T>(T command) where T : Command;
    }
}
