using FluentValidation;

namespace Identity.Application.Roles.Commands.CreateRole;

public class CreateRoleCommandValidator:AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Role name required")
            .MaximumLength(100)
            .WithMessage("Role name must less than 100 char");

    }
}
