using Inventory.Application.Dtos.Warehouses;
using SharedKernel.CQRS;

namespace Inventory.Application.Queries.WarehouseQueries;

public record GetWarehouseByIdQuery(Guid Id) : IQuery<WarehouseByIdDto>;

