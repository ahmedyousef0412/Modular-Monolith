using Inventory.Application.Dtos.Products;
using Inventory.Application.Dtos.StockItems;
using Inventory.Application.Queries.ProductQueries;
using Inventory.Domain.Entity;

namespace Inventory.Application.Services;

internal class InventoryMappingService : IInventoryMappingService
{
    public ProductDto MapToProductDto(Product product, IReadOnlyList<StockItemDto> stockItems)
    {
        return new ProductDto(
            Id: product.Id,
            Name: product.Name,
            Sku: product.Sku,
            Description: product.Description,
            Stock: [.. stockItems] 
        );
    }

    public StockItemDto MapToStockItemDto(StockItem stockItem)
    {
        return new StockItemDto(
            Id: stockItem.Id,
            WarehouseId: stockItem.WarehouseId,
            Quantity: stockItem.Quantity,
            MinimumQuantity: stockItem.MinimumQuantity,
            MaximumQuantity: stockItem.MaximumQuantity,
            Status: stockItem.Status
        );
    }
}
