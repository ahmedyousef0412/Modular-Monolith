using Identity.Domain.Abstractions;
using Identity.Domain.Entity;
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

    public async Task<bool> IsNameUniqueAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Roles.AsNoTracking();

        // If we are updating, ignore the record we are currently editing
        //SELECT Count(*) FROM Roles WHERE Name = 'Admin' AND ID != excludeId.HasValue

        if (excludeId.HasValue)
        {
            //Is there any role name admin and id not  roleId will send in command?
            query = query.Where(r => r.Id != excludeId.Value);
        }

        var isTaken = await query.AnyAsync(r => r.Name == name, cancellationToken);

        return !isTaken;
    }
}
