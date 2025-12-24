using BuildingBlocks.Application.CQRS;
using Identity.Application.Authentication.Dtos;

namespace Identity.Application.Authentication.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : ICommand<AuthResponse>;

