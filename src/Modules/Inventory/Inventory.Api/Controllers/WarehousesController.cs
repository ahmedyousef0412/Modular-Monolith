using BuildingBlocks.Application.Security;
using Inventory.Application.Commands.WarehouseCommands;
using Inventory.Application.Queries.WarehouseQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Constants;

namespace Inventory.Api.Controllers;


[ApiController]
[Authorize]
[Route("api/inventory/warehouses")]
public class WarehousesController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    [HasPermission(PermissionsHelper.Warehouse.View)]

    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetWarehousesQuery(false),cancellationToken);

        return Ok(result);
    }
    [HasPermission(PermissionsHelper.Warehouse.View)]

    [HttpGet("{id:guid}", Name = "GetWarehouseById")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetWarehouseByIdQuery(id), cancellationToken);

        return  Ok(result);
    }

    [HttpPost]
    [HasPermission(PermissionsHelper.Warehouse.Create)]

    public async Task<IActionResult> Create([FromBody] CreateWarehouseCommand command )
    {
        var warehouseId = await mediator.Send(command);
       
        return CreatedAtAction(nameof(GetById), new { id = warehouseId }, new { id = warehouseId });
    }

    [HttpPut("{id:guid}")]
    [HasPermission(PermissionsHelper.Warehouse.Edit)]

    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWarehouseCommand command,CancellationToken cancellationToken)
    {
        await mediator.Send(command with { Id = id }, cancellationToken);

        return NoContent();
    }

    [HttpPatch("{id:guid}/deactivate")]
    [HasPermission(PermissionsHelper.Warehouse.Edit)]

    public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeactivateWarehouseCommand(id),cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id:guid}/activate")]
    [HasPermission(PermissionsHelper.Warehouse.Edit)]

    public async Task<IActionResult> Activate(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new ActivateWarehouseCommand(id), cancellationToken);
        return NoContent();
    }
}
