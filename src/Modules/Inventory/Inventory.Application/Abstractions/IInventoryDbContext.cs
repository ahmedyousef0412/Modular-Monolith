using Inventory.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application.Abstractions;

public interface IInventoryDbContext
{
    public DbSet<Product> Products { get;  }
}
