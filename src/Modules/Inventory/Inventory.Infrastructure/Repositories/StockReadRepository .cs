using Inventory.Application.Abstractions;
using Inventory.Application.Dtos.StockItems;
using Inventory.Application.Mappings;
using Inventory.Application.Queries.ProductQueries;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Inventory.Infrastructure.Repositories;

public class StockReadRepository(InventoryDbContext context) : IStockReadRepository
{

    private readonly InventoryDbContext _context = context;

    public async Task<StockItemDto?> GetById(Guid stockItemId, CancellationToken cancellationToken = default)
    {
        return await _context.StockItems
            .AsNoTracking()
            .Where(s => s.Id == stockItemId)  //<-- IQueryable<StockItem>  Build-Query
                                              //.Select(_toDto)
             .ProjectToDto()     // <-- IQueryable<StockItemDto>  Still Deferred
             .FirstOrDefaultAsync(cancellationToken);  //triggers execution


        #region SQL generated 
        /*
         SELECT TOP(1) 
            Id, 
            WarehouseId, 
            Quantity, 
            MinimumQuantity, 
            MaximumQuantity,
            CASE WHEN Quantity > 0 THEN 'InStock' ELSE 'OutOfStock' END AS Status
        FROM StockItems
        WHERE Id = 'your-guid-here';
         */

        #endregion
    }

    public async Task<StockItemDto?> GetByProductAndWarehouseAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default)
    {
        return await _context.StockItems
              .AsNoTracking()
              .Where(si => si.ProductId == productId && si.WarehouseId == warehouseId)
               //.Select(_toDto)
               .ProjectToDto()
              .SingleOrDefaultAsync(cancellationToken);

    }

    public async Task<IReadOnlyList<StockItemDto>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return await _context.StockItems
            .AsNoTracking()
            .Where(si => si.ProductId == productId)
            //.Select(_toDto)
            .ProjectToDto()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<StockItemDto>> GetByProductIdsAndWarehouseAsync(IEnumerable<Guid> productIds, Guid warehouseId, CancellationToken cancellationToken = default)
    {
        // 1. Optimize: Don't hit DB if input is empty
        if (productIds == null || !productIds.Any())
        {
            return [];
        }

        //SELECT* FROM inventory.StockItems
        //WHERE WarehouseId = 'CAIRO_GUID'-- 1.The Scope
        // AND ProductId IN('IPHONE_ID', 'CASE_ID', 'CHARGER_ID') --2.The List


        var stockItems = await _context.StockItems
            .AsNoTracking()
            .Where(si => si.WarehouseId == warehouseId
            // 2. SQL Translation: WHERE WarehouseId = '...' AND ProductId IN ('...', '...')
                && productIds.Contains(si.ProductId))
               //.Select(_toDto)
               .ProjectToDto()
               .ToListAsync(cancellationToken);

        return stockItems;
    }

    public async Task<int> GetTotalQuantityForProductAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        //SELECT SUM(t.Quantity)
        //FROM inventory.StockItems AS t
        //WHERE t.ProductId = @productId

        return await _context.StockItems
             .AsNoTracking()
             .Where(si => si.ProductId == productId)
             .SumAsync(x => x.Quantity, cancellationToken);

    }


    #region Expression

    //Convert a StockItem to StockItemDto (It is data (an expression tree)
    //private static readonly Expression<Func<StockItem, StockItemDto>> _toDto =
    //    si => new StockItemDto(
    //        si.Id,
    //        si.WarehouseId,
    //        si.Quantity,
    //        si.MinimumQuantity,
    //        si.MaximumQuantity,
    //        si.Quantity > 0 ? StockStatus.InStock : StockStatus.OutOfStock
    //    );

    #endregion
}
