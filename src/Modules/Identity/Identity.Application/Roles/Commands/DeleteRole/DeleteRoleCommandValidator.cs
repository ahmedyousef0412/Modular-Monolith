using FluentValidation;

namespace Identity.Application.Roles.Commands.DeleteRole;

public class DeleteRoleCommandValidator:AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(r => r.Id).NotEmpty().WithMessage("Role id required");
    }
}
