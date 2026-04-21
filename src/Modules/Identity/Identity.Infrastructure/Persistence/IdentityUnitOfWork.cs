using BuildingBlocks.Application.Persistence;
using MediatR;


namespace Identity.Infrastructure.Persistence;

public class IdentityUnitOfWork : BaseUnitOfWork<IdentityDbContext>, IIdentityUnitOfWork
{
    public IdentityUnitOfWork(IdentityDbContext context, IMediator mediator) : base(context, mediator)
    {
    }
}
