using Sales.Application.Services;
using Sales.Domain.Repository;
using SharedKernel.CQRS;

namespace Sales.Application.Queries;

public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto?>
{

    private readonly IOrderRepository _orderRepository;
    private readonly IOrderMappingService _orderMappingService;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IOrderMappingService orderMappingService)
    {
        _orderRepository = orderRepository;
        _orderMappingService = orderMappingService;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(query.OrderId, cancellationToken)
            ?? throw new ArgumentException($"Order with ID {query.OrderId} not found.");

        return _orderMappingService.MapToDto(order);
    }
}
