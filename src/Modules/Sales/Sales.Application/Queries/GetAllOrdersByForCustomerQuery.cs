using SharedKernel.CQRS;

namespace Sales.Application.Queries;

public record GetAllOrdersByForCustomerQuery(Guid CustomerId) : IQuery<IEnumerable<OrderDto>>;

