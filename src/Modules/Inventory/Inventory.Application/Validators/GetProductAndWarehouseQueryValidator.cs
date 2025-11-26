using FluentValidation;
using Inventory.Application.Queries.StockItemQueries;


namespace Inventory.Application.Validators;

public class GetProductAndWarehouseQueryValidator:AbstractValidator<GetProductAndWarehouseQuery>
{
    public GetProductAndWarehouseQueryValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId cannot be empty.");
        RuleFor(x => x.WarehouseId)
            .NotEmpty().WithMessage("WarehouseId cannot be empty.");
    }
}
