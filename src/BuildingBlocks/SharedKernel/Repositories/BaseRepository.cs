using Microsoft.EntityFrameworkCore;


namespace SharedKernel.Repositories;

//public abstract class BaseRepository<T> where T : AggregateRoot
public abstract class BaseRepository<T> where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    protected BaseRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, ct);
    }

    public void Add(T entity) => _dbSet.Add(entity);

    public void Update(T entity) => _dbSet.Update(entity);

  
    public void Delete(T entity) => _dbSet.Remove(entity);
}
