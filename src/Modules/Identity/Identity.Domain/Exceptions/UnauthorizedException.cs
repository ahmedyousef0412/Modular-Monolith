using SharedKernel.Exceptions;

namespace Identity.Domain.Exceptions;

public class UnauthorizedException : DomainException
{
    public UnauthorizedException() : base("Unauthorized")
    {
    }
}
