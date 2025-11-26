using Inventory.Application.Repository;
using Inventory.Application.Services;
using Inventory.Domain.Entity;
using SharedKernel.CQRS;
using SharedKernel.Exceptions;

namespace Inventory.Application.Queries.ProductQueries;

internal class GetProductBySkuQueryHandler : IQueryHandler<GetProductBySkuQuery, ProductDto>
{

    private readonly IProductRepository _productRepository;
    private readonly IStockReadRepository _stockReadRepository;
    private readonly IInventoryMappingService _inventoryMappingService;

    public GetProductBySkuQueryHandler(IProductRepository productRepository,
        IStockReadRepository stockReadRepository,
        IInventoryMappingService inventoryMappingService)
    {
        _productRepository = productRepository;
        _stockReadRepository = stockReadRepository;
        _inventoryMappingService = inventoryMappingService;
    }

    public async Task<ProductDto> Handle(GetProductBySkuQuery query, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetBySkuAsync(query.Sku, cancellationToken)
            ?? throw new NotFoundException(nameof(Product), query.Sku);
        

        var stockItems = await _stockReadRepository.GetByProductIdAsync(product.Id, cancellationToken);


        return _inventoryMappingService.MapToProductDto(product, stockItems);
    }
}
