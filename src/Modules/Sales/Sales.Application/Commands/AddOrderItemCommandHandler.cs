using BuildingBlocks.Application.CQRS;
using MediatR;
using Sales.Application.Abstractions;
using Sales.Domain.Entity;
using SharedKernel.Domain;
using SharedKernel.Exceptions;

namespace Sales.Application.Commands;

public class AddOrderItemCommandHandler : ICommandHandler<AddOrderItemCommand,Guid>
{
    private readonly IOrderRepository _repository;
    private readonly ISalesUnitOfWork _unitOfWork;

    public AddOrderItemCommandHandler(IOrderRepository repository, ISalesUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(AddOrderItemCommand command, CancellationToken cancellationToken)
    {
       var order = await _repository.GetByIdAsync(command.OrderId, cancellationToken)
            ?? throw new NotFoundException(nameof(Order), command.OrderId);

        order.AddItem(command.ProductId,command.ProductName, command.Quantity, command.UnitPrice);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(order.Id);


    }

   
}
