using SharedKernel.Entities;
using SharedKernel.Exceptions;

namespace Sales.Domain.Entity;

public class Order : BaseEntity
{

    private readonly List<OrderItem> _items = [];
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public Guid CustomerId { get; private set; }

    public OrderStatus Status { get; private set; } = OrderStatus.Draft;
    public decimal TotalAmount => _items.Sum(item => item.TotalPrice);


    private Order() { } // for EF
    private Order(Guid customerId)
    {
        if (customerId == Guid.Empty)
            throw new DomainException("CustomerId is required.");

        CustomerId = customerId;
        Status = OrderStatus.Draft;
        CreatedAt = DateTime.UtcNow;
    }


    //For an Order, the only thing you must know when creating it is =>  Which customer placed the order.
    public static Order Create(Guid customerId)
    {
        return new Order(customerId);
    }

    public OrderItem AddItem(string productName, int quantity, decimal unitPrice)
    {
        EnsureDraft();

        if (_items.Any(i => i.ProductName == productName))
            throw new DomainException($"The product '{productName}' is already added to the order.");


        var orderItem = OrderItem.Create(this.Id, productName, quantity, unitPrice);
        _items.Add(orderItem);

        return orderItem;
    }

    public void RemoveItem(Guid itemId)
    {
        EnsureDraft();

        var item = _items.FirstOrDefault(i => i.Id == itemId)
            ?? throw new DomainException("The order item does not exist.");

        _items.Remove(item);
    }

    public void MarkAsPendingPayment()
    {
        EnsureDraft();

        if (_items.Count is 0)
            throw new DomainException("Order must have at least one item.");

        Status = OrderStatus.PendingPayment;
    }

    public void MarkAsPaid()
    {
        if (Status != OrderStatus.PendingPayment)
            throw new DomainException("Only orders pending payment can be marked as paid.");


        Status = OrderStatus.Paid;

        //I will raise a domain event here later
    }

    public void Delete()
    {
        if(Status == OrderStatus.Paid)
            throw new DomainException("Paid orders cannot be deleted.");

        SoftDelete();
    }

    private void EnsureDraft()
    {
        if (Status != OrderStatus.Draft)
            throw new DomainException("Only draft orders can be modified.");
    }
}
