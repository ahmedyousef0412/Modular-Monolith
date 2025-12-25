using Microsoft.AspNetCore.Mvc;
using SharedKernel.Constants;

namespace Identity.Api.Controllers.Permissions;

[Route("api/permissions")]
[ApiController]
public class PermissionsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllPermissions()
    {
        var permissions = PermissionsHelper.GetAllPermissions();

        return Ok(permissions);
    }
}
