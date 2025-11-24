using Inventory.Application.Repository;
using Inventory.Application.Services;
using SharedKernel.CQRS;
using SharedKernel.Exceptions;

namespace Inventory.Application.Queries.Product;

internal class GetProductBySkuQueryHandler : IQueryHandler<GetProductBySkuQuery, ProductDto>
{

    private readonly IProductRepository _productRepository;
    private readonly IStockRepository _stockRepository;
    private readonly IInventoryMappingService _inventoryMappingService;

    public GetProductBySkuQueryHandler(IProductRepository productRepository, 
        IStockRepository stockRepository,
        IInventoryMappingService inventoryMappingService)
    {
        _productRepository = productRepository;
        _stockRepository = stockRepository;
        _inventoryMappingService = inventoryMappingService;
    }

    public async Task<ProductDto> Handle(GetProductBySkuQuery query, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetBySkuAsync(query.Sku, cancellationToken)
            ?? throw new NotFoundException(nameof(Product), query.Sku);


        var stockItems = await _stockRepository.GetByProductIdAsync(product.Id, cancellationToken);


        return _inventoryMappingService.MapToProductDto(product, stockItems);
    }
}
