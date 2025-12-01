using Inventory.Application.Dtos.StockItems;
using SharedKernel.CQRS;

namespace Inventory.Application.Queries.StockItemQueries;

public record GetStockByProductIdQuery(Guid ProductId) : IQuery<IReadOnlyList<StockItemDto>>;


