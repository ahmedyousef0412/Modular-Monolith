using SharedKernel.Common;

namespace Identity.Domain.Entity;

public class UserRole
{
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public Guid RoleId { get; private set; }
    public Role Role { get; private set; }

    public DateTime AssignedAt { get; private set; }

    protected UserRole() { }

    public static UserRole Create(User user, Role role)
    {
        Guard.AgainstNullOrEmpty(user, nameof(user));
        Guard.AgainstNullOrEmpty(role, nameof(role));

        return new UserRole
        {
            UserId = user.Id,
            RoleId = role.Id,
            AssignedAt = DateTime.UtcNow,
            User = user,
            Role = role
        };
    }
}
