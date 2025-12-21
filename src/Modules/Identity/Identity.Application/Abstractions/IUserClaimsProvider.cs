using Identity.Domain.Entity;
using System.Security.Claims;

namespace Identity.Application.Abstractions;

public interface IUserClaimsProvider
{
    Task<IReadOnlyCollection<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken = default);

}
