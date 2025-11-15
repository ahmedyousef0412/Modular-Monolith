namespace SharedKernel.Entities;

public abstract class BaseEntity
{

    public Guid Id { get; protected set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; protected set; }

    public bool IsDeleted { get; protected set; } = false;

    public DateTime? DeletedAt { get; protected set; }

    public void SoftDelete()
    {
        if (IsDeleted) return;

        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }
}
//protected set Only this class & derived classe