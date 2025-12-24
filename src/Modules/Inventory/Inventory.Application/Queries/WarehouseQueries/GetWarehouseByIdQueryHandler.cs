using BuildingBlocks.Application.CQRS;
using Inventory.Application.Abstractions;
using Inventory.Application.Dtos.Warehouses;
using Inventory.Domain.Entity;
using SharedKernel.Domain;

namespace Inventory.Application.Queries.WarehouseQueries;

public class GetWarehouseByIdQueryHandler : IQueryHandler<GetWarehouseByIdQuery, WarehouseByIdDto>
{

    private readonly IWarehouseReadRepository _warehouseReadRepository;

    public GetWarehouseByIdQueryHandler(IWarehouseReadRepository warehouseReadRepository)
    {
        _warehouseReadRepository = warehouseReadRepository;
    }

    public async Task<Result<WarehouseByIdDto>> Handle(GetWarehouseByIdQuery query, CancellationToken cancellationToken)
    {
        var warehouse = await _warehouseReadRepository.GetByIdAsync(query.Id, cancellationToken);

        if (warehouse is null)
        {
            return Result<WarehouseByIdDto>.Failure(Error.NotFound(nameof(Warehouse), query.Id));
        }

        return Result<WarehouseByIdDto>.Success(warehouse);
    }
}
