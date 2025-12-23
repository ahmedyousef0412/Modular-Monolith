using MediatR;
using SharedKernel.Domain;


namespace SharedKernel.CQRS;

public interface IQueryHandler<TQuery, TQueryResult> 
    : IRequestHandler<TQuery,Result< TQueryResult>> where TQuery : IQuery<TQueryResult> where TQueryResult : notnull
{
}

