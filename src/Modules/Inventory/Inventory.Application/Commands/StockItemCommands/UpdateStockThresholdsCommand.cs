using MediatR;


namespace Inventory.Application.Commands.StockItemCommands;

public record UpdateStockThresholdsCommand(
    Guid ProductId,
    Guid WarehouseId,
    int MinimumQuantity,
    int MaximumQuantity
) : IRequest;