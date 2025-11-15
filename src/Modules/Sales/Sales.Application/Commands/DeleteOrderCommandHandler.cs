using Sales.Domain.Repository;
using SharedKernel.CQRS;
using SharedKernel.Entities;

namespace Sales.Application.Commands;

public class DeleteOrderCommandHandler : IResultCommandHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderCommandHandler(IOrderRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(command.OrderId, cancellationToken);

        if (order is null) 
            return CommandResult.Failure(["Order not found."]);


        order.Delete();
        
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        //I will raise a OrderCanceledEvent here later

        return CommandResult.Success(); //Command handlers should only say Success/Failure No data to return

    }
}
