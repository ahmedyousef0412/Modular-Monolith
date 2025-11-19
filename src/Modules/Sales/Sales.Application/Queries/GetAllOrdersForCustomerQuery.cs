using SharedKernel.CQRS;

namespace Sales.Application.Queries;

public record GetAllOrdersForCustomerQuery(Guid CustomerId) : IQuery<IEnumerable<OrderDto>>;

