using Inventory.Application.Abstractions;
using MediatR;
using SharedKernel.Exceptions;

namespace Inventory.Application.Commands.WarehouseCommands;

public class DeactivateWarehouseCommandHandler(IInventoryUnitOfWork inventoryUnitOfWork, IWarehouseRepository warehouseRepository)
    : IRequestHandler<DeactivateWarehouseCommand>
{

    private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;
    private readonly IInventoryUnitOfWork _inventoryUnitOfWork = inventoryUnitOfWork;

    public async Task Handle(DeactivateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(request.Id, cancellationToken)
        ?? throw new NotFoundException("Warehouse not found.");

        warehouse.Deactivate();

        await _inventoryUnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
