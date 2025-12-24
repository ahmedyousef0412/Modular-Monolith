using Inventory.Application.Abstractions;
using Inventory.Domain.Entity;
using MediatR;
using SharedKernel.Exceptions;

namespace Inventory.Application.Commands.WarehouseCommands;

public class CreateWarehouseCommandHandler(IWarehouseRepository warehouseRepository,IInventoryUnitOfWork inventoryUnitOfWork)
    : IRequestHandler<CreateWarehouseCommand,Guid>
{

    private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;
    private readonly IInventoryUnitOfWork _inventoryUnitOfWork = inventoryUnitOfWork;
    public async Task<Guid> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var exists = await _warehouseRepository.ExistsByNameAsync(request.Name, cancellationToken);

        if (exists)
            throw new DomainException($"A warehouse with the name '{request.Name}' already exists.");

        var warehouse = Warehouse.Create(request.Name, request.Location);

        _warehouseRepository.Add(warehouse);

        await _inventoryUnitOfWork.SaveChangesAsync(cancellationToken);

        return warehouse.Id;

    }
}
