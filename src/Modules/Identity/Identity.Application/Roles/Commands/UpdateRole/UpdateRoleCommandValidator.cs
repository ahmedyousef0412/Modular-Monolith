using FluentValidation;

namespace Identity.Application.Roles.Commands.UpdateRole;

public class UpdateRoleCommandValidator:AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(r => r.RoleId)
            .NotEmpty()
            .WithMessage("Role id required");

        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Role name required")
            .MaximumLength(100)
            .WithMessage("Role name should be less than 100 char");
    }
}
