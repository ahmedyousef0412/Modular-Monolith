using Identity.Domain.Exceptions;
using Identity.Domain.ValueObjects;
using SharedKernel.Common;
using SharedKernel.Entities;
using SharedKernel.Exceptions;

namespace Identity.Domain.Entity;

public class User : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    //public string? AvatarUrl { get; private set; }
    public Email Email { get; private set; }


    public string PasswordHash { get; private set; }
    public bool IsActive { get; private set; }

    public string? PasswordResetToken { get; private set; }
    public DateTime? PasswordResetTokenExpiresOn { get; private set; }

    private readonly List<UserRole> _userRoles = [];
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    private readonly List<RefreshToken> _refreshTokens = [];
    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    private const int MaxRefreshTokens = 5;
    private User() { }


    private User(Guid id, string firstName, string lastName, Email email, string passwordHash)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        IsActive = true;
    }

    public static User Create(string firstName, string lastName, Email email, string passwordHash)
    {

        //enforce invariants 
        Guard.AgainstNullOrEmpty(firstName, nameof(firstName));
        Guard.AgainstNullOrEmpty(lastName, nameof(lastName));
        Guard.AgainstNullOrEmpty(passwordHash, nameof(passwordHash));



        // Raise Event (e.g., to send Welcome Email)
        //user.AddDomainEvent(new UserRegisteredEvent(user.Id, user.Email));

        return new User(Guid.NewGuid(), firstName, lastName, email, passwordHash);
    }


    public void UpdateProfile(string firstName,string lastName)
    {
        if (!IsActive) throw new DomainException("Cann't update profile for inactive user");

        Guard.AgainstNullOrEmpty(firstName, nameof(firstName));
        Guard.AgainstNullOrEmpty(lastName,nameof(lastName));


        FirstName = firstName;
        LastName = lastName;
    }

    //public void UpdateAvatar(string avatarUrl)
    //{
    //    AvatarUrl = avatarUrl;
    //}

    public void ChangePassword(string newPasswordHash)
    {
        Guard.AgainstNullOrEmpty(newPasswordHash, nameof(newPasswordHash));

        PasswordHash = newPasswordHash;

        RevokeAllRefreshTokens("the password changed");
    }


    public void RequestPasswordReset(string token ,DateTime expiresOn)
    {
        if (!IsActive)
            throw new AccountLockedException(Email.Value);


        PasswordResetToken = token;
        PasswordResetTokenExpiresOn = expiresOn;
    }

    public bool VerifyResetToken(string token)
    {
        return PasswordResetToken == token &&
               PasswordResetTokenExpiresOn.HasValue &&
               PasswordResetTokenExpiresOn.Value > DateTime.UtcNow;
    }
    public void CompletePasswordReset()
    {
        PasswordResetToken = null;
        PasswordResetTokenExpiresOn = null;
    }

    public void AssignRole(Role role)
    {
        Guard.AgainstNullOrEmpty(role, nameof(role));

        if (_userRoles.Any(ur => ur.RoleId == role.Id))
            throw new DomainException("User already has this role assigned.");

        var userRole = UserRole.Create(this, role);

        _userRoles.Add(userRole);
    }

    public void RemoveRole(Guid roleId)
    {
        var role = _userRoles.FirstOrDefault(x => x.RoleId == roleId);
        if (role != null)
        {
            _userRoles.Remove(role);
        }
    }

    public void AddSession(string token, DateTime expiresAt)
    {

        // Clean up expired or inactive tokens
        var expiredTokens = _refreshTokens
            .Where(t => t.IsExpired || !t.IsActive)
            .ToList();

        foreach (var expiredToken in expiredTokens)
        {
            _refreshTokens.Remove(expiredToken);
        }


        // Enforce maximum number of active refresh tokens
        var activeTokens = _refreshTokens
            .Where(t => t.IsActive)
            .ToList();


        // get oldest active token and revoke it
        if (activeTokens.Count >= MaxRefreshTokens)
        {
            var oldestToken = activeTokens
                .OrderBy(t => t.CreatedOn)
                .First();

            oldestToken.Revoke("Maximum number of active refresh tokens exceeded.");
        }


        // Add the new refresh token
        var refreshToken = new RefreshToken(token, expiresAt,this.Id); 
        _refreshTokens.Add(refreshToken);
     
    }

    public void RevokeRefreshToken(string token, string reason)
    {
        var refreshToken = _refreshTokens
            .SingleOrDefault(t => t.Token == token && t.IsActive) 
            ?? throw new DomainException("Refresh token not found or already revoked.");


        if (!refreshToken.IsActive)
            throw new DomainException("Refresh token is not active.");

        refreshToken.Revoke(reason);
    }

    public void RevokeAllRefreshTokens(string reason)
    {
        foreach (var token in _refreshTokens.Where(t => t.IsActive))
            token.Revoke(reason);
    }


    public void RotateRefreshToken(string oldToken, string newToken, DateTime expiresAt)
    {
        var tokenToRotate = _refreshTokens.Single(t => t.Token == oldToken);
           
        if (tokenToRotate is null)
        {
            AddSession(newToken, expiresAt);
            return;
        }

        tokenToRotate.Revoke("Replaced by new token.");

        this.AddSession(newToken,expiresAt);
    }


    public void Deactivate() => IsActive = false;

    public void Activate() => IsActive = true;
}
