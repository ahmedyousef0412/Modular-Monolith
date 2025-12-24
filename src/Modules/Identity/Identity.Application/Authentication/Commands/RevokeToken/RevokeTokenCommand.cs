using BuildingBlocks.Application.CQRS;

namespace Identity.Application.Authentication.Commands.RevokeToken;

public record RevokeTokenCommand(string RefreshToken) : ICommand<CommandResult>;
