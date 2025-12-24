namespace BuildingBlocks.Application.Abstractions;

public interface ICurrentUserService
{
    Guid UserId { get; }
    string? Username { get; }
    string? Email { get; }
    bool IsAuthenticated { get; }
    IEnumerable<string> Roles { get; }
}
