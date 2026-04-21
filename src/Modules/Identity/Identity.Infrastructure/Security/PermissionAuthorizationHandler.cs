using Microsoft.AspNetCore.Authorization;


namespace Identity.Infrastructure.Security;

public class PermissionAuthorizationHandler(IServiceProvider serviceProvider) : AuthorizationHandler<PermissionRequirement>
{
   

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var idClaim = context.User?.FindFirstValue(ClaimTypes.NameIdentifier)
                                     ?? context.User?.FindFirstValue("sub");


        if (!Guid.TryParse(idClaim, out var userId))
        {
            return; 
        }
        

        using var scope = serviceProvider.CreateScope();

        var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

        if (await permissionService.HasPermissionAsync(userId, requirement.Permission))
            context.Succeed(requirement);
    }
}
