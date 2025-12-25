using BuildingBlocks.Application.CQRS;

namespace Sales.Application.Commands;

public record DeleteOrderCommand(Guid OrderId) : ICommand;

