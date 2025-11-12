using MediatR;

namespace SharedKernel.CQRS;

public interface IQuery<TQueryResult> : IRequest<TQueryResult>
{
}
