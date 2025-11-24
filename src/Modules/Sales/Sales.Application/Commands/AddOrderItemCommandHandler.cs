using Sales.Domain.Entity;
using Sales.Domain.Repository;
using SharedKernel.CQRS;
using SharedKernel.Entities;
using SharedKernel.Exceptions;

namespace Sales.Application.Commands;

public class AddOrderItemCommandHandler : ICommandHandler<AddOrderItemCommand>
{
    private readonly IOrderRepository _repository;
    private readonly ISalesUnitOfWork _unitOfWork;

    public AddOrderItemCommandHandler(IOrderRepository repository, ISalesUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Guid> Handle(AddOrderItemCommand command, CancellationToken cancellationToken)
    {
       var order = await _repository.GetByIdAsync(command.OrderId, cancellationToken)
            ?? throw new NotFoundException(nameof(Order), command.OrderId);

        order.AddItem(command.ProductId,command.ProductName, command.Quantity, command.UnitPrice);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Id;


    }
}
