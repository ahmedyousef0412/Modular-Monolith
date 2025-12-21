using Identity.Application.Authentication.Dtos;
using SharedKernel.CQRS;

namespace Identity.Application.Authentication.Commands.Login;

public record LoginCommand
(
    string Email,
    string Password
) : ICommand<AuthResponse>;

