using Identity.Domain.ValueObjects;
using SharedKernel.Domain;


namespace Identity.Domain.Events.Dtos;

public class UserRegistereddEvent : DomainEvent
{
    public Guid UserId { get; }
    public Email Email { get; }

    public UserRegistereddEvent(Guid userId, Email email)
    {
        UserId = userId;
        Email = email;
    }
}