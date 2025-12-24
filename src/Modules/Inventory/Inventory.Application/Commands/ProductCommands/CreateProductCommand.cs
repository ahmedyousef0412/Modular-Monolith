using BuildingBlocks.Application.CQRS;


namespace Inventory.Application.Commands.ProductCommands;

public record CreateProductCommand(
    string Name,
    string Sku,
    string Description,
    decimal Price
) : ICommand<Guid>;
