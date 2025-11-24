using SharedKernel.CQRS;

namespace Inventory.Application.Commands.ProductCommands;

public record ActiveProductCommand(Guid ProductId) : ICommand<bool>;
