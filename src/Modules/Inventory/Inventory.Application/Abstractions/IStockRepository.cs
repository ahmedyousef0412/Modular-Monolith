using Inventory.Domain.Entity;

namespace Inventory.Application.Abstractions;

public interface IStockRepository
{
    // Required to check if stock exists before adding/updating
    Task<StockItem?> GetByProductAndWarehouseAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default);
    void Add(StockItem stockItem);
    void Update(StockItem stockItem);

}
