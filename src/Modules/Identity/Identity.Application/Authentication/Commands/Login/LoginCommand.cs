using BuildingBlocks.Application.CQRS;
using Identity.Application.Authentication.Dtos;

namespace Identity.Application.Authentication.Commands.Login;

public record LoginCommand
(
    string Email,
    string Password
) : ICommand<AuthResponse>;

