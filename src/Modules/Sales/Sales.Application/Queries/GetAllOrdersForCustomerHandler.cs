using Sales.Application.Services;
using Sales.Domain.Repository;
using SharedKernel.CQRS;
using SharedKernel.Domain;

namespace Sales.Application.Queries;

public class GetAllOrdersForCustomerHandler : IQueryHandler<GetAllOrdersForCustomerQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderMappingService _orderMappingService;

    public GetAllOrdersForCustomerHandler(IOrderRepository orderRepository, 
        IOrderMappingService orderMappingService)
    {
        _orderRepository = orderRepository;
        _orderMappingService = orderMappingService;
    }

    public async Task<Result<IEnumerable<OrderDto>>> Handle(GetAllOrdersForCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetByCustomerIdAsync(query.CustomerId, cancellationToken);

        var dto = orders.Select(order => _orderMappingService.MapToDto(order));

        return Result<IEnumerable<OrderDto>>.Success(dto);
    }
}
