using Microsoft.AspNetCore.Authorization;

namespace Identity.Infrastructure.Security;

public class PermissionRequirement(string permission):IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}
