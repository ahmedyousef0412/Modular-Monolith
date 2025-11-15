using Sales.Domain.Repository;
using SharedKernel.CQRS;
using SharedKernel.Entities;

namespace Sales.Application.Commands;

public class AddOrderItemCommandHandler : ICommandHandler<AddOrderItemCommand>
{
    private readonly IOrderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public AddOrderItemCommandHandler(IOrderRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Guid> Handle(AddOrderItemCommand command, CancellationToken cancellationToken)
    {
       var order = await _repository.GetByIdAsync(command.OrderId, cancellationToken)
            ?? throw new ArgumentException($"Order with ID {command.OrderId} not found.");

        order.AddItem(command.ProductName, command.Quantity, command.UnitPrice);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Id;


    }
}
