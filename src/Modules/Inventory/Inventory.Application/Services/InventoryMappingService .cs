using Inventory.Application.Queries.Product;
using Inventory.Domain.Entity;

namespace Inventory.Application.Services;

internal class InventoryMappingService : IInventoryMappingService
{
    public ProductDto MapToProductDto(Product product, List<StockItem> stockItems)
    {
        var stockDtos = stockItems.Select(MapToStockItemDto).ToList();

        return new ProductDto(
            Id: product.Id,
            Name: product.Name,
            Sku: product.Sku,
            Description: product.Description,
            Stock: stockDtos
        );
    }

    public StockItemDto MapToStockItemDto(StockItem stockItem)
    {
        return new StockItemDto(
            WarehouseId: stockItem.WarehouseId,
            Quantity: stockItem.Quantity,
            MinimumQuantity: stockItem.MinimumQuantity,
            MaximumQuantity: stockItem.MaximumQuantity,
            Status: stockItem.Status
        );
    }
}
