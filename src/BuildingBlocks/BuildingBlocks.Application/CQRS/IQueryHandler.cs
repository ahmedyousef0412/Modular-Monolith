using MediatR;
using SharedKernel.Domain;


namespace BuildingBlocks.Application.CQRS;

public interface IQueryHandler<TQuery, TQueryResult> 
    : IRequestHandler<TQuery,Result< TQueryResult>> where TQuery : IQuery<TQueryResult> 
{
}

