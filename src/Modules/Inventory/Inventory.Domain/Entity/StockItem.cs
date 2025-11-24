using SharedKernel.Entities;
using SharedKernel.Exceptions;

namespace Inventory.Domain.Entity;

public class StockItem : BaseEntity
{

    public Guid WarehouseId { get; private set; }
    public int Quantity { get; private set; }
    public Guid ProductId { get; private set; }
    public int MinimumQuantity { get; private set; }

    public int MaximumQuantity { get; private set; }

    public StockStatus Status =>
        Quantity == 0 ? StockStatus.OutOfStock
        : Quantity < MinimumQuantity ? StockStatus.LowStock
        : Quantity == MaximumQuantity ? StockStatus.OverStock
        : StockStatus.InStock;


    private StockItem() { }


    private StockItem(Guid productId, Guid warehouseId, int quantity, int minQty, int maxQty)
    {
        if (quantity < 0)
            throw new DomainException("Quantity cannot be negative.");
        if (minQty < 0)
            throw new DomainException("Minimum quantity cannot be negative.");
        if (maxQty < minQty)
            throw new DomainException("Maximum quantity cannot be less than minimum.");

        ProductId = productId;
        WarehouseId = warehouseId;
        Quantity = quantity;
        MinimumQuantity = minQty;
        MaximumQuantity = maxQty;
    }


    public static StockItem Create(Guid productId, Guid warehouseId, int quantity, int minQty, int maxQty)
    {
        return new StockItem(productId, warehouseId, quantity, minQty, maxQty);
    }

    public void IncreaseQuantity(int amount)
    {
        if (amount <= 0)
            throw new DomainException("Increase amount must be greater than zero.");

        if (Quantity + amount > MaximumQuantity)
            throw new DomainException("Stock cannot exceed maximum quantity.");

        Quantity += amount;
    }

    public void DecreaseQuantity(int amount)
    {
        if (amount <= 0)
            throw new DomainException("Decrease amount must be greater than zero.");

        if (Quantity - amount < MinimumQuantity)
            throw new DomainException("Stock cannot go below minimum quantity.");

        Quantity -= amount;
    }

}
