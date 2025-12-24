using Inventory.Application.Dtos.StockItems;


namespace Inventory.Application.Abstractions;

public interface IStockReadRepository
{

    Task<StockItemDto?> GetById(Guid stockItemId, CancellationToken cancellationToken = default); 
    Task<StockItemDto?> GetByProductAndWarehouseAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<StockItemDto>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
    Task<int> GetTotalQuantityForProductAsync(Guid productId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<StockItemDto>> GetByProductIdsAndWarehouseAsync(IEnumerable<Guid> productIds, Guid warehouseId, CancellationToken cancellationToken = default);

}
