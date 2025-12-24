using BuildingBlocks.Application.CQRS;
using Sales.Application.Abstractions;
using Sales.Application.Services;
using SharedKernel.Domain;

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

    public async Task<Result<IEnumerable<OrderDto>>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync(cancellationToken);

        var dto = orders.Select(order => _orderMappingService.MapToDto(order));

        return Result<IEnumerable<OrderDto>>.Success(dto);

    }
}
