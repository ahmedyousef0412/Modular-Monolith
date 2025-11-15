using SharedKernel.CQRS;

namespace Sales.Application.Commands;

public record AddOrderItemCommand(
    Guid OrderId,
    string ProductName,
    int Quantity,
    decimal UnitPrice) : ICommand;

