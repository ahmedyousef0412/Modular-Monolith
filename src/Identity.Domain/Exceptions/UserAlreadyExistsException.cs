using SharedKernel.Exceptions;

namespace Identity.Domain.Exceptions;

public class UserAlreadyExistsException:DomainException
{
    public UserAlreadyExistsException(string email)
        : base($"A user with the email '{email}' already exists.")
    {
    }
}
