using SharedKernel.Entities.Behaviors;

namespace SharedKernel.Entities;

public abstract class BaseEntity:IAuditableEntity,ISoftDeletable
{

    public Guid Id { get; protected set; } = Guid.NewGuid();


    // Auditing
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Soft Delete
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    public void SoftDelete()
    {
        if (IsDeleted) return;

        IsDeleted = true;

    }

    //protected void AddDomainEvent(IDomainEvent ev) { }
}
//protected set Only this class & derived classe