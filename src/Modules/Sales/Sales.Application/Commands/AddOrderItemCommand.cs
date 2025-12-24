using BuildingBlocks.Application.CQRS;


namespace Sales.Application.Commands;

public record AddOrderItemCommand(
    Guid OrderId,
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice) : ICommand;

