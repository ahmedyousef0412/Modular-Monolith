
using BuildingBlocks.Application.Persistence;
using Sales.Application.Abstractions;

namespace Sales.Infrastructure.Persistence;

public class SalesUnitOfWork : BaseUnitOfWork<SalesDbContext>, ISalesUnitOfWork
{
    public SalesUnitOfWork(SalesDbContext context) : base(context)
    {
    }
}
