using Microsoft.EntityFrameworkCore;
using Sales.Domain.Entity;
using Sales.Infrastructure.Persistence.Configurations;

namespace Sales.Infrastructure.Persistence;

public class SalesDbContext: DbContext
{

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }



    public SalesDbContext(DbContextOptions<SalesDbContext> options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());

        modelBuilder.HasDefaultSchema("sales");


        base.OnModelCreating(modelBuilder);
    }
}
