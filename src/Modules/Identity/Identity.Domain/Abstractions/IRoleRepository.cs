using Identity.Domain.Entity;

namespace Identity.Domain.Abstractions;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<List<Role>> GetListByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    // IRoleRepository.cs
    Task<bool> IsNameUniqueAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default);
    Task AddAsync(Role role);
}
