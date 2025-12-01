using Inventory.Application.Dtos.StockItems;
using SharedKernel.CQRS;

namespace Inventory.Application.Queries.StockItemQueries;

public record GetStockByProductIdsQuery(IEnumerable<Guid> ProductIds, Guid WarehouseId) :
    IQuery<IReadOnlyList<StockItemDto>>;

