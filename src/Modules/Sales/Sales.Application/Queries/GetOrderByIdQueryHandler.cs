using Sales.Application.Services;
using Sales.Domain.Entity;
using Sales.Domain.Repository;
using SharedKernel.CQRS;
using SharedKernel.Domain;

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

    public async Task<Result<OrderDto?>> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(query.OrderId, cancellationToken);

        if (order is null)
            return Result<OrderDto?>.Failure(Error.NotFound(nameof(Order), query.OrderId));

        var dto = _orderMappingService.MapToDto(order);
        return Result<OrderDto?>.Success(dto);
    }
}
