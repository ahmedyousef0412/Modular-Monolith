using MediatR;
using SharedKernel.Domain;

namespace BuildingBlocks.Application.CQRS;


//// Generic command handler interface with a return type (Custom)
//public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand,Result<TResult>> where TCommand : ICommand<TResult> { }


//// Returns the Guid of the created entity from the command
//public interface ICommandHandler<TCommand> : ICommandHandler<TCommand, Guid> where TCommand : ICommand { }


//// Returns a CommandResult from the command (success/failure with errors)
//public interface IResultCommandHandler<TCommand> : ICommandHandler<TCommand, CommandResult> where TCommand : IResultCommand { }


public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

// Shortcut for Value Handlers
public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}