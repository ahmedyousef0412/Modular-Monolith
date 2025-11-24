
using Sales.Domain.Repository;
using Sales.Infrastructure.Persistence;

using SharedKernel.Persistence;

namespace Sales.Infrastructure.Repositories;

public class SalesUnitOfWork : BaseUnitOfWork<SalesDbContext>, ISalesUnitOfWork
{
    public SalesUnitOfWork(SalesDbContext context) : base(context)
    {
    }
}
