using Identity.Domain.Abstractions;
using Identity.Domain.Entity;
using Identity.Domain.ValueObjects;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _dbContext;

    public UserRepository(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
       await _dbContext.Users.AddAsync(user, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Include(u => u.UserRoles)
             .ThenInclude(ur => ur.Role)
            .Include(u => u.RefreshTokens)
            .SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
                    .Include(x => x.UserRoles)
                      .ThenInclude(ur => ur.Role)
                    .Include(x => x.RefreshTokens)
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Include(u => u.UserRoles)
             .ThenInclude(ur => ur.Role)
            .Include(u => u.RefreshTokens)
            .SingleOrDefaultAsync(u => u.RefreshTokens.Any(rt => rt.Token == refreshToken), cancellationToken);
    }

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
    {
        return !await _dbContext.Users.AnyAsync(x => x.Email== email, cancellationToken);
    }
}
