using BuildingBlocks.Application.CQRS;
using Sales.Application.Abstractions;
using Sales.Domain.Entity;
using SharedKernel.Domain;
using SharedKernel.Exceptions;

namespace Sales.Application.Commands;

public class MarkOrderAsPaidHandler : ICommandHandler<MarkOrderAsPaidCommand,bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ISalesUnitOfWork _unitOfWork;

    public MarkOrderAsPaidHandler(IOrderRepository orderRepository, ISalesUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(MarkOrderAsPaidCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(command.OrderId, cancellationToken) 
            ?? throw new NotFoundException(nameof(Order), command.OrderId);


        order.MarkAsPaid();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(true);
    }
}