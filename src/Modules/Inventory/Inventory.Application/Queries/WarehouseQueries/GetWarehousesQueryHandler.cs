using Inventory.Application.Abstractions;
using Inventory.Application.Dtos.Warehouses;
using MediatR;

namespace Inventory.Application.Queries.WarehouseQueries;

public class GetWarehousesQueryHandler(IWarehouseReadRepository warehouseReadRepository) : IRequestHandler<GetWarehousesQuery, List<WarehouseDto>>
{

    private readonly IWarehouseReadRepository _warehouseReadRepository = warehouseReadRepository;

    public async Task<List<WarehouseDto>> Handle(GetWarehousesQuery query, CancellationToken cancellationToken)
    {
        var result = await _warehouseReadRepository.GetAllAsync(query.IncludeInactive, cancellationToken);

        return [.. result];
    }
}
