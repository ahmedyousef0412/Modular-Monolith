using SharedKernel.Interfaces;

namespace SharedKernel.Entities;

public abstract class BaseEntity:IAuditableEntity
{

    public Guid Id { get; protected set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; protected set; } = false;

    public DateTime? DeletedAt { get; protected set; }

    public void SoftDelete()
    {
        if (IsDeleted) return;

        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

    }

    //protected void AddDomainEvent(IDomainEvent ev) { }
}
//protected set Only this class & derived classe