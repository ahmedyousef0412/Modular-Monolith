using Inventory.Application.Dtos.StockItems;
using Inventory.Application.Repository;
using Inventory.Domain.Entity;
using SharedKernel.CQRS;
using SharedKernel.Domain;



namespace Inventory.Application.Queries.StockItemQueries;

public class GetStockProductAndWarehouseQueryHandler(IStockReadRepository stockReadRepository)
    : IQueryHandler<GetStockProductAndWarehouseQuery, StockItemDto>
{

    private readonly IStockReadRepository _stockReadRepository = stockReadRepository;

    public async Task<Result<StockItemDto>> Handle(GetStockProductAndWarehouseQuery query, CancellationToken cancellationToken)
    {
        var stockItemDto = await _stockReadRepository
             .GetByProductAndWarehouseAsync(query.ProductId, query.WarehouseId, cancellationToken);

        var dto = stockItemDto ?? new StockItemDto
            (
                        Id: Guid.Empty,
                        WarehouseId: query.WarehouseId,
                        Quantity: 0,
                        MinimumQuantity: 0,
                        MaximumQuantity: 0,
                        Status: StockStatus.OutOfStock
            );

        return Result<StockItemDto>.Success(dto);
    }

}
