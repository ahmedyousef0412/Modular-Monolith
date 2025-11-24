using Inventory.Domain.Repositories;
using Inventory.Infrastructure.Persistence;

using SharedKernel.Persistence;


namespace Inventory.Infrastructure.Repositories;

public class InventoryUnitOfWork : BaseUnitOfWork<InventoryDbContext>, IInventoryUnitOfWork
{
    public InventoryUnitOfWork(InventoryDbContext context) : base(context)
    {
    }
}
