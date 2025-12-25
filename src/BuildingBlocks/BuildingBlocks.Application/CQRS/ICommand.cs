using MediatR;
using SharedKernel.Domain;

namespace BuildingBlocks.Application.CQRS;


// Generic command interface with a return type (Custom)
//public interface ICommand<TResult> : IRequest<Result<TResult>> { }


// Returns the Guid of the created entity from the command
//public interface ICommand : ICommand<Guid> { }

//Returns a CommandResult from the command (success/failure with errors)
//public interface IResultCommand : ICommand<CommandResult> { }
public interface ICommand : IRequest<Result>
{
}

// 2. Value Command (Create, or Complex Actions)
// It returns "Result<TResponse>" (Success/Fail + Data)
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}