using FluentValidation;
using Inventory.Application.Queries.StockItemQueries;


namespace Inventory.Application.Validators;

public class GetTotalQuantityForProductQueryValidator : AbstractValidator<GetTotalQuantityForProductQuery>
{
    public GetTotalQuantityForProductQueryValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId must be provided.");
    }
}
