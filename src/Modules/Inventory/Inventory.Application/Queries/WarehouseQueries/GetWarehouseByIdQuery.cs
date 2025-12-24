using BuildingBlocks.Application.CQRS;
using Inventory.Application.Dtos.Warehouses;

namespace Inventory.Application.Queries.WarehouseQueries;

public record GetWarehouseByIdQuery(Guid Id) : IQuery<WarehouseByIdDto>;

