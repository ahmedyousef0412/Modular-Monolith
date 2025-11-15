using Sales.Application.Commands;
using SharedKernel.CQRS;

namespace Sales.Application.Queries;

public record GetAllOrdersQuery: IQuery<IEnumerable<OrderDto>>;

public record OrderDto(
    Guid Id,
    Guid CustomerId,
    int ItemsCount,
    decimal Total,
    string Status,
    DateTime CreatedAt,
    IEnumerable<OrderItemDto> Items
);
