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
}
