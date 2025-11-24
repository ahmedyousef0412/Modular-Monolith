using Inventory.Application.Repository;
using Inventory.Domain.Repositories;
using MediatR;
using SharedKernel.CQRS;

namespace Inventory.Application.Commands.ProductCommands;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductRequest, CommandResult>
{

    private readonly IProductRepository _productRepository;
    private readonly IInventoryUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IProductRepository productRepository, IInventoryUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return CommandResult.Failure(new List<string> { "Product not found." });
        }

        product.UpdateDetails(request.Command.Name, request.Command.Description, request.Command.Price);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult.Success();
    }
}
    
