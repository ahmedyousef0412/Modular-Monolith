using MediatR;

namespace SharedKernel.CQRS;


// Generic command handler interface with a return type (Custom)
public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult> { }


// Returns the Guid of the created entity from the command
public interface ICommandHandler<TCommand> : ICommandHandler<TCommand, Guid> where TCommand : ICommand { }


// Returns a CommandResult from the command (success/failure with errors)
public interface IResultCommandHandler<TCommand> : ICommandHandler<TCommand, CommandResult> where TCommand : IResultCommand { }
