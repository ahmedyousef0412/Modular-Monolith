using Identity.Domain.Repositories;
using Identity.Infrastructure.Persistence;
using SharedKernel.Persistence;

namespace Identity.Infrastructure.Repositories;

public class IdentityUnitOfWork : BaseUnitOfWork<IdentityDbContext>, IIdentityUnitOfWork
{
    public IdentityUnitOfWork(IdentityDbContext context) : base(context)
    {
    }
}
