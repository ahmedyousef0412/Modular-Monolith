using BuildingBlocks.Application.CQRS;
using Inventory.Application.Abstractions;
using Inventory.Domain.Entity;
using SharedKernel.Domain;


namespace Inventory.Application.Commands.WarehouseCommands;

public class UpdateWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IInventoryUnitOfWork inventoryUnitOfWork) 
    : ICommandHandler<UpdateWarehouseCommand>
{
    private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;
    private readonly IInventoryUnitOfWork _inventoryUnitOfWork = inventoryUnitOfWork;

    public async Task<Result> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(request.Id, cancellationToken);

        if (warehouse is null)
            return Result.Failure(Error.NotFound(nameof(Warehouse),request.Id));


        if (!string.Equals(warehouse.Name, request.Name, StringComparison.OrdinalIgnoreCase))
        {
            bool isNameTaken = await _warehouseRepository.ExistsByNameAsync(request.Name, cancellationToken);
            if (isNameTaken)
            {
                return Result.Failure(Error.Conflict("Warehouse with this name  already exists."));
            }
        }

        warehouse.UpdateDetails(request.Name, request.Location);

        _warehouseRepository.Update(warehouse);

        await  _inventoryUnitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();

    }
}
