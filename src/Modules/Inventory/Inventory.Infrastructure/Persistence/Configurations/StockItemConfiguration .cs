using Inventory.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.Configurations;

public class StockItemConfiguration : IEntityTypeConfiguration<StockItem>
{
    public void Configure(EntityTypeBuilder<StockItem> builder)
    {
        builder.ToTable("StockItems");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Quantity)
               .IsRequired();


        // This prevents duplicate "iPhone 15" entries in "Warehouse A"
        builder.HasIndex(s => new { s.ProductId, s.WarehouseId })
               .IsUnique();

        builder.Property(s => s.MinimumQuantity)
               .IsRequired()
               .HasDefaultValue(0); 

        builder.Property(s => s.MaximumQuantity)
               .IsRequired();

        // Relationship with Product
        builder.HasOne<Product>()
               .WithMany()
               .HasForeignKey(s => s.ProductId)
               .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a Product if stock exists;
    }
}
