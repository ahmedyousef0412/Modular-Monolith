using Inventory.Application.Repository;
using SharedKernel.CQRS;

namespace Inventory.Application.Queries.StockItemQueries;

public class GetTotalQuantityForProductQueryHandler : IQueryHandler<GetTotalQuantityForProductQuery, int>
{

    private readonly IStockReadRepository _stockReadRepository;

    public GetTotalQuantityForProductQueryHandler(IStockReadRepository stockReadRepository)
    {
        _stockReadRepository = stockReadRepository;
    }

    public async Task<int> Handle(GetTotalQuantityForProductQuery query, CancellationToken cancellationToken)
    {
        var totalQuantity = await _stockReadRepository.GetTotalQuantityForProductAsync(query.ProductId, cancellationToken);

        return totalQuantity;
    }
}
