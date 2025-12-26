using BuildingBlocks.Application.Security;
using Inventory.Application.Commands.StockItemCommands;
using Inventory.Application.Dtos.StockItems;
using Inventory.Application.Queries.StockItemQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Constants;

namespace Inventory.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/inventory/stocks")]
public class StockItemsController(IMediator mediator) : ControllerBase
{

    // GET /api/inventory/stocks/product/{id}
    [HttpGet("product/{productId:guid}")]
    [HasPermission(PermissionsHelper.Inventory.View)]

    [ProducesResponseType(typeof(IEnumerable<StockItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByProductId(Guid productId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetStockByProductIdQuery(productId),cancellationToken);
        return Ok(result);
    }


    //This is a standard industry pattern often called "Query over POST" or "POST Search".
    // POST /api/inventory/stocks/multiple
    // Query over POST (Bulk Fetch)
    [HttpPost("multiple")]
    [ProducesResponseType(typeof(IEnumerable<StockItemDto>), StatusCodes.Status200OK)]
    [HasPermission(PermissionsHelper.Inventory.View)]

    public async Task<IActionResult> GetByProductIds([FromBody] GetStockByProductIdsQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query,cancellationToken);
        return Ok(result);
    }


    // GET /api/stock/{productId}/warehouse/{warehouseId}
    [HttpGet("{productId:guid}/warehouse/{warehouseId:guid}")]
    [ProducesResponseType(typeof(StockItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HasPermission(PermissionsHelper.Inventory.View)]
    public async Task<IActionResult> GetByProductAndWarehouse(Guid productId, Guid warehouseId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new GetStockProductAndWarehouseQuery(productId, warehouseId),cancellationToken
        );
        return Ok(result);
    }


    // Used for: Storefront "In Stock" label
    // GET /api/inventory/stocks/{productId}/total-quantity
    [HttpGet("{productId:guid}/total-quantity")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [HasPermission(PermissionsHelper.Inventory.View)]

    public async Task<IActionResult> GetTotalQuantity(Guid productId, CancellationToken cancellationToken)
    {
        var query = new GetTotalQuantityForProductQuery(productId);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }


    // POST /api/inventory/stocks
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HasPermission(PermissionsHelper.Inventory.Create)]

    public async Task<IActionResult> CreateStock([FromBody] AddStockCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }




    // POST /api/inventory/stocks/reduce
    [HttpPost("reduce")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HasPermission(PermissionsHelper.Inventory.Edit)]

    public async Task<IActionResult> ReduceStock([FromBody] ReduceStockCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }




    // PUT /api/inventory/stocks/thresholds
    [HttpPut("thresholds")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HasPermission(PermissionsHelper.Inventory.Edit)]

    public async Task<IActionResult> UpdateThresholds([FromBody] UpdateStockThresholdsCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

}
