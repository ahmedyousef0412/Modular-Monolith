using Inventory.Application.Repository;
using SharedKernel.CQRS;
using SharedKernel.Domain;

namespace Inventory.Application.Queries.StockItemQueries;

public class GetTotalQuantityForProductQueryHandler : IQueryHandler<GetTotalQuantityForProductQuery, int>
{

    private readonly IStockReadRepository _stockReadRepository;

    public GetTotalQuantityForProductQueryHandler(IStockReadRepository stockReadRepository)
    {
        _stockReadRepository = stockReadRepository;
    }

    public async Task<Result<int>> Handle(GetTotalQuantityForProductQuery query, CancellationToken cancellationToken)
    {
        var totalQuantity = await _stockReadRepository.GetTotalQuantityForProductAsync(query.ProductId, cancellationToken);

        return Result<int>.Success(totalQuantity);
    }
}
