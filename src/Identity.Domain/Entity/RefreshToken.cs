using SharedKernel.Entities;

namespace Identity.Domain.Entity;

public class RefreshToken : BaseEntity
{
    public string Token { get; private set; }

    public DateTime ExpiresOn { get; private set; }

    public DateTime? RevokedOn { get; private set; }
    public string? ReasonRevoked { get; private set; }


    public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
    public bool IsRevoked => RevokedOn != null;
    public bool IsActive => !IsRevoked && !IsExpired;


    private RefreshToken() { }

    internal RefreshToken(string token, DateTime expires)
    {
        Id = Guid.NewGuid();
        Token = token;
        ExpiresOn = expires;
    }

    public void Revoke(string reason = null)
    {
        RevokedOn = DateTime.UtcNow;
        ReasonRevoked = reason;
    }
}
