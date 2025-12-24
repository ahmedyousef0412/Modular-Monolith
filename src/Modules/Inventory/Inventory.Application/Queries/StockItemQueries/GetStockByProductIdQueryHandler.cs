using BuildingBlocks.Application.CQRS;
using Inventory.Application.Abstractions;
using Inventory.Application.Dtos.StockItems;
using SharedKernel.Domain;

namespace Inventory.Application.Queries.StockItemQueries;

public class GetStockByProductIdQueryHandler : IQueryHandler<GetStockByProductIdQuery, IReadOnlyList< StockItemDto>>
{

    private readonly IStockReadRepository _stockReadRepository;

    public GetStockByProductIdQueryHandler(IStockReadRepository stockReadRepository)
    {
        _stockReadRepository = stockReadRepository;
    }

    public async Task<Result<IReadOnlyList<StockItemDto>>> Handle(GetStockByProductIdQuery query, CancellationToken cancellationToken)
    {
        var stockItems = await _stockReadRepository.GetByProductIdAsync(query.ProductId, cancellationToken); ;

        return Result<IReadOnlyList<StockItemDto>>.Success(stockItems);
    }
}
