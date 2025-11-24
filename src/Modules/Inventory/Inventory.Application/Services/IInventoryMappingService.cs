using Inventory.Application.Queries.Product;
using Inventory.Domain.Entity;


namespace Inventory.Application.Services;

public interface IInventoryMappingService
{
    ProductDto MapToProductDto(Product product, List<StockItem> stockItems);
    StockItemDto  MapToStockItemDto(StockItem stockItem);
}
