using Inventory.Application.Commands.WarehouseCommands;
using Inventory.Application.Queries.WarehouseQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;


[ApiController]
[Route("api/inventory/warehouses")]
public class WarehousesController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetWarehousesQuery(false),cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}", Name = "GetWarehouseById")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetWarehouseByIdQuery(id), cancellationToken);

        return  Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateWarehouseCommand command )
    {
        var warehouseId = await mediator.Send(command);
       
        return CreatedAtAction(nameof(GetById), new { id = warehouseId }, new { id = warehouseId });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWarehouseCommand command,CancellationToken cancellationToken)
    {
        await mediator.Send(command with { Id = id }, cancellationToken);

        return NoContent();
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeactivateWarehouseCommand(id),cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new ActivateWarehouseCommand(id), cancellationToken);
        return NoContent();
    }
}
