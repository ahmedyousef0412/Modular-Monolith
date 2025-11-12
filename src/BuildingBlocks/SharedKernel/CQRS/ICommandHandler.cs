using MediatR;

namespace SharedKernel.CQRS;

public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
}
public interface ICommandHandler<TCommand> : ICommandHandler<TCommand, Guid> where TCommand : ICommand
{
}

public interface IResultCommandHandler<TCommand> : ICommandHandler<TCommand, CommandResult> where TCommand : IResultCommand
{
}