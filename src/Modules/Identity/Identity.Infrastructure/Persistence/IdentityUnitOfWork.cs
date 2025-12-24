using BuildingBlocks.Application.Persistence;
using Identity.Application.Abstractions;

namespace Identity.Infrastructure.Persistence;

public class IdentityUnitOfWork : BaseUnitOfWork<IdentityDbContext>, IIdentityUnitOfWork
{
    public IdentityUnitOfWork(IdentityDbContext context) : base(context)
    {
    }
}
