using BuildingBlocks.Application.Abstractions;
using Inventory.Application.Abstractions;
using Inventory.Domain.Entity;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories;

public class StockRepository : BaseRepository<StockItem>, IStockRepository
{
    public StockRepository(InventoryDbContext context) : base(context)
    {
    }

    public async Task<StockItem?> GetByProductAndWarehouseAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(s => s.ProductId == productId && s.WarehouseId == warehouseId, cancellationToken);
            
    }
}
