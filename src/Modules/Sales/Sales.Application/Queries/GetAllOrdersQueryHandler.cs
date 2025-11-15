using Sales.Application.Services;
using Sales.Domain.Repository;
using SharedKernel.CQRS;

namespace Sales.Application.Queries;

public class GetAllOrdersQueryHandler : IQueryHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
{

   private readonly IOrderRepository _orderRepository;
   private readonly IOrderMappingService _orderMappingService;

    public GetAllOrdersQueryHandler(IOrderRepository orderRepository, IOrderMappingService orderMappingService)
    {
        _orderRepository = orderRepository;
        _orderMappingService = orderMappingService;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync(cancellationToken);

        return orders.Select(order => _orderMappingService.MapToDto(order));

    }
}
