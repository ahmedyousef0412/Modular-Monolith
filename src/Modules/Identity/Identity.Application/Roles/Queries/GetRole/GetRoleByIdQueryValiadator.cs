using FluentValidation;

namespace Identity.Application.Roles.Queries.GetRole;

public class GetRoleByIdQueryValiadator:AbstractValidator<GetRoleByIdQuery>
{
    public GetRoleByIdQueryValiadator()
    {
        RuleFor(x => x.RoleId).NotEmpty().WithMessage("Role Id cannot empty");
    }
}
