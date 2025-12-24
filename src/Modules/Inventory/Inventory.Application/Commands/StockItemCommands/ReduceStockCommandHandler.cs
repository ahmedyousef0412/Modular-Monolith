using Inventory.Application.Abstractions;
using MediatR;
using SharedKernel.Exceptions;

namespace Inventory.Application.Commands.StockItemCommands;

public class ReduceStockCommandHandler : IRequestHandler<ReduceStockCommand>
{

    private readonly IStockRepository _stockRepository;
    private readonly IInventoryUnitOfWork _unitOfWork;

    public ReduceStockCommandHandler(IStockRepository stockRepository, IInventoryUnitOfWork unitOfWork)
    {
        _stockRepository = stockRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ReduceStockCommand request, CancellationToken cancellationToken)
    {
        var stockItem = await _stockRepository
            .GetByProductAndWarehouseAsync(request.ProductId, request.WarehouseId, cancellationToken)

            ?? throw new NotFoundException($"No stock record found for Product {request.ProductId} in Warehouse {request.WarehouseId}");


        stockItem.DecreaseQuantity(request.Quantity);

        _stockRepository.Update(stockItem);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

    }
}
