using BuildingBlocks.Application.CQRS;
using Inventory.Application.Abstractions;
using Inventory.Domain.Entity;
using SharedKernel.Domain;
using SharedKernel.Exceptions;

namespace Inventory.Application.Commands.ProductCommands;

public class ActiveProductCommandHandler : ICommandHandler<ActiveProductCommand, bool>
{

    private readonly IProductRepository _productRepository;
    private readonly IInventoryUnitOfWork _unitOfWork;

    public ActiveProductCommandHandler(IProductRepository productRepository, IInventoryUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(ActiveProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(command.ProductId, cancellationToken)
            ?? throw new NotFoundException(nameof(Product), command.ProductId);

        product.ActivateProduct();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(true);


    }
}
