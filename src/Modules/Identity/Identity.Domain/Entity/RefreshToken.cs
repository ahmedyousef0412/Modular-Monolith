namespace Identity.Domain.Entity;


public class RefreshToken 
{
    public Guid UserId { get; private set; }
    public string Token { get; private set; }
    public DateTime ExpiresOn { get; private set; }
    public DateTime CreatedOn { get; private set; }

    public DateTime? RevokedOn { get; private set; }
    public string? ReasonRevoked { get; private set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
    public bool IsRevoked => RevokedOn != null;
    public bool IsActive => !IsRevoked && !IsExpired;


    private RefreshToken() { }

    internal RefreshToken(string token, DateTime expiresOn, Guid userId)
    {
        Token = token;
        ExpiresOn = expiresOn;
        UserId = userId;
        CreatedOn = DateTime.UtcNow; 
    }

    // Methods
    public void Revoke(string reason)
    {
        if (IsRevoked || IsExpired) return; 

        RevokedOn = DateTime.UtcNow;
        ReasonRevoked = reason;
    }
}
