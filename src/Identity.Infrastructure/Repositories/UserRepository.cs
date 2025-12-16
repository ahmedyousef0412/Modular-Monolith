using Identity.Domain.Entity;
using Identity.Domain.Repositories;
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

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Include(u =>u.UserRoles)
            .Include(u =>u.RefreshTokens)
            .SingleOrDefaultAsync(u => u.Email.Value == email, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
                    .Include(x => x.UserRoles)
                    .Include(x => x.RefreshTokens)
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default)
    {
        return !await _dbContext.Users.AnyAsync(x => x.Email.Value == email, cancellationToken);
    }
}
