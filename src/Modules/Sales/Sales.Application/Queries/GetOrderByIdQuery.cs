using SharedKernel.CQRS;

namespace Sales.Application.Queries;

public record GetOrderByIdQuery(Guid OrderId) : IQuery<OrderDto?>;

