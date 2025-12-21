using Identity.Domain.Entity;
using Identity.Domain.Repositories;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Common;

namespace Identity.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{

    private readonly IdentityDbContext _dbContext;

    public RoleRepository(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Role role)
    {
         await _dbContext.Roles.AddAsync(role);
    }

    public async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles
           .Include(r => r.Permissions)
           .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
       
    }

    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNullOrEmpty(name, nameof(name));

      
        return await _dbContext.Roles
            .Include(r => r.Permissions)
            .SingleOrDefaultAsync(r => r.Name == name, cancellationToken);

    }

    public async Task<List<Role>> GetListByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles
        .Include(r => r.Permissions) 
        .Where(r => ids.Contains(r.Id))
        .ToListAsync(cancellationToken);
    }
}
