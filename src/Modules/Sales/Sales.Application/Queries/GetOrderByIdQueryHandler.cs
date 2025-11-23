using Sales.Application.Services;
using Sales.Domain.Entity;
using Sales.Domain.Repository;
using SharedKernel.CQRS;
using SharedKernel.Exceptions;

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
            ?? throw new NotFoundException(nameof(Order), query.OrderId);

        return _orderMappingService.MapToDto(order);
    }
}
