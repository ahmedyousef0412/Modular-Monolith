using BuildingBlocks.Application.CQRS;

namespace Inventory.Application.Commands.ProductCommands;

public record DeactiveProductCommand(Guid ProductId) : ICommand<bool>;
   
