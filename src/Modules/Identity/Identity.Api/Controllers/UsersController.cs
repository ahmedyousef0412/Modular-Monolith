using BuildingBlocks.Application.Security;
using Identity.Application.Roles.Commands.AssignRole;
using Identity.Application.Roles.Commands.RevokeRole;
using Identity.Application.Users.Queries.GetUser;
using Identity.Application.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Constants;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/identity/users")]
[Authorize]
public class UsersController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    [HasPermission(PermissionsHelper.Users.View)]
    public async Task<IActionResult> GetAll([FromQuery]GetAllUsersQuery query, CancellationToken cancellationToken )
    {
        var result = await mediator.Send(query,cancellationToken); 
        return Ok(result);
    }


    [HttpGet("{id:guid}",Name ="GetUserById")]
    [HasPermission(PermissionsHelper.Users.View)]
    public async Task<IActionResult> GetUser(Guid id  ,CancellationToken cancellationToken )
    {
        var query = new GetUserByIdQuery(id);

        var result = await mediator.Send(query, cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error.Description);

        return Ok(result);
    }


    [HttpPost("assign-role")]
    [HasPermission(PermissionsHelper.Users.ManageRoles)]

    public async Task<IActionResult> AssignRole([FromBody] AssignRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result.IsSuccess ? result : result.Error);
    }



    [HttpDelete("{userId}/roles/{roleId}")]
    [HasPermission(PermissionsHelper.Users.ManageRoles)]

    public async Task<IActionResult> UnassignRole(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        var command = new RevokeRoleCommand(userId, roleId);

        var result = await mediator.Send(command, cancellationToken);

        return Ok(result); // Returns 204 No Content or 404
    }
}
