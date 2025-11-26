using Inventory.Application.Queries.ProductQueries;
using Inventory.Domain.Entity;


namespace Inventory.Application.Services;

public interface IInventoryMappingService
{
    ProductDto MapToProductDto(Product product, IReadOnlyList<StockItemDto> stockItems);
    StockItemDto  MapToStockItemDto(StockItem stockItem);
}
