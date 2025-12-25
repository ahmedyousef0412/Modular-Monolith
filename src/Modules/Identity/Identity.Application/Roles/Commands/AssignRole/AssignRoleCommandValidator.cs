using FluentValidation;

namespace Identity.Application.Roles.Commands.AssignRole;

public class AssignRoleCommandValidator:AbstractValidator<AssignRoleCommand>
{
    public AssignRoleCommandValidator()
    {
        RuleFor(x =>x.UserId).NotEmpty().WithMessage("UserId is required");
        RuleFor(x =>x.RoleId).NotEmpty().WithMessage("RoleId is required");
    }
}
