using MediatR;
using NewNerdStore.Core.Events.Types;

namespace NewNerdStore.Core.Bus
{
    public class CommandMediatorHandler : ICommandMediatorHandler
    {
        private readonly IMediator _mediator;

        public CommandMediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Send<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}
