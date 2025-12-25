using FluentValidation;

namespace Identity.Application.Roles.Commands.RevokeRole;

public class RoveRoleCommandValidator:AbstractValidator<RevokeRoleCommand>
{
    public RoveRoleCommandValidator()
    {
        RuleFor(r => r.UserId).NotEmpty().WithMessage("UserId  required");
        RuleFor(r => r.RoleId).NotEmpty().WithMessage("RoleId  required");
    }
}
