
using SharedKernel.Entities;
using SharedKernel.Exceptions;

namespace Inventory.Domain.Entity;

public class Product : BaseEntity
{

    public string Name { get; private set; }
    public string Sku { get; private set; }
    public string Description { get; private set; }

    public decimal Price { get; private set; }




    private Product() { } // Private constructor for EF Core


    // Factory Method
    public static Product Create(string name, string sku, string description , decimal price)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Name is required.");
        if (string.IsNullOrWhiteSpace(sku)) throw new DomainException("SKU is required.");
        if (string.IsNullOrWhiteSpace(description)) throw new DomainException("Description is required.");

        if(price < 0)    throw new DomainException("Price cannot be negative.");

        return new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Sku = sku,
            Description = description,
            Price = price
        };
    }


    public void UpdateDetails(string name, string description, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Name is required.");
        
        Name = name;
        Description = description;
        Price = price;
    }

    public void DeactiveProduct() => IsDeleted = true;
  
    public void ActivateProduct() => IsDeleted = false;
    
}
