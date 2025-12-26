using BuildingBlocks.Application.Security;
using Inventory.Application.Commands.ProductCommands;
using Inventory.Application.Queries.ProductQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Constants;

namespace Inventory.Api.Controllers;


[ApiController]
[Authorize]
[Route("api/inventory/products")]
public class ProductsController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    [HasPermission(PermissionsHelper.Products.View)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllProductsQuery query ,CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}", Name = "GetProductById")]
    [HasPermission(PermissionsHelper.Products.View)]

    public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{sku}")]
    [HasPermission(PermissionsHelper.Products.View)]

    public async Task<IActionResult> GetProductBySku(string sku, CancellationToken cancellationToken)
    {
        var query = new GetProductBySkuQuery(sku);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [HasPermission(PermissionsHelper.Products.Create)]

    public async Task<IActionResult> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
       var result =  await mediator.Send(command,cancellationToken);
        return CreatedAtAction(nameof(GetProductById), new { id = result.Value }, new { id = result.Value });
    }


    [HttpPut("{id:guid}")]
    [HasPermission(PermissionsHelper.Products.Edit)]

    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateProductRequest(id, command),cancellationToken);
        return NoContent();

    }
    [HttpPatch("{id:guid}/deactivate")]
    [HasPermission(PermissionsHelper.Products.Edit)]

    public async Task<IActionResult> Deactive(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeactiveProductCommand(id), cancellationToken);
        return NoContent();
    }



    [HttpPatch("{id:guid}/activate")]
    [HasPermission(PermissionsHelper.Products.Edit)]

    public async Task<IActionResult> Activate(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new ActiveProductCommand(id), cancellationToken);
        return NoContent();
    }
}
