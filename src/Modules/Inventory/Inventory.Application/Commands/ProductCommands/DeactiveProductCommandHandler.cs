using BuildingBlocks.Application.CQRS;
using Inventory.Application.Abstractions;
using Inventory.Domain.Entity;
using SharedKernel.Exceptions;

namespace Inventory.Application.Commands.ProductCommands;

public class DeactiveProductCommandHandler : ICommandHandler<DeactiveProductCommand, bool>
{

    private readonly IProductRepository _productRepository;
    private readonly IInventoryUnitOfWork _unitOfWork;

    public DeactiveProductCommandHandler(IProductRepository productRepository, IInventoryUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeactiveProductCommand command, CancellationToken cancellationToken)
    {
       var product = await _productRepository.GetByIdAsync(command.ProductId, cancellationToken) 
            ?? throw new NotFoundException(nameof(Product), command.ProductId);

        product.DeactiveProduct();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
