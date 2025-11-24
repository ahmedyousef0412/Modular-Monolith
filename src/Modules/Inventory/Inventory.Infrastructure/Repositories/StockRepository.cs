using Inventory.Application.Repository;
using Inventory.Domain.Entity;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Inventory.Infrastructure.Repositories;

public class StockRepository : IStockRepository
{

    private readonly InventoryDbContext _context;

    public StockRepository(InventoryDbContext context)
    {
        _context = context;
    }

  
    public async Task<StockItem?> GetByProductAndWarehouseAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default)
    {
        return await _context.StockItems
                .FirstOrDefaultAsync(s => s.ProductId == productId && s.WarehouseId == warehouseId, cancellationToken);
    }

    public async Task<List<StockItem>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return await _context.StockItems
            .Where(si => si.ProductId == productId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<StockItem>> GetByProductIdsAsync(IEnumerable<Guid> productIds, Guid warehouseId, CancellationToken cancellationToken = default)
    {
        return await _context.StockItems
            .Where(s => s.WarehouseId == warehouseId
            && productIds.Contains(s.ProductId))
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetTotalQuantityForProductAsync(Guid productId, CancellationToken cancellationToken = default)
    {

        // This is highly efficient as it returns a single integer, not the entities.
        return await _context.StockItems
            .Where(si => si.ProductId == productId)
            .SumAsync(si => si.Quantity, cancellationToken);
    }

    public void Add(StockItem stockItem)
    {
        _context.StockItems.Add(stockItem);
    }
   
    public void Update(StockItem stockItem)
    {
        _context.StockItems.Update(stockItem);
    }
}
