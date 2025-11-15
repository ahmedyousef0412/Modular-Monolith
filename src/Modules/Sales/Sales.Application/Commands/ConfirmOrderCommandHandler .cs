using Sales.Domain.Repository;
using SharedKernel.CQRS;
using SharedKernel.Entities;
using SharedKernel.Exceptions;


namespace Sales.Application.Commands;

public class ConfirmOrderCommandHandler : ICommandHandler<ConfirmOrderCommand, bool>
{
    private readonly IOrderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmOrderCommandHandler(IOrderRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(ConfirmOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(command.OrderId, cancellationToken)
           ?? throw new ArgumentException("Order not found");

        order.MarkAsPaid();

        return await _unitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
