using Inventory.Application.Dtos.StockItems;

namespace Inventory.Application.Dtos.Products;

public record ProductDto(

    Guid Id,
   string Name,
   string Sku,
   string Description,

   //Stock details
   List<StockItemDto> Stock
);
