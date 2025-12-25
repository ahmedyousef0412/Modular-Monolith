using BuildingBlocks.Application.CQRS;
using Sales.Application.Abstractions;
using Sales.Domain.Entity;
using SharedKernel.Domain;

namespace Sales.Application.Commands;

public class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _repository;
    private readonly ISalesUnitOfWork _unitOfWork;

    public DeleteOrderCommandHandler(IOrderRepository repository, ISalesUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(command.OrderId, cancellationToken);

        if (order is null) 
            return Result.Failure(Error.NotFound(nameof(Order),command.OrderId));


        order.Delete();
        
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        //I will raise a OrderCanceledEvent here later

        return Result.Success(); //Command handlers should only say Success/Failure No data to return

    }
}
