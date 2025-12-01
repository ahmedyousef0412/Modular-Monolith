using Inventory.Application.Dtos.Products;
using Inventory.Application.Repository;
using Inventory.Application.Services;
using Inventory.Domain.Entity;
using SharedKernel.CQRS;
using SharedKernel.Exceptions;

namespace Inventory.Application.Queries.ProductQueries;

public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDto>
{

    private readonly IProductRepository _productRepository;
    private readonly IStockReadRepository _stockReadRepository;
    private readonly IInventoryMappingService _inventoryMappingService;

    public GetProductByIdQueryHandler(IProductRepository productRepository, 
        IStockReadRepository stockReadRepository, IInventoryMappingService inventoryMappingService)
    {
        _productRepository = productRepository;
        _stockReadRepository = stockReadRepository;
        _inventoryMappingService = inventoryMappingService;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(query.ProductId, cancellationToken) 
            ?? throw new NotFoundException(nameof(Product), query.ProductId);


        var stockItems = await _stockReadRepository.GetByProductIdAsync(product.Id, cancellationToken);

        
        return _inventoryMappingService.MapToProductDto(product,stockItems);
    }
}
