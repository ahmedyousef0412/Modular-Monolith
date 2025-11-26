using FluentValidation;
using Inventory.Application.Queries.ProductQueries;

namespace Inventory.Application.Validators;

public class GetByProductIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetByProductIdQueryValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProuctId is required");
    }
}
