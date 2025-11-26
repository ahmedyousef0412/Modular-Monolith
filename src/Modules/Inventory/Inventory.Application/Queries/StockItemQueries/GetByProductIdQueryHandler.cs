using Inventory.Application.Queries.ProductQueries;
using Inventory.Application.Repository;
using SharedKernel.CQRS;

namespace Inventory.Application.Queries.StockItemQueries;

public class GetByProductIdQueryHandler : IQueryHandler<GetByProductIdQuery, IReadOnlyList< StockItemDto>>
{

    private readonly IStockReadRepository _stockReadRepository;

    public GetByProductIdQueryHandler(IStockReadRepository stockReadRepository)
    {
        _stockReadRepository = stockReadRepository;
    }

    public async Task<IReadOnlyList<StockItemDto>> Handle(GetByProductIdQuery query, CancellationToken cancellationToken)
    {
        var stockItems = await _stockReadRepository.GetByProductIdAsync(query.ProductId, cancellationToken); ;

        return stockItems ?? [];
    }
}
