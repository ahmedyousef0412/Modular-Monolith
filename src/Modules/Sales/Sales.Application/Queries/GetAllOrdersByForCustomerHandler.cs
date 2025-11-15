using Sales.Application.Services;
using Sales.Domain.Repository;
using SharedKernel.CQRS;

namespace Sales.Application.Queries;

public class GetAllOrdersByForCustomerHandler : IQueryHandler<GetAllOrdersByForCustomerQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderMappingService _orderMappingService;

    public GetAllOrdersByForCustomerHandler(IOrderRepository orderRepository, 
        IOrderMappingService orderMappingService)
    {
        _orderRepository = orderRepository;
        _orderMappingService = orderMappingService;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersByForCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetByCustomerIdAsync(query.CustomerId, cancellationToken);

        return orders.Select(order => _orderMappingService.MapToDto(order));
    }
}
