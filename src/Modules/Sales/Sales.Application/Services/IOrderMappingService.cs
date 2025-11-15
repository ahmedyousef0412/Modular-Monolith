using Sales.Application.Commands;
using Sales.Application.Queries;
using Sales.Domain.Entity;

namespace Sales.Application.Services;

public interface IOrderMappingService
{
    OrderDto MapToDto(Order order);
}

public class OrderMappingService : IOrderMappingService
{
    public OrderDto MapToDto(Order order)
    {
        var orderDto = new OrderDto
        (
            Id: order.Id,
            CustomerId: order.CustomerId,
            ItemsCount: order.Items.Count,
            Total: order.TotalAmount,
            Status: order.Status.ToString(),
            CreatedAt: order.CreatedAt,
            Items: [.. order.Items.Select(item => new OrderItemDto
            (
                
                ProductName: item.ProductName,
                Quantity: item.Quantity,
                UnitPrice: item.UnitPrice
               
            ))]
        );
        

        return orderDto;
    }
}