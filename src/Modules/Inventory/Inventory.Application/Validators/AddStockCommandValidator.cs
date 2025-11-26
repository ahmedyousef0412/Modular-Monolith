using FluentValidation;
using Inventory.Application.Commands.StockItemCommands;

namespace Inventory.Application.Validators;

public class AddStockCommandValidator:AbstractValidator<AddStockCommand>
{
    public AddStockCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEqual(Guid.Empty)
            .WithMessage("ProductId must provided");


        RuleFor(x => x.WarehouseId)
            .NotEqual(Guid.Empty)
            .WithMessage("WarehouseId must provided");

        RuleFor(x => x.Quantity)
           .GreaterThan(0)
           .WithMessage("Quantity must be greater than zero.");

        RuleFor(x => x.MinimumQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("MinimumQuantity cannot be negative.");

        RuleFor(x => x.MaximumQuantity)
            .GreaterThan(x => x.MinimumQuantity)
            .WithMessage("MaximumQuantity must be greater than MinimumQuantity.");

        RuleFor(x => x.Quantity)
            .LessThanOrEqualTo(x => x.MaximumQuantity)
            .WithMessage("Quantity cannot exceed MaximumQuantity.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(x => x.MinimumQuantity)
            .WithMessage("Quantity cannot be lower than MinimumQuantity.");
    }
}
