using BuildingBlocks.Application.Persistence;
using Inventory.Application.Abstractions;
using MediatR;


namespace Inventory.Infrastructure.Persistence;

public class InventoryUnitOfWork : BaseUnitOfWork<InventoryDbContext>, IInventoryUnitOfWork
{
    public InventoryUnitOfWork(InventoryDbContext context, IMediator mediator) : base(context, mediator)
    {
    }
}
