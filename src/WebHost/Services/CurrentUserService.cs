using BuildingBlocks.Application.Abstractions;
using System.Security.Claims;

namespace WebHost.Services;

public class CurrentUserService(IHttpContextAccessor contextAccessor) : ICurrentUserService
{

    public Guid UserId
    {
        get
        {
            var idClaim = contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)
                          ?? contextAccessor.HttpContext?.User?.FindFirstValue("sub");

            return Guid.TryParse(idClaim, out var userId) ? userId : Guid.Empty;
        }
    }

    public string? Username => contextAccessor.HttpContext?.User?.Identity?.Name;

    public string? Email => contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

    public bool IsAuthenticated => contextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;


    public IEnumerable<string> Roles => contextAccessor.HttpContext?
        .User
        .Claims.Where(c => c.Type == ClaimTypes.Role)
        .Select(c => c.Value) ?? [];

}
