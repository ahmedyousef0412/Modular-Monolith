using BuildingBlocks.Application.CQRS;
using Inventory.Application.Dtos.StockItems;

namespace Inventory.Application.Queries.StockItemQueries;

public record GetStockByProductIdsQuery(IEnumerable<Guid> ProductIds, Guid WarehouseId) :
    IQuery<IReadOnlyList<StockItemDto>>;

