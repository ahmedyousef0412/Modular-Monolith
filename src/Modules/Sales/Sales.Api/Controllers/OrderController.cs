using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.Commands;
using Sales.Application.Queries;


namespace Sales.Api.Controllers;

[ApiController]
[Route("api/sales/orders")]
public class OrderController(IMediator mediator) : Controller
{

    [HttpGet]
    public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
    {
        var query = new GetAllOrdersQuery();
        var result = await mediator.Send(query ,cancellationToken);
        return Ok(result);
    }

  
    [HttpGet("{id:guid}",Name ="GetOrderById")]
    public async Task<IActionResult> GetOrderById(Guid id,CancellationToken cancellationToken)
    {
        var query = new GetOrderByIdQuery(id);
        var result = await mediator.Send(query,cancellationToken);
       
        return Ok(result);
    }

   
    [HttpGet("customer/{customerId:guid}")]
    public async Task<IActionResult> GetOrdersByCustomerId(Guid customerId,CancellationToken cancellationToken)
    {
        var query = new GetAllOrdersForCustomerQuery(customerId);
        var result = await mediator.Send(query,cancellationToken);
        return Ok(result);
    }

   
     [HttpPost]
  
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command,CancellationToken cancellationToken)
    {
      var id = await mediator.Send(command,cancellationToken);
        return CreatedAtRoute("GetOrderById", new { id }, new { id });
    }


    [HttpPut("update-order-quantity/{orderId}")]
    public async Task<IActionResult> UpdateOrderQuantity(Guid orderId, [FromBody] UpdateQuantityCommand command, CancellationToken cancellationToken)
    {

        //// This creates a NEW object (copy) with the updated OrderId
        var commandWithId = command with { OrderId = orderId };
        await mediator.Send(commandWithId, cancellationToken);
        return Ok();
    }

    [HttpPut("{orderId:guid}/mark-as-paid")]
    public async Task<IActionResult> MarkAsPaid(Guid orderId, CancellationToken cancellationToken)
    {
        var command = new MarkOrderAsPaidCommand(orderId);

        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPost("{orderId:guid}/confirm")]
    public async Task<IActionResult> ConfirmOrder(Guid orderId, CancellationToken cancellationToken)
    {
        var command = new ConfirmOrderCommand(orderId);
         
        await mediator.Send(command,cancellationToken);
        return Ok();

    }
  
    
    [HttpDelete("{orderId:guid}")]
    public async Task<IActionResult> Delete(Guid orderId,CancellationToken cancellationToken)
    {
        var command = new DeleteOrderCommand(orderId);
        await mediator.Send(command,cancellationToken);
        return Ok();
    }
}
