using SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain.Exceptions;

public class InvalidTokenException : DomainException
{
    public InvalidTokenException(string message) : base(message)
    {
    }
}
