using Sales.Domain.Entity;
using Sales.Domain.Repository;
using SharedKernel.CQRS;
using SharedKernel.Entities;

namespace Sales.Application.Commands;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Guid>
{

    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = Order.Create(command.CustomerId);


        foreach (var item in command.Items)
            order.AddItem(item.ProductName, item.Quantity, item.UnitPrice);

         _orderRepository.Add(order, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Id;

    }
}
