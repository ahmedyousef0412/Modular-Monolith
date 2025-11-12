namespace SharedKernel;

public class IntegrationEvent
{
    public Guid Id { get; }
    public DateTime OccurredAt { get; }

    protected IntegrationEvent()
    {
        Id = Guid.NewGuid();
        OccurredAt = DateTime.UtcNow;
    }
}
