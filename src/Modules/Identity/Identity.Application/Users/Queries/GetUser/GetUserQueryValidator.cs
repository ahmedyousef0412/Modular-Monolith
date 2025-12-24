using FluentValidation;

namespace Identity.Application.Users.Queries.GetUser;

public class GetUserQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserQueryValidator()
    {

        RuleFor(x => x.Id).NotEmpty();
    }
}