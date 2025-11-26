using Inventory.Application.Queries.ProductQueries;


namespace Inventory.Application.Repository;

public interface IStockReadRepository
{

    Task<StockItemDto?> GetById(Guid stockItemId, CancellationToken cancellationToken = default); 
    Task<StockItemDto?> GetByProductAndWarehouseAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<StockItemDto>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
    Task<int> GetTotalQuantityForProductAsync(Guid productId, CancellationToken cancellationToken = default);


    // 4. Bulk Loading (Optional but recommended for Orders)
    // "I need stock for these 5 products to check an order."
    //"Do we have the iPhone AND the Case AND the Charger in the Cairo Branch?"
    Task<IReadOnlyList<StockItemDto>> GetByProductIdsAndWarehouseAsync(IEnumerable<Guid> productIds, Guid warehouseId, CancellationToken cancellationToken = default);

}
