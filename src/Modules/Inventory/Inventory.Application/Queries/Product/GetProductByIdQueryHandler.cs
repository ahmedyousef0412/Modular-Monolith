using Inventory.Application.Repository;
using Inventory.Application.Services;
using SharedKernel.CQRS;
using SharedKernel.Exceptions;

namespace Inventory.Application.Queries.Product;

public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDto>
{

    private readonly IProductRepository _productRepository;
    private readonly IStockRepository _stockRepository;
    private readonly IInventoryMappingService _inventoryMappingService;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IStockRepository stockRepository, IInventoryMappingService inventoryMappingService)
    {
        _productRepository = productRepository;
        _stockRepository = stockRepository;
        _inventoryMappingService = inventoryMappingService;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(query.ProductId, cancellationToken) 
            ?? throw new NotFoundException(nameof(Product), query.ProductId);


        var stockItems = await _stockRepository.GetByProductIdAsync(product.Id, cancellationToken);

        
        return _inventoryMappingService.MapToProductDto(product, stockItems);
    }
}
