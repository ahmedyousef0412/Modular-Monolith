using FluentValidation;

namespace Identity.Application.Authentication.Commands.RevokeToken;

public class RevokeTokenCommandValidator:  AbstractValidator<RevokeTokenCommand>
{
    public RevokeTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token must be provided.");
    }
}
