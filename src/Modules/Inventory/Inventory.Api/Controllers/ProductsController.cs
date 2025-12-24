using Inventory.Application.Commands.ProductCommands;
using Inventory.Application.Queries.ProductQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;


[ApiController]
[Route("api/inventory/products")]
public class ProductsController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllProductsQuery query ,CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}", Name = "GetProductById")]
    public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{sku}")]
    public async Task<IActionResult> GetProductBySku(string sku, CancellationToken cancellationToken)
    {
        var query = new GetProductBySkuQuery(sku);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        Guid productId = await mediator.Send(command,cancellationToken);
        return CreatedAtAction(nameof(GetProductById), new { id = productId }, new { id = productId });
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateProductRequest(id, command),cancellationToken);
        return NoContent();

    }
    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactive(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeactiveProductCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new ActiveProductCommand(id), cancellationToken);
        return NoContent();
    }
}
