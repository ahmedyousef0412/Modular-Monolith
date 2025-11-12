using MediatR;

namespace SharedKernel.CQRS;


// Generic command interface with a return type (Custom)
public interface ICommand<TResult> : IRequest<TResult> { }


// Returns the Guid of the created entity from the command
public interface ICommand : ICommand<Guid> { }

//Returns a CommandResult from the command (success/failure with errors)
public interface IResultCommand : ICommand<CommandResult> { }
