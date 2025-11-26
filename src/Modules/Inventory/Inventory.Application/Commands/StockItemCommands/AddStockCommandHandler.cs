using Inventory.Application.Repository;
using Inventory.Domain.Entity;
using Inventory.Domain.Repositories;
using MediatR;

namespace Inventory.Application.Commands.StockItemCommands;

public class AddStockCommandHandler : IRequestHandler<AddStockCommand>
{
    private readonly IStockRepository _stockRepository;
    private readonly IInventoryUnitOfWork _unitOfWork;

    public AddStockCommandHandler(IStockRepository stockRepository, IInventoryUnitOfWork unitOfWork)
    {
        _stockRepository = stockRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddStockCommand request, CancellationToken cancellationToken)
    {
        var stockItem = await _stockRepository
            .GetByProductAndWarehouseAsync(request.ProductId, request.WarehouseId, cancellationToken);

        if (stockItem is null)
        {
            stockItem = StockItem.Create(
                request.ProductId,
                request.WarehouseId,
                request.Quantity,
                request.MinimumQuantity,
                request.MaximumQuantity
             );

            _stockRepository.Add(stockItem);
        }
        else
        {
            stockItem.IncreaseQuantity( request.Quantity );
            _stockRepository.Update(stockItem);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
