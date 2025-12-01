using Inventory.Application.Dtos.Warehouses;
using Inventory.Application.Repository;
using MediatR;
using SharedKernel.CQRS;
using SharedKernel.Exceptions;

namespace Inventory.Application.Queries.WarehouseQueries;

public class GetWarehouseByIdQueryHandler : IQueryHandler<GetWarehouseByIdQuery, WarehouseByIdDto>
{

    private readonly IWarehouseReadRepository _warehouseReadRepository;

    public GetWarehouseByIdQueryHandler(IWarehouseReadRepository warehouseReadRepository)
    {
        _warehouseReadRepository = warehouseReadRepository;
    }

    public async Task<WarehouseByIdDto> Handle(GetWarehouseByIdQuery query, CancellationToken cancellationToken)
    {
        var warehouse = await _warehouseReadRepository.GetByIdAsync(query.Id, cancellationToken)
            ?? throw new NotFoundException("Warehouse", query.Id);


        return warehouse;
    }
}
