using MediatR;


namespace SharedKernel.CQRS;

public interface IQueryHandler<TQuery, TQueryResult> : IRequestHandler<TQuery, TQueryResult> where TQuery : IQuery<TQueryResult>
{
}

