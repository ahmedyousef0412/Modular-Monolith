using Inventory.Application.Queries.ProductQueries;
using SharedKernel.CQRS;

namespace Inventory.Application.Queries.StockItemQueries;

public record GetByProductIdQuery(Guid ProductId) : IQuery<IReadOnlyList<StockItemDto>>;


