using SharedKernel.Common;
using SharedKernel.Entities;
using SharedKernel.Exceptions;

namespace Identity.Domain.Entity;

public class User : BaseEntity
{

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }


    public string PasswordHash { get; private set; }
    public bool IsActive { get; private set; }

    private readonly List<UserRole> _userRoles = [];
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    private readonly List<RefreshToken> _refreshTokens = [];
    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

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

    public void ChangePassword(string newPasswordHash)
    {
        Guard.AgainstNullOrEmpty(newPasswordHash, nameof(newPasswordHash));

        PasswordHash = newPasswordHash;

        // Security requirement: Revoke all existing sessions on password change
    }


    public void AssignRole(Guid roleId)
    {
        if(_userRoles.Any(ur => ur.RoleId == roleId))
        {
            throw new DomainException("User already has this role assigned.");
        }

        _userRoles.Add(new UserRole(this.Id, roleId));
    }

    public void RemoveRole(Guid roleId)
    {
        var role = _userRoles.FirstOrDefault(x => x.RoleId == roleId);
        if (role != null)
        {
            _userRoles.Remove(role);
        }
    }


    public RefreshToken AddRefreshToken(string token, DateTime expiresAt)
    {
        var refreshToken = new RefreshToken(token, expiresAt);
        _refreshTokens.Add(refreshToken);
        return refreshToken;
    }

    public void RevokeRefreshToken(string token, string reason)
    {
        var refreshToken = _refreshTokens.SingleOrDefault(t => t.Token == token && t.IsActive) 
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

    public void Deactivate() => IsActive = false;

    public void Activate() => IsActive = true;
}
