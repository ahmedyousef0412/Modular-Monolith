using Inventory.Domain.Entity;
using SharedKernel.CQRS;


namespace Inventory.Application.Queries.Product;

public record GetProductByIdQuery(Guid ProductId) : IQuery<ProductDto>;

public record ProductDto(

    Guid Id,
   string Name,
   string Sku,
   string Description,

   //Stock details
   List<StockItemDto> Stock
);

public record StockItemDto(
    Guid WarehouseId,
    int Quantity,
    int MinimumQuantity,
    int MaximumQuantity,
    StockStatus Status
);
