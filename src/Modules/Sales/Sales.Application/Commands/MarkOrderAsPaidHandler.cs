using MediatR;
using Sales.Domain.Repository;
using SharedKernel.CQRS;
using SharedKernel.Entities;

namespace Sales.Application.Commands;

public class MarkOrderAsPaidHandler : ICommandHandler<MarkOrderAsPaidCommand,bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MarkOrderAsPaidHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(MarkOrderAsPaidCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(command.OrderId, cancellationToken) 
            ?? throw new KeyNotFoundException($"Order {command.OrderId} not found.");


        order.MarkAsPaid();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}