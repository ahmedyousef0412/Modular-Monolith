using Sales.Domain.Repository;
using SharedKernel.CQRS;
using SharedKernel.Entities;

namespace Sales.Application.Commands;

public class UpdateQuantityCommandHandler : ICommandHandler<UpdateQuantityCommand, bool>
{

    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateQuantityCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateQuantityCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(command.OrderId, cancellationToken)
            ?? throw new ArgumentException($"Order with ID {command.OrderId} not found.");

       order.UpdateItemQuantity(command.OrderItemId, command.NewQuantity);



        return await _unitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
