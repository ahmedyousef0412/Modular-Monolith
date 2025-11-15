
using SharedKernel.CQRS;

namespace Sales.Application.Commands;

public record CreateOrderCommand(
    Guid CustomerId,
    IEnumerable<OrderItemDto> Items)
    : ICommand<Guid>;



// DTO for items
public record OrderItemDto(
    string ProductName,
    int Quantity,
    decimal UnitPrice
);