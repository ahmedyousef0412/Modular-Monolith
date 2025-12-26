using BuildingBlocks.Application.Api;
using BuildingBlocks.Application.Security;
using Identity.Api.Controllers.Roles.Requests;
using Identity.Application.Roles.Commands.CreateRole;
using Identity.Application.Roles.Commands.DeleteRole;
using Identity.Application.Roles.Commands.UpdatePermissions;
using Identity.Application.Roles.Commands.UpdateRole;
using Identity.Application.Roles.Queries.GetRole;
using Identity.Application.Roles.Queries.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Constants;

namespace Identity.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/identity/roles")]
public class RolesController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    [HasPermission(PermissionsHelper.Roles.View)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllRolesQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result.Value);

        //return RequestResult(result);
    }


    [HttpGet("{id:guid}", Name = "GetRoleById")]
    [HasPermission(PermissionsHelper.Roles.View)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetRoleByIdQuery(id);

        var result = await mediator.Send(query, cancellationToken);

        //Change later because the controller not responsible for logic
        return Ok(result.IsSuccess ? result.Value : result.Error);
        //return RequestResult(result);
    }


    [HttpPost]
    [HasPermission(PermissionsHelper.Roles.Create)]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        //return Ok(result.IsSuccess ? result : result.Error); //This wrong because when fail return 200 , change later.
        return CreatedAtRoute("GetRoleById", new { id = result.Value }, result.Value);
    }


    [HttpPut("{id}")]
    [HasPermission(PermissionsHelper.Roles.Edit)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoleRequest request, CancellationToken cancellationToken)
    {

        var command = new UpdateRoleCommand(id, request.Name, request.Description);

        var result = await mediator.Send(command, cancellationToken);

        return Ok(result.IsSuccess ? result : result.Error);

    }


    [HttpDelete("{id}")]
    [HasPermission(PermissionsHelper.Roles.Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteRoleCommand(id);
        return Ok(await mediator.Send(command, cancellationToken));
    }


    [HttpPut("{id}/permissions")]
    [HasPermission(PermissionsHelper.Roles.Edit)]
    public async Task<IActionResult> UpdatePermissions(Guid id, [FromBody] UpdateRolePermissionsCommand command, CancellationToken cancellationToken)
    {
        //replacing the empty zeros with the real ID from the URL.
       var result = await mediator.Send(command with { RoleId = id }, cancellationToken);

        return Ok(result.IsSuccess ? result : result.Error);
    }

}

