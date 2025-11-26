using Inventory.Application.Repository;
using Inventory.Domain.Repositories;
using MediatR;
using SharedKernel.Exceptions;

namespace Inventory.Application.Commands.StockItemCommands;

public class UpdateStockThresholdsCommandHandler : IRequestHandler<UpdateStockThresholdsCommand>
{

    private readonly IStockRepository _stockRepository;
    private readonly IInventoryUnitOfWork _unitOfWork;

    public UpdateStockThresholdsCommandHandler(IStockRepository stockRepository, IInventoryUnitOfWork unitOfWork)
    {
        _stockRepository = stockRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateStockThresholdsCommand request, CancellationToken cancellationToken)
    {
        var stockItem = await _stockRepository.GetByProductAndWarehouseAsync
            (
            request.ProductId,
            request.WarehouseId,
            cancellationToken
            ) ?? throw new NotFoundException($"Stock record not found for Product {request.ProductId} in Warehouse {request.WarehouseId}");
    
       
        stockItem.UpdateThresholds(request.MaximumQuantity, request.MinimumQuantity);

        _stockRepository.Update(stockItem);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
