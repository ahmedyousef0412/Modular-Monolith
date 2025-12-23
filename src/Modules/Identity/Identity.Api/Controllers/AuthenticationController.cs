using Identity.Application.Authentication.Commands.ForgotPassword;
using Identity.Application.Authentication.Commands.Login;
using Identity.Application.Authentication.Commands.RefreshToken;
using Identity.Application.Authentication.Commands.ResetPassword;
using Identity.Application.Authentication.Commands.RevokeToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;


[ApiController]
[Route("api/identity/auth")]

public class AuthenticationController(IMediator mediator) : ControllerBase
{

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command,cancellationToken);
        return Ok(result);
    }


    [HttpPost("refresh-token")]
    public async Task<IActionResult> RevokeToken([FromBody] RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command,cancellationToken);   
        return Ok(result);
    }


    [HttpPost("revoke")] 
    public async Task<IActionResult> Revoke([FromBody] RevokeTokenCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }


    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken );
        return Ok(result);
    }


    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
}

//Why forgot-password is here:
//    Even though it changes a user's password, the user is usually not logged in when they request it.
//    It is part of the "Recovery Flow" to regain access.