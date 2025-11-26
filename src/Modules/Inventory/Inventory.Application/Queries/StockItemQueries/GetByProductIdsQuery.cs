using Inventory.Application.Queries.ProductQueries;
using SharedKernel.CQRS;

namespace Inventory.Application.Queries.StockItemQueries;

public record GetByProductIdsQuery(IEnumerable<Guid> ProductIds, Guid WarehouseId) :
    IQuery<IReadOnlyList<StockItemDto>>;

