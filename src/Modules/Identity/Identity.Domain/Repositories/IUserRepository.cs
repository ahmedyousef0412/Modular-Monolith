using Identity.Domain.Entity;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Repositories;

public interface IUserRepository
{

    Task AddAsync(User user, CancellationToken cancellationToken= default);

    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);

    
    // Useful to check uniqueness without loading the heavy object
    Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);


    Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);

}
