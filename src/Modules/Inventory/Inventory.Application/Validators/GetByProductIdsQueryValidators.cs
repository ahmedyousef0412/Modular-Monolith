using FluentValidation;
using Inventory.Application.Queries.StockItemQueries;

namespace Inventory.Application.Validators;

public class GetByProductIdsQueryValidators : AbstractValidator<GetStockByProductIdsQuery>
{
    public GetByProductIdsQueryValidators()
    {
        RuleFor(x => x.ProductIds)
             .NotEmpty().WithMessage("ProductIds must contain at least one id.")
             .Must(ids => ids.All(id => id != Guid.Empty))
             .WithMessage("ProductIds cannot contain an empty Guid.")
             .Must(ids => ids.Distinct().Count() == ids.Count())
             .WithMessage("ProductIds contains duplicate values.");

        RuleFor(x => x.WarehouseId)
             .NotEqual(Guid.Empty)
            .WithMessage("WarehouseId must be provided");
    }
}
