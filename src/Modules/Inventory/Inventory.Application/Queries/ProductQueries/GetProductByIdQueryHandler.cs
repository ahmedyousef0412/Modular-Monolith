using Inventory.Application.Dtos.Products;
using Inventory.Application.Repository;
using Inventory.Application.Services;
using Inventory.Domain.Entity;
using SharedKernel.CQRS;
using SharedKernel.Domain;

namespace Inventory.Application.Queries.ProductQueries;

public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery,ProductDto>
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

    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(query.ProductId, cancellationToken);


        if (product is null)
        {
            return Result<ProductDto>.Failure(Error.NotFound(nameof(Product), query.ProductId));
        }

        var stockItems = await _stockReadRepository.GetByProductIdAsync(product.Id, cancellationToken);

        
        var dto = _inventoryMappingService.MapToProductDto(product,stockItems);

        return Result<ProductDto>.Success(dto);
    }

    
}
