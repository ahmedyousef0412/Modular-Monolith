using Inventory.Application.Repository;
using Inventory.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Repositories;

namespace Inventory.Infrastructure.Repositories;

public class StockRepository : BaseRepository<StockItem>, IStockRepository
{
    public StockRepository(DbContext context) : base(context)
    {
    }

    public async Task<StockItem?> GetByProductAndWarehouseAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(s => s.ProductId == productId && s.WarehouseId == warehouseId, cancellationToken);
            
    }
}
