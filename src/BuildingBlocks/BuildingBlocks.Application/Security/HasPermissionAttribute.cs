using Microsoft.AspNetCore.Authorization;

namespace BuildingBlocks.Application.Security;

public class HasPermissionAttribute(string permission) : AuthorizeAttribute(policy: permission)
{
}
