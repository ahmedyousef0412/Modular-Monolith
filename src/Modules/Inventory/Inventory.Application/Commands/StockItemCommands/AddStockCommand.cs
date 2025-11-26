using MediatR;


namespace Inventory.Application.Commands.StockItemCommands;

public record AddStockCommand(
    Guid ProductId,
    Guid WarehouseId,
    int Quantity,
    // New Fields required for creating the record
    int MinimumQuantity = 0,
    int MaximumQuantity = 1000
 ) : IRequest;

