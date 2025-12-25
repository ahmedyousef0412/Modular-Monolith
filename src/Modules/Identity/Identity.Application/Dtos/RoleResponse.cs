namespace Identity.Application.Dtos;

public record RoleResponse(Guid Id, string Name, string? Description , IReadOnlyList<string> Permissions);
