using BuildingBlocks.Application.CQRS;

namespace Identity.Application.Roles.Commands.CreateRole;

public record CreateRoleCommand(string Name, string? Description) : ICommand<Guid>;

