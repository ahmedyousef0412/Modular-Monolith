using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SharedKernel.Entities.Behaviors;

namespace BuildingBlocks.Application.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{

    // Synchronous Save
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }


    // Asynchronous Save (The one you use 99% of the time)
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }


    private static void UpdateEntities(DbContext? context)
    {
        if (context == null) return;


        // 1. Handle Auditing (Created/Updated)
        foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }
            if (entry.State == EntityState.Modified)
            {
                // Set update time
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        // 2. Handle Soft Delete
        foreach (var entry in context.ChangeTracker.Entries<ISoftDeletable>())
        {
            
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;

                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedAt = DateTime.UtcNow;
            }
        }
    }
}
