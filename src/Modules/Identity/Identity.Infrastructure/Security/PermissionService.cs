

namespace Identity.Infrastructure.Security;

public class PermissionService(IIdentityDbContext dbContext) : IPermissionService
{
   

    public async Task<bool> HasPermissionAsync(Guid userId, string permission)
    {
        var hasPermission = await dbContext.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
              .SelectMany(u => u.UserRoles)
                .Select(ur => ur.Role)
                 .SelectMany(r => r.Permissions)
                  .AnyAsync(p => p.PermissionCode == permission);

        return hasPermission;

    }
}
