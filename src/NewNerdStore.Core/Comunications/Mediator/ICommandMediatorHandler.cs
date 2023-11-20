using NewNerdStore.Core.Messages.Abstracts;

namespace NewNerdStore.Core.Comunications.Mediator
{
    public interface ICommandMediatorHandler
    {
        Task<bool> Send<T>(T command) where T : Command;
    }
}
