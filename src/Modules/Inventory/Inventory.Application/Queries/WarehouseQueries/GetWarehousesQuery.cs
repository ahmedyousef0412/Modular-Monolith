using Inventory.Application.Dtos.Warehouses;
using MediatR;

namespace Inventory.Application.Queries.WarehouseQueries;

public record GetWarehousesQuery(bool IncludeInactive = false) : IRequest<List<WarehouseDto>>;



