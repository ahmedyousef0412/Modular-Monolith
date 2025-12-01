using SharedKernel.Entities;
using SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Domain.Entity;

//public class Warehouse:AggregateRoot
public class Warehouse : BaseEntity
{
    public string Name { get; private set; }
    public string Location { get; private set; } //I will change this to (value object)
    public bool IsActive { get; private set; }

    private Warehouse() { } //for EFCore

    public static Warehouse Create(string name, string location) //Factory Method
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Warehouse name is required.");

        if (string.IsNullOrWhiteSpace(location))
            throw new DomainException("Warehouse location is required.");

        return new Warehouse
        {
            Id = Guid.NewGuid(),
            Name = name,
            Location = location,
            IsActive = true, // Active by default
            CreatedAt = DateTime.Now,
        };
    }

    public void UpdateDetails(string name, string location)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Warehouse name is required.");

        if (string.IsNullOrWhiteSpace(location))
            throw new DomainException("Warehouse location is required.");

        Name = name;
        Location = location;
    }



    //When I Deactivate the Warehouse I should block AddStockCommand for this warehouse (WarehouseId in Stock)
    public void Deactivate()
    {
        if (!IsActive)
            throw new DomainException("Warehouse is already inactive.");

        IsActive = false;
    }

    public void Activate()
    {
        if (IsActive)
            throw new DomainException("Warehouse is already active.");
        IsActive = true;

    }

}
