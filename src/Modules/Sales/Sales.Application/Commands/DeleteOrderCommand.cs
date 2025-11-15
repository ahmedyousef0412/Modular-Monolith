using SharedKernel.CQRS;

namespace Sales.Application.Commands;

public record DeleteOrderCommand(Guid OrderId) : IResultCommand;

