using SharedKernel.CQRS;

namespace Inventory.Application.Commands.ProductCommands;

public record DeactiveProductCommand(Guid ProductId) : ICommand<bool>;
   
