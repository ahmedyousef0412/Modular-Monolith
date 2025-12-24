using BuildingBlocks.Application.CQRS;
using Sales.Application.Commands;

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
