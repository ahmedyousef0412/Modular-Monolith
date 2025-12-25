using BuildingBlocks.Application.CQRS;

namespace Inventory.Application.Commands.ProductCommands;

public record UpdateProductCommand(
    string Name,
    string Sku,
    string Description,
    decimal Price
) : ICommand;

public record UpdateProductRequest(Guid Id, UpdateProductCommand Command) : ICommand;
