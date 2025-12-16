using Identity.Domain.Entity;

namespace Identity.Domain.Repositories;

public interface IUserRepository
{

    Task AddAsync(User user, CancellationToken cancellationToken= default);

    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    
    // Useful to check uniqueness without loading the heavy object
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);

}
