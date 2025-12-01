using Inventory.Application.Repository;
using Inventory.Domain.Entity;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Repositories;

namespace Inventory.Infrastructure.Repositories;

public class WarehouseRepository(InventoryDbContext context) : BaseRepository<Warehouse>(context), IWarehouseRepository
{
    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(w => w.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(w => w.Name == name, cancellationToken);
    }
}
