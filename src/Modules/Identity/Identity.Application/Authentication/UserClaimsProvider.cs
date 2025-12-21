using Identity.Application.Abstractions;
using Identity.Domain.Entity;
using Identity.Domain.Repositories;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Identity.Application.Authentication;

public class UserClaimsProvider : IUserClaimsProvider
{
    private readonly IRoleRepository _roleRepository;

    public UserClaimsProvider(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
    public async Task<IReadOnlyCollection<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken = default)
    {
        var roleIds = user.UserRoles.Select(ur => ur.RoleId).ToList();

        var roles = await _roleRepository.GetListByIdsAsync(roleIds, cancellationToken);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email.Value),
            new(JwtRegisteredClaimNames.GivenName, user.FirstName), 
            new(JwtRegisteredClaimNames.FamilyName, user.LastName), 
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name));

            foreach (var permission in role.Permissions)
            {
                claims.Add(new Claim("permission", permission.PermissionCode));
            }
        }

        return claims;
    }
}
