using MediatR;

namespace SharedKernel.CQRS;

public interface ICommand<TResult> : IRequest<TResult>
{
}
public interface ICommand : ICommand<Guid>
{

}

public interface IResultCommand : ICommand<CommandResult>
{

}