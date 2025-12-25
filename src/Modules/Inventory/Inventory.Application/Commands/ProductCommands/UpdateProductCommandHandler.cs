using BuildingBlocks.Application.CQRS;
using Inventory.Application.Abstractions;
using Inventory.Domain.Entity;
using SharedKernel.Domain;

namespace Inventory.Application.Commands.ProductCommands;

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductRequest>
{

    private readonly IProductRepository _productRepository;
    private readonly IInventoryUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IProductRepository productRepository, IInventoryUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Failure(Error.NotFound(nameof(Product),request.Id));
        }

        product.UpdateDetails(request.Command.Name, request.Command.Description, request.Command.Price);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
    
