using SharedKernel.Abstractions;
using System.Security.Claims;

namespace WebHost.Services;

public class CurrentUserService(IHttpContextAccessor contextAccessor) : ICurrentUserService
{

    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    public string? UserId => _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? Username => _contextAccessor.HttpContext?.User?.Identity?.Name;

    public string? Email => _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

    public bool IsAuthenticated => _contextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;


    public IEnumerable<string> Roles => _contextAccessor.HttpContext?
        .User
        .Claims.Where(c => c.Type == ClaimTypes.Role)
        .Select(c => c.Value) ?? [];

}
