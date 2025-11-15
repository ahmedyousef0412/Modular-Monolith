using SharedKernel.Entities;
using SharedKernel.Exceptions;

namespace Sales.Domain.Entity;

public class OrderItem : BaseEntity
{
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    public decimal TotalPrice => Quantity * UnitPrice; // Calculated property  , Not Stored in DB

    public Guid OrderId { get; private set; }
    public Order Order { get; private set; }
    private OrderItem() { } // for EF



    private OrderItem(Guid orderId,string productName , int quantity, decimal unitPrice)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");


        if (unitPrice < 0)
            throw new DomainException("Unit price cannot be negative.");

        OrderId = orderId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

        Quantity = quantity;
    }

    public void UpdateUnitPrice(decimal unitPrice)
    {
        if (unitPrice < 0)
            throw new DomainException("Unit price cannot be negative.");

        UnitPrice = unitPrice;
    }

    public static OrderItem Create(Guid orderId,string productName, int quantity, decimal unitPrice)
    {

        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

        if (unitPrice < 0)
            throw new DomainException("Unit price cannot be negative.");

        return new OrderItem(orderId,productName, quantity, unitPrice);
    }
}
