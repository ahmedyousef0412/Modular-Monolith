using Inventory.Application.Repository;
using Inventory.Domain.Repositories;
using MediatR;
using SharedKernel.Exceptions;

namespace Inventory.Application.Commands.WarehouseCommands;

public class ActivateWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IInventoryUnitOfWork inventoryUnitOfWork) 
    : IRequestHandler<ActivateWarehouseCommand>
{

    private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;
    private readonly IInventoryUnitOfWork _inventoryUnitOfWork = inventoryUnitOfWork;

    public async Task Handle(ActivateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(request.Id, cancellationToken)
        ?? throw new NotFoundException("Warehouse not found.");

        warehouse.Activate();

      await  _inventoryUnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
