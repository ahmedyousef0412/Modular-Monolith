using Identity.Application.Roles.Commands.AssignRole;
using Identity.Application.Roles.Commands.RevokeRole;
using Identity.Application.Users.Commands.ChangePassword;
using Identity.Application.Users.Commands.UpdateProfile;
using Identity.Application.Users.Queries.GetUser;
using Identity.Application.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/identity/users")]
[Authorize]
public class UsersController(IMediator mediator) : ControllerBase
{

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommand command,CancellationToken cancellationToken)
    {
        await mediator.Send(command,cancellationToken);

        return NoContent();
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command,CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAll([FromQuery]GetAllUsersQuery query, CancellationToken cancellationToken )
    {
        var result = await mediator.Send(query,cancellationToken); 
        return Ok(result);
    }


    [HttpGet("{id:guid}",Name ="GetUserById")]
    public async Task<IActionResult> GetUser(Guid id  ,CancellationToken cancellationToken )
    {
        var query = new GetUserByIdQuery(id);

        var result = await mediator.Send(query, cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error.Description);

        return Ok(result);
    }


    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result.IsSuccess ? result : result.Error);
    }



    [HttpDelete("{userId}/roles/{roleId}")]
    public async Task<IActionResult> UnassignRole(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        var command = new RevokeRoleCommand(userId, roleId);

        var result = await mediator.Send(command, cancellationToken);

        return Ok(result); // Returns 204 No Content or 404
    }
}
