using Inventory.Application.Commands.StockItemCommands;
using Inventory.Application.Queries.ProductQueries;
using Inventory.Application.Queries.StockItemQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[Route("api/inventory/stocks")]
public class StockItemsController : ControllerBase
{

    private readonly IMediator _mediator;

    public StockItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    // GET /api/inventory/stocks/product/{id}
    [HttpGet("product/{productId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<StockItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByProductId(Guid productId)
    {
        var result = await _mediator.Send(new GetStockByProductIdQuery(productId));
        return Ok(result);
    }


    //This is a standard industry pattern often called "Query over POST" or "POST Search".
    // POST /api/inventory/stocks/multiple
    // Query over POST (Bulk Fetch)
    [HttpPost("multiple")]
    [ProducesResponseType(typeof(IEnumerable<StockItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByProductIds([FromBody] GetStockByProductIdsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }


    // GET /api/stock/{productId}/warehouse/{warehouseId}
    [HttpGet("{productId:guid}/warehouse/{warehouseId:guid}")]
    [ProducesResponseType(typeof(StockItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByProductAndWarehouse(Guid productId, Guid warehouseId)
    {
        var result = await _mediator.Send(
            new GetStockProductAndWarehouseQuery(productId, warehouseId)
        );
        return Ok(result);
    }


    // Used for: Storefront "In Stock" label
    // GET /api/inventory/stocks/{productId}/total-quantity
    [HttpGet("{productId:guid}/total-quantity")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTotalQuantity(Guid productId)
    {
        var query = new GetTotalQuantityForProductQuery(productId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }


    // POST /api/inventory/stocks
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CreateStock([FromBody] AddStockCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }




    // POST /api/inventory/stocks/reduce
    [HttpPost("reduce")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ReduceStock([FromBody] ReduceStockCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }




    // PUT /api/inventory/stocks/thresholds
    [HttpPut("thresholds")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateThresholds([FromBody] UpdateStockThresholdsCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

}
