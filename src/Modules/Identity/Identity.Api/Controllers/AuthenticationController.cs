using Identity.Application.Authentication.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;


[ApiController]
[Route("api/identity/auth")]
public class AuthenticationController : ControllerBase
{

    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command )
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    //Refresh , Revoke , Reset Password ,Forgot Password 
}

//Why forgot-password is here:
//    Even though it changes a user's password, the user is usually not logged in when they request it.
//    It is part of the "Recovery Flow" to regain access.