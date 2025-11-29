using Inventory.Application.Commands.ProductCommands;
using Inventory.Application.Queries.ProductQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;


[ApiController]
[Route("api/inventory/products")]
public class ProductsController : ControllerBase
{

    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}", Name = "GetProductById")]
    public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{sku}")]
    public async Task<IActionResult> GetProductBySku(string sku, CancellationToken cancellationToken)
    {
        var query = new GetProductBySkuQuery(sku);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        Guid productId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProductById), new { id = productId }, new { id = productId });
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
    {
        await _mediator.Send(new UpdateProductRequest(id, command));
        return NoContent();

    }
    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactive(Guid id)
    {
        await _mediator.Send(new DeactiveProductCommand(id));
        return NoContent();
    }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate(Guid id)
    {
        await _mediator.Send(new ActiveProductCommand(id));
        return NoContent();
    }
}
