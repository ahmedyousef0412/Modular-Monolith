using BuildingBlocks.Application.CQRS;

namespace Sales.Application.Commands;

public record ConfirmOrderCommand(Guid OrderId) : ICommand<bool>;