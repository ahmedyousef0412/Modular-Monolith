
using BuildingBlocks.Application.Persistence;
using MediatR;
using Sales.Application.Abstractions;

namespace Sales.Infrastructure.Persistence;

public class SalesUnitOfWork : BaseUnitOfWork<SalesDbContext>, ISalesUnitOfWork
{
    public SalesUnitOfWork(SalesDbContext context, IMediator mediator) : base(context, mediator)
    {
    }
}
