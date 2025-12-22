using Identity.Application.Authentication.Dtos;
using SharedKernel.CQRS;

namespace Identity.Application.Authentication.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : ICommand<AuthResponse>;

