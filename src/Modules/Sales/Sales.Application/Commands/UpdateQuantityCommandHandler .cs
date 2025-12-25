using BuildingBlocks.Application.CQRS;
using Sales.Application.Abstractions;
using Sales.Domain.Entity;
using SharedKernel.Domain;
using SharedKernel.Exceptions;

namespace Sales.Application.Commands;

public class UpdateQuantityCommandHandler : ICommandHandler<UpdateQuantityCommand, bool>
{

    private readonly IOrderRepository _orderRepository;
    private readonly ISalesUnitOfWork _unitOfWork;

    public UpdateQuantityCommandHandler(IOrderRepository orderRepository, ISalesUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(UpdateQuantityCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(command.OrderId, cancellationToken)
            ?? throw new NotFoundException(nameof(Order), command.OrderId);

        order.UpdateItemQuantity(command.OrderItemId, command.NewQuantity);


         await _unitOfWork.SaveEntitiesAsync(cancellationToken);
        return Result.Success(true);
    }
}
