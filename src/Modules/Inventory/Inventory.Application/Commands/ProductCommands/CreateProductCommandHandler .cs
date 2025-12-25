using BuildingBlocks.Application.CQRS;
using Inventory.Application.Abstractions;
using Inventory.Domain.Entity;
using SharedKernel.Domain;
using SharedKernel.Exceptions;

namespace Inventory.Application.Commands.ProductCommands;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{

    private readonly IProductRepository _repository;
    private readonly IInventoryUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductRepository repository, IInventoryUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
       
        var exists= await _repository.ExistsAsync(request.Sku, cancellationToken);

        if (exists)
            throw new DomainException($"Product with SKU '{request.Sku}' already exists.");

        var product = Product.Create(request.Name, request.Sku, request.Description, request.Price);


        _repository.Add(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(product.Id);
    }
}
