using BuildingBlocks.Application.CQRS;
using Inventory.Application.Abstractions;
using MediatR;


namespace Inventory.Application.Commands.WarehouseCommands;

public class UpdateWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IInventoryUnitOfWork inventoryUnitOfWork) 
    : IRequestHandler<UpdateWarehouseCommand, CommandResult>
{
    private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;
    private readonly IInventoryUnitOfWork _inventoryUnitOfWork = inventoryUnitOfWork;

    public async Task<CommandResult> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(request.Id, cancellationToken);

        if (warehouse is null)
            return CommandResult.Failure(["Warehouse not found."]);


        if (!string.Equals(warehouse.Name, request.Name, StringComparison.OrdinalIgnoreCase))
        {
            bool isNameTaken = await _warehouseRepository.ExistsByNameAsync(request.Name, cancellationToken);
            if (isNameTaken)
            {
                return CommandResult.Failure([$"The name '{request.Name}' is already in use."]);
            }
        }

        warehouse.UpdateDetails(request.Name, request.Location);

        _warehouseRepository.Update(warehouse);

        await  _inventoryUnitOfWork.SaveChangesAsync(cancellationToken);
        
        return CommandResult.Success();

    }
}
