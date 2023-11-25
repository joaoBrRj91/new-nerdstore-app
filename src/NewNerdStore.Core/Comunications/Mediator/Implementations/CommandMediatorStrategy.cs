using MediatR;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Abstracts;

namespace NewNerdStore.Core.Comunications.Mediator.Implementations
{
    public class CommandMediatorStrategy : ICommandMediatorStrategy
    {
        private readonly IMediator _mediator;

        public CommandMediatorStrategy(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Send<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}
