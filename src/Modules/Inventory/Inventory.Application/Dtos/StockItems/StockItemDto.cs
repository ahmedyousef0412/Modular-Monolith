using Inventory.Domain.Entity;

namespace Inventory.Application.Dtos.StockItems;

public record StockItemDto(
    Guid Id,
    Guid WarehouseId,
    int Quantity,
    int MinimumQuantity,
    int MaximumQuantity,
    StockStatus Status
);