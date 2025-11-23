using FluentValidation;


namespace Sales.Application.Commands;

public class CreateOrderCommandValidator: AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required.");
        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("At least one order item is required.");
        RuleForEach(x => x.Items).ChildRules(items =>
        {
           
            items.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
          
        });
    }
}
