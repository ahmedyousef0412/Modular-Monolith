using SharedKernel.CQRS;


namespace Sales.Application.Commands;


public record UpdateQuantityCommand(
    Guid OrderId,
    Guid OrderItemId,
    int NewQuantity
) : ICommand<bool>;
