

using Inventory.Domain.Entity;

namespace Inventory.Application.Repository;

public interface IStockRepository
{
    Task<StockItem?> GetByProductAndWarehouseAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default);
    Task<List<StockItem>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
    Task<int> GetTotalQuantityForProductAsync(Guid productId, CancellationToken cancellationToken = default);


    // 4. Bulk Loading (Optional but recommended for Orders)
    // "I need stock for these 5 products to check an order."
    Task<List<StockItem>> GetByProductIdsAsync(IEnumerable<Guid> productIds, Guid warehouseId, CancellationToken cancellationToken = default);

    void Add(StockItem stockItem);
    void Update(StockItem stockItem);

   
}
