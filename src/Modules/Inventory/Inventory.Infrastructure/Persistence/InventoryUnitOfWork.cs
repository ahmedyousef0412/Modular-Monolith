using BuildingBlocks.Application.Persistence;
using Inventory.Application.Abstractions;


namespace Inventory.Infrastructure.Persistence;

public class InventoryUnitOfWork : BaseUnitOfWork<InventoryDbContext>, IInventoryUnitOfWork
{
    public InventoryUnitOfWork(InventoryDbContext context) : base(context)
    {
    }
}
