using SharedKernel.Common;
using SharedKernel.Exceptions;
using System.Net.Mail;


namespace Identity.Domain.ValueObjects;

public sealed class Email
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        
       Guard.AgainstNullOrEmpty(email, nameof(email));
        try
        {
           
            var mailAddress = new MailAddress(email.Trim());
            return new Email(mailAddress.Address.ToLowerInvariant());
        }
        catch
        {
            throw new DomainException("Invalid email format.");
        }
    }

    public override string ToString() => Value;

    public override bool Equals(object? obj)
    {
        return obj is Email other && string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode() => Value?.ToLowerInvariant().GetHashCode() ?? 0;

}
