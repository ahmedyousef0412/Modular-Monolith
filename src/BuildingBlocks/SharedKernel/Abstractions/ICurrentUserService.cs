namespace SharedKernel.Abstractions;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? Username { get; }
    string? Email { get; }
    bool IsAuthenticated { get; }
    IEnumerable<string> Roles { get; }
}
