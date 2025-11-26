
using MediatR;

namespace Inventory.Application.Commands.StockItemCommands;

public record ReduceStockCommand(Guid ProductId, Guid WarehouseId, int Quantity) : IRequest;



//Stock decreases in many real systems:
//When a customer buys something
//When you ship an order
//When an item is damaged and removed
//When products expire
