
using NewNerdStore.Core.Events.Types;

namespace NewNerdStore.Core.Bus
{
    public interface ICommandMediatorHandler
    {
        Task<bool> Send<T>(T command) where T : Command;
    }
}
