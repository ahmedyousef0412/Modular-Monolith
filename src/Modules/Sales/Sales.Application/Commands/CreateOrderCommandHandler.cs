using BuildingBlocks.Application.CQRS;
using Sales.Application.Abstractions;
using Sales.Application.Ports;
using Sales.Domain.Entity;
using SharedKernel.Domain;

namespace Sales.Application.Commands;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Guid>
{

    private readonly IOrderRepository _orderRepository;
    private readonly IInventoryGateway _inventoryGateway;
    private readonly ISalesUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, ISalesUnitOfWork unitOfWork, IInventoryGateway inventoryGateway)
    {
        _orderRepository = orderRepository;
        _inventoryGateway = inventoryGateway;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = Order.Create(command.CustomerId);


        foreach (var item in command.Items)
        {
            ProductInfo? productInfo = await _inventoryGateway.GetProductInfoAsync(item.ProductId, cancellationToken)
                
                ?? throw new InvalidOperationException($"Product with ID {item.ProductId} not found.");


            order.AddItem(productInfo.Id, productInfo.Name, item.Quantity, productInfo.Price);
        }


        _orderRepository.Add(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(order.Id);

    }
}
