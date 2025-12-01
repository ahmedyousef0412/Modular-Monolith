using Inventory.Application.Repository;
using Inventory.Domain.Entity;
using Inventory.Domain.Repositories;
using MediatR;
using SharedKernel.Exceptions;

namespace Inventory.Application.Commands.StockItemCommands;

public class AddStockCommandHandler : IRequestHandler<AddStockCommand>
{
    private readonly IStockRepository _stockRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IInventoryUnitOfWork _unitOfWork;

    public AddStockCommandHandler(IStockRepository stockRepository,
         IWarehouseRepository warehouseRepository,
        IInventoryUnitOfWork unitOfWork
       )
    {
        _stockRepository = stockRepository;
        _warehouseRepository = warehouseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddStockCommand request, CancellationToken cancellationToken)
    {

        //I need to check first if warehouse isActive
        var warehouse = await _warehouseRepository.GetByIdAsync(request.WarehouseId,cancellationToken)
            ?? throw new NotFoundException("Warehouse not found.");

      
        if (!warehouse.IsActive)
            throw new DomainException("Cannot add stock to an inactive warehouse.");


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
