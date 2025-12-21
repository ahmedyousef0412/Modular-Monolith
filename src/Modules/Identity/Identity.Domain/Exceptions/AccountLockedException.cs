using SharedKernel.Exceptions;

namespace Identity.Domain.Exceptions;

public class AccountLockedException: DomainException
{
    public AccountLockedException(string email) 
        : base($"The account associated with the email '{email}' is locked. Please contact support to unlock your account.")
    {
    }
}
